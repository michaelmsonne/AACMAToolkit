using System;

namespace AACMAToolkit.Class
{
    public class Globals
    {
        // Global strings used in the application
        public static string logName = "AACMAToolkit.log";
        public static string logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AACMAToolkit\\" + logName;
        public static string logPathDynamic = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AACMAToolkit\\" + ApplicationFunctions.generateDynamicLogName(logName);
        public static string toolLongName = "Azure Connected Machine Agent";
        public static string azcmagentPath = @"C:\Program Files\AzureConnectedMachineAgent\azcmagent.exe"; // Update the path as needed
        public static string azcmagentServiceName = "himds"; // Update the service name as needed - Azure Hybrid Instance Metadata Service
    }
}
