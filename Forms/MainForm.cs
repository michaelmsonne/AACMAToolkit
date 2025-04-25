using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AACMAToolkit.Class;

namespace AACMAToolkit.Forms
{
    public partial class MainForm : Form
    {
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private string generateDynamicLogName(string logName)
        {
            var hostname = Environment.MachineName;
            var timestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            return $"{hostname}_{timestamp}_{logName}";
        }

        private async Task animateLabelText(Label label, string baseText, CancellationToken token)
        {
            // Animate the label text with dots
            string[] dots = { ".", "..", "..." };
            int index = 0;

            while (!token.IsCancellationRequested)
            {
                label.Text = baseText + dots[index];
                index = (index + 1) % dots.Length;
                await Task.Delay(500, token); // Update every 500ms
            }
        }

        private async Task<(bool, string, string)> isAzureArcAgentUpToDate()
        {
            string installedVersion = string.Empty;
            string latestVersion = string.Empty;

            try
            {
                // Step 1: Get the installed version
                string versionCommand = "show \"Agent Version\"";
                installedVersion = (await runAzCmAgentCommand(versionCommand)).Trim();

                // Extract the version number from the output
                if (installedVersion.Contains(":"))
                {
                    installedVersion = installedVersion.Split(':')[1].Trim();
                }

                // Normalize the installed version to match the format of the website version
                string normalizedInstalledVersion = installedVersion.Split('.')[0] + "." + installedVersion.Split('.')[1];

                // Step 2: Get the latest version from the Azure Arc release notes page
                string releaseNotesUrl = "https://learn.microsoft.com/en-us/azure/azure-arc/servers/agent-release-notes";
                using (WebClient client = new WebClient())
                {
                    string pageContent = client.DownloadString(releaseNotesUrl);

                    // Use regex to extract the latest version from the release notes
                    var match = System.Text.RegularExpressions.Regex.Match(pageContent, @"Version (\d+\.\d+)");
                    if (match.Success)
                    {
                        latestVersion = match.Groups[1].Value;
                    }
                    else
                    {
                        MessageBox.Show(@"Failed to extract the latest version from the release notes.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return (false, installedVersion, latestVersion);
                    }
                }

                // Step 3: Compare the normalized installed version with the latest version
                bool isUpToDate = string.Equals(normalizedInstalledVersion, latestVersion, StringComparison.OrdinalIgnoreCase);
                return (isUpToDate, installedVersion, latestVersion);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error while checking Azure Arc agent version: {ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false, installedVersion, latestVersion);
            }
        }

        ///create Process and give arguments via string
        ///Azcmagent(arguments) ex: Azcmagent(show config)
        ///RedirectStandard = capture Output & Error
        ///UseShellExecute = process won't use system shell
        ///CreateNoWindow = no window pop-up whatsoever

        private async Task<string> runAzCmAgentCommand(string args)
        {
            CancellationTokenSource cancellationTokenSource = null; // Declare the variable here
            try
            {
                // Set the label to show processing status
                lblStatus.Text = @"Processing...";
                lblStatus.ForeColor = System.Drawing.Color.Blue;
                lblStatus.Visible = true;

                // Start a task to animate the label text
                cancellationTokenSource = new CancellationTokenSource();
                var animationTask = animateLabelText(lblStatus, "Processing", cancellationTokenSource.Token);

                // Wait for the animation task to start and parse the task
                var psi = new ProcessStartInfo
                {
                    FileName = "azcmagent",
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process
                using (var process = new Process())
                {
                    process.StartInfo = psi;
                    var outputBuilder = new StringBuilder();
                    var errorBuilder = new StringBuilder();

                    var outputTaskCompletion = new TaskCompletionSource<bool>();
                    var errorTaskCompletion = new TaskCompletionSource<bool>();

                    // Handle output and error asynchronously
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                            outputTaskCompletion.TrySetResult(true); // Signal when output is done
                        else
                            outputBuilder.AppendLine(e.Data);
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                            errorTaskCompletion.TrySetResult(true); // Signal when error is done
                        else
                            errorBuilder.AppendLine(e.Data);
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await Task.Run(() => process.WaitForExit()); // Wait for the process to exit
                    await Task.WhenAll(outputTaskCompletion.Task, errorTaskCompletion.Task); // Wait for output/error reading to complete

                    // Stop the animation and update the label text for completion
                    lblStatus.Text = @"Task Complete";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    return string.IsNullOrWhiteSpace(outputBuilder.ToString()) ? errorBuilder.ToString() : outputBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and update the label text accordingly
                lblStatus.Text = @"Error: An exception occurred.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return $"Exception: {ex.Message}";
            }
            finally
            {
                // Stop the animation and hide the label
                cancellationTokenSource?.Cancel(); // Ensure cancellationTokenSource is not null
                lblStatus.Visible = false;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            // Set the title of the form to include the version number
            Text = Application.ProductName + @" v." +Application.ProductVersion;

            // Check if the application is running as admin or not
            var isAdmin = ApplicationFunctions.isRunningAsAdmin();

            // Adjust UI based on admin status
            if (isAdmin)
            {
                // Enable admin-specific options
                lblUpdateArcAgent.Enabled = true;
                lblChangeMode2Full.Enabled = true;
                label1.Enabled = true; // Monitor mode
                lblShowAgentMode.Enabled = true; // Show mode
                lblShowAgentConfig.Enabled = true; // Show config
                lblExportLogs.Enabled = true; // Export logs
            }
            else
            {
                // Disable admin-specific options
                lblUpdateArcAgent.Enabled = false;
                lblChangeMode2Full.Enabled = false;
                label1.Enabled = false; // Monitor mode
                lblShowAgentMode.Enabled = false; // Show mode
                lblShowAgentConfig.Enabled = false; // Show config
                lblExportLogs.Enabled = false; // Export logs

                MessageBox.Show(@"Some features are disabled because the application is not running as an administrator.",
                    @"Limited Access", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private async void lblUpdateArcAgent_Click(object sender, EventArgs e)
        {
            var (isUpToDate, installedVersion, latestVersion) = await isAzureArcAgentUpToDate();

            if (isUpToDate)
            {
                MessageBox.Show($@"Azure Arc agent is up-to-date.
Installed Version: {installedVersion}
Latest Version: {latestVersion}",
                    @"Version Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($@"Azure Arc agent is not up-to-date.
Installed Version: {installedVersion}
Latest Version: {latestVersion}",
                    @"Version Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Ask if the user wants to update
                var result = MessageBox.Show(@"Do you want to update the Azure Arc agent?", @"Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ApplicationFunctions.updateAzureArcAgent();
                }
            }
        }

        /// Execute the click event with the right params --- Params are CASE SENSITIVE
        /// Azcmagent show "Agent Version" "Agent Logfile" "Agent Status" "Agent Last Heartbeat"
        ///
        private async void lblCheckVersion_Click(object sender, EventArgs e)
        {
            const string agentversionCheck = "show \"Agent Version\" \"Agent Logfile\" \"Agent Status\" \"Agent Last Heartbeat\" ";
            txtOutput.Text = await runAzCmAgentCommand(agentversionCheck);
        }

        /// Azcmagent show "Agent Error Code" "Agent Error Details" "Agent Error Timestamp"
        private async void lblCheckAgentError_Click(object sender, EventArgs e)
        {
            const string agentErrorCheck = "show \"Agent Error Code\" \"Agent Error Details\" \"Agent Error Timestamp\"  ";
            txtOutput.Text = await runAzCmAgentCommand(agentErrorCheck);
        }

        /// Azcmagent config list
        private async void lblShowAgentConfig_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await runAzCmAgentCommand("config list");
        }

        /// Azcmagent config get config.mode
        private async void lblShowAgentMode_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await runAzCmAgentCommand("config get config.mode");
        }

        /// Put the agent in full mode
        private async void lblChangeMode2Full_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await runAzCmAgentCommand("config set config.mode full");
        }

        /// Put the agent in monitor mode
        private async void label1_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await runAzCmAgentCommand("config set config.mode monitor");
        }

        /// <summary>
        /// Export the logs to a zip file
        /// </summary>
        private async void lblExportLogs_Click(object sender, EventArgs e)
        {
            // Open a folder dialog to select the folder to save the logs
            using (var folderDialog = new FolderBrowserDialog())
            {
                // Set the initial directory to the user's documents folder
                folderDialog.Description = @"Select the folder to save the logs file";
                folderDialog.ShowNewFolderButton = true;

                // Show the folder dialog and check if the user selected a folder
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected path and create the log file name
                    var selectedPath = folderDialog.SelectedPath;
                    var strLogfilePath = Path.Combine(selectedPath, generateDynamicLogName("AzcmagentLogs") + ".zip");

                    // Log to txtOutput what is being done
                    txtOutput.Text = @"Exporting logs to '" + strLogfilePath + @"'...";

                    // Run the command to export logs
                    txtOutput.Text = await runAzCmAgentCommand("logs --full --output " + strLogfilePath);

                    // MessageBox to inform the user
                    MessageBox.Show($@"Logs have been exported to: '{strLogfilePath}'", @"Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // User canceled the folder selection
                    MessageBox.Show(@"No folder was selected. Export canceled.", @"Export Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Restart the application as administrator
        /// </summary>
        private void restartAsAdministratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call the function to restart the application as admin
            ApplicationFunctions.restartAsAdmin();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the about form
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }
    }
}