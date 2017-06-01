using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

using System.ComponentModel.Composition;
using PragmaTouchUtils;
using Shorthand.Common;


namespace Shorthand
{
  [Export(typeof(IPlugin))]
  public partial class frmXsltSandbox : Form, IPlugin
  {
    private IPluginContext _context;

    public frmXsltSandbox()
    {
      InitializeComponent();
    }


    public void Initialize(IPluginContext context)
    {
      _context = context;
      this.MdiParent = _context.Host;      

      var strips = _context.Host.MainMenuStrip.Items.Find("mnuTools", true);
      if (strips.Length == 0)
        return;

      var subItem = new ToolStripMenuItem(this.Text);
      if (this.Icon != null)
        subItem.Image = this.Icon.ToBitmap();
      (strips[0] as ToolStripMenuItem).DropDownItems.Add(subItem);
      subItem.Click += (object sender, EventArgs e) => { this.Show(); };

      this.FormClosing += (object sender, FormClosingEventArgs e) =>
      {
        e.Cancel = true;
        this.Hide();
      };

      this.InitializePlugin();
      this.InitializeUI();
    }



    private void InitializeUI()
    {
      tabSource.SelectedTab = tabXSL;
    }

    private void InitializePlugin()
    {

    }
    

    private void btnRun_Click(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        try
        {
          var html = this.RunTransform(txtSourceXML.Text, txtSourceXSL.Text, txtSourceCSS.Text);
          browser.Navigate("about:blank");
          if ( browser.Document != null )
            browser.Document.Write(string.Empty);

          browser.DocumentText = html;
          tabSource.SelectedTab = tabHTML;
        }
        catch ( Exception exception )
        {
          MessageBox.Show(exception.Message);
        }
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private string RunTransform(string xml, string xsl, string css)
    {
      var html = string.Empty;

      // read xml
      var orgDoc = new XmlDocument();
      orgDoc.LoadXml(xml);

      // read xsl
      var stringReader = new StringReader(xsl);
      using ( var reader = XmlReader.Create(stringReader) )
      {
        var trans = new XslCompiledTransform();
        trans.Load(reader);

        // MUST SELECT THE ROOT NODE
        XmlNode transNode = orgDoc.SelectSingleNode("/");
        var sb = new StringBuilder();
        var writer = XmlWriter.Create(sb);
        trans.Transform(transNode, writer);
        html = sb.ToString();
      }

      css = string.Format("<style>{0}</style>", css);
      return html.Replace("<style />", css);
    }


  }
}
