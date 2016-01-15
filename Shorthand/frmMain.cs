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

    private void frmCommentBuilder_Load(object sender, EventArgs e)
    {

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




  }
}
