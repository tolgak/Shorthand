using System;

using PragmaTouchUtils;

namespace Shorthand.Configuration
{
  public partial class ucGitLabOptions : ucOptionEditorBase, IConfigContentEditor
  {
    private GitLabOptions _options;
    private string _cleanHash;

    public ucGitLabOptions()
    {
      InitializeComponent();

      this.ItemClassName = "GitLabOptions";
      this.Caption = "Git Lab";
    }

    public override bool Modified { get { return string.IsNullOrEmpty(_cleanHash) ? false : _cleanHash != _options.GetMd5Hash(); } }

    protected override void LoadInitial()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as GitLabOptions;
      if (_options == null)
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      _cleanHash = _options.GetMd5Hash();

      txtDefaultGitProjectName.DataBindTo(_options, "DefaultGitProjectName");
      txtUrl.DataBindTo(_options, "Url");
      txtPrivateToken.DataBindTo(_options, "PrivateToken");
    }


  }
}
