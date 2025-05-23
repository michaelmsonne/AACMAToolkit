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

        private void SetLabelStatus(Label label, string text, System.Drawing.Color color, bool isVisible)
        {
            label.Text = text;
            label.ForeColor = color;
            label.Visible = isVisible;
        }

        private async Task AnimateLabelText(Label label, string baseText, CancellationToken token)
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

        private async Task<(bool, string, string)> IsAzureArcAgentUpToDate()
        {
            string installedVersion = string.Empty;
            string latestVersion = string.Empty;

            try
            {
                // Step 1: Get the installed version
                string versionCommand = "show \"Agent Version\"";
                installedVersion = (await RunAzCmAgentCommand(versionCommand)).Trim();

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

#if DEBUG
                // Always update/get URL in debug mode
                bool isUpToDate = false;
                latestVersion = "DebugModeLatestVersion"; // Simulate latest version for debug
#else
                // Step 3: Compare the normalized installed version with the latest version
                bool isUpToDate = string.Equals(normalizedInstalledVersion, latestVersion, StringComparison.OrdinalIgnoreCase);
#endif
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
        public async Task<string> RunAzCmAgentCommand(string args)
        {
            CancellationTokenSource cancellationTokenSource = null; // Declare the variable here
            try
            {
                // Set the label to show processing status
                SetLabelStatus(lblStatus, @"Processing...", System.Drawing.Color.Blue, true);

                // Start a task to animate the label text
                cancellationTokenSource = new CancellationTokenSource();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                AnimateLabelText(lblStatus, "Processing", cancellationTokenSource.Token);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

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
                    SetLabelStatus(lblStatus, @"Task Complete", System.Drawing.Color.Green, true);

                    return string.IsNullOrWhiteSpace(outputBuilder.ToString()) ? errorBuilder.ToString() : outputBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and update the label text accordingly
                SetLabelStatus(lblStatus, @"Error: An exception occurred.", System.Drawing.Color.Red, true);
                return $"Exception: {ex.Message}";
            }
            finally
            {
                // Stop the animation and hide the label
                cancellationTokenSource?.Cancel(); // Ensure cancellationTokenSource is not null
                SetLabelStatus(lblStatus, string.Empty, System.Drawing.Color.Black, false);
            }
        }

        public MainForm()
        {
            InitializeComponent();

            // Set the title of the form to include the version number
            Text = Globals.toolLongName + @" v." + Application.ProductVersion;

            // Check if the application is running as admin or not
            var isAdmin = ApplicationFunctions.isRunningAsAdmin();

            // Adjust UI based on admin status
            if (isAdmin)
            {
                // Enable admin-specific options
                lblUpdateArcAgent.Enabled = true;
                lblChangeModeToFull.Enabled = true;
                lblChangeModeToMonitor.Enabled = true; // Monitor mode
                lblShowAgentMode.Enabled = true; // Show mode
                lblShowAgentConfig.Enabled = true; // Show config
                lblExportLogs.Enabled = true; // Export logs
                lblRestartService.Enabled = true; // Restart service
                lblChangeTier0.Enabled = true; // Change to Tier 0 mode
                lblManageExtentions.Enabled = true; // Manage extensions
                lblDisableAllUseOfExtentions.Enabled = true; // Disable all extensions
                lblAllowAllUseOfExtentions.Enabled = true; // Allow all extensions
                lblGetAutomaticUpgradeConfig.Enabled = true; // Get automatic upgrade config
            }
            else
            {
                // Disable admin-specific options
                lblUpdateArcAgent.Enabled = false;
                lblChangeModeToFull.Enabled = false;
                lblChangeModeToMonitor.Enabled = false; // Monitor mode
                lblShowAgentMode.Enabled = false; // Show mode
                lblShowAgentConfig.Enabled = false; // Show config
                lblExportLogs.Enabled = false; // Export logs
                lblRestartService.Enabled = false; // Restart service
                lblChangeTier0.Enabled = false; // Change to Tier 0 mode
                lblManageExtentions.Enabled = false; // Manage extensions
                lblDisableAllUseOfExtentions.Enabled = false; // Disable all extensions
                lblAllowAllUseOfExtentions.Enabled = false; // Allow all extensions
                lblGetAutomaticUpgradeConfig.Enabled = false; // Get automatic upgrade config

                MessageBox.Show(@"Some features are disabled because the application is not running as an administrator.",
                    @"Limited Access", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Check if the azcmagent service is installed and the executable exists on this host before running the application  
            if (ApplicationFunctions.IsAzcmAgentInstalled(Globals.azcmagentPath))
            {
                // Show a message box indicating that the service is installed and the executable exists - tool can be used  
                txtOutput.Text = $@"The '{Globals.azcmagentServiceName}' service is installed and the executable '{Globals.azcmagentPath}' exists.";

                // Check if the executable is signed  
                if (ApplicationFunctions.IsAzcmAgentInstalled(Globals.azcmagentPath))
                {
                    // Check if the executable is codesigned by Microsoft
                    bool isExeCodeSigned = ApplicationFunctions.IsFileCodeSignedByMicrosoft(Globals.azcmagentPath);
                    if (!isExeCodeSigned)
                    {
                        MessageBox.Show($@"The executable {Globals.azcmagentPath} is not code signed by Microsoft.", @"Cant validate the Azure Arc Agent is from Microsoft", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtOutput.Text += Environment.NewLine + $@"The executable '{Globals.azcmagentPath}' is not digitally signed or the signature is invalid.";
                    }
                    else
                    {
                        txtOutput.Text += Environment.NewLine + $@"The executable '{Globals.azcmagentPath}' is digitally signed and verified.";
                    }
                }
                else
                {
                    MessageBox.Show($@"The executable '{Globals.azcmagentPath}' is not digitally signed or the signature is invalid.", @"Security Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Show a message box indicating that the service is not installed or the executable is missing - tool cannot be used  
                MessageBox.Show($@"Either the '{Globals.azcmagentServiceName}' service is not installed, or the executable '{Globals.azcmagentPath}' is missing on this host.", @"Checks failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                tabControlMainForm.Enabled = false; // Disable the tab control
            }
        }

        /// <summary>
        /// Check if the Azure Arc agent is up to date and prompt the user to update if necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lblUpdateArcAgent_Click(object sender, EventArgs e)
        {
            var (isUpToDate, installedVersion, latestVersion) = await IsAzureArcAgentUpToDate();

            if (ApplicationFunctions.IsRunningInAzureOrHardware())
            {
                MessageBox.Show(@"This machine is running in Azure. Azure Arc agent is not designed for Azure VMs.", @"Azure Environment Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //MessageBox.Show($@"Downloading the Azure Arc agent from '{ApplicationFunctions.GetAzureArcAgentInstallerUrl()}' to install the update.", @"Installer URL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Check if the agent is up to date
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
        /// Azcmagent show "Agent Version" "Agent Logfile" "Agent Status" "Agent Last Heartbeat
        private async void lblCheckVersion_Click(object sender, EventArgs e)
        {
            const string agentversionCheck = "show \"Agent Version\" \"Agent Logfile\" \"Agent Status\" \"Agent Last Heartbeat\" ";
            txtOutput.Text = await RunAzCmAgentCommand(agentversionCheck);
        }

        /// Azcmagent show "Agent Error Code" "Agent Error Details" "Agent Error Timestamp"
        private async void lblCheckAgentError_Click(object sender, EventArgs e)
        {
            const string agentErrorCheck = "show \"Agent Error Code\" \"Agent Error Details\" \"Agent Error Timestamp\"  ";
            txtOutput.Text = await RunAzCmAgentCommand(agentErrorCheck);
        }

        /// Azcmagent config list
        private async void lblShowAgentConfig_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config list");
        }

        /// Azcmagent config get config.mode
        private async void lblShowAgentMode_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config get config.mode");
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
                    var strLogfilePath = Path.Combine(selectedPath, ApplicationFunctions.generateDynamicLogName("AzcmagentLogs") + ".zip");

                    // Log to txtOutput what is being done
                    txtOutput.Text = @"Exporting logs to '" + strLogfilePath + @"'...";

                    // Run the command to export logs
                    txtOutput.Text = await RunAzCmAgentCommand("logs --full --output " + strLogfilePath);

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

        /// Azcmagent show full machine metadata and agent details
        private async void lblGetFullDetails_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("show");
        }

        /// Put the agent in full mode
        private async void lblChangeModeToFull_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config set config.mode full");
        }

        /// Put the agent in monitor mode
        private async void lblChangeModeToMonitor_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config set config.mode monitor");
        }

        /// Change the agent to tier 0 mode

        private async void lblChangeTier0_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config set incomingconnections.enabled false");
            txtOutput.Text += await RunAzCmAgentCommand("config set guestconfiguration.enabled false");
            txtOutput.Text += await RunAzCmAgentCommand("config set extensions.allowlist \"Microsoft.Azure.Monitor/AzureMonitorWindowsAgent,Microsoft.Azure.AzureDefenderForServers/MDE.Windows\"");
            txtOutput.Text += @"Incoming connections & guestconfiguration are disabled, only the Azure Monitor Agent and Defender extensions are enabled!";

        }

        /// <summary>
        /// Check the connection to Azure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lblCheckAgentConnection_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("check -v");
        }

        /// <summary>
        /// Restart the Azure Arc service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lblRestartService_Click(object sender, EventArgs e)
        {
            string serviceName = Globals.azcmagentServiceName; // Azure Hybrid Instance Metadata Service

            SetLabelStatus(lblStatus, @"Restarting...", System.Drawing.Color.Blue, true);

            txtOutput.Text = @"Restarting Azure Arc service...";
            string result = await ServiceManager.RestartServiceAsync(serviceName);

            txtOutput.Text = result;

            if (result.IndexOf($"Service '{serviceName}' started.", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                MessageBox.Show(@"Azure Arc service restarted successfully.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetLabelStatus(lblStatus, string.Empty, System.Drawing.Color.Black, false);
            }
            else
            {
                MessageBox.Show($@"Failed to restart Azure Arc service. Check the output for details.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetLabelStatus(lblStatus, @"Failed to restart Azure Arc service", System.Drawing.Color.Red, true);
            }
        }

        // Get the automatic upgrade configuration
        private async void lblGetAutomaticUpgradeConfig_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config get automaticupgrade.enabled");
        }

        private void lblManageExtentions_Click(object sender, EventArgs e)
        {
            using (var form = new ExtensionConfigForm())
            {
                form.Owner = this; // Set the owner to access shared methods
                form.ShowDialog();
            }
        }

        // Disable all extensions
        private async void lblDisableAllUseOfExtentions_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config set extensions.enabled false");
        }

        // Allow all extensions
        private async void lblAllowAllUseOfExtentions_Click(object sender, EventArgs e)
        {
            txtOutput.Text = await RunAzCmAgentCommand("config set extensions.enabled true");
        }

        // Open the GitHub repository in the default web browser and show the changelog file
        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Globals.changeLogURL);
        }
    }
}