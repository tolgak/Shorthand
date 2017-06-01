namespace Shorthand
{
  partial class frmIISAdminHelper
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
      this.txtDetails = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.grpAppPool = new System.Windows.Forms.GroupBox();
      this.grpApplication = new System.Windows.Forms.GroupBox();
      this.btnSetApplicationPassword = new System.Windows.Forms.Button();
      this.btnDumpAppProperties = new System.Windows.Forms.Button();
      this.btnDumpPoolProperties = new System.Windows.Forms.Button();
      this.btnSetPoolPassword = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.grpAppPool.SuspendLayout();
      this.grpApplication.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtDetails
      // 
      this.txtDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDetails.Location = new System.Drawing.Point(2, 221);
      this.txtDetails.Multiline = true;
      this.txtDetails.Name = "txtDetails";
      this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDetails.Size = new System.Drawing.Size(580, 266);
      this.txtDetails.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.button1);
      this.panel1.Controls.Add(this.grpApplication);
      this.panel1.Controls.Add(this.grpAppPool);
      this.panel1.Controls.Add(this.txtPassword);
      this.panel1.Controls.Add(this.txtUserName);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(2, 2);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(580, 219);
      this.panel1.TabIndex = 2;
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(75, 50);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new System.Drawing.Size(177, 20);
      this.txtPassword.TabIndex = 3;
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point(75, 16);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(246, 20);
      this.txtUserName.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 50);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "label2";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(20, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      // 
      // grpAppPool
      // 
      this.grpAppPool.Controls.Add(this.btnSetPoolPassword);
      this.grpAppPool.Controls.Add(this.btnDumpPoolProperties);
      this.grpAppPool.Location = new System.Drawing.Point(14, 91);
      this.grpAppPool.Name = "grpAppPool";
      this.grpAppPool.Size = new System.Drawing.Size(175, 100);
      this.grpAppPool.TabIndex = 7;
      this.grpAppPool.TabStop = false;
      this.grpAppPool.Text = "Application Pools";
      // 
      // grpApplication
      // 
      this.grpApplication.Controls.Add(this.btnSetApplicationPassword);
      this.grpApplication.Controls.Add(this.btnDumpAppProperties);
      this.grpApplication.Location = new System.Drawing.Point(195, 91);
      this.grpApplication.Name = "grpApplication";
      this.grpApplication.Size = new System.Drawing.Size(175, 100);
      this.grpApplication.TabIndex = 8;
      this.grpApplication.TabStop = false;
      this.grpApplication.Text = "Applications";
      // 
      // btnSetApplicationPassword
      // 
      this.btnSetApplicationPassword.Location = new System.Drawing.Point(9, 55);
      this.btnSetApplicationPassword.Name = "btnSetApplicationPassword";
      this.btnSetApplicationPassword.Size = new System.Drawing.Size(136, 23);
      this.btnSetApplicationPassword.TabIndex = 7;
      this.btnSetApplicationPassword.Text = "Set password";
      this.btnSetApplicationPassword.UseVisualStyleBackColor = true;
      this.btnSetApplicationPassword.Click += new System.EventHandler(this.btnSetApplicationPassword_Click);
      // 
      // btnDumpAppProperties
      // 
      this.btnDumpAppProperties.Location = new System.Drawing.Point(9, 26);
      this.btnDumpAppProperties.Name = "btnDumpAppProperties";
      this.btnDumpAppProperties.Size = new System.Drawing.Size(136, 23);
      this.btnDumpAppProperties.TabIndex = 6;
      this.btnDumpAppProperties.Text = "Dump properties";
      this.btnDumpAppProperties.UseVisualStyleBackColor = true;
      this.btnDumpAppProperties.Click += new System.EventHandler(this.btnDumpAppProperties_Click);
      // 
      // btnDumpPoolProperties
      // 
      this.btnDumpPoolProperties.Location = new System.Drawing.Point(9, 26);
      this.btnDumpPoolProperties.Name = "btnDumpPoolProperties";
      this.btnDumpPoolProperties.Size = new System.Drawing.Size(136, 23);
      this.btnDumpPoolProperties.TabIndex = 0;
      this.btnDumpPoolProperties.Text = "Dump properties";
      this.btnDumpPoolProperties.UseVisualStyleBackColor = true;
      this.btnDumpPoolProperties.Click += new System.EventHandler(this.btnDumpPoolProperties_Click);
      // 
      // btnSetPoolPassword
      // 
      this.btnSetPoolPassword.Location = new System.Drawing.Point(9, 55);
      this.btnSetPoolPassword.Name = "btnSetPoolPassword";
      this.btnSetPoolPassword.Size = new System.Drawing.Size(136, 23);
      this.btnSetPoolPassword.TabIndex = 1;
      this.btnSetPoolPassword.Text = "Set password";
      this.btnSetPoolPassword.UseVisualStyleBackColor = true;
      this.btnSetPoolPassword.Click += new System.EventHandler(this.btnSetPoolPassword_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(435, 22);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 9;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // frmIISAdminHelper
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 489);
      this.Controls.Add(this.txtDetails);
      this.Controls.Add(this.panel1);
      this.Name = "frmIISAdminHelper";
      this.Padding = new System.Windows.Forms.Padding(2);
      this.Text = "frmIISAdminHelper";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.grpAppPool.ResumeLayout(false);
      this.grpApplication.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtDetails;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox grpApplication;
    private System.Windows.Forms.Button btnSetApplicationPassword;
    private System.Windows.Forms.Button btnDumpAppProperties;
    private System.Windows.Forms.GroupBox grpAppPool;
    private System.Windows.Forms.Button btnSetPoolPassword;
    private System.Windows.Forms.Button btnDumpPoolProperties;
    private System.Windows.Forms.Button button1;
  }
}