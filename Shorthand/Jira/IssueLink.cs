using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{

  public class Issuelink
  {
    public string id { get; set; }
    public string self { get; set; }
    public Type type { get; set; }
    public Inwardissue inwardIssue { get; set; }
    public Outwardissue outwardIssue { get; set; }
  }

  public class Type
  {
    public string id { get; set; }
    public string name { get; set; }
    public string inward { get; set; }
    public string outward { get; set; }
    public string self { get; set; }
  }

  public class Inwardissue
  {
    public string id { get; set; }
    public string key { get; set; }
    public string self { get; set; }
    public Fields fields { get; set; }
  }

  public class Fields
  {
    public string summary { get; set; }
    public Status status { get; set; }
    public Priority priority { get; set; }
    public Issuetype issuetype { get; set; }
  }

  public class Status
  {
    public string self { get; set; }
    public string description { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public string id { get; set; }
    public Statuscategory statusCategory { get; set; }
  }

  public class Statuscategory
  {
    public string self { get; set; }
    public int id { get; set; }
    public string key { get; set; }
    public string colorName { get; set; }
    public string name { get; set; }
  }

  public class Priority
  {
    public string self { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public string id { get; set; }
  }

  public class Issuetype
  {
    public string self { get; set; }
    public string id { get; set; }
    public string description { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public bool subtask { get; set; }
  }

  public class Outwardissue
  {
    public string id { get; set; }
    public string key { get; set; }
    public string self { get; set; }
    public Fields fields { get; set; }
  }

}
