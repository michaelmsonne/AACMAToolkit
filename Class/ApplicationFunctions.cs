using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.ServiceProcess;
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

        public static string generateDynamicLogName(string logName)
        {
            var hostname = Environment.MachineName;
            var timestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            return $"{hostname}_{timestamp}_{logName}";
        }

        /// <summary>
        /// Checks if the Azure Arc agent service (azcmagent) is installed and if the executable exists in the specified path.
        /// </summary>
        /// <param name="exePath">The full path to the azcmagent.exe file.</param>
        /// <returns>True if both the service is installed and the executable exists, otherwise false.</returns>
        public static bool IsAzcmAgentInstalled(string exePath)
        {
            const string serviceName = "himds"; // Replace with the actual service name if different

            try
            {
                // Check if the service is installed
                bool isServiceInstalled = false;
                ServiceController[] services = ServiceController.GetServices();
                foreach (var service in services)
                {
                    if (service.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                    {
                        isServiceInstalled = true;
                        break;
                    }
                }

                // Check if the executable exists
                bool isExeExists = File.Exists(exePath);

                // Return true only if both conditions are met
                return isServiceInstalled && isExeExists;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                MessageBox.Show($"Error checking azcmagent installation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Function to restart the application is running as administrator
        public static void restartAsAdmin()
        {
            // Get the current process
            var process = Process.GetCurrentProcess();
            // Create a new process start info
            if (process.MainModule != null)
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = process.MainModule.FileName,
                    UseShellExecute = true,
                    Verb = "runas" // This will run the process as administrator
                };
                // Start the new process
                Process.Start(startInfo);
            }

            // Exit the current process
            Environment.Exit(0);
        }

        /// <summary>
        /// Checks if the current machine is running in Azure.
        /// </summary>
        /// <returns>True if the machine is in Azure, otherwise false.</returns>
        public static bool IsRunningInAzure()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Metadata", "true");
                    string metadataUrl = "http://169.254.169.254/metadata/instance/compute?api-version=2019-06-01";
                    string response = client.DownloadString(metadataUrl);

                    if (!string.IsNullOrEmpty(response) && response.Contains("resourceId"))
                    {
                        // Check for MSFT_ARC_TEST environment variable
                        string overrideTest = Environment.GetEnvironmentVariable("MSFT_ARC_TEST", EnvironmentVariableTarget.Machine);
                        if ("true".Equals(overrideTest, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show(
                                @"Running on an Azure Virtual Machine with MSFT_ARC_TEST set.
        Azure Connected Machine Agent is designed for use outside Azure.
        This virtual machine should only be used for testing purposes.
        See https://aka.ms/azcmagent-testwarning for more details.",
                                @"Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                @"Cannot install Azure Connected Machine agent on an Azure Virtual Machine.
        Azure Connected Machine Agent is designed for use outside Azure.
        To connect an Azure VM for TESTING PURPOSES ONLY, see https://aka.ms/azcmagent-testwarning for more details."
                            );
                        }

                        return true;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                // Ignore other exceptions and assume not running in Azure
            }

            return false;
        }

        /// <summary>
        /// Gets the appropriate Azure Arc agent installer URL based on the environment.
        /// </summary>
        /// <returns>The URL of the Azure Arc agent installer.</returns>
        public static string GetAzureArcAgentInstallerUrl()
        {
            string hisEndpoint = "https://gbl.his.arc.azure.com";

            // Determine the environment
            string cloudEnvironment = Environment.GetEnvironmentVariable("CLOUD");

            if (!string.IsNullOrEmpty(cloudEnvironment))
            {
                switch (cloudEnvironment)
                {
                    case "AzureUSGovernment":
                        hisEndpoint = "https://gbl.his.arc.azure.us";
                        break;
                    case "AzureChinaCloud":
                        hisEndpoint = "https://gbl.his.arc.azure.cn";
                        break;
                    case "AzureStackCloud":
                        string altHisEndpoint = Environment.GetEnvironmentVariable("AltHisEndpoint");
                        if (!string.IsNullOrEmpty(altHisEndpoint))
                        {
                            hisEndpoint = altHisEndpoint;
                        }
                        else
                        {
                            // Log a warning or handle the default case
                            MessageBox.Show(@"Invalid HIS endpoint for AzureStackCloud. Using default endpoint.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                }
            }

            // Construct the full installer URL
            return $"{hisEndpoint}/azcmagent/latest/AzureConnectedMachineAgent.msi";
        }

        public static void updateAzureArcAgent()
        {
            // Logic to update the Azure Arc agent
            var installerUrl = GetAzureArcAgentInstallerUrl();
            //var installerUrl = "https://aka.ms/AzureConnectedMachineAgent"; // Works too: https://gbl.his.arc.azure.com/azcmagent/latest/AzureConnectedMachineAgent.msi
                                                                            // See more here: https://gbl.his.arc.azure.com/azcmagent-windows

#if DEBUG
            // For debugging purposes, copy URL to clipboard
            Clipboard.SetText(GetAzureArcAgentInstallerUrl());
            MessageBox.Show(@"Installer URL copied to clipboard for debugging purposes.", @"Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif

            var tempPath = Path.GetTempPath();
            var installerPath = Path.Combine(tempPath, "AzureConnectedMachineAgent.msi");

            try
            {
                using (var client = new WebClient())
                {
                    // Download the installer
                    client.DownloadFile(installerUrl, installerPath);

                    // Show a message box to inform the user about the download
                    MessageBox.Show($@"Downloading {Globals.toolLongName}...");
                }

                // Start the installer process
                var installer = new Process();
                installer.StartInfo.FileName = "msiexec.exe";
                installer.StartInfo.Arguments = $"/i \"{installerPath}\" /quiet /qn /norestart";
                installer.StartInfo.Verb = "runas"; // Admin rights
                installer.StartInfo.UseShellExecute = true;
                installer.Start();

                // Wait for the installer to finish
                installer.WaitForExit();

                // Check if the installation was successful
                MessageBox.Show(@"Installation completed.", $@"Updated {Globals.toolLongName}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the download or installation
                MessageBox.Show($@"Error installing {Globals.toolLongName}: " + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
