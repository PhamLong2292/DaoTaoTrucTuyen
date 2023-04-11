using C1.C1Report;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneTSQ.PrintServer
{
    public class PrintQueueCls
    {
        public string XmlReportFile;
        public string PrinterName;
        public string KeyId;
        public string Info;
        public int RptIndex;
        public string RptName;
        public string ServiceId;
        public string ServiceName;
    }

    

    public class WinsocketApp
    {
        Timer OTimer = null;
        public Collection<PrintQueueCls> 
            PrintQueues = new Collection<PrintQueueCls> { };

        public delegate void LogEventHandler(string Message);
        public event LogEventHandler LogEvent;
        C1Report c1Report1 = new C1Report();
        C1ReportController
                    OC1ReportController = new C1ReportController();
        public void LogMsg(string Msg)
        {
            if (LogEvent != null)
            {
                LogEvent(Msg);
            }
        }


        public Collection<WebSocketSession>
            WebSocketSessions = new Collection<WebSocketSession> { };

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public void StopService()
        {
            OTimer.Enabled = false;
            OTimer.Dispose();

            for (int iIndex = 0;  iIndex < WebSocketSessions.Count; iIndex++)
            {
                WebSocketSessions[iIndex].Close();
                
            }
        }

        WebSocketServer WebSocketServer = new WebSocketServer();
        string LocalIPAddress = "";
        public void StartService()
        {
            OTimer = new Timer();
            OTimer.Interval = 1000;
            OTimer.Enabled = true;
            OTimer.Tick += OTimer_Tick;
            LocalIPAddress = GetLocalIPAddress();
            LogMsg("S247 Printer Server Service");
            LogMsg("S247 Printer Service sender started at [" + LocalIPAddress + "]...");
            OC1ReportController.ProcessMessageEvent += OC1ReportController_ProcessMessageEvent;
            
            WebSocketServer.NewDataReceived += new SessionEventHandler<WebSocketSession, byte[]>(WebSocketServer_NewDataReceived);
            WebSocketServer.NewSessionConnected += new SessionEventHandler<WebSocketSession>(WebSocketServer_NewSessionConnected);
            WebSocketServer.NewMessageReceived += new SessionEventHandler<WebSocketSession, string>(WebSocketServer_NewMessageReceived);
            WebSocketServer.SessionClosed += new SessionEventHandler<WebSocketSession, SuperSocket.SocketBase.CloseReason>(WebSocketServer_SessionClosed);
            WebSocketServer.Setup(new RootConfig(), new ServerConfig
            {
                Port = 4444,
                Ip = "Any",
                MaxConnectionNumber = 5000,
                Mode = SocketMode.Async,
                Name = "Printer Listener Service"
            }, SocketServerFactory.Instance);
            WebSocketServer.Start();

            LogMsg("Printer Listener Service");
            LogMsg("Printer Listener Service 4444...");
        }

        void OC1ReportController_ProcessMessageEvent(string Message)
        {
            LogMsg(Message);
        }

        

        void WebSocketServer_NewDataReceived(WebSocketSession session, byte[] e)
        {
            LogMsg("Data: " + e.ToString());
            for (int iIndex = 0; iIndex < WebSocketSessions.Count; iIndex++)
            {
                WebSocketSessions[iIndex].SendResponse(e.ToString());
            }
        }

        void WebSocketServer_NewSessionConnected(WebSocketSession session)
        {
            LogMsg("Connected : " + session.LocalEndPoint);// + " / SessionId: " + session.SessionID
        }

        void WebSocketServer_NewMessageReceived(WebSocketSession session, string e)
        {
            string data = e;
            ProcessData(session, e);
            //LogMsg(session.SessionID + " / Message: " + e);
        }

        void WebSocketServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason e)
        {
            int iIndex = 0;
            while (iIndex < WebSocketSessions.Count)
            {
                if (WebSocketSessions[iIndex] == session)
                {
                    WebSocketSessions.RemoveAt(iIndex);
                }
                else
                {
                    iIndex++;
                }
            }
            LogMsg("disconnected");
        }

        void ProcessData(WebSocketSession session, string e)
        {
            try
            {
                if (e.IndexOf("{connected-server}") != -1)
                {
                    string[] Items = e.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Items.Length == 2)
                    {
                        session.ShopCode = Items[1];
                    }
                    session.SendResponse(LocalIPAddress + ": Welcome Printer Service. Connected at " + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                    WebSocketSessions.Add(session);
                }
                if (e.IndexOf("print") != -1)
                {
                    string ConfigXml = Application.StartupPath + "\\xmls\\Config.xml";
                    if (!System.IO.File.Exists(ConfigXml))
                    {
                        LogEvent("Chưa thiết lập cấu hình máy in");
                        return;
                    }
                    string XmlReportFile = Application.StartupPath + "\\xmls\\PrintTemplate.xml";
                    if (!System.IO.File.Exists(XmlReportFile))
                    {
                        LogEvent("Chưa thiết lập mẫu in");
                        return;
                    }

                    DataSet dsPrinters = new DataSet();
                    dsPrinters.ReadXml(ConfigXml);
                    //split dữ liệu truyền từ client lên
                    string[] Items = e.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    string PrintServiceId = Items[1];
                    string KeyId = Items[2];
                    string Info = Items[3];

                    DataTable dtPrint = dsPrinters.Tables["Config"];
                    if (dtPrint == null)
                    {
                        throw new Exception("Chưa thiết lập cấu hình máy in");
                    }
                    
                    string[] reports = new string[0];

                    for (int iIndex = 0; iIndex < dtPrint.Rows.Count; iIndex++)
                    {
                        string ServiceId = (string)dtPrint.Rows[iIndex]["ServiceId"];
                        string ServiceName = (string)dtPrint.Rows[iIndex]["ServiceName"];
                        string Printer = (string)dtPrint.Rows[iIndex]["Printer"];
                        string PrintTemplate = (string)dtPrint.Rows[iIndex]["PrintTemplate"];

                        if (PrintServiceId.Trim().ToLower().Equals(ServiceId.Trim().ToLower()))
                        {
                            reports = c1Report1.GetReportInfo(XmlReportFile);
                            int RptIndex = -1;
                            for (int jIndex = 0; jIndex < reports.Length; jIndex++)
                            {
                                if (reports[jIndex].Equals(PrintTemplate))
                                {
                                    RptIndex = jIndex;
                                    break;
                                }
                            }
                            if (RptIndex == -1)
                            {
                                LogEvent("Không tìm thấy mẫu báo cáo tên [" + PrintTemplate + "]");
                            }
                            else
                            {
                                PrintQueueCls
                                    OPrintQueue = new PrintQueueCls();

                                OPrintQueue.XmlReportFile = XmlReportFile;
                                OPrintQueue.RptIndex = RptIndex;
                                OPrintQueue.RptName = PrintTemplate;
                                OPrintQueue.PrinterName = Printer;
                                OPrintQueue.ServiceId = ServiceId;
                                OPrintQueue.ServiceName = ServiceName;
                                OPrintQueue.KeyId = KeyId;
                                OPrintQueue.Info = Info;

                                PrintQueues.Add(OPrintQueue);
                            }
                        }
                    }
                    dsPrinters.Clear();
                    dsPrinters.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogMsg(ex.Message.ToString());
            }
        }


        bool Process = false;
        void OTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Process) return;
                Process = true;
                
                if (PrintQueues.Count > 0)
                {
                    PrintQueueCls OPrintQueue = PrintQueues[0];
                    PrintQueues.RemoveAt(0);

                    
                    LogEvent(OPrintQueue.ServiceName + ": " + OPrintQueue.Info + " -> " + OPrintQueue.PrinterName + " (" + OPrintQueue.RptName + ")");
                    try
                    {
                        OC1ReportController.ExecutePrint(OPrintQueue.ServiceId, OPrintQueue.KeyId, "In", OPrintQueue.XmlReportFile,  OPrintQueue.RptIndex, OPrintQueue.PrinterName);
                    }
                    catch (Exception ex)
                    {
                        ex.Source = "";
                        LogMsg(OPrintQueue.ServiceId + ": ERROR");// + ex.Message.ToString());
                    }
                }
                Process = false;
            }
            catch (Exception ex)
            {
                Process = false;
                LogMsg(ex.Message.ToString());
            }
        }

    }
}
