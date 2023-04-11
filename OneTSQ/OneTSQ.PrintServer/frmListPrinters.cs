using OneTSQ.PrintServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneTSQ.PrintServer
{
    public partial class frmListPrinters : Form
    {
        List<string> Printers = new List<string> { };
        string[] reports = new string[0];
        DataSet dsPrinters = new DataSet();
        PrintServiceTemplate[] 
            PrintServiceTemplates = new PrintServiceTemplate[0];

        public frmListPrinters()
        {
            InitializeComponent();

            dsPrinters.Namespace = "PrintConfig";
            dsPrinters.Tables.Add("Config");
            dsPrinters.Tables["Config"].Columns.Add("ServiceId");
            dsPrinters.Tables["Config"].Columns.Add("ServiceName");
            dsPrinters.Tables["Config"].Columns.Add("Printer");
            dsPrinters.Tables["Config"].Columns.Add("PrintTemplate");
            

            string ConfigXml = Application.StartupPath + "\\xmls\\Config.xml";
            if (System.IO.File.Exists(ConfigXml))
            {
                dsPrinters.ReadXml(ConfigXml);
            }

            PrintServiceTemplates = PrintServiceUtility.GetPrintServiceTemplate();
            for (int iIndex = 0; iIndex < PrintServiceTemplates.Length; iIndex++)
            {
                comboBoxServices.Items.Add(PrintServiceTemplates[iIndex].ServiceName);
            }
            comboBoxServices.SelectedIndex = 0;
        }

        public void InitPrinters()
        {
            Printers = PrinterServiceBll.GetPrinterList();
            
            for (int iIndex = 0; iIndex < Printers.Count; iIndex++)
            {
                comboBoxPrintBill.Items.Add(Printers[iIndex]);
                listBoxPrinters.Items.Add(Printers[iIndex]);
            }

            string XmlReportFile = Application.StartupPath + "\\xmls\\PrintTemplate.xml";
            reports = c1Report1.GetReportInfo(XmlReportFile);

            for (int iIndex = 0; iIndex < reports.Length; iIndex++)
            {
                comboBoxPrintBillTemplate.Items.Add(reports[iIndex]);
                comboBoxPrintBillTemplate.SelectedIndex = iIndex;
            }
            if (comboBoxPrintBill.SelectedIndex < 0) comboBoxPrintBill.SelectedIndex = 0;

            c1FlexGrid.DataSource = dsPrinters.Tables["Config"];
            c1FlexGrid.Cols["ServiceId"].Visible = false;

            c1FlexGrid.Cols["ServiceName"].Caption = "Dịch vụ";
            c1FlexGrid.Cols["ServiceName"].Width = 150;

            c1FlexGrid.Cols["Printer"].Caption = "Máy in";
            c1FlexGrid.Cols["Printer"].Width = 220;

            c1FlexGrid.Cols["PrintTemplate"].Caption = "Mẫu in";
            c1FlexGrid.Cols["PrintTemplate"].Width = 150;
            c1FlexGrid.Cols[0].Width = 10;
            
        }

       
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrintTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxPrinters.SelectedIndex < 0) throw new Exception("Chưa chọn máy in để in thử");
                Cursor.Current = Cursors.WaitCursor;
                string PrintName = Printers[listBoxPrinters.SelectedIndex];
                PrinterServiceBll.PrintTest(PrintName);
                Cursor.Current = Cursors.Default;
                
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(this, ex.Message.ToString(), "Thông báo");
            } 
            
        }

        private void cmdAddPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPrintBill.SelectedIndex < 0) throw new Exception("Chưa chọn máy in để in thử");
                if (comboBoxPrintBillTemplate.SelectedIndex < 0) throw new Exception("Chưa chọn mẫu in");
                Cursor.Current = Cursors.WaitCursor;
                string ServiceId = PrintServiceTemplates[comboBoxServices.SelectedIndex].ServiceId;
                string ServiceName = PrintServiceTemplates[comboBoxServices.SelectedIndex].ServiceName;
                string PrintName = Printers[comboBoxPrintBill.SelectedIndex];
                string Report = reports[comboBoxPrintBillTemplate.SelectedIndex];

                dsPrinters.Tables["Config"].Rows.Add(new object[]
                {
                    ServiceId,
                    ServiceName,
                    PrintName,
                    Report
                });
                string ConfigXml=Application.StartupPath + "\\xmls\\Config.xml";
                dsPrinters.WriteXml(ConfigXml);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(this, ex.Message.ToString(), "Thông báo");
            } 
        }

        private void c1FlexGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    int RowIndex = c1FlexGrid.Row - 1;
                    if (RowIndex < 0) return;
                    if (c1FlexGrid.Rows.Count <= 0) return;
                    dsPrinters.Tables["Config"].Rows.RemoveAt(RowIndex);
                    string ConfigXml = Application.StartupPath + "\\xmls\\Config.xml";
                    dsPrinters.WriteXml(ConfigXml);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(this, ex.Message.ToString(), "Thông báo");
            }
        }
    }
}
