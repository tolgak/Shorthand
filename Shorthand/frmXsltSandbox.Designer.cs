namespace Shorthand
{
  partial class frmXsltSandbox
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXsltSandbox));
      this.tabSource = new System.Windows.Forms.TabControl();
      this.tabXSL = new System.Windows.Forms.TabPage();
      this.txtSourceXSL = new System.Windows.Forms.RichTextBox();
      this.tabXML = new System.Windows.Forms.TabPage();
      this.txtSourceXML = new System.Windows.Forms.RichTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnRun = new System.Windows.Forms.Button();
      this.btnValidate = new System.Windows.Forms.Button();
      this.browser = new System.Windows.Forms.WebBrowser();
      this.tabCSS = new System.Windows.Forms.TabPage();
      this.txtSourceCSS = new System.Windows.Forms.RichTextBox();
      this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
      this.tabSource.SuspendLayout();
      this.tabXSL.SuspendLayout();
      this.tabXML.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabCSS.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
      this.SuspendLayout();
      // 
      // tabSource
      // 
      this.tabSource.Controls.Add(this.tabXSL);
      this.tabSource.Controls.Add(this.tabXML);
      this.tabSource.Controls.Add(this.tabCSS);
      this.tabSource.Dock = System.Windows.Forms.DockStyle.Left;
      this.tabSource.Location = new System.Drawing.Point(6, 52);
      this.tabSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabSource.Name = "tabSource";
      this.tabSource.SelectedIndex = 0;
      this.tabSource.Size = new System.Drawing.Size(380, 544);
      this.tabSource.TabIndex = 0;
      // 
      // tabXSL
      // 
      this.tabXSL.Controls.Add(this.txtSourceXSL);
      this.tabXSL.Location = new System.Drawing.Point(4, 26);
      this.tabXSL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXSL.Name = "tabXSL";
      this.tabXSL.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXSL.Size = new System.Drawing.Size(372, 514);
      this.tabXSL.TabIndex = 0;
      this.tabXSL.Text = "XSL";
      this.tabXSL.UseVisualStyleBackColor = true;
      // 
      // txtSourceXSL
      // 
      this.txtSourceXSL.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSourceXSL.Location = new System.Drawing.Point(3, 4);
      this.txtSourceXSL.Name = "txtSourceXSL";
      this.txtSourceXSL.Size = new System.Drawing.Size(366, 506);
      this.txtSourceXSL.TabIndex = 0;
      this.txtSourceXSL.Text = resources.GetString("txtSourceXSL.Text");
      // 
      // tabXML
      // 
      this.tabXML.Controls.Add(this.txtSourceXML);
      this.tabXML.Location = new System.Drawing.Point(4, 26);
      this.tabXML.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXML.Name = "tabXML";
      this.tabXML.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXML.Size = new System.Drawing.Size(372, 514);
      this.tabXML.TabIndex = 1;
      this.tabXML.Text = "XML";
      this.tabXML.UseVisualStyleBackColor = true;
      // 
      // txtSourceXML
      // 
      this.txtSourceXML.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSourceXML.Location = new System.Drawing.Point(3, 4);
      this.txtSourceXML.Name = "txtSourceXML";
      this.txtSourceXML.Size = new System.Drawing.Size(366, 506);
      this.txtSourceXML.TabIndex = 0;
      this.txtSourceXML.Text = resources.GetString("txtSourceXML.Text");
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnRun);
      this.panel1.Controls.Add(this.btnValidate);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(6, 7);
      this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(786, 45);
      this.panel1.TabIndex = 6;
      // 
      // btnRun
      // 
      this.btnRun.Location = new System.Drawing.Point(120, 11);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(75, 25);
      this.btnRun.TabIndex = 1;
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
      // 
      // btnValidate
      // 
      this.btnValidate.Location = new System.Drawing.Point(39, 11);
      this.btnValidate.Name = "btnValidate";
      this.btnValidate.Size = new System.Drawing.Size(75, 25);
      this.btnValidate.TabIndex = 0;
      this.btnValidate.Text = "Validate";
      this.btnValidate.UseVisualStyleBackColor = true;
      // 
      // browser
      // 
      this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browser.Location = new System.Drawing.Point(386, 52);
      this.browser.MinimumSize = new System.Drawing.Size(20, 20);
      this.browser.Name = "browser";
      this.browser.Size = new System.Drawing.Size(406, 544);
      this.browser.TabIndex = 7;
      // 
      // tabCSS
      // 
      this.tabCSS.Controls.Add(this.txtSourceCSS);
      this.tabCSS.Location = new System.Drawing.Point(4, 26);
      this.tabCSS.Name = "tabCSS";
      this.tabCSS.Padding = new System.Windows.Forms.Padding(3);
      this.tabCSS.Size = new System.Drawing.Size(372, 514);
      this.tabCSS.TabIndex = 2;
      this.tabCSS.Text = "CSS";
      this.tabCSS.UseVisualStyleBackColor = true;
      // 
      // txtSourceCSS
      // 
      this.txtSourceCSS.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSourceCSS.Location = new System.Drawing.Point(3, 3);
      this.txtSourceCSS.Name = "txtSourceCSS";
      this.txtSourceCSS.Size = new System.Drawing.Size(366, 508);
      this.txtSourceCSS.TabIndex = 0;
      this.txtSourceCSS.Text = "";
      // 
      // fileSystemWatcher1
      // 
      this.fileSystemWatcher1.EnableRaisingEvents = true;
      this.fileSystemWatcher1.SynchronizingObject = this;
      // 
      // frmXsltSandbox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(798, 603);
      this.Controls.Add(this.browser);
      this.Controls.Add(this.tabSource);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmXsltSandbox";
      this.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.Text = "XSLT Sandbox";
      this.tabSource.ResumeLayout(false);
      this.tabXSL.ResumeLayout(false);
      this.tabXML.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.tabCSS.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabSource;
    private System.Windows.Forms.TabPage tabXSL;
    private System.Windows.Forms.TabPage tabXML;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.RichTextBox txtSourceXSL;
    private System.Windows.Forms.RichTextBox txtSourceXML;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.Button btnValidate;
    private System.Windows.Forms.WebBrowser browser;
    private System.Windows.Forms.TabPage tabCSS;
    private System.Windows.Forms.RichTextBox txtSourceCSS;
    private System.IO.FileSystemWatcher fileSystemWatcher1;
  }
}