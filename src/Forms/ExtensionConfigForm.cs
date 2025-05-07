using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AACMAToolkit.Forms
{
    public partial class ExtensionConfigForm : Form
    {
        public ExtensionConfigForm()
        {
            InitializeComponent();
        }

        public class ExtensionInfo
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Path { get; set; }
            public string State { get; set; }
        }

        private async Task<string> RunAzCmAgentCommand(string args)
        {
            // Reuse the RunAzCmAgentCommand method from MainForm
            try
            {
                MessageBox.Show($"Executing command: azcmagent {args}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return await ((MainForm)Owner).RunAzCmAgentCommand(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private List<ExtensionInfo> ParseExtensions(string output)
        {
            // Parse the output of "azcmagent extension list" to extract extension details
            var extensions = new List<ExtensionInfo>();
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            ExtensionInfo currentExtension = null;
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
                extensions.Add(currentExtension);
            }

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
                    lvExtensions.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Failed to load extensions: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadAllowlist()
        {
            try
            {
                lbAllowlist.Items.Clear();
                string output = await RunAzCmAgentCommand("config get extensions.allowlist");
                var allowlist = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .ToList();

                foreach (var extension in allowlist)
                {
                    lbAllowlist.Items.Add(extension);
                }

                // Update the original allowlist
                originalAllowlist = new List<string>(allowlist);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Failed to load allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonReloadInstalledExtentions_Click(object sender, EventArgs e)
        {
            await LoadExtensions();
            MessageBox.Show(@"Installed extensions reloaded successfully.", @"Reload", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private async void ExtensionConfigForm_Load(object sender, EventArgs e)
        {
            await LoadExtensions();
            await LoadAllowlist();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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

                // Notify the user of success
                MessageBox.Show(@"Extension added to allowlist successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the operation
                MessageBox.Show($@"Failed to add extension to allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                MessageBox.Show(@"Extension removed from allowlist successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Format the allowlist as a single string in square brackets
                string allowlistString = "[" + string.Join(",", allowlist) + "]";

                // Update the allowlist using the azcmagent command
                await RunAzCmAgentCommand($"config set extensions.allowlist \"{allowlistString}\"");
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

        private List<string> originalAllowlist = new List<string>();

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Update the allowlist in the agent configuration
                await UpdateAllowlistConfig();

                // Update the original allowlist to match the current state
                originalAllowlist = lbAllowlist.Items.Cast<string>().ToList();

                MessageBox.Show(@"Allowlist updated successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Failed to update allowlist: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReloadAllowlist_Click(object sender, EventArgs e)
        {
            await LoadExtensions();
            await LoadAllowlist();
        }
    }
}
