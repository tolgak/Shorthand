using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

using System.ComponentModel.Composition;
using PragmaTouchUtils;
using Shorthand.Common;
using System.Threading.Tasks;
using System.Linq;

namespace Shorthand
{
  [Export(typeof(IPluginMarker))]
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

      var mnuTools = _context.Host.MainMenuStrip.Items.Find("mnuTools", true).FirstOrDefault();
      if (mnuTools == null)
        return;

      var subItem = new ToolStripMenuItem(this.Text);
      if (this.Icon != null)
        subItem.Image = this.Icon.ToBitmap();

      (mnuTools as ToolStripMenuItem).DropDownItems.Add(subItem);
      subItem.Click += (object sender, EventArgs e) => { this.Show(); };

      this.FormClosing += (object sender, FormClosingEventArgs e) => { e.Cancel = true; this.Hide(); };

      this.InitializePlugin();
      this.InitializeUI();
    }

    private void InitializePlugin()
    {

    }

    private void InitializeUI()
    {
      tabSource.SelectedTab = tabXSL;
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

      css = $"<style>{css}</style>";
      return html.Replace("<style />", css);
    }


  }
}
