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
      this.tabCSS = new System.Windows.Forms.TabPage();
      this.txtSourceCSS = new System.Windows.Forms.RichTextBox();
      this.tabHTML = new System.Windows.Forms.TabPage();
      this.browser = new System.Windows.Forms.WebBrowser();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnRun = new System.Windows.Forms.Button();
      this.tabSource.SuspendLayout();
      this.tabXSL.SuspendLayout();
      this.tabXML.SuspendLayout();
      this.tabCSS.SuspendLayout();
      this.tabHTML.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabSource
      // 
      this.tabSource.Controls.Add(this.tabXSL);
      this.tabSource.Controls.Add(this.tabXML);
      this.tabSource.Controls.Add(this.tabCSS);
      this.tabSource.Controls.Add(this.tabHTML);
      this.tabSource.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabSource.Font = new System.Drawing.Font("Consolas", 9.75F);
      this.tabSource.ItemSize = new System.Drawing.Size(75, 25);
      this.tabSource.Location = new System.Drawing.Point(6, 52);
      this.tabSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabSource.Name = "tabSource";
      this.tabSource.SelectedIndex = 0;
      this.tabSource.Size = new System.Drawing.Size(872, 552);
      this.tabSource.TabIndex = 0;
      // 
      // tabXSL
      // 
      this.tabXSL.Controls.Add(this.txtSourceXSL);
      this.tabXSL.ImageKey = "xsl.jpg";
      this.tabXSL.Location = new System.Drawing.Point(4, 29);
      this.tabXSL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXSL.Name = "tabXSL";
      this.tabXSL.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXSL.Size = new System.Drawing.Size(864, 519);
      this.tabXSL.TabIndex = 0;
      this.tabXSL.Text = "XSL";
      this.tabXSL.UseVisualStyleBackColor = true;
      // 
      // txtSourceXSL
      // 
      this.txtSourceXSL.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSourceXSL.EnableAutoDragDrop = true;
      this.txtSourceXSL.Location = new System.Drawing.Point(3, 4);
      this.txtSourceXSL.Name = "txtSourceXSL";
      this.txtSourceXSL.Size = new System.Drawing.Size(858, 511);
      this.txtSourceXSL.TabIndex = 0;
      this.txtSourceXSL.Text = resources.GetString("txtSourceXSL.Text");
      // 
      // tabXML
      // 
      this.tabXML.Controls.Add(this.txtSourceXML);
      this.tabXML.ImageKey = "(none)";
      this.tabXML.Location = new System.Drawing.Point(4, 29);
      this.tabXML.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXML.Name = "tabXML";
      this.tabXML.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabXML.Size = new System.Drawing.Size(664, 369);
      this.tabXML.TabIndex = 1;
      this.tabXML.Text = "XML";
      this.tabXML.UseVisualStyleBackColor = true;
      // 
      // txtSourceXML
      // 
      this.txtSourceXML.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSourceXML.Font = new System.Drawing.Font("Consolas", 9.75F);
      this.txtSourceXML.Location = new System.Drawing.Point(3, 4);
      this.txtSourceXML.Name = "txtSourceXML";
      this.txtSourceXML.Size = new System.Drawing.Size(658, 361);
      this.txtSourceXML.TabIndex = 0;
      this.txtSourceXML.Text = resources.GetString("txtSourceXML.Text");
      // 
      // tabCSS
      // 
      this.tabCSS.Controls.Add(this.txtSourceCSS);
      this.tabCSS.ImageKey = "css.jpg";
      this.tabCSS.Location = new System.Drawing.Point(4, 29);
      this.tabCSS.Name = "tabCSS";
      this.tabCSS.Padding = new System.Windows.Forms.Padding(3);
      this.tabCSS.Size = new System.Drawing.Size(664, 369);
      this.tabCSS.TabIndex = 2;
      this.tabCSS.Text = "CSS";
      this.tabCSS.UseVisualStyleBackColor = true;
      // 
      // txtSourceCSS
      // 
      this.txtSourceCSS.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSourceCSS.Location = new System.Drawing.Point(3, 3);
      this.txtSourceCSS.Name = "txtSourceCSS";
      this.txtSourceCSS.Size = new System.Drawing.Size(658, 363);
      this.txtSourceCSS.TabIndex = 0;
      this.txtSourceCSS.Text = "";
      // 
      // tabHTML
      // 
      this.tabHTML.Controls.Add(this.browser);
      this.tabHTML.ImageKey = "html.jpg";
      this.tabHTML.Location = new System.Drawing.Point(4, 29);
      this.tabHTML.Name = "tabHTML";
      this.tabHTML.Padding = new System.Windows.Forms.Padding(3);
      this.tabHTML.Size = new System.Drawing.Size(664, 369);
      this.tabHTML.TabIndex = 3;
      this.tabHTML.Text = "HTML";
      this.tabHTML.UseVisualStyleBackColor = true;
      // 
      // browser
      // 
      this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browser.Location = new System.Drawing.Point(3, 3);
      this.browser.MinimumSize = new System.Drawing.Size(20, 20);
      this.browser.Name = "browser";
      this.browser.Size = new System.Drawing.Size(658, 363);
      this.browser.TabIndex = 8;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnRun);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(6, 7);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(872, 45);
      this.pnlTop.TabIndex = 6;
      // 
      // btnRun
      // 
      this.btnRun.Location = new System.Drawing.Point(13, 10);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(75, 25);
      this.btnRun.TabIndex = 1;
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
      // 
      // frmXsltSandbox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 611);
      this.Controls.Add(this.tabSource);
      this.Controls.Add(this.pnlTop);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmXsltSandbox";
      this.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.Text = "XSLT Sandbox";
      this.tabSource.ResumeLayout(false);
      this.tabXSL.ResumeLayout(false);
      this.tabXML.ResumeLayout(false);
      this.tabCSS.ResumeLayout(false);
      this.tabHTML.ResumeLayout(false);
      this.pnlTop.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabSource;
    private System.Windows.Forms.TabPage tabXSL;
    private System.Windows.Forms.TabPage tabXML;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.RichTextBox txtSourceXSL;
    private System.Windows.Forms.RichTextBox txtSourceXML;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.TabPage tabCSS;
    private System.Windows.Forms.RichTextBox txtSourceCSS;
    private System.Windows.Forms.TabPage tabHTML;
    private System.Windows.Forms.WebBrowser browser;
  }
}