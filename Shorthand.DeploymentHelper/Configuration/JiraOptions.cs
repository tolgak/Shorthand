using System;
using PragmaTouchUtils;

namespace Shorthand
{
  [ConfigContentItem]
  [Serializable]
  public class JiraOptions
  {
    public string JiraBaseUrl { get; set; }    
    public string Username { get; set; }
    public string Password { get; set; }
    public string REQ_ProjectKey { get; set; }
    public string DPLY_ProjectKey { get; set; }
    public string UAT_ProjectKey{ get; set; }

    public bool IsValid {
      get {
        return !string.IsNullOrEmpty(this.JiraBaseUrl) 
            && !string.IsNullOrEmpty(this.Username) 
            && !string.IsNullOrEmpty(this.Password);
      }
    }

  }
}
