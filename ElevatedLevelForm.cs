using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AACMAToolkit
{
    public partial class ElevatedLevelForm : Form
    {
        public ElevatedLevelForm()
        {
            InitializeComponent();
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

        /// Execute the click event with the right params --- Params are CASE SENSITIVE
        /// Azcmagent show "Agent Version" "Agent Logfile" "Agent Status" "Agent Last Heartbeat"

        private void lblCheckVersion_Click(object sender, EventArgs e)
        {
            string AgentversionCheck = "show \"Agent Version\" \"Agent Logfile\" \"Agent Status\" \"Agent Last Heartbeat\" ";
            txtOutput.Text = RunAzCmAgentCommand(AgentversionCheck);
        }


        /// Azcmagent show "Agent Error Code" "Agent Error Details" "Agent Error Timestamp"

        private void lblCheckAgentError_Click(object sender, EventArgs e)
        {
            string AgentErrorCheck = "show \"Agent Error Code\" \"Agent Error Details\" \"Agent Error Timestamp\"  ";
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

        private void lblUpdateArcAgent_Click(object sender, EventArgs e)
        {

            string installerUrl = "https://aka.ms/AzureConnectedMachineAgent";
            string tempPath = Path.GetTempPath();
            string installerPath = Path.Combine(tempPath, "AzureConnectedMachineAgent.msi");

            try
            {
                using (WebClient client = new WebClient())
                {
                    MessageBox.Show("Downloading Azure Connected Machine Agent...");
                    client.DownloadFile(installerUrl, installerPath);
                }

                Process installer = new Process();
                installer.StartInfo.FileName = "msiexec.exe";
                installer.StartInfo.Arguments = $"/i \"{installerPath}\" /quiet /qn /norestart";
                installer.StartInfo.Verb = "runas"; // Admin rights
                installer.StartInfo.UseShellExecute = true;
                installer.Start();

                installer.WaitForExit();

                MessageBox.Show("Installation completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
