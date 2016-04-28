using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{

  public class IssueTransition
  {
    public string id { get; set; }
    public string name { get; set; }
    public To to { get; set; }
  }

  public class To
  {
    public string self { get; set; }
    public string description { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public string id { get; set; }
    public Statuscategory statusCategory { get; set; }
  }





}
