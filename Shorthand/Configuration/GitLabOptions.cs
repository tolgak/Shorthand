using PragmaTouchUtils;
using System;

namespace Shorthand
{
  [ConfigContentItem]
  [Serializable]
  public class GitLabOptions
  {
    public string DefaultGitProjectName { get; set; }
    public string Url { get; set; }
    public string PrivateToken { get; set; }

    public bool IsActive { get { return !string.IsNullOrEmpty(this.PrivateToken) && !string.IsNullOrEmpty(this.Url); } }
  }

}
