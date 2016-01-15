using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PragmaTouchUtils;

namespace Shorthand
{

  public partial class ucGuiOptions : ucOptionEditorBase, IConfigContentEditor
  {
    public ucGuiOptions()
    {
      InitializeComponent();

      this.ItemClassName = "GuiOptions";
      this.Caption = "GUI";
    }

    protected override void LoadInitial()
    {
      var options = _currentConfig.GetConfigContentItem(this.ItemClassName) as GuiOptions;
      if (options == null)
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));




      txtWidth.DataBindTo(options, "Width", this.ControlValueChanged);
      txtHeight.DataBindTo(options, "Height", this.ControlValueChanged);
      //txtPassword.DataBindTo(options, "Password", this.ControlValueChanged);

      //txtREQ_ProjectKey.DataBindTo(options, "REQ_ProjectKey", this.ControlValueChanged);
      //txtDPLY_ProjectKey.DataBindTo(options, "DPLY_ProjectKey", this.ControlValueChanged);
      //txtUAT_ProjectKey.DataBindTo(options, "UAT_ProjectKey", this.ControlValueChanged);

    }

    private void ControlValueChanged(object sender, EventArgs e)
    {
      this.Modified = true;
    }



  }

}
