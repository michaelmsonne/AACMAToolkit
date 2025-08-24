using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AACMAToolkit.Class
{
    internal class Files
    {
        public static string ProgramDataFilePath
        {
            get
            {
                // Path to the program data folder
                var programDataFilePathvar = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Globals.appName;
                return programDataFilePathvar;
            }
        }

        public static string LogFilePath
        {
            get
            {
                // Root folder for log files
                var logfilePathvar = ProgramDataFilePath + @"\Log";
                return logfilePathvar;
            }
        }
    }
}
