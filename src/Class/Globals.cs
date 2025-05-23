using System;

namespace AACMAToolkit.Class
{
    public class Globals
    {
        // Global strings used in the application
        public static string appName = "AACMAToolkit"; // Update the application name as needed
        public static string logName = "AACMAToolkit.log";
        public static string logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + appName +"\\" + logName;
        public static string logPathDynamic = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + appName + "\\" + ApplicationFunctions.GenerateDynamicLogName(logName);
        public static string toolLongName = "Azure Connected Machine Agent";
        public static string azcmagentPath = @"C:\Program Files\AzureConnectedMachineAgent\azcmagent.exe"; // Update the path as needed
        public static string azcmagentServiceName = "himds"; // Update the service name as needed - Azure Hybrid Instance Metadata Service
        public static string changeLogURL = "https://github.com/enderalci/AACMAToolkit/blob/master/CHANGELOG.md"; // Update the URL as needed
    }
}
