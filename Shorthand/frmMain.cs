using System;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Collections.Generic;

using PragmaTouchUtils;
using Shorthand.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Shorthand
{
  public partial class frmMain : Form, IPluginHost
  {
    private CompositionContainer _container;

    [ImportMany(typeof(IAsyncPlugin))]
    private  List<IAsyncPlugin> _plugins;

    private Action<object, ConfigEventArgs> _onSettingsChanged;
    public event Action<object, ConfigEventArgs> onSettingsChanged
    {
      add
      {
        _onSettingsChanged += value;
      }
      remove
      {
        _onSettingsChanged -= value;
      }
    }
    
    public frmMain()
    {
      try
      {
        this.Cursor = Cursors.AppStarting;

        this.InitializeComponent();
        this.initializeConfiguration();
        this.initializeUI();
       
        this.initializePluginContainer();
      }
      finally
      {        
        this.Cursor = Cursors.Default;
      }
    }

    private void initializeConfiguration()
    {
      ConfigContent.ApplicationName = "Dev Shorthand";
      ConfigContent.Current.LoadConfiguration();
    }

    private void initializeUI()
    {
      var options = ConfigContent.Current.GetConfigContentItem("GuiOptions") as GuiOptions;
      this.Width  = options.Width;
      this.Height = options.Height;

      var versionInfo = this.getVersionInfo();
      this.Text = $"{this.Text} - {versionInfo}";
    }


    private async void initializePluginContainer()
    {
      var pluginFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)  + @"\plugins\";
      if (!Directory.Exists(pluginFilePath))
        return;

      statMsg.Text = $"Loading plugins";

      var catalog = new AggregateCatalog();
      catalog.Catalogs.Add(new DirectoryCatalog(pluginFilePath));
      _container = new CompositionContainer(catalog);
      _container.ComposeParts(this);
      
      var initializers = _plugins.Select(async x => await this.RegisterAsync(x));
      await Task.WhenAll(initializers);

      statMsg.Text = $"Ready";
    }

    private async Task RegisterAsync(IAsyncPlugin y)
    {
      try
      {
        var context = new PluginContext { Configuration = ConfigContent.Current };
        var p = await y.InitializeAsync(context);

        p.MdiParent = this;
        var subItem = new ToolStripMenuItem(p.Text);
        if (p.Icon != null)
          subItem.Image = p.Icon.ToBitmap();

        mnuTools.DropDownItems.Add(subItem);
        subItem.Click += (object sender, EventArgs e) => { p.Show(); };

        this.onSettingsChanged += y.OnSettingsChangedEventHandler;

        Application.DoEvents();
      }
      catch (Exception compositionException)
      {
        MessageBox.Show(compositionException.Message);
      }
    }








    private void mnuItemExit_Click(object sender, EventArgs e)
    {     
      this.Close();
    }

    private void mnuAbout_Click(object sender, EventArgs e)
    {
      frmAbout.ShowAbout();
    }

    public void onFinalSelection(object sender, ConfigEventArgs e)
    {
      if (e.action == ConfigAction.None || e.action == ConfigAction.Cancel)
        return;

      if ( e.ChangedOptions.Contains("GuiOptions") )
      {
        this.Width  = (e.content.GetConfigContentItem("GuiOptions") as GuiOptions).Width;
        this.Height = (e.content.GetConfigContentItem("GuiOptions") as GuiOptions).Height;
      }

      _onSettingsChanged?.Invoke(this, e);
    }

    private string getVersionInfo()
    {
      var versionInfo = Assembly.GetExecutingAssembly().GetName().Version;
      var startDate = new DateTime(2000, 1, 1);      
      var lastBuilt = startDate.AddDays(versionInfo.Build).ToShortDateString();

      return $"Version {versionInfo.ToString()} ({lastBuilt})";
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {

    }

    private void mnuItemSettings_Click(object sender, EventArgs e)
    {
      frmConfigurationDlg.Show(ConfigContent.Current, this, this.onFinalSelection);
    }


  }
}
