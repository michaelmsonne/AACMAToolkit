using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Windows.Forms;

namespace AACMAToolkit.Class
{
    internal class ApplicationFunctions
    {
        public static bool isRunningAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        // Function to restart the application is running as administrator
        public static void restartAsAdmin()
        {
            // Get the current process
            var process = System.Diagnostics.Process.GetCurrentProcess();
            // Create a new process start info
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = process.MainModule.FileName,
                UseShellExecute = true,
                Verb = "runas" // This will run the process as administrator
            };
            // Start the new process
            System.Diagnostics.Process.Start(startInfo);
            // Exit the current process
            Environment.Exit(0);
        }

        public static void updateAzureArcAgent()
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
    }
}
