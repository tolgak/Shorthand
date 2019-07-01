namespace Shorthand.AdminPanel
{
  partial class frmAdminPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminPanel));
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lblTotal = new System.Windows.Forms.Label();
      this.txtLog = new System.Windows.Forms.TextBox();
      this.lblPasswordPolicy = new System.Windows.Forms.Label();
      this.btnChange = new System.Windows.Forms.Button();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnCheckSolution = new System.Windows.Forms.Button();
      this.txtSolutionFile = new System.Windows.Forms.TextBox();
      this.txtFolder = new System.Windows.Forms.TextBox();
      this.btnCheckFolder = new System.Windows.Forms.Button();
      this.btnRunningProcess = new System.Windows.Forms.Button();
      this.btnKill = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lblTotal);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(388, 75);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      // 
      // lblTotal
      // 
      this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblTotal.BackColor = System.Drawing.Color.Maroon;
      this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblTotal.ForeColor = System.Drawing.Color.White;
      this.lblTotal.Location = new System.Drawing.Point(17, 20);
      this.lblTotal.Name = "lblTotal";
      this.lblTotal.Size = new System.Drawing.Size(354, 40);
      this.lblTotal.TabIndex = 23;
      this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // txtLog
      // 
      this.txtLog.BackColor = System.Drawing.Color.Black;
      this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.txtLog.ForeColor = System.Drawing.Color.Lime;
      this.txtLog.Location = new System.Drawing.Point(2, 291);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLog.Size = new System.Drawing.Size(880, 318);
      this.txtLog.TabIndex = 23;
      this.txtLog.WordWrap = false;
      // 
      // lblPasswordPolicy
      // 
      this.lblPasswordPolicy.Location = new System.Drawing.Point(24, 172);
      this.lblPasswordPolicy.Name = "lblPasswordPolicy";
      this.lblPasswordPolicy.Size = new System.Drawing.Size(374, 48);
      this.lblPasswordPolicy.TabIndex = 25;
      this.lblPasswordPolicy.Text = "Deceive AD history to keep the same password and get rid of password expiry warni" +
    "ng";
      // 
      // btnChange
      // 
      this.btnChange.Location = new System.Drawing.Point(24, 225);
      this.btnChange.Name = "btnChange";
      this.btnChange.Size = new System.Drawing.Size(75, 27);
      this.btnChange.TabIndex = 33;
      this.btnChange.Text = "Deceive";
      this.btnChange.UseVisualStyleBackColor = true;
      this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point(141, 104);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(189, 25);
      this.txtUserName.TabIndex = 31;
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(141, 135);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(189, 25);
      this.txtPassword.TabIndex = 32;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(26, 109);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(71, 17);
      this.label1.TabIndex = 33;
      this.label1.Text = "User name";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(26, 140);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(111, 17);
      this.label2.TabIndex = 34;
      this.label2.Text = "Current Password";
      // 
      // btnCheckSolution
      // 
      this.btnCheckSolution.Location = new System.Drawing.Point(725, 26);
      this.btnCheckSolution.Name = "btnCheckSolution";
      this.btnCheckSolution.Size = new System.Drawing.Size(129, 25);
      this.btnCheckSolution.TabIndex = 35;
      this.btnCheckSolution.Text = "Check Solution";
      this.btnCheckSolution.UseVisualStyleBackColor = true;
      this.btnCheckSolution.Click += new System.EventHandler(this.btnCheckSolution_Click);
      // 
      // txtSolutionFile
      // 
      this.txtSolutionFile.AllowDrop = true;
      this.txtSolutionFile.Location = new System.Drawing.Point(409, 26);
      this.txtSolutionFile.Name = "txtSolutionFile";
      this.txtSolutionFile.Size = new System.Drawing.Size(310, 25);
      this.txtSolutionFile.TabIndex = 36;
      // 
      // txtFolder
      // 
      this.txtFolder.Location = new System.Drawing.Point(409, 59);
      this.txtFolder.Name = "txtFolder";
      this.txtFolder.Size = new System.Drawing.Size(310, 25);
      this.txtFolder.TabIndex = 37;
      // 
      // btnCheckFolder
      // 
      this.btnCheckFolder.Location = new System.Drawing.Point(725, 59);
      this.btnCheckFolder.Name = "btnCheckFolder";
      this.btnCheckFolder.Size = new System.Drawing.Size(129, 25);
      this.btnCheckFolder.TabIndex = 38;
      this.btnCheckFolder.Text = "Check Folder";
      this.btnCheckFolder.UseVisualStyleBackColor = true;
      this.btnCheckFolder.Click += new System.EventHandler(this.btnCheckFolder_Click);
      // 
      // btnRunningProcess
      // 
      this.btnRunningProcess.Location = new System.Drawing.Point(725, 168);
      this.btnRunningProcess.Name = "btnRunningProcess";
      this.btnRunningProcess.Size = new System.Drawing.Size(129, 25);
      this.btnRunningProcess.TabIndex = 39;
      this.btnRunningProcess.Text = "List Win Processes";
      this.btnRunningProcess.UseVisualStyleBackColor = true;
      this.btnRunningProcess.Click += new System.EventHandler(this.btnRunningProcess_Click);
      // 
      // btnKill
      // 
      this.btnKill.Location = new System.Drawing.Point(725, 199);
      this.btnKill.Name = "btnKill";
      this.btnKill.Size = new System.Drawing.Size(129, 25);
      this.btnKill.TabIndex = 40;
      this.btnKill.Text = "Kill";
      this.btnKill.UseVisualStyleBackColor = true;
      this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
      // 
      // frmAdminPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 611);
      this.Controls.Add(this.btnKill);
      this.Controls.Add(this.btnRunningProcess);
      this.Controls.Add(this.btnCheckFolder);
      this.Controls.Add(this.txtFolder);
      this.Controls.Add(this.txtSolutionFile);
      this.Controls.Add(this.btnCheckSolution);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.txtUserName);
      this.Controls.Add(this.btnChange);
      this.Controls.Add(this.lblPasswordPolicy);
      this.Controls.Add(this.txtLog);
      this.Controls.Add(this.groupBox1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmAdminPanel";
      this.Padding = new System.Windows.Forms.Padding(2);
      this.Text = "Admin Panel";
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Label lblTotal;
    private System.Windows.Forms.Label lblPasswordPolicy;
    private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnCheckSolution;
    private System.Windows.Forms.TextBox txtSolutionFile;
    private System.Windows.Forms.TextBox txtFolder;
    private System.Windows.Forms.Button btnCheckFolder;
    private System.Windows.Forms.Button btnRunningProcess;
    private System.Windows.Forms.Button btnKill;
  }
}