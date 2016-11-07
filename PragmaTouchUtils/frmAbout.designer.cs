namespace PragmaTouchUtils
{
  partial class frmAbout
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
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
    private void InitializeComponent( )
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
      this.panel1 = new System.Windows.Forms.Panel();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.lv = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label2 = new System.Windows.Forms.Label();
      this.lblCopyright = new System.Windows.Forms.Label();
      this.lblEdition = new System.Windows.Forms.Label();
      this.lblVersion = new System.Windows.Forms.Label();
      this.panel5 = new System.Windows.Forms.Panel();
      this.lblThrowException = new System.Windows.Forms.LinkLabel();
      this.btnClose = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      this.panel2.SuspendLayout();
      this.panel5.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.pictureBox3);
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.lblCopyright);
      this.panel1.Controls.Add(this.lblEdition);
      this.panel1.Controls.Add(this.lblVersion);
      this.panel1.Controls.Add(this.panel5);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(584, 361);
      this.panel1.TabIndex = 1;
      // 
      // pictureBox3
      // 
      this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
      this.pictureBox3.Location = new System.Drawing.Point(11, 11);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(93, 103);
      this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox3.TabIndex = 43;
      this.pictureBox3.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel2.BackColor = System.Drawing.SystemColors.Control;
      this.panel2.Controls.Add(this.lv);
      this.panel2.Location = new System.Drawing.Point(7, 123);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(565, 196);
      this.panel2.TabIndex = 32;
      // 
      // lv
      // 
      this.lv.BackColor = System.Drawing.SystemColors.Control;
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lv.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lv.ForeColor = System.Drawing.Color.Black;
      this.lv.FullRowSelect = true;
      this.lv.GridLines = true;
      this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lv.Location = new System.Drawing.Point(0, 0);
      this.lv.MultiSelect = false;
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(565, 196);
      this.lv.TabIndex = 21;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "";
      this.columnHeader1.Width = 151;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "";
      this.columnHeader2.Width = 265;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.Location = new System.Drawing.Point(110, 11);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(233, 35);
      this.label2.TabIndex = 30;
      this.label2.Text = "Dev Shorthand";
      // 
      // lblCopyright
      // 
      this.lblCopyright.AutoSize = true;
      this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
      this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblCopyright.ForeColor = System.Drawing.Color.Black;
      this.lblCopyright.Location = new System.Drawing.Point(113, 96);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(172, 13);
      this.lblCopyright.TabIndex = 28;
      this.lblCopyright.Text = "All rights reserved. © 2012 - 2016";
      // 
      // lblEdition
      // 
      this.lblEdition.AutoSize = true;
      this.lblEdition.BackColor = System.Drawing.Color.Transparent;
      this.lblEdition.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblEdition.ForeColor = System.Drawing.Color.Black;
      this.lblEdition.Location = new System.Drawing.Point(113, 50);
      this.lblEdition.Name = "lblEdition";
      this.lblEdition.Size = new System.Drawing.Size(194, 18);
      this.lblEdition.TabIndex = 27;
      this.lblEdition.Text = "Quick utilities and playground";
      // 
      // lblVersion
      // 
      this.lblVersion.AutoSize = true;
      this.lblVersion.BackColor = System.Drawing.Color.Transparent;
      this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblVersion.ForeColor = System.Drawing.Color.Black;
      this.lblVersion.Location = new System.Drawing.Point(113, 76);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(52, 13);
      this.lblVersion.TabIndex = 27;
      this.lblVersion.Text = "v 2.1.0.0";
      // 
      // panel5
      // 
      this.panel5.BackColor = System.Drawing.SystemColors.Control;
      this.panel5.Controls.Add(this.lblThrowException);
      this.panel5.Controls.Add(this.btnClose);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel5.Location = new System.Drawing.Point(0, 325);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(582, 34);
      this.panel5.TabIndex = 14;
      // 
      // lblThrowException
      // 
      this.lblThrowException.AutoSize = true;
      this.lblThrowException.Location = new System.Drawing.Point(7, 11);
      this.lblThrowException.Name = "lblThrowException";
      this.lblThrowException.Size = new System.Drawing.Size(106, 13);
      this.lblThrowException.TabIndex = 36;
      this.lblThrowException.TabStop = true;
      this.lblThrowException.Text = "Throw test exception";
      this.lblThrowException.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblThrowException_LinkClicked);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(499, 5);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 24);
      this.btnClose.TabIndex = 14;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // frmAbout
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(584, 361);
      this.Controls.Add(this.panel1);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAbout";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About";
      this.Load += new System.EventHandler(this.frmAbout_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblCopyright;
    private System.Windows.Forms.Label lblVersion;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.LinkLabel lblThrowException;
    private System.Windows.Forms.Label lblEdition;
    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.PictureBox pictureBox3;


  }
}