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
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnJIRA = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.txtDPLY = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.rdbTest = new System.Windows.Forms.RadioButton();
      this.rdbProduction = new System.Windows.Forms.RadioButton();
      this.btnBuild = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.cmbGitProjectName = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtGitMergeRequestNo = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtUAT = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtInternal = new System.Windows.Forms.TextBox();
      this.txtTalep = new System.Windows.Forms.TextBox();
      this.txtDump = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnJIRA);
      this.panel1.Controls.Add(this.label7);
      this.panel1.Controls.Add(this.txtDPLY);
      this.panel1.Controls.Add(this.label6);
      this.panel1.Controls.Add(this.rdbTest);
      this.panel1.Controls.Add(this.rdbProduction);
      this.panel1.Controls.Add(this.btnBuild);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.cmbGitProjectName);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.txtGitMergeRequestNo);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtUAT);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtInternal);
      this.panel1.Controls.Add(this.txtTalep);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.panel1.Location = new System.Drawing.Point(5, 5);
      this.panel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(674, 201);
      this.panel1.TabIndex = 11;
      // 
      // btnJIRA
      // 
      this.btnJIRA.Location = new System.Drawing.Point(113, 159);
      this.btnJIRA.Name = "btnJIRA";
      this.btnJIRA.Size = new System.Drawing.Size(146, 25);
      this.btnJIRA.TabIndex = 25;
      this.btnJIRA.Text = "Create JIRA  issue";
      this.btnJIRA.UseVisualStyleBackColor = true;
      this.btnJIRA.Click += new System.EventHandler(this.btnJIRA_Click);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(67, 120);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(49, 17);
      this.label7.TabIndex = 24;
      this.label7.Text = "DPLY #";
      // 
      // txtDPLY
      // 
      this.txtDPLY.Enabled = false;
      this.txtDPLY.Location = new System.Drawing.Point(121, 117);
      this.txtDPLY.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtDPLY.Name = "txtDPLY";
      this.txtDPLY.Size = new System.Drawing.Size(135, 25);
      this.txtDPLY.TabIndex = 3;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(275, 86);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(67, 17);
      this.label6.TabIndex = 22;
      this.label6.Text = "Deliver To";
      // 
      // rdbTest
      // 
      this.rdbTest.AutoSize = true;
      this.rdbTest.Checked = true;
      this.rdbTest.Location = new System.Drawing.Point(503, 84);
      this.rdbTest.Name = "rdbTest";
      this.rdbTest.Size = new System.Drawing.Size(50, 21);
      this.rdbTest.TabIndex = 7;
      this.rdbTest.TabStop = true;
      this.rdbTest.Text = "Test";
      this.rdbTest.UseVisualStyleBackColor = true;
      // 
      // rdbProduction
      // 
      this.rdbProduction.AutoSize = true;
      this.rdbProduction.Location = new System.Drawing.Point(408, 84);
      this.rdbProduction.Name = "rdbProduction";
      this.rdbProduction.Size = new System.Drawing.Size(89, 21);
      this.rdbProduction.TabIndex = 6;
      this.rdbProduction.Text = "Production";
      this.rdbProduction.UseVisualStyleBackColor = true;
      this.rdbProduction.CheckedChanged += new System.EventHandler(this.rdbProduction_CheckedChanged);
      // 
      // btnBuild
      // 
      this.btnBuild.Location = new System.Drawing.Point(15, 159);
      this.btnBuild.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.btnBuild.Name = "btnBuild";
      this.btnBuild.Size = new System.Drawing.Size(92, 25);
      this.btnBuild.TabIndex = 8;
      this.btnBuild.Text = "Build";
      this.btnBuild.UseVisualStyleBackColor = true;
      this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(275, 16);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(107, 17);
      this.label5.TabIndex = 19;
      this.label5.Text = "Git Project Name";
      // 
      // cmbGitProjectName
      // 
      this.cmbGitProjectName.Enabled = false;
      this.cmbGitProjectName.FormattingEnabled = true;
      this.cmbGitProjectName.Items.AddRange(new object[] {
            "Bilgi.Els",
            "Bilgi.Els.BackOffice",
            "Bilgi.Scientia.Integration",
            "Bilgi.Sis.BackOffice",
            "BilgiCampus"});
      this.cmbGitProjectName.Location = new System.Drawing.Point(408, 11);
      this.cmbGitProjectName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.cmbGitProjectName.Name = "cmbGitProjectName";
      this.cmbGitProjectName.Size = new System.Drawing.Size(233, 25);
      this.cmbGitProjectName.TabIndex = 4;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(275, 52);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(130, 17);
      this.label4.TabIndex = 17;
      this.label4.Text = "Git Merge Request #";
      // 
      // txtGitMergeRequestNo
      // 
      this.txtGitMergeRequestNo.Enabled = false;
      this.txtGitMergeRequestNo.Location = new System.Drawing.Point(408, 47);
      this.txtGitMergeRequestNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtGitMergeRequestNo.Name = "txtGitMergeRequestNo";
      this.txtGitMergeRequestNo.Size = new System.Drawing.Size(135, 25);
      this.txtGitMergeRequestNo.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(72, 85);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(44, 17);
      this.label3.TabIndex = 15;
      this.label3.Text = "UAT #";
      // 
      // txtUAT
      // 
      this.txtUAT.Enabled = false;
      this.txtUAT.Location = new System.Drawing.Point(121, 82);
      this.txtUAT.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtUAT.Name = "txtUAT";
      this.txtUAT.Size = new System.Drawing.Size(135, 25);
      this.txtUAT.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 14);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(109, 17);
      this.label2.TabIndex = 13;
      this.label2.Text = "Internal Issue Key";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(61, 49);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 17);
      this.label1.TabIndex = 12;
      this.label1.Text = "TALEP #";
      // 
      // txtInternal
      // 
      this.txtInternal.Location = new System.Drawing.Point(121, 11);
      this.txtInternal.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtInternal.Name = "txtInternal";
      this.txtInternal.Size = new System.Drawing.Size(135, 25);
      this.txtInternal.TabIndex = 0;
      // 
      // txtTalep
      // 
      this.txtTalep.Location = new System.Drawing.Point(121, 47);
      this.txtTalep.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.txtTalep.Name = "txtTalep";
      this.txtTalep.Size = new System.Drawing.Size(135, 25);
      this.txtTalep.TabIndex = 1;
      // 
      // txtDump
      // 
      this.txtDump.BackColor = System.Drawing.Color.Black;
      this.txtDump.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDump.ForeColor = System.Drawing.Color.Lime;
      this.txtDump.Location = new System.Drawing.Point(5, 206);
      this.txtDump.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtDump.Multiline = true;
      this.txtDump.Name = "txtDump";
      this.txtDump.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtDump.Size = new System.Drawing.Size(674, 250);
      this.txtDump.TabIndex = 12;
      // 
      // frmDeployment
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(684, 461);
      this.Controls.Add(this.txtDump);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmDeployment";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Deployment Helper";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
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
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtUAT;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtInternal;
    private System.Windows.Forms.TextBox txtTalep;
    private System.Windows.Forms.TextBox txtDump;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.RadioButton rdbTest;
    private System.Windows.Forms.RadioButton rdbProduction;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtDPLY;
    private System.Windows.Forms.Button btnJIRA;
    //private System.Windows.Forms.TextBox txtDump;
  }
}