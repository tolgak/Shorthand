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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.btnCheckCPU = new System.Windows.Forms.Button();
            this.pbCPU = new System.Windows.Forms.ProgressBar();
            this.chkRepeat = new System.Windows.Forms.CheckBox();
            this.lblCPU = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
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
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(12, 282);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(830, 318);
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
            this.label1.Location = new System.Drawing.Point(24, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 33;
            this.label1.Text = "User name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 34;
            this.label2.Text = "Current Password";
            // 
            // btnCheckCPU
            // 
            this.btnCheckCPU.Location = new System.Drawing.Point(557, 195);
            this.btnCheckCPU.Name = "btnCheckCPU";
            this.btnCheckCPU.Size = new System.Drawing.Size(120, 23);
            this.btnCheckCPU.TabIndex = 36;
            this.btnCheckCPU.Text = "Check CPU";
            this.btnCheckCPU.UseVisualStyleBackColor = true;
            this.btnCheckCPU.Click += new System.EventHandler(this.btnCheckCPU_Click);
            // 
            // pbCPU
            // 
            this.pbCPU.Location = new System.Drawing.Point(468, 151);
            this.pbCPU.Name = "pbCPU";
            this.pbCPU.Size = new System.Drawing.Size(379, 18);
            this.pbCPU.Step = 1;
            this.pbCPU.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCPU.TabIndex = 34;
            // 
            // chkRepeat
            // 
            this.chkRepeat.AutoSize = true;
            this.chkRepeat.Location = new System.Drawing.Point(468, 197);
            this.chkRepeat.Name = "chkRepeat";
            this.chkRepeat.Size = new System.Drawing.Size(83, 21);
            this.chkRepeat.TabIndex = 35;
            this.chkRepeat.Text = "Repeated";
            this.chkRepeat.UseVisualStyleBackColor = true;
            // 
            // lblCPU
            // 
            this.lblCPU.AutoSize = true;
            this.lblCPU.Location = new System.Drawing.Point(465, 172);
            this.lblCPU.Name = "lblCPU";
            this.lblCPU.Size = new System.Drawing.Size(15, 17);
            this.lblCPU.TabIndex = 38;
            this.lblCPU.Text = "0";
            this.lblCPU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(468, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.CustomProperties = "IsXAxisQuantitative=True";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(379, 133);
            this.chart1.TabIndex = 39;
            this.chart1.TabStop = false;
            this.chart1.Text = "chart1";
            // 
            // frmAdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 612);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lblCPU);
            this.Controls.Add(this.chkRepeat);
            this.Controls.Add(this.pbCPU);
            this.Controls.Add(this.btnCheckCPU);
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
            this.Text = "Admin Panel";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
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
        private System.Windows.Forms.Button btnCheckCPU;
        private System.Windows.Forms.ProgressBar pbCPU;
        private System.Windows.Forms.CheckBox chkRepeat;
        private System.Windows.Forms.Label lblCPU;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}