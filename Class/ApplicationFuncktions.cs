using System;

namespace AACMAToolkit.Class
{
    internal class ApplicationFuncktions
    {
        // Function to restart the application is running as administrator
        public static void RestartAsAdmin()
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
    }
}
