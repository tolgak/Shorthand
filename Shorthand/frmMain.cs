using System;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Collections.Generic;

using PragmaTouchUtils;
using Shorthand.Common;
using System.Security.Principal;

namespace Shorthand
{
  public partial class frmMain : Form, IPluginHost
  {

    private CompositionContainer _container;

    [ImportMany(typeof(IPlugin))]
    private  List<IPlugin> _plugins;

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

        this.setVersionInfo();
        this.initializeConfiguration();
        this.initializePluginContainer();

        this.initializUI();
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

    private void initializePluginContainer()
    {
      var pluginFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)  + @"\plugins\";
      if (!Directory.Exists(pluginFilePath))
        return;

      var catalog = new AggregateCatalog();
      catalog.Catalogs.Add(new DirectoryCatalog(pluginFilePath));
      _container = new CompositionContainer(catalog);

      var context = new PluginContext { Host = this, Configuration = ConfigContent.Current };
      _container.ComposeParts(this);
      foreach (var x in _plugins)
      {
        try
        {
          x.Initialize(context);
        }
        catch (Exception compositionException)
        {
          MessageBox.Show(compositionException.Message);
        }
      }       

    }

    private void initializUI()
    {
      var options = ConfigContent.Current.GetConfigContentItem("GuiOptions") as GuiOptions;
      this.Width = options.Width;
      this.Height = options.Height;

      var strips = this.MainMenuStrip.Items.Find("mnuTools", true);
      if (strips.Length == 1)
      {        
        (strips[0] as ToolStripMenuItem).DropDownItems.Add(new ToolStripSeparator());
        var subItem = new ToolStripMenuItem("Settings");
        (strips[0] as ToolStripMenuItem).DropDownItems.Add(subItem);
        subItem.Click += (object sender, EventArgs e) => {
          frmConfigurationDlg.ShowConfigurationDlg(ConfigContent.Current, this, this.onFinalSelection);
        };
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



  }
}
