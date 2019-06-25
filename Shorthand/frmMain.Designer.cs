namespace Shorthand
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
        if (_container != null)
          _container.Dispose();

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.appStat = new System.Windows.Forms.StatusStrip();
      this.statMsg = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      this.appStat.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuWindow,
            this.mnuHelp});
      this.mnuMain.Location = new System.Drawing.Point(2, 2);
      this.mnuMain.MdiWindowListItem = this.mnuWindow;
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(780, 25);
      this.mnuMain.TabIndex = 12;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemSettings,
            this.toolStripSeparator1,
            this.mnuItemExit});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(39, 21);
      this.mnuFile.Text = "File";
      // 
      // mnuItemExit
      // 
      this.mnuItemExit.Name = "mnuItemExit";
      this.mnuItemExit.Size = new System.Drawing.Size(180, 22);
      this.mnuItemExit.Text = "Exit";
      this.mnuItemExit.Click += new System.EventHandler(this.mnuItemExit_Click);
      // 
      // mnuTools
      // 
      this.mnuTools.Name = "mnuTools";
      this.mnuTools.Size = new System.Drawing.Size(51, 21);
      this.mnuTools.Text = "Tools";
      // 
      // mnuWindow
      // 
      this.mnuWindow.Name = "mnuWindow";
      this.mnuWindow.Size = new System.Drawing.Size(67, 21);
      this.mnuWindow.Text = "Window";
      // 
      // mnuHelp
      // 
      this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.Size = new System.Drawing.Size(47, 21);
      this.mnuHelp.Text = "Help";
      // 
      // mnuAbout
      // 
      this.mnuAbout.Name = "mnuAbout";
      this.mnuAbout.Size = new System.Drawing.Size(180, 22);
      this.mnuAbout.Text = "About";
      this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
      // 
      // appStat
      // 
      this.appStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statMsg});
      this.appStat.Location = new System.Drawing.Point(2, 537);
      this.appStat.Name = "appStat";
      this.appStat.Size = new System.Drawing.Size(780, 22);
      this.appStat.TabIndex = 14;
      this.appStat.Text = "status";
      // 
      // statMsg
      // 
      this.statMsg.AutoSize = false;
      this.statMsg.Name = "statMsg";
      this.statMsg.Size = new System.Drawing.Size(200, 17);
      this.statMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
      // 
      // mnuItemSettings
      // 
      this.mnuItemSettings.Name = "mnuItemSettings";
      this.mnuItemSettings.Size = new System.Drawing.Size(180, 22);
      this.mnuItemSettings.Text = "Settings";
      this.mnuItemSettings.Click += new System.EventHandler(this.mnuItemSettings_Click);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(784, 561);
      this.Controls.Add(this.appStat);
      this.Controls.Add(this.mnuMain);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(2);
      this.Text = "DevShorthand";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.appStat.ResumeLayout(false);
      this.appStat.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuItemExit;
    private System.Windows.Forms.ToolStripMenuItem mnuTools;
    private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    private System.Windows.Forms.ToolStripMenuItem mnuWindow;
    private System.Windows.Forms.StatusStrip appStat;
    private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    private System.Windows.Forms.ToolStripStatusLabel statMsg;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSettings;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
  }
}

