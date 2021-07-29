using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shaker
{
    public partial class SDlg : Form
    {
        public SDlg()
        {
            InitializeComponent();
        }

        private bool cursorUp = false;
        private const uint MOUSEMOVE = 0X0001;
        private const uint LBTNDOWN = 0X0002;
        private const uint LBTNUP = 0X0004;
        private const uint ABSMOVE = 0X0008;
        private int moveGap = 1;


        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        private void MouseShake()
        {
            mouse_event(MOUSEMOVE, 0, cursorUp ? moveGap : -moveGap, 0, 0);
            cursorUp = !cursorUp;
        }

        private void timerTick_Tick(object sender, EventArgs e)
        {
            MouseShake();
        }

        private void timerAltTab_Tick_1(object sender, EventArgs e)
        {
            SendKeys.Send("%{Tab}");
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            stopToolStripMenuItem.Checked = !stopToolStripMenuItem.Checked;
            ChangeStatus();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }

        private void ChangeStatus()
        {
            if(stopToolStripMenuItem.Checked == false)
            {
                timerTick.Start();
                notifyIcon.Text = "Screen Saver Off";
                notifyIcon.Icon = Resource1.scon;
            }
            else
            {
                timerTick.Stop();
                notifyIcon.Text = "Screen Saver On";
                notifyIcon.Icon = Resource1.scoff;
            }
        }

        private void SDlg_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            notifyIcon.Icon = Resource1.scon;
            stopToolStripMenuItem.Checked = false;
            altTabToolStripMenuItem.Checked = false;
        }

        private void ChangeAltTabStatus()
        {
            if(altTabToolStripMenuItem.Checked == true)
            {
                timerAltTab.Start();
            }
            else
            {
                timerAltTab.Stop();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit?", "Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void altTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeAltTabStatus();
        }
    }
}
