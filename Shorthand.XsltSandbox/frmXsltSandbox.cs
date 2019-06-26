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
  [Export(typeof(IAsyncPlugin))]
  public partial class frmXsltSandbox : Form, IAsyncPlugin
  {
    private IPluginContext _context;

    public frmXsltSandbox()
    {
      InitializeComponent();
    }

    public async Task<Form> InitializeAsync(IPluginContext context)
    {
      return await Task.Run(async () =>
      {
        _context = context;

        this.FormClosing += (object sender, FormClosingEventArgs e) =>
        {
          e.Cancel = true;
          this.Hide();
        };

        await this.InitializePlugin();
        await this.InitializeUI();

        return this;
      });
    }
    private async Task<bool> InitializePlugin()
    {
      return await Task.Run(() => {
        return true;
      });
    }
    private async Task<bool> InitializeUI()
    {
      return await Task.Run(() =>
      {
        tabSource.SelectedTab = tabXSL;
        return true;
      });
    }
    public async void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    {
      var shouldRefresh = e.ChangedOptions.Contains("FieldSelectOptions");
      if (!shouldRefresh)
        return;

      await this.InitializePlugin();
      await this.InitializeUI();
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
