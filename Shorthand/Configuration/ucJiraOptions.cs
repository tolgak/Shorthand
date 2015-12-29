using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PragmaTouchUtils;


namespace Shorthand
{
  public partial class ucJiraOptions : ucOptionEditorBase, IConfigContentEditor
  {

    public ucJiraOptions()
    {
      InitializeComponent();

      this.ItemClassName = "JiraOptions";
      this.Caption = "JIRA";
    }

    protected override void LoadInitial()
    {
      var options = _currentConfig.GetConfigContentItem(this.ItemClassName) as JiraOptions;
      if ( options == null )
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      txtJiraBaseUrl.DataBindTo(options, "JiraBaseUrl", this.ControlValueChanged);
      txtUsername.DataBindTo(options, "Username", this.ControlValueChanged);
      txtPassword.DataBindTo(options, "Password", this.ControlValueChanged);

      txtREQ_ProjectKey.DataBindTo(options, "REQ_ProjectKey", this.ControlValueChanged);
      txtDPLY_ProjectKey.DataBindTo(options, "DPLY_ProjectKey", this.ControlValueChanged);
      txtUAT_ProjectKey.DataBindTo(options, "UAT_ProjectKey", this.ControlValueChanged);

    }

    private void RequireAuthentication(bool enabled)
    {
      txtUsername.Enabled = enabled;
      txtPassword.Enabled = enabled;
    }

    private void ControlValueChanged(object sender, EventArgs e)
    {
      this.Modified = true;
    }



  }
}
