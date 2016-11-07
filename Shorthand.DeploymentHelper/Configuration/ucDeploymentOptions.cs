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

    private DeploymentOptions _options;
    private string _cleanHash;

    public ucDeploymentOptions()
    {
      InitializeComponent();

      this.ItemClassName = "DeploymentOptions";
      this.Caption = "Deployment";
    }

    public override bool Modified { get { return string.IsNullOrEmpty(_cleanHash) ? false : _cleanHash != _options.GetMd5Hash();} }

    protected override void LoadInitial()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as DeploymentOptions;
      if ( _options == null )
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      _cleanHash = _options.GetMd5Hash();

      txtLocalBinPath.DataBindTo(_options, "LocalBinPath");      
      txtArchiveToolPath.DataBindTo(_options, "ArchiveToolPath");
      txtArchiveToolSwitches.DataBindTo(_options, "ArchiveToolSwitches");
      txtTestDeliveryFolder.DataBindTo(_options, "TestDeliveryFolder");
      txtProductionDeliveryFolder.DataBindTo(_options, "ProductionDeliveryFolder");
    }



  }
}
