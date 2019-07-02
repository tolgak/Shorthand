namespace Shorthand.FieldSelector
{
  partial class frmFields
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFields));
      this.panel1 = new System.Windows.Forms.Panel();
      this.tvFieldList = new System.Windows.Forms.TreeView();
      this.imgTreeView = new System.Windows.Forms.ImageList(this.components);
      this.lblStatus = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.txtLog = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.grd = new System.Windows.Forms.DataGridView();
      this.panel2 = new System.Windows.Forms.Panel();
      this.btnTraverse = new System.Windows.Forms.Button();
      this.btnFill = new System.Windows.Forms.Button();
      this.lblConnection = new System.Windows.Forms.Label();
      this.txtConnection = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.tvFieldList);
      this.panel1.Controls.Add(this.lblStatus);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(310, 605);
      this.panel1.TabIndex = 7;
      // 
      // tvFieldList
      // 
      this.tvFieldList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvFieldList.HideSelection = false;
      this.tvFieldList.Location = new System.Drawing.Point(0, 0);
      this.tvFieldList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tvFieldList.Name = "tvFieldList";
      this.tvFieldList.Size = new System.Drawing.Size(308, 579);
      this.tvFieldList.StateImageList = this.imgTreeView;
      this.tvFieldList.TabIndex = 1;
      this.tvFieldList.DoubleClick += new System.EventHandler(this.tvFieldList_DoubleClick);
      // 
      // imgTreeView
      // 
      this.imgTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeView.ImageStream")));
      this.imgTreeView.TransparentColor = System.Drawing.Color.Transparent;
      this.imgTreeView.Images.SetKeyName(0, "selected_0.png");
      this.imgTreeView.Images.SetKeyName(1, "selected_1.png");
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.lblStatus.Location = new System.Drawing.Point(0, 579);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(308, 24);
      this.lblStatus.TabIndex = 3;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(313, 143);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(568, 465);
      this.tabControl1.TabIndex = 10;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.txtLog);
      this.tabPage1.Location = new System.Drawing.Point(4, 26);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(5);
      this.tabPage1.Size = new System.Drawing.Size(560, 435);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "script";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // txtLog
      // 
      this.txtLog.BackColor = System.Drawing.Color.Black;
      this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.txtLog.Location = new System.Drawing.Point(5, 5);
      this.txtLog.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtLog.Size = new System.Drawing.Size(550, 425);
      this.txtLog.TabIndex = 11;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.grd);
      this.tabPage2.Location = new System.Drawing.Point(4, 26);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(5);
      this.tabPage2.Size = new System.Drawing.Size(562, 435);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "result";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(5, 5);
      this.grd.Name = "grd";
      this.grd.ShowEditingIcon = false;
      this.grd.Size = new System.Drawing.Size(552, 425);
      this.grd.TabIndex = 0;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.btnTraverse);
      this.panel2.Controls.Add(this.btnFill);
      this.panel2.Controls.Add(this.lblConnection);
      this.panel2.Controls.Add(this.txtConnection);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(313, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(568, 140);
      this.panel2.TabIndex = 11;
      // 
      // btnTraverse
      // 
      this.btnTraverse.Location = new System.Drawing.Point(9, 98);
      this.btnTraverse.Name = "btnTraverse";
      this.btnTraverse.Size = new System.Drawing.Size(144, 25);
      this.btnTraverse.TabIndex = 13;
      this.btnTraverse.Text = "Traverse selected";
      this.btnTraverse.UseVisualStyleBackColor = true;
      this.btnTraverse.Click += new System.EventHandler(this.btnTraverse_Click);
      // 
      // btnFill
      // 
      this.btnFill.Location = new System.Drawing.Point(9, 69);
      this.btnFill.Name = "btnFill";
      this.btnFill.Size = new System.Drawing.Size(75, 25);
      this.btnFill.TabIndex = 12;
      this.btnFill.Text = "Fill";
      this.btnFill.UseVisualStyleBackColor = true;
      this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
      // 
      // lblConnection
      // 
      this.lblConnection.AutoSize = true;
      this.lblConnection.Location = new System.Drawing.Point(6, 14);
      this.lblConnection.Name = "lblConnection";
      this.lblConnection.Size = new System.Drawing.Size(110, 17);
      this.lblConnection.TabIndex = 11;
      this.lblConnection.Text = "Connection string";
      // 
      // txtConnection
      // 
      this.txtConnection.Location = new System.Drawing.Point(9, 36);
      this.txtConnection.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtConnection.Name = "txtConnection";
      this.txtConnection.Size = new System.Drawing.Size(535, 25);
      this.txtConnection.TabIndex = 10;
      // 
      // frmFields
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 611);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmFields";
      this.Padding = new System.Windows.Forms.Padding(3);
      this.Text = "Field Selector";
      this.panel1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TreeView tvFieldList;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ImageList imgTreeView;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnTraverse;
    private System.Windows.Forms.Button btnFill;
    private System.Windows.Forms.Label lblConnection;
    private System.Windows.Forms.TextBox txtConnection;
  }
}

