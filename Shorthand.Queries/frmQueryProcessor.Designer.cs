namespace Shorthand.Queries
{
  partial class frmQueryProcessor
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
      this.tvQuery = new System.Windows.Forms.TreeView();
      this.lblStatus = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.pnlParams = new System.Windows.Forms.Panel();
      this.btnRun = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.pnlParams.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.tvQuery);
      this.panel1.Controls.Add(this.lblStatus);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(310, 605);
      this.panel1.TabIndex = 3;
      // 
      // tvQuery
      // 
      this.tvQuery.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvQuery.Location = new System.Drawing.Point(0, 0);
      this.tvQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tvQuery.Name = "tvQuery";
      this.tvQuery.Size = new System.Drawing.Size(310, 581);
      this.tvQuery.TabIndex = 1;
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 581);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(310, 24);
      this.lblStatus.TabIndex = 2;
      // 
      // panel2
      // 
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(313, 317);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(568, 291);
      this.panel2.TabIndex = 5;
      // 
      // pnlParams
      // 
      this.pnlParams.Controls.Add(this.btnRun);
      this.pnlParams.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlParams.Location = new System.Drawing.Point(313, 3);
      this.pnlParams.Name = "pnlParams";
      this.pnlParams.Size = new System.Drawing.Size(568, 314);
      this.pnlParams.TabIndex = 6;
      // 
      // btnRun
      // 
      this.btnRun.Location = new System.Drawing.Point(6, 285);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(75, 23);
      this.btnRun.TabIndex = 7;
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      // 
      // frmQueryProcessor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 611);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.pnlParams);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmQueryProcessor";
      this.Padding = new System.Windows.Forms.Padding(3);
      this.Text = "Query Processor";
      this.panel1.ResumeLayout(false);
      this.pnlParams.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TreeView tvQuery;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel pnlParams;
    private System.Windows.Forms.Button btnRun;
  }
}