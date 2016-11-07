namespace Shorthand
{
  partial class frmDataDump
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataDump));
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.txtFileNameFormat = new System.Windows.Forms.TextBox();
      this.rdCSV = new System.Windows.Forms.RadioButton();
      this.rdExcel = new System.Windows.Forms.RadioButton();
      this.lblFilePath = new System.Windows.Forms.Label();
      this.btnLoad = new System.Windows.Forms.Button();
      this.txtFilePath = new System.Windows.Forms.TextBox();
      this.lblConnection = new System.Windows.Forms.Label();
      this.txtConnection = new System.Windows.Forms.TextBox();
      this.btnDump = new System.Windows.Forms.Button();
      this.txtEditor = new System.Windows.Forms.TextBox();
      this.dlgLoad = new System.Windows.Forms.OpenFileDialog();
      this.panel2 = new System.Windows.Forms.Panel();
      this.pbRecordsetCounter = new System.Windows.Forms.ProgressBar();
      this.pbRecordCounter = new System.Windows.Forms.ProgressBar();
      this.btnSave = new System.Windows.Forms.Button();
      this.dlgSave = new System.Windows.Forms.SaveFileDialog();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtFileNameFormat);
      this.panel1.Controls.Add(this.rdCSV);
      this.panel1.Controls.Add(this.rdExcel);
      this.panel1.Controls.Add(this.lblFilePath);
      this.panel1.Controls.Add(this.btnLoad);
      this.panel1.Controls.Add(this.txtFilePath);
      this.panel1.Controls.Add(this.lblConnection);
      this.panel1.Controls.Add(this.txtConnection);
      this.panel1.Controls.Add(this.btnDump);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(2, 3);
      this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(680, 152);
      this.panel1.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 107);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(106, 17);
      this.label1.TabIndex = 10;
      this.label1.Text = "File name format";
      // 
      // txtFileNameFormat
      // 
      this.txtFileNameFormat.Location = new System.Drawing.Point(114, 103);
      this.txtFileNameFormat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtFileNameFormat.Name = "txtFileNameFormat";
      this.txtFileNameFormat.Size = new System.Drawing.Size(375, 25);
      this.txtFileNameFormat.TabIndex = 9;
      // 
      // rdCSV
      // 
      this.rdCSV.AutoSize = true;
      this.rdCSV.Location = new System.Drawing.Point(589, 47);
      this.rdCSV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.rdCSV.Name = "rdCSV";
      this.rdCSV.Size = new System.Drawing.Size(49, 21);
      this.rdCSV.TabIndex = 8;
      this.rdCSV.Text = "CSV";
      this.rdCSV.UseVisualStyleBackColor = true;
      // 
      // rdExcel
      // 
      this.rdExcel.AutoSize = true;
      this.rdExcel.Checked = true;
      this.rdExcel.Location = new System.Drawing.Point(589, 17);
      this.rdExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.rdExcel.Name = "rdExcel";
      this.rdExcel.Size = new System.Drawing.Size(55, 21);
      this.rdExcel.TabIndex = 7;
      this.rdExcel.TabStop = true;
      this.rdExcel.Text = "Excel";
      this.rdExcel.UseVisualStyleBackColor = true;
      // 
      // lblFilePath
      // 
      this.lblFilePath.AutoSize = true;
      this.lblFilePath.Location = new System.Drawing.Point(4, 49);
      this.lblFilePath.Name = "lblFilePath";
      this.lblFilePath.Size = new System.Drawing.Size(57, 17);
      this.lblFilePath.TabIndex = 6;
      this.lblFilePath.Text = "File path";
      // 
      // btnLoad
      // 
      this.btnLoad.Location = new System.Drawing.Point(498, 44);
      this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(45, 30);
      this.btnLoad.TabIndex = 5;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // txtFilePath
      // 
      this.txtFilePath.Location = new System.Drawing.Point(114, 47);
      this.txtFilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtFilePath.Name = "txtFilePath";
      this.txtFilePath.Size = new System.Drawing.Size(375, 25);
      this.txtFilePath.TabIndex = 4;
      // 
      // lblConnection
      // 
      this.lblConnection.AutoSize = true;
      this.lblConnection.Location = new System.Drawing.Point(4, 17);
      this.lblConnection.Name = "lblConnection";
      this.lblConnection.Size = new System.Drawing.Size(110, 17);
      this.lblConnection.TabIndex = 3;
      this.lblConnection.Text = "Connection string";
      // 
      // txtConnection
      // 
      this.txtConnection.Location = new System.Drawing.Point(114, 13);
      this.txtConnection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtConnection.Name = "txtConnection";
      this.txtConnection.Size = new System.Drawing.Size(459, 25);
      this.txtConnection.TabIndex = 2;
      // 
      // btnDump
      // 
      this.btnDump.Location = new System.Drawing.Point(498, 101);
      this.btnDump.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnDump.Name = "btnDump";
      this.btnDump.Size = new System.Drawing.Size(75, 30);
      this.btnDump.TabIndex = 1;
      this.btnDump.Text = "Dump";
      this.btnDump.UseVisualStyleBackColor = true;
      this.btnDump.Click += new System.EventHandler(this.btnDump_Click);
      // 
      // txtEditor
      // 
      this.txtEditor.BackColor = System.Drawing.Color.Black;
      this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtEditor.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.txtEditor.ForeColor = System.Drawing.Color.Lime;
      this.txtEditor.Location = new System.Drawing.Point(2, 155);
      this.txtEditor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtEditor.Multiline = true;
      this.txtEditor.Name = "txtEditor";
      this.txtEditor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtEditor.Size = new System.Drawing.Size(680, 257);
      this.txtEditor.TabIndex = 2;
      // 
      // dlgLoad
      // 
      this.dlgLoad.DefaultExt = "sql";
      this.dlgLoad.Filter = "SQL files|*.sql|All files|*.*";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.pbRecordsetCounter);
      this.panel2.Controls.Add(this.pbRecordCounter);
      this.panel2.Controls.Add(this.btnSave);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point(2, 412);
      this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(680, 46);
      this.panel2.TabIndex = 4;
      // 
      // pbRecordsetCounter
      // 
      this.pbRecordsetCounter.Location = new System.Drawing.Point(7, 27);
      this.pbRecordsetCounter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.pbRecordsetCounter.Maximum = 0;
      this.pbRecordsetCounter.Name = "pbRecordsetCounter";
      this.pbRecordsetCounter.Size = new System.Drawing.Size(490, 12);
      this.pbRecordsetCounter.TabIndex = 10;
      // 
      // pbRecordCounter
      // 
      this.pbRecordCounter.Location = new System.Drawing.Point(7, 8);
      this.pbRecordCounter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.pbRecordCounter.Maximum = 0;
      this.pbRecordCounter.Name = "pbRecordCounter";
      this.pbRecordCounter.Size = new System.Drawing.Size(490, 12);
      this.pbRecordCounter.TabIndex = 9;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(589, 8);
      this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(87, 30);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // dlgSave
      // 
      this.dlgSave.DefaultExt = "sql";
      this.dlgSave.Filter = "SQL Files|*.sql";
      this.dlgSave.RestoreDirectory = true;
      this.dlgSave.SupportMultiDottedExtensions = true;
      this.dlgSave.ValidateNames = false;
      // 
      // frmDataDump
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(684, 461);
      this.Controls.Add(this.txtEditor);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmDataDump";
      this.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Text = "Dump To Excel";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblConnection;
    private System.Windows.Forms.TextBox txtConnection;
    private System.Windows.Forms.Button btnDump;
    private System.Windows.Forms.TextBox txtEditor;
    private System.Windows.Forms.TextBox txtFilePath;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Label lblFilePath;
    private System.Windows.Forms.OpenFileDialog dlgLoad;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.SaveFileDialog dlgSave;
    private System.Windows.Forms.ProgressBar pbRecordsetCounter;
    private System.Windows.Forms.ProgressBar pbRecordCounter;
    private System.Windows.Forms.RadioButton rdCSV;
    private System.Windows.Forms.RadioButton rdExcel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtFileNameFormat;
  }
}

