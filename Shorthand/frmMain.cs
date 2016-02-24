using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PragmaTouchUtils;
using System.Reflection;

namespace Shorthand
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      try
      {
        this.Cursor = Cursors.AppStarting;

        this.InitializeComponent();
        //this.InitializeDockPanel();

        //FormFactory.Instance.MdiContainer = this;
        //FormFactory.Instance.ShowDockContent = this.ShowDockContent;

        ConfigContent.ApplicationName = "Dev Shorthand";
        ConfigContent.Current.LoadConfiguration();

        var options = ConfigContent.Current.GetConfigContentItem("GuiOptions") as GuiOptions;
        this.Width = options.Width;
        this.Height = options.Height;

        //_clpHook = new ClipboardHook();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }

    }


 
    private void mnuItemExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void mnuDeploymentHelper_Click(object sender, EventArgs e)
    {
      var frm = new frmDeployment();
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

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
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
      Version versionInfo = Assembly.GetExecutingAssembly().GetName().Version;
      DateTime startDate = new DateTime(2000, 1, 1);
      int diffDays = versionInfo.Build;
      DateTime computedDate = startDate.AddDays(diffDays);
      string lastBuilt = computedDate.ToShortDateString();
      this.Text = string.Format("{0} - Version {1} ({2})", this.Text, versionInfo.ToString(), lastBuilt);
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      this.SetVersionInfo();
    }







  }
}
