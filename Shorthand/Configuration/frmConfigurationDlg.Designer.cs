using System.Windows.Forms;

namespace Shorthand
{
  partial class frmConfigurationDlg
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
      if ( disposing && ( components != null ) )
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigurationDlg));
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnApply = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.panel2 = new System.Windows.Forms.Panel();
      this.pnlContent = new System.Windows.Forms.Panel();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.tv = new System.Windows.Forms.TreeView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.header = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.pnlContent.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnApply);
      this.panel1.Controls.Add(this.btnSave);
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 370);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(636, 37);
      this.panel1.TabIndex = 1;
      // 
      // btnApply
      // 
      this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnApply.Location = new System.Drawing.Point(306, 2);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(87, 32);
      this.btnApply.TabIndex = 2;
      this.btnApply.Text = "Apply";
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnSave.Location = new System.Drawing.Point(440, 2);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(87, 32);
      this.btnSave.TabIndex = 1;
      this.btnSave.Text = "Save";
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(534, 2);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(87, 32);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.pnlContent);
      this.panel2.Controls.Add(this.splitter1);
      this.panel2.Controls.Add(this.tv);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(636, 370);
      this.panel2.TabIndex = 3;
      // 
      // pnlContent
      // 
      this.pnlContent.Controls.Add(this.header);
      this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlContent.Location = new System.Drawing.Point(206, 0);
      this.pnlContent.Name = "pnlContent";
      this.pnlContent.Padding = new System.Windows.Forms.Padding(2);
      this.pnlContent.Size = new System.Drawing.Size(430, 370);
      this.pnlContent.TabIndex = 4;
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(203, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 370);
      this.splitter1.TabIndex = 3;
      this.splitter1.TabStop = false;
      // 
      // tv
      // 
      this.tv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tv.Cursor = System.Windows.Forms.Cursors.Hand;
      this.tv.Dock = System.Windows.Forms.DockStyle.Left;
      this.tv.FullRowSelect = true;
      this.tv.HideSelection = false;
      this.tv.ItemHeight = 21;
      this.tv.Location = new System.Drawing.Point(0, 0);
      this.tv.Name = "tv";
      this.tv.PathSeparator = "->";
      this.tv.Size = new System.Drawing.Size(203, 370);
      this.tv.StateImageList = this.imageList1;
      this.tv.TabIndex = 1;
      this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "folder.png");
      // 
      // header
      // 
      this.header.BackColor = System.Drawing.SystemColors.ActiveBorder;
      this.header.Dock = System.Windows.Forms.DockStyle.Top;
      this.header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.header.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.header.ForeColor = System.Drawing.Color.White;
      this.header.Location = new System.Drawing.Point(2, 2);
      this.header.Name = "header";
      this.header.Size = new System.Drawing.Size(426, 25);
      this.header.TabIndex = 0;
      this.header.Text = "Options";
      this.header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // frmConfigurationDlg
      // 
      this.AcceptButton = this.btnSave;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(636, 407);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = new System.Drawing.Point(50, 50);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmConfigurationDlg";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Settings";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConfigurationDlg_FormClosed);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.pnlContent.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Panel panel1;
    private Button btnApply;
    private Button btnSave;
    private Button btnCancel;
    private Panel panel2;
    private Panel pnlContent;
    private System.Windows.Forms.Splitter splitter1;
    private TreeView tv;
    private System.Windows.Forms.ImageList imageList1;
    private Label header;
  }
}