using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Path = System.IO.Path;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;

namespace AACMAToolkit


///Developed by Ender Alci (Enter Consult BV)
{
    public partial class AACMAToolkit : Form
    {
        public AACMAToolkit()
        {
            InitializeComponent();
        }
        private void AACMAToolkit_Load(object sender, EventArgs e)
        {

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

        private void lblOpenConfigMode_Click(object sender, EventArgs e)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Arguments = "ElevatedLevelForm", // Pass argument!
                    UseShellExecute = true,
                    Verb = "runas"
                };

                Process.Start(psi);
                Application.Exit(); // Exit current instance
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to elevate:\n" + ex.Message);
            }
        }
    }
}
