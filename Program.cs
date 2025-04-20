using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AACMAToolkit
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

                bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent())
                    .IsInRole(WindowsBuiltInRole.Administrator);

                if (args.Length > 0 && args[0] == "ElevatedLevelForm")
                {
                    if (!isAdmin)
                    {
                        // Relaunch with elevation if not already elevated
                        var psi = new ProcessStartInfo
                        {
                            FileName = Application.ExecutablePath,
                            Arguments = "ElevatedLevelForm",
                            Verb = "runas",
                            UseShellExecute = true
                        };

                        try
                        {
                            Process.Start(psi);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("UAC Elevation failed:\n" + ex.Message);
                        }

                        return;
                    }

                    // Already elevated, open the form
                    Application.Run(new ElevatedLevelForm());
                }
                else
                {
                    Application.Run(new AACMAToolkit()); // Default form
                }
            }
        }
    }
