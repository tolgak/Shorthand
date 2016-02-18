using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace Shorthand
{
  public partial class frmXsltSandbox : Form
  {

    public frmXsltSandbox()
    {
      InitializeComponent();
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
      using ( var stringReader = new StringReader(xsl) )
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
