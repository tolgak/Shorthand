using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
  }
}
