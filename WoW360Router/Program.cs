using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace DavidNikdel
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("Bad.dll")]
        private static extern int Test();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // test to make sure the DLL is loaded
                XInputDotNet.XInput.GetPadState(0);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Could not load XInput9_1_0.dll. Please check Microsoft's website to be sure you have the most current version of DirectX9 installed (Feb 2006 release).",
                    "DirectX Error - Could not load XInput9_1_0.dll");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (WoW360Router app = new WoW360Router())
            {
                Application.Run(app);
            }
        }
    }
}