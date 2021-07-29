using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shaker
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new SDlg());

            bool createNew;
            Mutex dup = new Mutex(true, "Sker", out createNew);

            if (createNew)
            {
                Application.Run(new SDlg());
                dup.ReleaseMutex();
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Already running", "Screen Saver", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


        }
    }
}
