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

    [ImportMany(typeof(IPluginMarker))]
    private  List<IPluginMarker> _plugins;

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

        this.setVersionInfo();

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
    }


    private async void initializePluginContainer()
    {
      var pluginFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)  + @"\plugins\";
      if (!Directory.Exists(pluginFilePath))
        return;

      var catalog = new AggregateCatalog();
      catalog.Catalogs.Add(new DirectoryCatalog(pluginFilePath));
      _container = new CompositionContainer(catalog);

      _container.ComposeParts(this);

      foreach (var x in _plugins)
      {
        try
        {
          switch (x)
          {
            case IAsyncPlugin y :
              await this.RegisterAsync(y);
              break;
            case IPlugin y:
              this.Register(y);
              break;
          }

          Application.DoEvents();
        }
        catch (Exception compositionException)
        {
          MessageBox.Show(compositionException.Message);
        }
      }

      //statMsg.Text = "Ready";
    }

    public async Task RegisterAsync(IAsyncPlugin y)
    {
      var context = new PluginContext { Configuration = ConfigContent.Current };
      var p = await y.InitializeAsync(context);
      statMsg.Text = $"Loading plugin - {p.Text}";

      p.MdiParent = this;
      var subItem = new ToolStripMenuItem(p.Text);
      if (p.Icon != null)
        subItem.Image = p.Icon.ToBitmap();

      mnuTools.DropDownItems.Add(subItem);
      subItem.Click += (object sender, EventArgs e) => { p.Show(); };

      this.onSettingsChanged += y.OnSettingsChangedEventHandler;

      statMsg.Text = string.Empty;
    }

    public void Register(IPlugin y)
    {
      var context = new PluginContext { Configuration = ConfigContent.Current };
      var p = y.Initialize(context);
      statMsg.Text = $"Loading plugin - {p.Text}";

      p.MdiParent = this;
      var subItem = new ToolStripMenuItem(p.Text);
      if (p.Icon != null)
        subItem.Image = p.Icon.ToBitmap();

      mnuTools.DropDownItems.Add(subItem);
      subItem.Click += (object sender, EventArgs e) => { p.Show(); };

      this.onSettingsChanged += y.OnSettingsChangedEventHandler;

      statMsg.Text = string.Empty;
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

    private void setVersionInfo()
    {
      var versionInfo = Assembly.GetExecutingAssembly().GetName().Version;
      var startDate = new DateTime(2000, 1, 1);      
      var lastBuilt = startDate.AddDays(versionInfo.Build).ToShortDateString();

      this.Text = $"{this.Text} - Version {versionInfo.ToString()} ({lastBuilt})";
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
