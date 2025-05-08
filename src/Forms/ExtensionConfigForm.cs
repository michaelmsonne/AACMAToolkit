using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AACMAToolkit.Forms
{
    public partial class ExtensionConfigForm : Form
    {
        private List<string> _originalAllowlist = new List<string>();

        public ExtensionConfigForm()
        {
            InitializeComponent();
            InitializeContextMenuAllowList(); // Initialize the context menu
        }

        public class ExtensionInfo
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Path { get; set; }
            public string State { get; set; }
        }

        private void InitializeContextMenuAllowList()
        {
            // Create a context menu
            var contextMenu = new ContextMenuStrip();

            // Add "Copy" option to the context menu
            var copyMenuItem = new ToolStripMenuItem("Copy extension string");
            copyMenuItem.BackColor = System.Drawing.Color.White;
            copyMenuItem.Click += (s, e) =>
            {
                if (lbAllowlist.SelectedItem != null)
                {
                    // Copy the selected item to the clipboard
                    Clipboard.SetText(lbAllowlist.SelectedItem.ToString());
                }
            };

            // Add the menu item to the context menu
            contextMenu.Items.Add(copyMenuItem);

            // Assign the context menu to the ListBox
            lbAllowlist.ContextMenuStrip = contextMenu;
        }

        private async Task<string> RunAzCmAgentCommand(string args)
        {
            try
            {
#if DEBUG
                MessageBox.Show($"Executing command: azcmagent {args}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);  
#endif
                // Execute the command and get the output  
                string output = await ((MainForm)Owner).RunAzCmAgentCommand(args);

                // Check if the output contains the specific error message  
                if (output.Contains("Supplied arguments do not satisfy the requirements"))
                {
                    // Append the command and its output to the action log  
                    //AppendToActionLog($"> Command: azcmagent {args}\r\n> Output: {output}\r");
                }

                return output;
            }
            catch (Exception ex)
            {
                // Log the error to the action log  
                //AppendToActionLog($"> Command: azcmagent {args}\r\n> Error: {ex.Message}\r");

                MessageBox.Show($@"Error executing command: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private List<ExtensionInfo> ParseExtensions(string output)
        {
            // Parse the output of "azcmagent extension list" to extract extension details
            var extensions = new List<ExtensionInfo>();
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Initialize a new ExtensionInfo object to hold the current extension details
            ExtensionInfo currentExtension = null;

            // Iterate through each line of the output
            foreach (var line in lines)
            {
                if (line.StartsWith("Extension:"))
                {
                    if (currentExtension != null)
                    {
                        extensions.Add(currentExtension);
                    }

                    currentExtension = new ExtensionInfo
                    {
                        Name = line.Replace("Extension:", "").Trim()
                    };
                }
                else if (line.StartsWith("Version:") && currentExtension != null)
                {
                    currentExtension.Version = line.Replace("Version:", "").Trim();
                }
                else if (line.StartsWith("Extension Path:") && currentExtension != null)
                {
                    currentExtension.Path = line.Replace("Extension Path:", "").Trim();
                }
                else if (line.StartsWith("State:") && currentExtension != null)
                {
                    currentExtension.State = line.Replace("State:", "").Trim();
                }
            }

            // Add the last extension if it exists
            if (currentExtension != null)
            {
                // Check if the extension is already in the list to avoid duplicates
                extensions.Add(currentExtension);
            }

            // Return the list of parsed extensions
            return extensions;
        }

        private async Task LoadExtensions()
        {
            try
            {
                // Clear the ListView
                lvExtensions.Items.Clear();

                // Fetch the current extensions list
                string output = await RunAzCmAgentCommand("extension list");

                // Parse the output and populate the ListView
                var extensions = ParseExtensions(output);
                foreach (var extension in extensions)
                {
                    var item = new ListViewItem(extension.Name);
                    item.SubItems.Add(extension.Version);
                    item.SubItems.Add(extension.Path);
                    item.SubItems.Add(extension.State);

                    // Add the item to the ListView
                    lvExtensions.Items.Add(item);
                }

                // Notify the user of success
                MessageBox.Show(@"Installed extensions reloaded successfully.", @"Reload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the operation
                MessageBox.Show($@"Failed to load extensions: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadAllowlist()
        {
            try
            {
                lbAllowlist.Items.Clear();
                string output = await RunAzCmAgentCommand("config get extensions.allowlist");

                // Parse the output and filter out "[]"
                var allowlist = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(line => !string.IsNullOrWhiteSpace(line) && line != "[]")
                    .ToList();

                // Add each valid extension to the ListBox
                foreach (var extension in allowlist)
                {
                    lbAllowlist.Items.Add(extension);
                }

                // Update the original allowlist
                _originalAllowlist = new List<string>(allowlist);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Failed to load allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonReloadInstalledExtentions_Click(object sender, EventArgs e)
        {
            await LoadExtensions();
        }

        private async void btnAddCustomExtension_Click(object sender, EventArgs e)
        {
            // Get the custom extension name from the TextBox
            string extension = txtAllowlistExtension.Text.Trim();

            // Validate the input
            if (string.IsNullOrEmpty(extension))
            {
                MessageBox.Show(@"Please enter a valid extension name.", @"Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the extension already exists in the allowlist
            if (lbAllowlist.Items.Contains(extension))
            {
                MessageBox.Show(@"This extension is already in the allowlist.", @"Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Add the extension to the ListBox
                lbAllowlist.Items.Add(extension);

                // Update the allowlist in the agent configuration
                await UpdateAllowlistConfig();

                // Clear the TextBox for new input
                txtAllowlistExtension.Clear();
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the operation
                //MessageBox.Show($@"Failed to add extension to allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRemoveFromAllowlist_Click(object sender, EventArgs e)
        {
            if (lbAllowlist.SelectedItem == null)
            {
                MessageBox.Show(@"Please select an extension to remove.", @"No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string extension = lbAllowlist.SelectedItem.ToString();

            try
            {
                // Remove the extension from the ListBox
                lbAllowlist.Items.Remove(extension);

                // Update the allowlist in the agent configuration
                await UpdateAllowlistConfig();

                // Notify the user of success
                //MessageBox.Show(@"Extension removed from allowlist successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Failed to remove extension from allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateAllowlistConfig()
        {
            try
            {
                // Collect all extensions from the ListBox  
                var allowlist = lbAllowlist.Items.Cast<string>().ToList();

                // Format the allowlist as a single string in the required format  
                string allowlistString = string.Join(",", allowlist);

                // Update the allowlist using the azcmagent command  
                string result = await RunAzCmAgentCommand($"config set extensions.allowlist \"{allowlistString}\"");

                if (string.IsNullOrEmpty(result) || result.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show($@"Failed to update allowlist configuration: {result ?? "null"}", @"Error update allowlist", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // If the update fails, revert the ListBox to the original allowlist with the content loaded from the agent configuration
                    lbAllowlist.Items.Clear();

                    // Repopulate the ListBox with the original allowlist
                    await LoadAllowlist();
                }
                else
                {
                    MessageBox.Show(@"Allowlist updated successfully.", @"Success update allowlist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($@"Failed to update allowlist configuration: {ex.Message}");
            }
        }

        private async void btnClearAllowlist_Click(object sender, EventArgs e)
        {
            // Confirm the action with the user
            var result = MessageBox.Show(
                @"Are you sure you want to clear the entire allowlist?",
                @"Confirm Clear Allowlist",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Clear the ListBox
                    lbAllowlist.Items.Clear();

                    // Update the allowlist in the agent configuration
                    await UpdateAllowlistConfig();

                    MessageBox.Show(@"Allowlist cleared successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Failed to clear allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Update the allowlist in the agent configuration
                await UpdateAllowlistConfig();

                // Update the original allowlist to match the current state
                _originalAllowlist = lbAllowlist.Items.Cast<string>().ToList();

                //MessageBox.Show(@"Allowlist updated successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($@"Failed to update allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReloadAllowlist_Click(object sender, EventArgs e)
        {
            await LoadExtensions();
            await LoadAllowlist();
        }

        private void linkLabelExtentionListMS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open the specified URL in the default web browser  
            System.Diagnostics.Process.Start("https://learn.microsoft.com/en-us/azure/azure-arc/servers/manage-vm-extensions#extensions");
        }

        private async void ExtensionConfigForm_Shown(object sender, EventArgs e)
        {
            await LoadExtensions();
            await LoadAllowlist();
        }
    }
}
