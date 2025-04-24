using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AACMAToolkit
{
    public partial class MainForm : Form
    {
        private bool IsAzureArcAgentUpToDate(out string installedVersion, out string latestVersion)
        {
            installedVersion = string.Empty;
            latestVersion = string.Empty;

            try
            {
                // Step 1: Get the installed version
                string versionCommand = "show \"Agent Version\"";
                installedVersion = RunAzCmAgentCommand(versionCommand).Trim();

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
                        MessageBox.Show("Failed to extract the latest version from the release notes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Step 3: Compare the normalized installed version with the latest version
                return string.Equals(normalizedInstalledVersion, latestVersion, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while checking Azure Arc agent version: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public MainForm()
        {
            InitializeComponent();

            // Check if the application is running as admin or not
            var isAdmin = Class.ApplicationsChecks.IsRunningAsAdmin();

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

        ///create Process and give arguments via string
        ///Azcmagent(arguments) ex: Azcmagent(show config)
        ///RedirectStandard = capture Output & Error
        ///UseShellExecute = process won't use system shell
        ///CreateNoWindow = no window pop-up whatsoever

        private string RunAzCmAgentCommand(string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "azcmagent",
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                /// Launch process with arguments
                /// Captures Output/error from the command
                /// Waits the process to finish
                /// Returns the output/error

                using (var process = Process.Start(psi))
                {
                    var output = process.StandardOutput.ReadToEnd();
                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return string.IsNullOrWhiteSpace(output) ? error : output;
                }
            }

            /// Exception handling
            ///
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        private void UpdateAzureArcAgent()
        {
            // Logic to update the Azure Arc agent
            var installerUrl = "https://aka.ms/AzureConnectedMachineAgent"; // Works too: https://gbl.his.arc.azure.com/azcmagent/latest/AzureConnectedMachineAgent.msi
                                                                            // See more here: https://gbl.his.arc.azure.com/azcmagent-windows
            var tempPath = Path.GetTempPath();
            var installerPath = Path.Combine(tempPath, "AzureConnectedMachineAgent.msi");

            try
            {
                using (var client = new WebClient())
                {
                    MessageBox.Show(@"Downloading Azure Connected Machine Agent...");
                    client.DownloadFile(installerUrl, installerPath);
                }

                var installer = new Process();
                installer.StartInfo.FileName = "msiexec.exe";
                installer.StartInfo.Arguments = $"/i \"{installerPath}\" /quiet /qn /norestart";
                installer.StartInfo.Verb = "runas"; // Admin rights
                installer.StartInfo.UseShellExecute = true;
                installer.Start();

                // Wait for the installer to finish
                installer.WaitForExit();

                // Check if the installation was successful
                MessageBox.Show(@"Installation completed.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the download or installation
                MessageBox.Show(@"Error: " + ex.Message);
            }
        }

        private void lblUpdateArcAgent_Click(object sender, EventArgs e)
        {
            if (IsAzureArcAgentUpToDate(out string installedVersion, out string latestVersion))
            {
                MessageBox.Show($"Azure Arc agent is up-to-date.\nInstalled Version: {installedVersion}\nLatest Version: {latestVersion}",
                    "Version Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Azure Arc agent is not up-to-date.\nInstalled Version: {installedVersion}\nLatest Version: {latestVersion}",
                    "Version Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Ask if the user wants to update
                var result = MessageBox.Show("Do you want to update the Azure Arc agent?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    UpdateAzureArcAgent();
                }
            }
        }

        /// Execute the click event with the right params --- Params are CASE SENSITIVE
        /// Azcmagent show "Agent Version" "Agent Logfile" "Agent Status" "Agent Last Heartbeat"
        ///
        private void lblCheckVersion_Click(object sender, EventArgs e)
        {
            var AgentversionCheck = "show \"Agent Version\" \"Agent Logfile\" \"Agent Status\" \"Agent Last Heartbeat\" ";
            txtOutput.Text = RunAzCmAgentCommand(AgentversionCheck);
        }

        /// Azcmagent show "Agent Error Code" "Agent Error Details" "Agent Error Timestamp"
        private void lblCheckAgentError_Click(object sender, EventArgs e)
        {
            var AgentErrorCheck = "show \"Agent Error Code\" \"Agent Error Details\" \"Agent Error Timestamp\"  ";
            txtOutput.Text = RunAzCmAgentCommand(AgentErrorCheck);
        }

        /// Azcmagent config list
        private void lblShowAgentConfig_Click(object sender, EventArgs e)
        {
            txtOutput.Text = RunAzCmAgentCommand("config list");
        }

        /// Azcmagent config get config.mode
        private void lblShowAgentMode_Click(object sender, EventArgs e)
        {
            txtOutput.Text = RunAzCmAgentCommand("config get config.mode");
        }

        /// Put the agent in full mode
        private void lblChangeMode2Full_Click(object sender, EventArgs e)
        {
            txtOutput.Text = RunAzCmAgentCommand("config set config.mode full");
        }

        /// Put the agent in monitor mode
        private void label1_Click(object sender, EventArgs e)
        {
            txtOutput.Text = RunAzCmAgentCommand("config set config.mode monitor");
        }

        private void lblExportLogs_Click(object sender, EventArgs e)
        {
            string strLogspath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string strLogfilePath = @"C:\temp\AzcmagentLogs.zip"; // Fixed unrecognized escape sequence by using @  

            txtOutput.Text = RunAzCmAgentCommand("logs --full --output " + strLogfilePath);
        }

        private void restartAsAdministratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call the function to restart the application as admin
            Class.ApplicationFuncktions.RestartAsAdmin();
        }
    }
}