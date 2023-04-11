//----------------------------------------------------------------------
//
// ReportTest.cs
//
// Test application for the C1Report object.
//
//----------------------------------------------------------------------
using System;
using System.Data.SqlClient;
using System.Xml;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using C1.C1Report;

namespace OneTSQ.PrintServer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMainWithDataSource : System.Windows.Forms.Form
	{
        private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox lstReports;
		private System.Windows.Forms.StatusBar stat;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton tbActual;
		private System.Windows.Forms.ToolBarButton tbTwo;
        private System.Windows.Forms.ToolBarButton tbPage;
		private System.Windows.Forms.ToolBarButton tbPreview;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton tbPrint;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxZoom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxReport;
    
		private int SelectIndexReport;
		private System.Windows.Forms.PrintPreviewControl ppv;
		private DataTable dataSource;
        //private DataView dataView;
        private C1Report c1Report1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem chứcNăngToolStripMenuItem;
        private ToolStripMenuItem menuPrint;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem tExit;
        private Button cmdSaveDefault;

        //string C1ReportConnectionString = null;
        public string PrinterName;
        public string PrintServiceId;

        public delegate void RequestDataHandler(string PrintServiceId, int ReportIndex,string ReportTitle, ref DataTable dt);
        public event RequestDataHandler RequestDataEvent;
        public frmMainWithDataSource()
		{
			InitializeComponent();
            TopMost = true;
		}


        public void InitForm(
            string _PrintServiceId,
            string ReportTitle,
            string XMLReportFile,
            bool UseCombobox,
            int _SelectIndexReport,
            string _PrinterName)
        {
            SelectIndexReport = _SelectIndexReport;
            lstReports.Visible = !UseCombobox;
            comboBoxReport.Visible = UseCombobox;
            PrintServiceId = _PrintServiceId;
            this.Text = ReportTitle;
            SetReportDefinitionFile(XMLReportFile);
            comboBoxReport.SelectedIndex = SelectIndexReport;
            lstReports.SelectedIndex = SelectIndexReport;
            if (RequestDataEvent != null)
            {
                RequestDataEvent(PrintServiceId, comboBoxReport.SelectedIndex, comboBoxReport.Items[comboBoxReport.SelectedIndex].ToString(),  ref dataSource);
            }


            PrinterName = _PrinterName;
            LoadReport(lstReports.Text);
            RenderReport();
        }


        

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainWithDataSource));
            this.lstReports = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.stat = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbPreview = new System.Windows.Forms.ToolBarButton();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbActual = new System.Windows.Forms.ToolBarButton();
            this.tbPage = new System.Windows.Forms.ToolBarButton();
            this.tbTwo = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ppv = new System.Windows.Forms.PrintPreviewControl();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxZoom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxReport = new System.Windows.Forms.ComboBox();
            this.c1Report1 = new C1.C1Report.C1Report();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.chứcNăngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSaveDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstReports
            // 
            this.lstReports.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstReports.IntegralHeight = false;
            this.lstReports.ItemHeight = 20;
            this.lstReports.Location = new System.Drawing.Point(0, 61);
            this.lstReports.Name = "lstReports";
            this.lstReports.Size = new System.Drawing.Size(230, 433);
            this.lstReports.TabIndex = 0;
            this.lstReports.DoubleClick += new System.EventHandler(this.lstReports_DoubleClick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(230, 61);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 433);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // stat
            // 
            this.stat.Location = new System.Drawing.Point(0, 494);
            this.stat.Name = "stat";
            this.stat.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1});
            this.stat.ShowPanels = true;
            this.stat.Size = new System.Drawing.Size(768, 34);
            this.stat.TabIndex = 2;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 743;
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbPreview,
            this.tbPrint,
            this.toolBarButton1,
            this.tbActual,
            this.tbPage,
            this.tbTwo});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 33);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(768, 28);
            this.toolBar1.TabIndex = 4;
            this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbPreview
            // 
            this.tbPreview.ImageIndex = 9;
            this.tbPreview.Name = "tbPreview";
            this.tbPreview.ToolTipText = "Preview report in dialog";
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 10;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.ToolTipText = "Print report";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbActual
            // 
            this.tbActual.ImageIndex = 0;
            this.tbActual.Name = "tbActual";
            this.tbActual.ToolTipText = "Zoom to actual page";
            // 
            // tbPage
            // 
            this.tbPage.ImageIndex = 2;
            this.tbPage.Name = "tbPage";
            this.tbPage.ToolTipText = "Zoom to whole page";
            // 
            // tbTwo
            // 
            this.tbTwo.ImageIndex = 1;
            this.tbTwo.Name = "tbTwo";
            this.tbTwo.ToolTipText = "Zoom to two pages";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Red;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ppv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(240, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 433);
            this.panel1.TabIndex = 5;
            // 
            // ppv
            // 
            this.ppv.AutoZoom = false;
            this.ppv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ppv.Location = new System.Drawing.Point(0, 0);
            this.ppv.Name = "ppv";
            this.ppv.Size = new System.Drawing.Size(524, 429);
            this.ppv.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(422, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Zoom:";
            // 
            // comboBoxZoom
            // 
            this.comboBoxZoom.Items.AddRange(new object[] {
            "75",
            "100",
            "200"});
            this.comboBoxZoom.Location = new System.Drawing.Point(512, 4);
            this.comboBoxZoom.Name = "comboBoxZoom";
            this.comboBoxZoom.Size = new System.Drawing.Size(64, 28);
            this.comboBoxZoom.TabIndex = 7;
            this.comboBoxZoom.Text = "75";
            this.comboBoxZoom.TextChanged += new System.EventHandler(this.comboBoxZoom_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(602, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Báo cáo:";
            // 
            // comboBoxReport
            // 
            this.comboBoxReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReport.Location = new System.Drawing.Point(683, 4);
            this.comboBoxReport.Name = "comboBoxReport";
            this.comboBoxReport.Size = new System.Drawing.Size(367, 28);
            this.comboBoxReport.TabIndex = 9;
            this.comboBoxReport.TabStop = false;
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = resources.GetString("c1Report1.ReportDefinition");
            this.c1Report1.ReportName = "";
            this.c1Report1.StartReport += new System.EventHandler(this.c1Report1_StartReport);
            this.c1Report1.EndReport += new System.EventHandler(this.c1Report1_EndReport);
            this.c1Report1.NoData += new System.EventHandler(this.c1Report1_NoData);
            this.c1Report1.StartPage += new C1.C1Report.ReportEventHandler(this.c1Report1_StartPage);
            this.c1Report1.ReportError += new C1.C1Report.ReportEventHandler(this.c1Report1_ReportError);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chứcNăngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(768, 33);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // chứcNăngToolStripMenuItem
            // 
            this.chứcNăngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPrint,
            this.toolStripMenuItem1,
            this.tExit});
            this.chứcNăngToolStripMenuItem.Name = "chứcNăngToolStripMenuItem";
            this.chứcNăngToolStripMenuItem.Size = new System.Drawing.Size(109, 29);
            this.chứcNăngToolStripMenuItem.Text = "Chức năng";
            // 
            // menuPrint
            // 
            this.menuPrint.Name = "menuPrint";
            this.menuPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menuPrint.Size = new System.Drawing.Size(221, 30);
            this.menuPrint.Text = "In phiếu";
            this.menuPrint.Click += new System.EventHandler(this.menuPrint_Click_1);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(218, 6);
            // 
            // tExit
            // 
            this.tExit.Name = "tExit";
            this.tExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tExit.Size = new System.Drawing.Size(221, 30);
            this.tExit.Text = "Thoát";
            this.tExit.Click += new System.EventHandler(this.tExit_Click);
            // 
            // cmdSaveDefault
            // 
            this.cmdSaveDefault.Location = new System.Drawing.Point(1061, 3);
            this.cmdSaveDefault.Name = "cmdSaveDefault";
            this.cmdSaveDefault.Size = new System.Drawing.Size(149, 32);
            this.cmdSaveDefault.TabIndex = 11;
            this.cmdSaveDefault.Text = "Đặt ngầm định";
            this.cmdSaveDefault.UseVisualStyleBackColor = true;
            this.cmdSaveDefault.Click += new System.EventHandler(this.cmdSaveDefault_Click);
            // 
            // frmMainWithDataSource
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(768, 528);
            this.Controls.Add(this.cmdSaveDefault);
            this.Controls.Add(this.comboBoxReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxZoom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lstReports);
            this.Controls.Add(this.stat);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Name = "frmMainWithDataSource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IN PHIẾU";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        #endregion


		// current report definition file
		private string m_ReportFile;

		// last type of file exported
		private int m_FilterIndex;

		// for benchmarking
		private long m_Ticks;

		// open a new report definition file
		private void OpenReportDefinitionFile()
		{
			OpenFileDialog of = new OpenFileDialog();
			of.Filter = "Report Definition Files (*.xml)|*.xml";
			if (of.ShowDialog() == DialogResult.OK)
				SetReportDefinitionFile(of.FileName);
		}

		// save a report definition file containing the current report
		// or all reports in the current file
		private void SaveReportDefinitionFile(bool all)
		{
			// get file name
			SaveFileDialog sf = new SaveFileDialog();
			sf.Filter = "Report Definition Files (*.xml)|*.xml";
			sf.FileName = (all)
				? Path.GetFileName(m_ReportFile)
				: lstReports.Text + ".xml";
			if (sf.ShowDialog() != DialogResult.OK) return;

			// save a single report
			if (!all)
			{
                if (!LoadReport(lstReports.Text)) return;
				c1Report1.Save(sf.FileName);
				return;
			}

			// open xml file
			XmlTextWriter writer = new XmlTextWriter(sf.FileName, System.Text.Encoding.Default);
			writer.Formatting = Formatting.Indented;
			writer.Indentation = 2;
			writer.WriteStartDocument();
			writer.WriteStartElement("Reports");

			// save each report
			for (int i = 0; i < lstReports.Items.Count; i++)
			{
				lstReports.SelectedIndex = i;
                LoadReport(lstReports.Text);
				ShowStatus(string.Format("[{0}]: Đang ghi báo cáo...", lstReports.Text));
				c1Report1.Save(writer);
			}

			// close xml file
			writer.WriteEndElement(); // </Reports>
			writer.Close();
			ShowStatus("Sẵn sàng");
		}

		// populate the listbox with a list of all reports available 
		// in the selected file, save file name to open them later
		private bool SetReportDefinitionFile(string xmlFile)
		{
			// get list of reports in the file
			string[] reports;
			ShowStatus(string.Format("[{0}]: Đang mở báo cáo...", lstReports.Text));
			try
			{
				reports = c1Report1.GetReportInfo(xmlFile);
			}
			catch
			{
				MessageBox.Show("Lỗi khi mở file XML định nghĩa báo cáo.");
				return false;
			}
			ShowStatus("Sẵn sàng");

			// save file name to load reports later
			m_ReportFile = xmlFile;
			//Text = "C1Report for .NET - " + xmlFile;

			// populate listbox with report names
			lstReports.Items.Clear();
			comboBoxReport.Items.Clear();
			foreach (string report in reports)
			{
				lstReports.Items.Add(report);
				comboBoxReport.Items.Add(report);
			}

			// select first report
			if (lstReports.Items.Count > 0)
			{
				lstReports.SelectedIndex = 0;
				comboBoxReport.SelectedIndex=0;
			}

			
			// done
			return true;
		}

		// load the report that is currently selected in the listbox
		private bool LoadReport(string ReportName)
		{
			if (string.IsNullOrEmpty(ReportName))
				return false;
            //lstReports.Text
			bool retval = true;
            ShowStatus(string.Format("[{0}]: Loading...", ReportName));
			Cursor = Cursors.WaitCursor;
			try
			{
                c1Report1.Load(m_ReportFile, ReportName);
                c1Report1.DataSource.Recordset = dataSource;
			}
			catch (Exception x)
			{
				MessageBox.Show("Lỗi khi nạp báo cáo:\r\n" + x.Message);
				retval = false;
			}

			Cursor = Cursors.Default;
			ShowStatus("Sẵn sàng");
			return retval;
		}

		// render the report that is currently selected in the listbox
		private bool RenderReport()
		{
			// load report
            if (!LoadReport(lstReports.Text)) return false;

			// render report into ppv control
			ppv.StartPage = 0;
			ppv.Rows = 1;
			ppv.Columns = 1;
			ppv.AutoZoom = true;
			ppv.Document = c1Report1.Document;

			// done
			return true;
		}

		// print the report that is currently selected in the listbox
		public bool PrintReport(string PrinterName)
		{
			// load report
            if (!LoadReport(lstReports.Text)) return false;
            PrintDocument pDoc = c1Report1.Document;
            pDoc.PrinterSettings.PrinterName = PrinterName;
            pDoc.Print();

            //ConfigItemCls 
            //    OConfigItem = new ConfigItemCls();
            //OConfigItem.Init();
			// show print dialog
			

            //if (ShowDialog)
            //{
            //    PrintDialog pDlg = new PrintDialog();
            //    //if (!string.IsNullOrEmpty(OConfigItem.BillPrinterName))
            //    //{
            //    //     OConfigItem.BillPrinterName;
            //    //}
            //    pDoc.PrinterSettings.PrinterName = PrinterName;
            //    pDlg.PrinterSettings = pDoc.PrinterSettings;
            //    pDlg.Document = pDoc;
            //    DialogResult dr = pDlg.ShowDialog();

            //    // print the document
            //    if (dr == DialogResult.OK)
            //    {
            //        pDoc.PrinterSettings=pDlg.PrinterSettings;
            //        pDoc.Print();
            //        return true;
            //    }
            //}
            //else
            //{
            //    //pDoc.PrinterSettings.PrinterName = "HP LaserJet Professional P1102";
            //    //if (!string.IsNullOrEmpty(OConfigItem.BillPrinterName))
            //    //{
            //    //     OConfigItem.BillPrinterName;
            //    //}
            //    pDoc.PrinterSettings.PrinterName = PrinterName;
            //    pDoc.Print();
            //    return true;
            //}

			// canceled
			return true;
		}

        

		// print the report that is currently selected
        //private bool PreviewReport()
        //{
        //    if (lstReports.SelectedIndex < 0)
        //    {
        //        MessageBox.Show("Chọn báo cáo trước khi xem.");
        //        return false;
        //    }
        //    try
        //    {
        //        ConfigItemCls OConfigItem = new ConfigItemCls();
        //        OConfigItem.Init();
        //        // load report
        //        if (!LoadReport(lstReports.Text)) return false;

        //        // show the dialog
                
        //        PrintPreviewDialog dlg = new PrintPreviewDialog();
        //        dlg.Document = c1Report1.Document;
        //        if (!string.IsNullOrEmpty(OConfigItem.BillPrinterName))
        //        {
        //            dlg.Document.PrinterSettings.PrinterName = OConfigItem.BillPrinterName;
        //        }
        //        dlg.ShowDialog();
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBox.Show("Lỗi khi xem báo cáo " + x.Message);
        //        return false;
        //    }
        //    return true;
        //}

		// show what we're doing
		internal void ShowStatus(string msg)
		{
			stat.Panels[0].Text = msg;
		}

		// ** event handlers

		// load default file on startup
		private void frmMain_Load(object sender, System.EventArgs e)
		{
			// if we loaded a design-time report, render it
			if (c1Report1.Fields.Count > 0)
				ppv.Document = c1Report1.Document;
		}

		private void frmMain_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 27 && c1Report1.IsBusy)
				c1Report1.Cancel = true;
		}

		private void menuOpen_Click(object sender, System.EventArgs e)
		{
			OpenReportDefinitionFile();
		}
		private void menuSave_Click(object sender, System.EventArgs e)
		{
			SaveReportDefinitionFile(false);
		}
		private void menuSaveAll_Click(object sender, System.EventArgs e)
		{
			SaveReportDefinitionFile(true);
		}
		private void menuExport_Click(object sender, System.EventArgs e)
		{
			// make sure we have a report loaded
            if (!LoadReport(lstReports.Text)) return;

			// show dialog
			ShowStatus(string.Format("[{0}] Ready to Export.", lstReports.Text));
			SaveFileDialog sf = new SaveFileDialog();
			sf.Filter = " HTML đơn giản(*.htm)|*.htm|" + 
				"Paged HTML (*htm)|*.htm|" + 
				"Drill down HTML (*.htm)|*.htm|" +
				"Portable Document Format (*.pdf)|*.pdf|" +
				"Plain text (*.txt)|*.txt|"+
				"Excel file (*.xls)|*.xls";
			sf.FileName = lstReports.Text;
			sf.AddExtension = true;
			sf.FilterIndex = m_FilterIndex;
			if (sf.ShowDialog() != DialogResult.OK) return;
			m_FilterIndex = sf.FilterIndex;

			// select file format
			FileFormatEnum fmt = FileFormatEnum.HTML;
			string ext = Path.GetExtension(sf.FileName);
			switch (ext)
			{
				case ".htm":
				switch (sf.FilterIndex)
				{
					case 1: fmt = FileFormatEnum.HTML; break;
					case 2: fmt = FileFormatEnum.HTMLPaged; break;
					case 3: fmt = FileFormatEnum.HTMLDrillDown; break;
				}
					break;
				case ".pdf":
					fmt = FileFormatEnum.PDF;
					break;
				case ".txt":
					fmt = FileFormatEnum.Text;

					break;

				case ".xls":
					fmt = FileFormatEnum.Excel;

					break;
			}

			// render the report
			c1Report1.RenderToFile(sf.FileName, fmt);
		}
		private void menuPreview_Click(object sender, System.EventArgs e)
		{
			//PreviewReport();
		}
		private void menuPrint_Click(object sender, System.EventArgs e)
		{
			//PrintReport(false);
		}
		private void menuExit_Click(object sender, System.EventArgs e)
		{
		
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			// preview report
			if (e.Button == tbPreview)
			{
				//PreviewReport();
				return;
			}

			// print report
			if (e.Button == tbPrint)
			{
				//PrintReport(false);
				return;
			}

			

            //// handle preview buttons
            int cnt = c1Report1.GetPageCount();// c1Report1.PageImages.Count;

			if (e.Button == tbActual)
			{
				ppv.AutoZoom = false;
				ppv.Columns = 1;
				ppv.Zoom = 1;
			}
			if (e.Button == tbPage)
			{
				ppv.AutoZoom = true;
				ppv.Columns = 1;
			}
			if (e.Button == tbTwo)
			{
				ppv.AutoZoom = true;
				ppv.Columns = 2;
			}
			ShowStatus(string.Format("Trang {0} / {1}", ppv.StartPage+1, cnt));
		}

		// render the report that is currently selected in the listbox
		private void lstReports_DoubleClick(object sender, System.EventArgs e)
		{
			comboBoxReport.SelectedIndex=lstReports.SelectedIndex;
			//RenderReport();     
		}

		// feedback while rendering report
		private void c1Report1_StartReport(object sender, System.EventArgs e)
		{
			ShowStatus(string.Format("[{0}]: Rendering", c1Report1.ReportName));
			m_Ticks = DateTime.Now.Ticks;
		}
		private void c1Report1_EndReport(object sender, System.EventArgs e)
		{
			m_Ticks = DateTime.Now.Ticks - m_Ticks;
			double seconds = m_Ticks / (double)TimeSpan.TicksPerSecond;
			ShowStatus(string.Format("[{0}]: Hoàn thành, {1} trang báo cáo trong {2:0.00} giây",
				c1Report1.ReportName,
				c1Report1.GetPageCount(),//c1Report1.PageImages.Count,
				seconds));
			ppv.Zoom=0.90;
		}
		private void c1Report1_StartPage(object sender, C1.C1Report.ReportEventArgs e)
		{
			ShowStatus(string.Format("[{0}]: Đang xử lý trang {1}", c1Report1.ReportName, e.Page));
		}
		private void c1Report1_NoData(object sender, System.EventArgs e)
		{
			//MessageBox.Show("Không có dữ liệu cho báo cáo này.", "Hủy bỏ xem báo cáo");
			c1Report1.Cancel = true;
		}

		// trace errors
		private void c1Report1_ReportError(object sender, C1.C1Report.ReportEventArgs e)
		{
			Console.WriteLine("** Lỗi khi lập báo cáo: {0}", e.Exception.Message);
		}

		private void comboBoxZoom_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				int Value=int.Parse(comboBoxZoom.Text);
				ppv.Zoom=Value/100;
			}
			catch
			{
				ppv.Zoom=0.90;
			}
		}

        //private void comboBoxReport_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (RequestDataEvent != null)
        //    {
        //        RequestDataEvent(PrintServiceId, comboBoxReport.SelectedIndex, comboBoxReport.Items[comboBoxReport.SelectedIndex].ToString(), CustomerPaid, MoneyReturnBack, ref dataSource);
        //    }
        //    lstReports.SelectedIndex=comboBoxReport.SelectedIndex;
        //    RenderReport();
        //}

        private void tExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuPrint_Click_1(object sender, EventArgs e)
        {
            //PrintReport(false);
        }

        private void cmdSaveDefault_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (comboBoxReport.SelectedIndex < 0) throw new Exception("Không tìm thấy mẫu phiếu in");
            //    ConfigItemCls OConfigItem = new ConfigItemCls();
            //    OConfigItem.Init();
            //    OConfigItem.SelectedReportIndex = comboBoxReport.SelectedIndex;
            //    OConfigItem.Save();
            //    MessageBox.Show(this, "Thiết lập mẫu in ngầm định thành công", "Thông báo");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message.ToString(), "Thông báo");
            //}
        }
	}
}

