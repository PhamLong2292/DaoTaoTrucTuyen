namespace OneTSQ.PrintServer
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.cmdConfigPrinter = new System.Windows.Forms.Button();
            this.cmdDesigner = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "S247.vn Net Printer Service";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mConfig,
            this.toolStripMenuItem1,
            this.mExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(161, 54);
            // 
            // mConfig
            // 
            this.mConfig.Name = "mConfig";
            this.mConfig.Size = new System.Drawing.Size(160, 22);
            this.mConfig.Text = "Thiết lập";
            this.mConfig.Click += new System.EventHandler(this.mConfig_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(160, 22);
            this.mExit.Text = "Thoát ứng dụng";
            this.mExit.Click += new System.EventHandler(this.mExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::OneTSQ.PrintServer.Properties.Resources._1478974059_Print;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 115);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new System.Drawing.Point(145, 12);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new System.Drawing.Size(641, 270);
            this.textBoxLog.TabIndex = 2;
            // 
            // cmdConfigPrinter
            // 
            this.cmdConfigPrinter.Location = new System.Drawing.Point(10, 133);
            this.cmdConfigPrinter.Name = "cmdConfigPrinter";
            this.cmdConfigPrinter.Size = new System.Drawing.Size(129, 34);
            this.cmdConfigPrinter.TabIndex = 3;
            this.cmdConfigPrinter.Text = "Thiết lập máy in";
            this.cmdConfigPrinter.UseVisualStyleBackColor = true;
            this.cmdConfigPrinter.Click += new System.EventHandler(this.cmdConfigPrinter_Click);
            // 
            // cmdDesigner
            // 
            this.cmdDesigner.Location = new System.Drawing.Point(10, 173);
            this.cmdDesigner.Name = "cmdDesigner";
            this.cmdDesigner.Size = new System.Drawing.Size(129, 34);
            this.cmdDesigner.TabIndex = 4;
            this.cmdDesigner.Text = "Thiết kế mẫu in";
            this.cmdDesigner.UseVisualStyleBackColor = true;
            this.cmdDesigner.Click += new System.EventHandler(this.cmdDesigner_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ẩn vào taskbar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 294);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdDesigner);
            this.Controls.Add(this.cmdConfigPrinter);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S247.vn Net Printer Service";
            this.MaximumSizeChanged += new System.EventHandler(this.frmMain_MaximumSizeChanged);
            this.MinimumSizeChanged += new System.EventHandler(this.frmMain_MinimumSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button cmdConfigPrinter;
        private System.Windows.Forms.Button cmdDesigner;
        private System.Windows.Forms.Button button1;
    }
}