using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
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
        
        ///create Process and give arguments via string
        ///Azcmagent(arguments) ex: Azcmagent(show config)
        ///RedirectStandard = capture Output & Error
        ///UseShellExecute = process won't use system shell
        ///CreateNoWindow = no window pop-up whatsoever

        private async Task<string> runAzCmAgentCommand(string args)
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

                // Launch process with arguments asynchronously
                // Captures Output/error from the command
                // Waits the process to finish
                // Returns the output/error

                using (var process = new Process())
                {
                    process.StartInfo = psi;
                    process.EnableRaisingEvents = true;
                    var outputBuilder = new StringBuilder();
                    var errorBuilder = new StringBuilder();

                    process.OutputDataReceived += (sender, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
                    process.ErrorDataReceived += (sender, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await Task.Run(() => process.WaitForExit());

                    var output = outputBuilder.ToString();
                    var error = errorBuilder.ToString();

                    return string.IsNullOrWhiteSpace(output) ? error : output;
                }
            }

            // Handle specific exceptions
            catch (FileNotFoundException ex)
            {
                return $"Error: {ex.Message} - Azcmagent not found. Please ensure it is installed.";
            }
            catch (UnauthorizedAccessException ex)
            {
                return $"Error: {ex.Message} - Access denied. Please run the application as administrator.";
            }
            catch (InvalidOperationException ex)
            {
                return $"Error: {ex.Message} - Invalid operation. Please check the command.";
            }
            // Exception handling
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
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
            //string strLogspath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string strLogfilePath = @"C:\temp\AzcmagentLogs.zip"; // Fixed unrecognized escape sequence by using @  

            txtOutput.Text = await runAzCmAgentCommand("logs --full --output " + strLogfilePath);
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
    }
}