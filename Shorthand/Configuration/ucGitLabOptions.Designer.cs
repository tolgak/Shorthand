namespace Shorthand.Configuration
{
  partial class ucGitLabOptions
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.txtDefaultGitProjectName = new System.Windows.Forms.TextBox();
      this.txtUrl = new System.Windows.Forms.TextBox();
      this.txtPrivateToken = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtDefaultGitProjectName
      // 
      this.txtDefaultGitProjectName.Location = new System.Drawing.Point(168, 31);
      this.txtDefaultGitProjectName.Name = "txtDefaultGitProjectName";
      this.txtDefaultGitProjectName.Size = new System.Drawing.Size(247, 25);
      this.txtDefaultGitProjectName.TabIndex = 0;
      // 
      // txtUrl
      // 
      this.txtUrl.Location = new System.Drawing.Point(168, 63);
      this.txtUrl.Name = "txtUrl";
      this.txtUrl.Size = new System.Drawing.Size(247, 25);
      this.txtUrl.TabIndex = 1;
      // 
      // txtPrivateToken
      // 
      this.txtPrivateToken.Location = new System.Drawing.Point(168, 94);
      this.txtPrivateToken.Name = "txtPrivateToken";
      this.txtPrivateToken.Size = new System.Drawing.Size(247, 25);
      this.txtPrivateToken.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 35);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(152, 17);
      this.label1.TabIndex = 3;
      this.label1.Text = "Default Git Project Name";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 65);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(31, 17);
      this.label2.TabIndex = 4;
      this.label2.Text = "URL";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 97);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(85, 17);
      this.label3.TabIndex = 5;
      this.label3.Text = "Private Token";
      // 
      // ucGitLabOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtPrivateToken);
      this.Controls.Add(this.txtUrl);
      this.Controls.Add(this.txtDefaultGitProjectName);
      this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.Name = "ucGitLabOptions";
      this.Size = new System.Drawing.Size(637, 607);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtDefaultGitProjectName;
    private System.Windows.Forms.TextBox txtUrl;
    private System.Windows.Forms.TextBox txtPrivateToken;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
  }
}
