﻿namespace Shorthand
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeployment));
      this.panel1 = new System.Windows.Forms.Panel();
      this.chkCopyExecutables = new System.Windows.Forms.CheckBox();
      this.grpTask = new System.Windows.Forms.GroupBox();
      this.lblCustomize = new System.Windows.Forms.Label();
      this.chkCreateMergeRequest = new System.Windows.Forms.CheckBox();
      this.chkCreateUAT = new System.Windows.Forms.CheckBox();
      this.chkCreateDPLY = new System.Windows.Forms.CheckBox();
      this.rdbTest = new System.Windows.Forms.RadioButton();
      this.rdbProduction = new System.Windows.Forms.RadioButton();
      this.btnDeploy = new System.Windows.Forms.Button();
      this.btnMakeExecutable = new System.Windows.Forms.Button();
      this.lblInternal_Status = new System.Windows.Forms.Label();
      this.lblUAT_Status = new System.Windows.Forms.Label();
      this.lblDPLY_Status = new System.Windows.Forms.Label();
      this.lblREQ_Status = new System.Windows.Forms.Label();
      this.btnTest = new System.Windows.Forms.Button();
      this.lblMergeRequestLink = new System.Windows.Forms.LinkLabel();
      this.btnClearLog = new System.Windows.Forms.Button();
      this.btnRefresh = new System.Windows.Forms.Button();
      this.lblDPLY_IssueKey = new System.Windows.Forms.Label();
      this.txtDPLY = new System.Windows.Forms.TextBox();
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
      this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
      this.panel1.SuspendLayout();
      this.grpTask.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.chkCopyExecutables);
      this.panel1.Controls.Add(this.grpTask);
      this.panel1.Controls.Add(this.btnMakeExecutable);
      this.panel1.Controls.Add(this.lblInternal_Status);
      this.panel1.Controls.Add(this.lblUAT_Status);
      this.panel1.Controls.Add(this.lblDPLY_Status);
      this.panel1.Controls.Add(this.lblREQ_Status);
      this.panel1.Controls.Add(this.btnTest);
      this.panel1.Controls.Add(this.lblMergeRequestLink);
      this.panel1.Controls.Add(this.btnClearLog);
      this.panel1.Controls.Add(this.btnRefresh);
      this.panel1.Controls.Add(this.lblDPLY_IssueKey);
      this.panel1.Controls.Add(this.txtDPLY);
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
      this.panel1.Size = new System.Drawing.Size(745, 301);
      this.panel1.TabIndex = 0;
      // 
      // chkCopyExecutables
      // 
      this.chkCopyExecutables.AutoSize = true;
      this.chkCopyExecutables.Checked = true;
      this.chkCopyExecutables.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCopyExecutables.Location = new System.Drawing.Point(398, 250);
      this.chkCopyExecutables.Name = "chkCopyExecutables";
      this.chkCopyExecutables.Size = new System.Drawing.Size(129, 21);
      this.chkCopyExecutables.TabIndex = 33;
      this.chkCopyExecutables.Text = "Copy executables";
      this.chkCopyExecutables.UseVisualStyleBackColor = true;
      // 
      // grpTask
      // 
      this.grpTask.Controls.Add(this.lblCustomize);
      this.grpTask.Controls.Add(this.chkCreateMergeRequest);
      this.grpTask.Controls.Add(this.chkCreateUAT);
      this.grpTask.Controls.Add(this.chkCreateDPLY);
      this.grpTask.Controls.Add(this.rdbTest);
      this.grpTask.Controls.Add(this.rdbProduction);
      this.grpTask.Controls.Add(this.btnDeploy);
      this.grpTask.Location = new System.Drawing.Point(380, 94);
      this.grpTask.Name = "grpTask";
      this.grpTask.Size = new System.Drawing.Size(350, 200);
      this.grpTask.TabIndex = 38;
      this.grpTask.TabStop = false;
      this.grpTask.Text = "Deployment options";
      // 
      // lblCustomize
      // 
      this.lblCustomize.AutoSize = true;
      this.lblCustomize.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblCustomize.Location = new System.Drawing.Point(15, 55);
      this.lblCustomize.Name = "lblCustomize";
      this.lblCustomize.Size = new System.Drawing.Size(72, 17);
      this.lblCustomize.TabIndex = 32;
      this.lblCustomize.Text = "Customize";
      // 
      // chkCreateMergeRequest
      // 
      this.chkCreateMergeRequest.AutoSize = true;
      this.chkCreateMergeRequest.Location = new System.Drawing.Point(18, 129);
      this.chkCreateMergeRequest.Name = "chkCreateMergeRequest";
      this.chkCreateMergeRequest.Size = new System.Drawing.Size(155, 21);
      this.chkCreateMergeRequest.TabIndex = 31;
      this.chkCreateMergeRequest.Text = "Create merge request";
      this.chkCreateMergeRequest.UseVisualStyleBackColor = true;
      // 
      // chkCreateUAT
      // 
      this.chkCreateUAT.AutoSize = true;
      this.chkCreateUAT.Checked = true;
      this.chkCreateUAT.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCreateUAT.Location = new System.Drawing.Point(18, 102);
      this.chkCreateUAT.Name = "chkCreateUAT";
      this.chkCreateUAT.Size = new System.Drawing.Size(152, 21);
      this.chkCreateUAT.TabIndex = 30;
      this.chkCreateUAT.Text = "Create user test issue";
      this.chkCreateUAT.UseVisualStyleBackColor = true;
      // 
      // chkCreateDPLY
      // 
      this.chkCreateDPLY.AutoSize = true;
      this.chkCreateDPLY.Location = new System.Drawing.Point(18, 75);
      this.chkCreateDPLY.Name = "chkCreateDPLY";
      this.chkCreateDPLY.Size = new System.Drawing.Size(171, 21);
      this.chkCreateDPLY.TabIndex = 29;
      this.chkCreateDPLY.Text = "Create deployment issue";
      this.chkCreateDPLY.UseVisualStyleBackColor = true;
      // 
      // rdbTest
      // 
      this.rdbTest.AutoSize = true;
      this.rdbTest.Checked = true;
      this.rdbTest.Location = new System.Drawing.Point(131, 27);
      this.rdbTest.Name = "rdbTest";
      this.rdbTest.Size = new System.Drawing.Size(67, 21);
      this.rdbTest.TabIndex = 10;
      this.rdbTest.TabStop = true;
      this.rdbTest.Text = "To Test";
      this.rdbTest.UseVisualStyleBackColor = true;
      // 
      // rdbProduction
      // 
      this.rdbProduction.AutoSize = true;
      this.rdbProduction.Location = new System.Drawing.Point(18, 27);
      this.rdbProduction.Name = "rdbProduction";
      this.rdbProduction.Size = new System.Drawing.Size(107, 21);
      this.rdbProduction.TabIndex = 9;
      this.rdbProduction.Text = "To Production";
      this.rdbProduction.UseVisualStyleBackColor = true;
      // 
      // btnDeploy
      // 
      this.btnDeploy.Location = new System.Drawing.Point(252, 162);
      this.btnDeploy.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.btnDeploy.Name = "btnDeploy";
      this.btnDeploy.Size = new System.Drawing.Size(92, 26);
      this.btnDeploy.TabIndex = 11;
      this.btnDeploy.Text = "Deploy";
      this.btnDeploy.UseVisualStyleBackColor = true;
      this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
      // 
      // btnMakeExecutable
      // 
      this.btnMakeExecutable.Location = new System.Drawing.Point(9, 206);
      this.btnMakeExecutable.Name = "btnMakeExecutable";
      this.btnMakeExecutable.Size = new System.Drawing.Size(123, 26);
      this.btnMakeExecutable.TabIndex = 37;
      this.btnMakeExecutable.Text = "Make Executable";
      this.btnMakeExecutable.UseVisualStyleBackColor = true;
      this.btnMakeExecutable.Click += new System.EventHandler(this.btnMakeExecutable_Click);
      // 
      // lblInternal_Status
      // 
      this.lblInternal_Status.AutoSize = true;
      this.lblInternal_Status.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblInternal_Status.Location = new System.Drawing.Point(185, 22);
      this.lblInternal_Status.Name = "lblInternal_Status";
      this.lblInternal_Status.Size = new System.Drawing.Size(31, 12);
      this.lblInternal_Status.TabIndex = 36;
      this.lblInternal_Status.Text = "Status";
      // 
      // lblUAT_Status
      // 
      this.lblUAT_Status.AutoSize = true;
      this.lblUAT_Status.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblUAT_Status.Location = new System.Drawing.Point(185, 135);
      this.lblUAT_Status.Name = "lblUAT_Status";
      this.lblUAT_Status.Size = new System.Drawing.Size(31, 12);
      this.lblUAT_Status.TabIndex = 35;
      this.lblUAT_Status.Text = "Status";
      // 
      // lblDPLY_Status
      // 
      this.lblDPLY_Status.AutoSize = true;
      this.lblDPLY_Status.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblDPLY_Status.Location = new System.Drawing.Point(185, 100);
      this.lblDPLY_Status.Name = "lblDPLY_Status";
      this.lblDPLY_Status.Size = new System.Drawing.Size(31, 12);
      this.lblDPLY_Status.TabIndex = 34;
      this.lblDPLY_Status.Text = "Status";
      // 
      // lblREQ_Status
      // 
      this.lblREQ_Status.AutoSize = true;
      this.lblREQ_Status.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblREQ_Status.Location = new System.Drawing.Point(185, 64);
      this.lblREQ_Status.Name = "lblREQ_Status";
      this.lblREQ_Status.Size = new System.Drawing.Size(31, 12);
      this.lblREQ_Status.TabIndex = 33;
      this.lblREQ_Status.Text = "Status";
      // 
      // btnTest
      // 
      this.btnTest.Location = new System.Drawing.Point(252, 174);
      this.btnTest.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(92, 26);
      this.btnTest.TabIndex = 32;
      this.btnTest.Text = "Test";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // lblMergeRequestLink
      // 
      this.lblMergeRequestLink.AutoSize = true;
      this.lblMergeRequestLink.Location = new System.Drawing.Point(549, 50);
      this.lblMergeRequestLink.Name = "lblMergeRequestLink";
      this.lblMergeRequestLink.Size = new System.Drawing.Size(181, 17);
      this.lblMergeRequestLink.TabIndex = 29;
      this.lblMergeRequestLink.TabStop = true;
      this.lblMergeRequestLink.Text = "merge request state unknown";
      // 
      // btnClearLog
      // 
      this.btnClearLog.Location = new System.Drawing.Point(138, 174);
      this.btnClearLog.Name = "btnClearLog";
      this.btnClearLog.Size = new System.Drawing.Size(108, 26);
      this.btnClearLog.TabIndex = 25;
      this.btnClearLog.Text = "Clear Log";
      this.btnClearLog.UseVisualStyleBackColor = true;
      this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
      // 
      // btnRefresh
      // 
      this.btnRefresh.Location = new System.Drawing.Point(9, 174);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(123, 26);
      this.btnRefresh.TabIndex = 9;
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
      // 
      // lblDPLY_IssueKey
      // 
      this.lblDPLY_IssueKey.AutoSize = true;
      this.lblDPLY_IssueKey.Location = new System.Drawing.Point(33, 98);
      this.lblDPLY_IssueKey.Name = "lblDPLY_IssueKey";
      this.lblDPLY_IssueKey.Size = new System.Drawing.Size(36, 17);
      this.lblDPLY_IssueKey.TabIndex = 24;
      this.lblDPLY_IssueKey.Text = "DPLY";
      // 
      // txtDPLY
      // 
      this.txtDPLY.Location = new System.Drawing.Point(81, 94);
      this.txtDPLY.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtDPLY.Name = "txtDPLY";
      this.txtDPLY.ReadOnly = true;
      this.txtDPLY.Size = new System.Drawing.Size(97, 25);
      this.txtDPLY.TabIndex = 2;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(326, 16);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(107, 17);
      this.label5.TabIndex = 19;
      this.label5.Text = "Git Project Name";
      // 
      // cmbGitProjectName
      // 
      this.cmbGitProjectName.DisplayMember = "Text";
      this.cmbGitProjectName.FormattingEnabled = true;
      this.cmbGitProjectName.Location = new System.Drawing.Point(459, 12);
      this.cmbGitProjectName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.cmbGitProjectName.Name = "cmbGitProjectName";
      this.cmbGitProjectName.Size = new System.Drawing.Size(271, 25);
      this.cmbGitProjectName.TabIndex = 4;
      this.cmbGitProjectName.ValueMember = "Value";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(326, 50);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(130, 17);
      this.label4.TabIndex = 17;
      this.label4.Text = "Git Merge Request #";
      // 
      // txtGitMergeRequestNo
      // 
      this.txtGitMergeRequestNo.Enabled = false;
      this.txtGitMergeRequestNo.Location = new System.Drawing.Point(459, 46);
      this.txtGitMergeRequestNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtGitMergeRequestNo.Name = "txtGitMergeRequestNo";
      this.txtGitMergeRequestNo.ReadOnly = true;
      this.txtGitMergeRequestNo.Size = new System.Drawing.Size(86, 25);
      this.txtGitMergeRequestNo.TabIndex = 5;
      // 
      // lblUAT_IssueKey
      // 
      this.lblUAT_IssueKey.AutoSize = true;
      this.lblUAT_IssueKey.Location = new System.Drawing.Point(33, 133);
      this.lblUAT_IssueKey.Name = "lblUAT_IssueKey";
      this.lblUAT_IssueKey.Size = new System.Drawing.Size(31, 17);
      this.lblUAT_IssueKey.TabIndex = 15;
      this.lblUAT_IssueKey.Text = "UAT";
      // 
      // txtUAT
      // 
      this.txtUAT.Location = new System.Drawing.Point(81, 129);
      this.txtUAT.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtUAT.Name = "txtUAT";
      this.txtUAT.ReadOnly = true;
      this.txtUAT.Size = new System.Drawing.Size(97, 25);
      this.txtUAT.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 11);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(62, 34);
      this.label2.TabIndex = 13;
      this.label2.Text = "Internal\r\nIssue Key";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblREQ_IssueKey
      // 
      this.lblREQ_IssueKey.AutoSize = true;
      this.lblREQ_IssueKey.Location = new System.Drawing.Point(33, 60);
      this.lblREQ_IssueKey.Name = "lblREQ_IssueKey";
      this.lblREQ_IssueKey.Size = new System.Drawing.Size(33, 17);
      this.lblREQ_IssueKey.TabIndex = 12;
      this.lblREQ_IssueKey.Text = "REQ";
      // 
      // txtInternal
      // 
      this.txtInternal.Location = new System.Drawing.Point(81, 16);
      this.txtInternal.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtInternal.Name = "txtInternal";
      this.txtInternal.Size = new System.Drawing.Size(97, 25);
      this.txtInternal.TabIndex = 0;
      // 
      // txtREQ
      // 
      this.txtREQ.Location = new System.Drawing.Point(81, 57);
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
      this.txtDump.Location = new System.Drawing.Point(5, 306);
      this.txtDump.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtDump.Multiline = true;
      this.txtDump.Name = "txtDump";
      this.txtDump.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtDump.Size = new System.Drawing.Size(745, 331);
      this.txtDump.TabIndex = 1;
      // 
      // ımageList1
      // 
      this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.ımageList1.ImageSize = new System.Drawing.Size(16, 16);
      this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // frmDeployment
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(755, 642);
      this.Controls.Add(this.txtDump);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmDeployment";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.ShowInTaskbar = false;
      this.Text = "Deployment Helper";
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDeployment_KeyUp);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.grpTask.ResumeLayout(false);
      this.grpTask.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
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
    private System.Windows.Forms.Label lblDPLY_IssueKey;
    private System.Windows.Forms.TextBox txtDPLY;
    private System.Windows.Forms.Button btnRefresh;
    private System.Windows.Forms.Button btnClearLog;
    private System.Windows.Forms.LinkLabel lblMergeRequestLink;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.Label lblUAT_Status;
    private System.Windows.Forms.Label lblDPLY_Status;
    private System.Windows.Forms.Label lblREQ_Status;
    private System.Windows.Forms.ImageList ımageList1;
    private System.Windows.Forms.Label lblInternal_Status;
    private System.Windows.Forms.Button btnMakeExecutable;
    private System.Windows.Forms.GroupBox grpTask;
    private System.Windows.Forms.Label lblCustomize;
    private System.Windows.Forms.CheckBox chkCreateMergeRequest;
    private System.Windows.Forms.CheckBox chkCreateUAT;
    private System.Windows.Forms.CheckBox chkCreateDPLY;
    private System.Windows.Forms.RadioButton rdbTest;
    private System.Windows.Forms.RadioButton rdbProduction;
    private System.Windows.Forms.Button btnDeploy;
    private System.Windows.Forms.CheckBox chkCopyExecutables;
    //private System.Windows.Forms.TextBox txtDump;
  }
}