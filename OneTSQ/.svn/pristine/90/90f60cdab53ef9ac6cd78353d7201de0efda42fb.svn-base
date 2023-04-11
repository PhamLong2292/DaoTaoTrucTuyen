using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneTSQ.PrintServer
{
    public partial class frmMain : Form
    {
        bool AllowClosed = false;
        WinsocketApp OWinsocketApp = new WinsocketApp();
        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBoxLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBoxLog.Text = text;
            }
        }

        public frmMain()
        {
            InitializeComponent();

            OWinsocketApp.LogEvent += OWinsocketApp_LogEvent;
            OWinsocketApp.StartService();
        }

        void OWinsocketApp_LogEvent(string Message)
        {
            

            if (this.textBoxLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { Message + "\r\n" + textBoxLog.Text });
            }
            else
            {
                textBoxLog.Text = Message + "\r\n" + textBoxLog.Text;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!AllowClosed)
            {
               // MessageBox.Show(this, "Dịch vụ điều khiển máy in không cho phép đóng từ cửa sổ", "Thông báo");
                
                notifyIcon.Visible = true;
                this.Visible = false;
                e.Cancel=true;
                return;
            }
            OWinsocketApp.StopService();
        }

        private void frmMain_MinimumSizeChanged(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
        }

        private void frmMain_MaximumSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                this.Visible = false;
            }
            if (WindowState == FormWindowState.Normal || WindowState==FormWindowState.Maximized)
            {
                notifyIcon.Visible = false;
                this.Visible = true;
            }
            
        }

        private void mExit_Click(object sender, EventArgs e)
        {
            AllowClosed = true;
            this.Close();
        }

        private void mConfig_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.BringToFront();
            notifyIcon.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void cmdConfigPrinter_Click(object sender, EventArgs e)
        {
            frmListPrinters frmListPrinters = new frmListPrinters();
            frmListPrinters.InitPrinters();
            frmListPrinters.ShowDialog();
            frmListPrinters.Dispose();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process proc = Process.GetCurrentProcess();
            //kill it and close programm
            proc.Kill();
            Application.ExitThread();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
            this.Visible = false;
        }

        private void cmdDesigner_Click(object sender, EventArgs e)
        {
            string App = Application.StartupPath + "\\C1ReportDesigner.exe";
            Process.Start(App);
        }
    }
}
