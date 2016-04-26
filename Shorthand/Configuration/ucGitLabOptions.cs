using System;

using PragmaTouchUtils;

namespace Shorthand.Configuration
{
  public partial class ucGitLabOptions : ucOptionEditorBase, IConfigContentEditor
  {
    public ucGitLabOptions()
    {
      InitializeComponent();

      this.ItemClassName = "GitLabOptions";
      this.Caption = "Git Lab";
    }

    protected override void LoadInitial()
    {
      var options = _currentConfig.GetConfigContentItem(this.ItemClassName) as GitLabOptions;
      if (options == null)
        options = new GitLabOptions();
      //throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      txtDefaultGitProjectName.DataBindTo(options, "DefaultGitProjectName", this.ControlValueChanged);
      txtUrl.DataBindTo(options, "Url", this.ControlValueChanged);
      txtPrivateToken.DataBindTo(options, "PrivateToken", this.ControlValueChanged);
    }

    private void ControlValueChanged(object sender, EventArgs e)
    {
      this.Modified = true;
    }

  }
}
