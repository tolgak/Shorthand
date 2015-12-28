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
      this.txtUsername.TabIndex = 3;
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(100, 98);
      this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new System.Drawing.Size(116, 25);
      this.txtPassword.TabIndex = 4;
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
      // ucJiraOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
  }
}
