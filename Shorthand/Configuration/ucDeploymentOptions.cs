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
      txtDefaultGitProjectName.DataBindTo(options, "DefaultGitProjectName", this.ControlValueChanged);
    }

    private void ControlValueChanged(object sender, EventArgs e)
    {
      this.Modified = true;
    }

    private void btnDocOutput_Click(object sender, EventArgs e)
    {
      //DialogResult dr = dlgFolder.ShowDialog();
      //if ( dr == System.Windows.Forms.DialogResult.OK )
      //  txtDocumentOutputPath.Text = dlgFolder.SelectedPath.Replace(_appPath, "." + Path.DirectorySeparatorChar);
    }

    private void btnTemplate1_Click(object sender, EventArgs e)
    {
      //string selectedDirectory = Path.GetDirectoryName(txtTemplateFileName.Text);
      //dlgFile.InitialDirectory = Path.GetFullPath(selectedDirectory);
      //dlgFile.FileName = Path.GetFileName(txtTemplateFileName.Text);

      //DialogResult dr = dlgFile.ShowDialog();
      //if ( dr == System.Windows.Forms.DialogResult.OK )
      //  txtTemplateFileName.Text = dlgFile.FileName.Replace(_appPath, "." + Path.DirectorySeparatorChar);
    }







  }
}
