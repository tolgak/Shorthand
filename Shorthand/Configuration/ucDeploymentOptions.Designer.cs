namespace Shorthand
{
  partial class ucDeploymentOptions
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
      this.label1 = new System.Windows.Forms.Label();
      this.txtLocalBinPath = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtArchiveToolSwitches = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtArchiveToolPath = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtDestinationFolder = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtRemoteBinPath = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 35);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(94, 17);
      this.label1.TabIndex = 0;
      this.label1.Text = "Local bin path ";
      // 
      // txtLocalBinPath
      // 
      this.txtLocalBinPath.Location = new System.Drawing.Point(125, 32);
      this.txtLocalBinPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtLocalBinPath.Name = "txtLocalBinPath";
      this.txtLocalBinPath.Size = new System.Drawing.Size(238, 25);
      this.txtLocalBinPath.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtArchiveToolSwitches);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.txtArchiveToolPath);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Location = new System.Drawing.Point(10, 98);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(370, 99);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Archive tool";
      // 
      // txtArchiveToolSwitches
      // 
      this.txtArchiveToolSwitches.Location = new System.Drawing.Point(106, 55);
      this.txtArchiveToolSwitches.Name = "txtArchiveToolSwitches";
      this.txtArchiveToolSwitches.Size = new System.Drawing.Size(246, 25);
      this.txtArchiveToolSwitches.TabIndex = 2;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 58);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(85, 17);
      this.label4.TabIndex = 11;
      this.label4.Text = "Tool switches";
      // 
      // txtArchiveToolPath
      // 
      this.txtArchiveToolPath.Location = new System.Drawing.Point(106, 24);
      this.txtArchiveToolPath.Name = "txtArchiveToolPath";
      this.txtArchiveToolPath.Size = new System.Drawing.Size(246, 25);
      this.txtArchiveToolPath.TabIndex = 1;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 27);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(33, 17);
      this.label3.TabIndex = 9;
      this.label3.Text = "Path";
      // 
      // txtDestinationFolder
      // 
      this.txtDestinationFolder.Location = new System.Drawing.Point(134, 209);
      this.txtDestinationFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtDestinationFolder.Name = "txtDestinationFolder";
      this.txtDestinationFolder.Size = new System.Drawing.Size(228, 25);
      this.txtDestinationFolder.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 212);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(114, 17);
      this.label2.TabIndex = 9;
      this.label2.Text = "Destination Folder";
      // 
      // txtRemoteBinPath
      // 
      this.txtRemoteBinPath.Location = new System.Drawing.Point(125, 65);
      this.txtRemoteBinPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtRemoteBinPath.Name = "txtRemoteBinPath";
      this.txtRemoteBinPath.Size = new System.Drawing.Size(238, 25);
      this.txtRemoteBinPath.TabIndex = 1;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(14, 68);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(109, 17);
      this.label5.TabIndex = 11;
      this.label5.Text = "Remote bin path ";
      // 
      // ucDeploymentOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtRemoteBinPath);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtDestinationFolder);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.txtLocalBinPath);
      this.Controls.Add(this.label1);
      this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
      this.Name = "ucDeploymentOptions";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtLocalBinPath;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtArchiveToolSwitches;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtArchiveToolPath;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtDestinationFolder;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtRemoteBinPath;
    private System.Windows.Forms.Label label5;
  }
}
