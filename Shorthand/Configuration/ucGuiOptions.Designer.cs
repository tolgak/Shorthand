namespace Shorthand
{
  partial class ucGuiOptions
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
      this.txtWidth = new System.Windows.Forms.TextBox();
      this.txtHeight = new System.Windows.Forms.TextBox();
      this.lblWidth = new System.Windows.Forms.Label();
      this.lblHeight = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtWidth
      // 
      this.txtWidth.Location = new System.Drawing.Point(79, 32);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(100, 25);
      this.txtWidth.TabIndex = 0;
      // 
      // txtHeight
      // 
      this.txtHeight.Location = new System.Drawing.Point(79, 63);
      this.txtHeight.Name = "txtHeight";
      this.txtHeight.Size = new System.Drawing.Size(100, 25);
      this.txtHeight.TabIndex = 1;
      // 
      // lblWidth
      // 
      this.lblWidth.AutoSize = true;
      this.lblWidth.Location = new System.Drawing.Point(14, 35);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(42, 17);
      this.lblWidth.TabIndex = 2;
      this.lblWidth.Text = "Width";
      // 
      // lblHeight
      // 
      this.lblHeight.AutoSize = true;
      this.lblHeight.Location = new System.Drawing.Point(14, 66);
      this.lblHeight.Name = "lblHeight";
      this.lblHeight.Size = new System.Drawing.Size(46, 17);
      this.lblHeight.TabIndex = 3;
      this.lblHeight.Text = "Height";
      // 
      // ucGuiOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.lblHeight);
      this.Controls.Add(this.lblWidth);
      this.Controls.Add(this.txtHeight);
      this.Controls.Add(this.txtWidth);
      this.Name = "ucGuiOptions";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtWidth;
    private System.Windows.Forms.TextBox txtHeight;
    private System.Windows.Forms.Label lblWidth;
    private System.Windows.Forms.Label lblHeight;
  }
}
