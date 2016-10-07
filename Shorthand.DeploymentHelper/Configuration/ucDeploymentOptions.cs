using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using PragmaTouchUtils;

namespace Shorthand
{
  public partial class ucDeploymentOptions : ucOptionEditorBase, IConfigContentEditor
  {
    public ucDeploymentOptions()
    {
      InitializeComponent();

      this.ItemClassName = "DeploymentOptions";
      this.Caption = "Deployment";
    }

    protected override void LoadInitial()
    {
      var options = _currentConfig.GetConfigContentItem(this.ItemClassName) as DeploymentOptions;
      if ( options == null )
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      txtLocalBinPath.DataBindTo(options, "LocalBinPath", this.ControlValueChanged);      
      txtArchiveToolPath.DataBindTo(options, "ArchiveToolPath", this.ControlValueChanged);
      txtArchiveToolSwitches.DataBindTo(options, "ArchiveToolSwitches", this.ControlValueChanged);
      txtTestDeliveryFolder.DataBindTo(options, "TestDeliveryFolder", this.ControlValueChanged);
      txtProductionDeliveryFolder.DataBindTo(options, "ProductionDeliveryFolder", this.ControlValueChanged);
      
    }

    private void ControlValueChanged(object sender, EventArgs e)
    {
      this.Modified = true;
    }


  }
}
