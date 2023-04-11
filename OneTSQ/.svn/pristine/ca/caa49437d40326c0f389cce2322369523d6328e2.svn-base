using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneTSQ.PrintServer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show(null, "Dịch vụ đã được chạy vui lòng kiểm tra trong taskbar hoặc endtask trong Task Manager","Thông báo");
                return;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
    
}
