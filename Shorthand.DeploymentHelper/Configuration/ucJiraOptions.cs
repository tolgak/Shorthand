using System;

using PragmaTouchUtils;


namespace Shorthand
{
  public partial class ucJiraOptions : ucOptionEditorBase, IConfigContentEditor
  {
    private JiraOptions _options;

    public ucJiraOptions()
    {
      InitializeComponent();

      this.ItemClassName = "JiraOptions";
      this.Caption = "JIRA";
    }

    protected override object LoadUnderlyingOption()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as JiraOptions;
      if ( _options == null )
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      txtJiraBaseUrl.DataBindTo(_options, "JiraBaseUrl");
      txtUsername.DataBindTo(_options, "Username");
      txtPassword.DataBindTo(_options, "Password");

      txtREQ_ProjectKey.DataBindTo(_options, "REQ_ProjectKey");
      txtDPLY_ProjectKey.DataBindTo(_options, "DPLY_ProjectKey");
      txtUAT_ProjectKey.DataBindTo(_options, "UAT_ProjectKey");

      return _options;
    }

  }

}
