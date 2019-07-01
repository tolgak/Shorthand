namespace Shorthand.DataScraper
{
  partial class frmScraper
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScraper));
      this.txtLog = new System.Windows.Forms.TextBox();
      this.btnGetData = new System.Windows.Forms.Button();
      this.txtPage = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txtLog
      // 
      this.txtLog.BackColor = System.Drawing.Color.Black;
      this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.txtLog.ForeColor = System.Drawing.Color.Lime;
      this.txtLog.Location = new System.Drawing.Point(0, 196);
      this.txtLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLog.Size = new System.Drawing.Size(884, 415);
      this.txtLog.TabIndex = 24;
      this.txtLog.WordWrap = false;
      // 
      // btnGetData
      // 
      this.btnGetData.Location = new System.Drawing.Point(12, 125);
      this.btnGetData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnGetData.Name = "btnGetData";
      this.btnGetData.Size = new System.Drawing.Size(87, 30);
      this.btnGetData.TabIndex = 25;
      this.btnGetData.Text = "Get Data";
      this.btnGetData.UseVisualStyleBackColor = true;
      this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
      // 
      // txtPage
      // 
      this.txtPage.Location = new System.Drawing.Point(45, 29);
      this.txtPage.Name = "txtPage";
      this.txtPage.Size = new System.Drawing.Size(100, 25);
      this.txtPage.TabIndex = 26;
      // 
      // frmScraper
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 611);
      this.Controls.Add(this.txtPage);
      this.Controls.Add(this.btnGetData);
      this.Controls.Add(this.txtLog);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmScraper";
      this.Text = "Data Scraper";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Button btnGetData;
    private System.Windows.Forms.TextBox txtPage;
  }
}

