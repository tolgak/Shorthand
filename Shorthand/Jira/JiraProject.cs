using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{


    public class AvatarUrls
    {
      public string large_48x48 { get; set; }
      public string small_24x24 { get; set; }
      public string tiny_16x16 { get; set; }
      public string medium_32x32 { get; set; }
    }

    public class ProjectCategory
    {
      public string self { get; set; }
      public string id { get; set; }
      public string name { get; set; }
      public string description { get; set; }
    }

    public class JiraProject
  {
      public string self { get; set; }
      public string id { get; set; }
      public string key { get; set; }
      public string name { get; set; }
      public AvatarUrls avatarUrls { get; set; }
      public ProjectCategory projectCategory { get; set; }
    }





  public class IssueOverview
  {

    public string id { get; set; }
    public string key { get; set; }
    public string reporter { get; set; }
    public string assignee { get; set; }
    public string dueDate { get; set; }


  }



}
