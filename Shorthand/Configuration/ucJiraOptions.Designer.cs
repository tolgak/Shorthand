namespace Shorthand
{
  partial class ucJiraOptions
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.txtJiraBaseUrl = new System.Windows.Forms.TextBox();
      this.txtUsername = new System.Windows.Forms.TextBox();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtDPLY_ProjectKey = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtUAT_ProjectKey = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtREQ_ProjectKey = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txtJiraBaseUrl
      // 
      this.txtJiraBaseUrl.Location = new System.Drawing.Point(100, 32);
      this.txtJiraBaseUrl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtJiraBaseUrl.Name = "txtJiraBaseUrl";
      this.txtJiraBaseUrl.Size = new System.Drawing.Size(243, 25);
      this.txtJiraBaseUrl.TabIndex = 0;
      // 
      // txtUsername
      // 
      this.txtUsername.Location = new System.Drawing.Point(100, 65);
      this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtUsername.Name = "txtUsername";
      this.txtUsername.Size = new System.Drawing.Size(116, 25);
      this.txtUsername.TabIndex = 1;
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(100, 98);
      this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(116, 25);
      this.txtPassword.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 35);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 17);
      this.label1.TabIndex = 5;
      this.label1.Text = "Jira Base Url";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 68);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(67, 17);
      this.label3.TabIndex = 7;
      this.label3.Text = "Username";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(14, 101);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 17);
      this.label4.TabIndex = 8;
      this.label4.Text = "Password";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 181);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(104, 17);
      this.label2.TabIndex = 10;
      this.label2.Text = "DPLY project key";
      // 
      // txtDPLY_ProjectKey
      // 
      this.txtDPLY_ProjectKey.Location = new System.Drawing.Point(124, 178);
      this.txtDPLY_ProjectKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtDPLY_ProjectKey.Name = "txtDPLY_ProjectKey";
      this.txtDPLY_ProjectKey.Size = new System.Drawing.Size(97, 25);
      this.txtDPLY_ProjectKey.TabIndex = 4;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(14, 214);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(99, 17);
      this.label5.TabIndex = 12;
      this.label5.Text = "UAT project key";
      // 
      // txtUAT_ProjectKey
      // 
      this.txtUAT_ProjectKey.Location = new System.Drawing.Point(124, 211);
      this.txtUAT_ProjectKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtUAT_ProjectKey.Name = "txtUAT_ProjectKey";
      this.txtUAT_ProjectKey.Size = new System.Drawing.Size(97, 25);
      this.txtUAT_ProjectKey.TabIndex = 5;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(14, 148);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(101, 17);
      this.label6.TabIndex = 14;
      this.label6.Text = "REQ project key";
      // 
      // txtREQ_ProjectKey
      // 
      this.txtREQ_ProjectKey.Location = new System.Drawing.Point(124, 145);
      this.txtREQ_ProjectKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtREQ_ProjectKey.Name = "txtREQ_ProjectKey";
      this.txtREQ_ProjectKey.Size = new System.Drawing.Size(97, 25);
      this.txtREQ_ProjectKey.TabIndex = 3;
      // 
      // ucJiraOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.label6);
      this.Controls.Add(this.txtREQ_ProjectKey);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtUAT_ProjectKey);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtDPLY_ProjectKey);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.txtUsername);
      this.Controls.Add(this.txtJiraBaseUrl);
      this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.Name = "ucJiraOptions";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtJiraBaseUrl;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtDPLY_ProjectKey;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtUAT_ProjectKey;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtREQ_ProjectKey;
  }
}
