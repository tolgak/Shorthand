using PragmaTouchUtils;
using System;

namespace Shorthand
{
  public partial class ucDeploymentOptions : ucOptionEditorBase, IConfigContentEditor
  {
    private DeploymentOptions _options;

    public ucDeploymentOptions()
    {
      InitializeComponent();

      this.ItemClassName = "DeploymentOptions";
      this.Caption = "Deployment";
    }

    protected override object LoadUnderlyingOption()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as DeploymentOptions;
      if ( _options == null )
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      txtLocalBinPath.DataBindTo(_options, "LocalBinPath");      
      txtArchiveToolPath.DataBindTo(_options, "ArchiveToolPath");
      txtArchiveToolSwitches.DataBindTo(_options, "ArchiveToolSwitches");
      txtTestDeliveryFolder.DataBindTo(_options, "TestDeliveryFolder");
      txtProductionDeliveryFolder.DataBindTo(_options, "ProductionDeliveryFolder");

      return _options;
    }



  }
}
