namespace Shorthand
{
  partial class frmDeployment
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeployment));
      this.panel1 = new System.Windows.Forms.Panel();
      this.imgGitLabActive = new System.Windows.Forms.PictureBox();
      this.imgJiraActive = new System.Windows.Forms.PictureBox();
      this.lblMergeRequestLink = new System.Windows.Forms.LinkLabel();
      this.checkBox3 = new System.Windows.Forms.CheckBox();
      this.checkBox2 = new System.Windows.Forms.CheckBox();
      this.btnClearLog = new System.Windows.Forms.Button();
      this.btnRefresh = new System.Windows.Forms.Button();
      this.lblDPLY_IssueKey = new System.Windows.Forms.Label();
      this.txtDPLY = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.rdbTest = new System.Windows.Forms.RadioButton();
      this.rdbProduction = new System.Windows.Forms.RadioButton();
      this.btnBuild = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.cmbGitProjectName = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtGitMergeRequestNo = new System.Windows.Forms.TextBox();
      this.lblUAT_IssueKey = new System.Windows.Forms.Label();
      this.txtUAT = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.lblREQ_IssueKey = new System.Windows.Forms.Label();
      this.txtInternal = new System.Windows.Forms.TextBox();
      this.txtREQ = new System.Windows.Forms.TextBox();
      this.txtDump = new System.Windows.Forms.TextBox();
      this.btnTest = new System.Windows.Forms.Button();
      this.lblREQ_Status = new System.Windows.Forms.Label();
      this.lblDPLY_Status = new System.Windows.Forms.Label();
      this.lblUAT_Status = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.imgGitLabActive)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgJiraActive)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblUAT_Status);
      this.panel1.Controls.Add(this.lblDPLY_Status);
      this.panel1.Controls.Add(this.lblREQ_Status);
      this.panel1.Controls.Add(this.btnTest);
      this.panel1.Controls.Add(this.imgGitLabActive);
      this.panel1.Controls.Add(this.imgJiraActive);
      this.panel1.Controls.Add(this.lblMergeRequestLink);
      this.panel1.Controls.Add(this.checkBox3);
      this.panel1.Controls.Add(this.checkBox2);
      this.panel1.Controls.Add(this.btnClearLog);
      this.panel1.Controls.Add(this.btnRefresh);
      this.panel1.Controls.Add(this.lblDPLY_IssueKey);
      this.panel1.Controls.Add(this.txtDPLY);
      this.panel1.Controls.Add(this.label6);
      this.panel1.Controls.Add(this.rdbTest);
      this.panel1.Controls.Add(this.rdbProduction);
      this.panel1.Controls.Add(this.btnBuild);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.cmbGitProjectName);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.txtGitMergeRequestNo);
      this.panel1.Controls.Add(this.lblUAT_IssueKey);
      this.panel1.Controls.Add(this.txtUAT);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.lblREQ_IssueKey);
      this.panel1.Controls.Add(this.txtInternal);
      this.panel1.Controls.Add(this.txtREQ);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.panel1.Location = new System.Drawing.Point(5, 5);
      this.panel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(753, 201);
      this.panel1.TabIndex = 0;
      // 
      // imgGitLabActive
      // 
      this.imgGitLabActive.Location = new System.Drawing.Point(443, 117);
      this.imgGitLabActive.Name = "imgGitLabActive";
      this.imgGitLabActive.Padding = new System.Windows.Forms.Padding(5);
      this.imgGitLabActive.Size = new System.Drawing.Size(38, 36);
      this.imgGitLabActive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.imgGitLabActive.TabIndex = 31;
      this.imgGitLabActive.TabStop = false;
      // 
      // imgJiraActive
      // 
      this.imgJiraActive.InitialImage = null;
      this.imgJiraActive.Location = new System.Drawing.Point(399, 117);
      this.imgJiraActive.Name = "imgJiraActive";
      this.imgJiraActive.Padding = new System.Windows.Forms.Padding(5);
      this.imgJiraActive.Size = new System.Drawing.Size(38, 36);
      this.imgJiraActive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.imgJiraActive.TabIndex = 30;
      this.imgJiraActive.TabStop = false;
      // 
      // lblMergeRequestLink
      // 
      this.lblMergeRequestLink.AutoSize = true;
      this.lblMergeRequestLink.Location = new System.Drawing.Point(489, 50);
      this.lblMergeRequestLink.Name = "lblMergeRequestLink";
      this.lblMergeRequestLink.Size = new System.Drawing.Size(181, 17);
      this.lblMergeRequestLink.TabIndex = 29;
      this.lblMergeRequestLink.TabStop = true;
      this.lblMergeRequestLink.Text = "merge request state unknown";
      // 
      // checkBox3
      // 
      this.checkBox3.AutoSize = true;
      this.checkBox3.Location = new System.Drawing.Point(224, 122);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new System.Drawing.Size(15, 14);
      this.checkBox3.TabIndex = 28;
      this.checkBox3.UseVisualStyleBackColor = true;
      // 
      // checkBox2
      // 
      this.checkBox2.AutoSize = true;
      this.checkBox2.Location = new System.Drawing.Point(224, 87);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new System.Drawing.Size(15, 14);
      this.checkBox2.TabIndex = 27;
      this.checkBox2.UseVisualStyleBackColor = true;
      // 
      // btnClearLog
      // 
      this.btnClearLog.Location = new System.Drawing.Point(123, 159);
      this.btnClearLog.Name = "btnClearLog";
      this.btnClearLog.Size = new System.Drawing.Size(108, 25);
      this.btnClearLog.TabIndex = 25;
      this.btnClearLog.Text = "Clear Log";
      this.btnClearLog.UseVisualStyleBackColor = true;
      this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
      // 
      // btnRefresh
      // 
      this.btnRefresh.Location = new System.Drawing.Point(9, 159);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(108, 25);
      this.btnRefresh.TabIndex = 9;
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
      // 
      // lblDPLY_IssueKey
      // 
      this.lblDPLY_IssueKey.AutoSize = true;
      this.lblDPLY_IssueKey.Location = new System.Drawing.Point(62, 85);
      this.lblDPLY_IssueKey.Name = "lblDPLY_IssueKey";
      this.lblDPLY_IssueKey.Size = new System.Drawing.Size(36, 17);
      this.lblDPLY_IssueKey.TabIndex = 24;
      this.lblDPLY_IssueKey.Text = "DPLY";
      // 
      // txtDPLY
      // 
      this.txtDPLY.Location = new System.Drawing.Point(121, 81);
      this.txtDPLY.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtDPLY.Name = "txtDPLY";
      this.txtDPLY.ReadOnly = true;
      this.txtDPLY.Size = new System.Drawing.Size(97, 25);
      this.txtDPLY.TabIndex = 2;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(266, 85);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(66, 17);
      this.label6.TabIndex = 22;
      this.label6.Text = "Deliver To";
      // 
      // rdbTest
      // 
      this.rdbTest.AutoSize = true;
      this.rdbTest.Checked = true;
      this.rdbTest.Location = new System.Drawing.Point(494, 83);
      this.rdbTest.Name = "rdbTest";
      this.rdbTest.Size = new System.Drawing.Size(49, 21);
      this.rdbTest.TabIndex = 7;
      this.rdbTest.TabStop = true;
      this.rdbTest.Text = "Test";
      this.rdbTest.UseVisualStyleBackColor = true;
      // 
      // rdbProduction
      // 
      this.rdbProduction.AutoSize = true;
      this.rdbProduction.Location = new System.Drawing.Point(399, 83);
      this.rdbProduction.Name = "rdbProduction";
      this.rdbProduction.Size = new System.Drawing.Size(89, 21);
      this.rdbProduction.TabIndex = 6;
      this.rdbProduction.Text = "Production";
      this.rdbProduction.UseVisualStyleBackColor = true;
      // 
      // btnBuild
      // 
      this.btnBuild.Location = new System.Drawing.Point(578, 159);
      this.btnBuild.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.btnBuild.Name = "btnBuild";
      this.btnBuild.Size = new System.Drawing.Size(92, 25);
      this.btnBuild.TabIndex = 8;
      this.btnBuild.Text = "Deploy";
      this.btnBuild.UseVisualStyleBackColor = true;
      this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(266, 16);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(107, 17);
      this.label5.TabIndex = 19;
      this.label5.Text = "Git Project Name";
      // 
      // cmbGitProjectName
      // 
      this.cmbGitProjectName.DisplayMember = "Text";
      this.cmbGitProjectName.FormattingEnabled = true;
      this.cmbGitProjectName.Location = new System.Drawing.Point(399, 12);
      this.cmbGitProjectName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.cmbGitProjectName.Name = "cmbGitProjectName";
      this.cmbGitProjectName.Size = new System.Drawing.Size(271, 25);
      this.cmbGitProjectName.TabIndex = 4;
      this.cmbGitProjectName.ValueMember = "Value";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(266, 50);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(130, 17);
      this.label4.TabIndex = 17;
      this.label4.Text = "Git Merge Request #";
      // 
      // txtGitMergeRequestNo
      // 
      this.txtGitMergeRequestNo.Enabled = false;
      this.txtGitMergeRequestNo.Location = new System.Drawing.Point(399, 46);
      this.txtGitMergeRequestNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtGitMergeRequestNo.Name = "txtGitMergeRequestNo";
      this.txtGitMergeRequestNo.ReadOnly = true;
      this.txtGitMergeRequestNo.Size = new System.Drawing.Size(86, 25);
      this.txtGitMergeRequestNo.TabIndex = 5;
      // 
      // lblUAT_IssueKey
      // 
      this.lblUAT_IssueKey.AutoSize = true;
      this.lblUAT_IssueKey.Location = new System.Drawing.Point(62, 120);
      this.lblUAT_IssueKey.Name = "lblUAT_IssueKey";
      this.lblUAT_IssueKey.Size = new System.Drawing.Size(31, 17);
      this.lblUAT_IssueKey.TabIndex = 15;
      this.lblUAT_IssueKey.Text = "UAT";
      // 
      // txtUAT
      // 
      this.txtUAT.Location = new System.Drawing.Point(121, 116);
      this.txtUAT.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtUAT.Name = "txtUAT";
      this.txtUAT.ReadOnly = true;
      this.txtUAT.Size = new System.Drawing.Size(97, 25);
      this.txtUAT.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(109, 17);
      this.label2.TabIndex = 13;
      this.label2.Text = "Internal Issue Key";
      // 
      // lblREQ_IssueKey
      // 
      this.lblREQ_IssueKey.AutoSize = true;
      this.lblREQ_IssueKey.Location = new System.Drawing.Point(62, 50);
      this.lblREQ_IssueKey.Name = "lblREQ_IssueKey";
      this.lblREQ_IssueKey.Size = new System.Drawing.Size(33, 17);
      this.lblREQ_IssueKey.TabIndex = 12;
      this.lblREQ_IssueKey.Text = "REQ";
      // 
      // txtInternal
      // 
      this.txtInternal.Location = new System.Drawing.Point(121, 12);
      this.txtInternal.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtInternal.Name = "txtInternal";
      this.txtInternal.Size = new System.Drawing.Size(97, 25);
      this.txtInternal.TabIndex = 0;
      // 
      // txtREQ
      // 
      this.txtREQ.Location = new System.Drawing.Point(121, 46);
      this.txtREQ.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtREQ.Name = "txtREQ";
      this.txtREQ.ReadOnly = true;
      this.txtREQ.Size = new System.Drawing.Size(97, 25);
      this.txtREQ.TabIndex = 1;
      // 
      // txtDump
      // 
      this.txtDump.BackColor = System.Drawing.Color.Black;
      this.txtDump.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDump.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.txtDump.ForeColor = System.Drawing.Color.Lime;
      this.txtDump.Location = new System.Drawing.Point(5, 206);
      this.txtDump.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtDump.Multiline = true;
      this.txtDump.Name = "txtDump";
      this.txtDump.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtDump.Size = new System.Drawing.Size(753, 299);
      this.txtDump.TabIndex = 1;
      // 
      // btnTest
      // 
      this.btnTest.Location = new System.Drawing.Point(240, 159);
      this.btnTest.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(92, 25);
      this.btnTest.TabIndex = 32;
      this.btnTest.Text = "Test";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // lblREQ_Status
      // 
      this.lblREQ_Status.AutoSize = true;
      this.lblREQ_Status.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblREQ_Status.Location = new System.Drawing.Point(8, 54);
      this.lblREQ_Status.Name = "lblREQ_Status";
      this.lblREQ_Status.Size = new System.Drawing.Size(31, 12);
      this.lblREQ_Status.TabIndex = 33;
      this.lblREQ_Status.Text = "Status";
      // 
      // lblDPLY_Status
      // 
      this.lblDPLY_Status.AutoSize = true;
      this.lblDPLY_Status.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblDPLY_Status.Location = new System.Drawing.Point(9, 88);
      this.lblDPLY_Status.Name = "lblDPLY_Status";
      this.lblDPLY_Status.Size = new System.Drawing.Size(31, 12);
      this.lblDPLY_Status.TabIndex = 34;
      this.lblDPLY_Status.Text = "Status";
      // 
      // lblUAT_Status
      // 
      this.lblUAT_Status.AutoSize = true;
      this.lblUAT_Status.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblUAT_Status.Location = new System.Drawing.Point(9, 123);
      this.lblUAT_Status.Name = "lblUAT_Status";
      this.lblUAT_Status.Size = new System.Drawing.Size(31, 12);
      this.lblUAT_Status.TabIndex = 35;
      this.lblUAT_Status.Text = "Status";
      // 
      // frmDeployment
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(763, 510);
      this.Controls.Add(this.txtDump);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmDeployment";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.ShowInTaskbar = false;
      this.Text = "Deployment Helper";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.imgGitLabActive)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgJiraActive)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnBuild;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cmbGitProjectName;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtGitMergeRequestNo;
    private System.Windows.Forms.Label lblUAT_IssueKey;
    private System.Windows.Forms.TextBox txtUAT;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblREQ_IssueKey;
    private System.Windows.Forms.TextBox txtInternal;
    private System.Windows.Forms.TextBox txtREQ;
    private System.Windows.Forms.TextBox txtDump;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RadioButton rdbTest;
    private System.Windows.Forms.RadioButton rdbProduction;
    private System.Windows.Forms.Label lblDPLY_IssueKey;
    private System.Windows.Forms.TextBox txtDPLY;
    private System.Windows.Forms.Button btnRefresh;
    private System.Windows.Forms.Button btnClearLog;
    private System.Windows.Forms.CheckBox checkBox3;
    private System.Windows.Forms.CheckBox checkBox2;
    private System.Windows.Forms.LinkLabel lblMergeRequestLink;
    private System.Windows.Forms.PictureBox imgGitLabActive;
    private System.Windows.Forms.PictureBox imgJiraActive;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.Label lblUAT_Status;
    private System.Windows.Forms.Label lblDPLY_Status;
    private System.Windows.Forms.Label lblREQ_Status;
    //private System.Windows.Forms.TextBox txtDump;
  }
}