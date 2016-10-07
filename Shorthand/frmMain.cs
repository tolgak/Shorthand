using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel.Composition.Hosting;
using System.Windows.Forms;
using PragmaTouchUtils;
using System.Reflection;
using System.ComponentModel.Composition;
using System.IO;
using System.Collections;
using Shorthand.Common;
using System.Collections.Generic;
using System.Linq;

namespace Shorthand
{
  
  
  public partial class frmMain : Form
  {

    private CompositionContainer _container;

    [ImportMany(typeof(IPlugin))]
    private  List<IPlugin> _plugins;

    public frmMain()
    {
      try
      {
        this.Cursor = Cursors.AppStarting;

        this.InitializeComponent();

        this.SetVersionInfo();
        this.InitializeConfiguration();
        this.InitializePluginContainer();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }

    }

    private void InitializePluginContainer()
    {
      var pluginFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)  + @"\plugins\";
      if (!Directory.Exists(pluginFilePath))
        return;

      var catalog = new AggregateCatalog();
      catalog.Catalogs.Add(new DirectoryCatalog(pluginFilePath));
      _container = new CompositionContainer(catalog);

      var context = new PluginContext { Host = this, Configuration = ConfigContent.Current };
      try
      {
        _container.ComposeParts(this);
        _plugins.ForEach(x => x.PerformAction(context));
      }
      catch (Exception compositionException)
      {
        MessageBox.Show(compositionException.Message);        
      }
    }

    private void InitializeConfiguration()
    {
      ConfigContent.ApplicationName = "Dev Shorthand";
      ConfigContent.Current.LoadConfiguration();

      var options = ConfigContent.Current.GetConfigContentItem("GuiOptions") as GuiOptions;
      this.Width = options.Width;
      this.Height = options.Height;
    }


    private void mnuItemExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }


    private void mnuFlywayHelper_Click(object sender, EventArgs e)
    {
      var frm = new frmFlywayHelper();
      frm.MdiParent = this;
      frm.Show();
    }

    private void mnuXsltSandbox_Click(object sender, EventArgs e)
    {
      var frm = new frmXsltSandbox();
      frm.MdiParent = this;
      frm.Show();
    }

    private void mnuDataDump_Click(object sender, EventArgs e)
    {
      var frm = new frmDataDump();
      frm.MdiParent = this;
      frm.Show();
    }

    private void mnuSettings_Click(object sender, EventArgs e)
    {
      frmConfigurationDlg.ShowConfigurationDlg(ConfigContent.Current, this, this.OnFinalSelection);
    }

    public void OnFinalSelection(object sender, ConfigEventArgs args)
    {
      if ( args.ChangedOptions.Contains("GuiOptions") )
      {
        this.Width  = (args.content.GetConfigContentItem("GuiOptions") as GuiOptions).Width;
        this.Height = (args.content.GetConfigContentItem("GuiOptions") as GuiOptions).Height;
      }
    }

    private void SetVersionInfo()
    {
      var versionInfo = Assembly.GetExecutingAssembly().GetName().Version;
      var startDate = new DateTime(2000, 1, 1);      
      var lastBuilt = startDate.AddDays(versionInfo.Build).ToShortDateString();

      this.Text = $"{this.Text} - Version {versionInfo.ToString()} ({lastBuilt})";
    }

    private void frmMain_Load(object sender, EventArgs e)
    {

    }




  }
}
