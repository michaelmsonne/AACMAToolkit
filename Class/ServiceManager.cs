using System;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AACMAToolkit.Class
{
    public static class ServiceManager
    {
        /// <summary>
        /// Restarts a Windows service by stopping and starting it.
        /// </summary>
        /// <param name="serviceName">The name of the service to restart.</param>
        /// <param name="timeoutSeconds">The timeout in seconds for each operation (stop/start).</param>
        /// <returns>A string containing the status of the operation.</returns>
        public static async Task<string> RestartServiceAsync(string serviceName, int timeoutSeconds = 30)
        {
            try
            {
                using (var serviceController = new ServiceController(serviceName))
                {
                    var output = new StringBuilder();

                    // Stop the service if it's running
                    if (serviceController.Status == ServiceControllerStatus.Running)
                    {
                        output.AppendLine("Stopping service...");
                        serviceController.Stop();
                        await Task.Run(() => serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(timeoutSeconds)));
                        output.AppendLine("Service stopped.");
                    }

                    // Start the service
                    output.AppendLine("Starting service...");
                    serviceController.Start();
                    await Task.Run(() => serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(timeoutSeconds)));
                    output.AppendLine("Service started.");

                    return output.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
