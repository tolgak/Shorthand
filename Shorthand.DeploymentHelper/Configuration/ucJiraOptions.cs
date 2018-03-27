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
    private JiraOptions _options;
    private string _cleanHash;

    public ucJiraOptions()
    {
      InitializeComponent();

      this.ItemClassName = "JiraOptions";
      this.Caption = "JIRA";
    }

    public override bool Modified => string.IsNullOrEmpty(_cleanHash) ? false : _cleanHash != _options.GetMd5Hash();

    protected override void LoadInitial()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as JiraOptions;
      if ( _options == null )
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      _cleanHash = _options.GetMd5Hash();

      txtJiraBaseUrl.DataBindTo(_options, "JiraBaseUrl");
      txtUsername.DataBindTo(_options, "Username");
      txtPassword.DataBindTo(_options, "Password");

      txtREQ_ProjectKey.DataBindTo(_options, "REQ_ProjectKey");
      txtDPLY_ProjectKey.DataBindTo(_options, "DPLY_ProjectKey");
      txtUAT_ProjectKey.DataBindTo(_options, "UAT_ProjectKey");
    }

    private void RequireAuthentication(bool enabled)
    {
      txtUsername.Enabled = enabled;
      txtPassword.Enabled = enabled;
    }

    public override bool SaveContent()
    {
      var result = base.SaveContent();
      _cleanHash = _options.GetMd5Hash();

      return result;
    }

  }
}
