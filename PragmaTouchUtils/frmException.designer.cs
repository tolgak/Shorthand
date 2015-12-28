namespace PragmaTouchUtils
{
  partial class frmException
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmException));
      this.panel1 = new System.Windows.Forms.Panel();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.btnReport = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnContinue = new System.Windows.Forms.Button();
      this.panel3 = new System.Windows.Forms.Panel();
      this.panel4 = new System.Windows.Forms.Panel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.txtShortMsg = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.txtDetailMsg = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.Controls.Add(this.pictureBox1);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(515, 81);
      this.panel1.TabIndex = 5;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(7, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(48, 48);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 6;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.Color.Red;
      this.label1.Location = new System.Drawing.Point(65, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(333, 32);
      this.label1.TabIndex = 5;
      this.label1.Text = "An unexpected error occured. \r\nYou may inspect the details in the following pages" +
          "";
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.SystemColors.Control;
      this.panel2.Controls.Add(this.btnReport);
      this.panel2.Controls.Add(this.btnClose);
      this.panel2.Controls.Add(this.btnContinue);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point(0, 277);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(515, 45);
      this.panel2.TabIndex = 6;
      // 
      // btnReport
      // 
      this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnReport.Location = new System.Drawing.Point(341, 10);
      this.btnReport.Name = "btnReport";
      this.btnReport.Size = new System.Drawing.Size(75, 24);
      this.btnReport.TabIndex = 6;
      this.btnReport.Text = "E-Mail Gönder";
      this.btnReport.UseVisualStyleBackColor = true;
      this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.btnClose.Location = new System.Drawing.Point(7, 10);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(124, 24);
      this.btnClose.TabIndex = 5;
      this.btnClose.Text = "Close application";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // btnContinue
      // 
      this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnContinue.Location = new System.Drawing.Point(422, 10);
      this.btnContinue.Name = "btnContinue";
      this.btnContinue.Size = new System.Drawing.Size(81, 24);
      this.btnContinue.TabIndex = 4;
      this.btnContinue.Text = "Close";
      this.btnContinue.UseVisualStyleBackColor = true;
      this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.panel4);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 81);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(515, 196);
      this.panel3.TabIndex = 7;
      // 
      // panel4
      // 
      this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel4.Controls.Add(this.tabControl1);
      this.panel4.Location = new System.Drawing.Point(8, 7);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(502, 181);
      this.panel4.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(502, 181);
      this.tabControl1.TabIndex = 6;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.txtShortMsg);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(494, 155);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Error";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // txtShortMsg
      // 
      this.txtShortMsg.BackColor = System.Drawing.Color.White;
      this.txtShortMsg.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtShortMsg.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.txtShortMsg.ForeColor = System.Drawing.Color.Red;
      this.txtShortMsg.Location = new System.Drawing.Point(3, 3);
      this.txtShortMsg.Multiline = true;
      this.txtShortMsg.Name = "txtShortMsg";
      this.txtShortMsg.ReadOnly = true;
      this.txtShortMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtShortMsg.Size = new System.Drawing.Size(488, 149);
      this.txtShortMsg.TabIndex = 5;
      this.txtShortMsg.Text = "Example error message";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.txtDetailMsg);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(494, 155);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Details";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // txtDetailMsg
      // 
      this.txtDetailMsg.BackColor = System.Drawing.Color.White;
      this.txtDetailMsg.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDetailMsg.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.txtDetailMsg.ForeColor = System.Drawing.Color.Red;
      this.txtDetailMsg.Location = new System.Drawing.Point(3, 3);
      this.txtDetailMsg.Multiline = true;
      this.txtDetailMsg.Name = "txtDetailMsg";
      this.txtDetailMsg.ReadOnly = true;
      this.txtDetailMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDetailMsg.Size = new System.Drawing.Size(488, 149);
      this.txtDetailMsg.TabIndex = 2;
      this.txtDetailMsg.WordWrap = false;
      // 
      // frmException
      // 
      this.AcceptButton = this.btnReport;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnContinue;
      this.ClientSize = new System.Drawing.Size(515, 322);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmException";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "OMBL Office Error";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

		private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnReport;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnContinue;
    private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox txtShortMsg;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox txtDetailMsg;
  }
}