using System;

using PragmaTouchUtils;

namespace Shorthand.Configuration
{
  public partial class ucGitLabOptions : ucOptionEditorBase, IConfigContentEditor
  {
    private GitLabOptions _options;

    public ucGitLabOptions()
    {
      InitializeComponent();

      this.ItemClassName = "GitLabOptions";
      this.Caption = "Git Lab";
    }
    
    protected override object LoadUnderlyingOption()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as GitLabOptions;
      if (_options == null)
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      //_cleanHash = _options.GetMd5Hash();

      txtDefaultGitProjectName.DataBindTo(_options, "DefaultGitProjectName");
      txtUrl.DataBindTo(_options, "Url");
      txtPrivateToken.DataBindTo(_options, "PrivateToken");
      txtConfigSnippetId.DataBindTo(_options, "ConfigSnippetId");

      return _options;
    }


  }
}
