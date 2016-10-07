using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{

  public class Author
  {
    public int id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public string state { get; set; }
    public string created_at { get; set; }
  }

  public class Assignee
  {
    public int id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public string state { get; set; }
    public string created_at { get; set; }
  }

  public class MergeRequestResponse
  {
    public int id { get; set; }
    public int iid { get; set; }
    public string target_branch { get; set; }
    public string source_branch { get; set; }
    public int project_id { get; set; }
    public string title { get; set; }
    public string state { get; set; }
    public int upvotes { get; set; }
    public int downvotes { get; set; }
    public Author author { get; set; }
    public Assignee assignee { get; set; }
    public string description { get; set; }

  }





  public class Owner
  {
    public int? id { get; set; }
    public string name { get; set; }
    public string created_at { get; set; }
  }

  public class Namespace
  {
    public string created_at { get; set; }
    public string description { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int? owner_id { get; set; }
    public string path { get; set; }
    public string updated_at { get; set; }
  }

  public class Project
  {
    public int id { get; set; }
    public object description { get; set; }
    public string default_branch { get; set; }
    public bool @public { get; set; }
    public int visibility_level { get; set; }
    public string ssh_url_to_repo { get; set; }
    public string http_url_to_repo { get; set; }
    public string web_url { get; set; }
    public Owner owner { get; set; }
    public string name { get; set; }
    public string name_with_namespace { get; set; }
    public string path { get; set; }
    public string path_with_namespace { get; set; }
    public bool issues_enabled { get; set; }
    public bool merge_requests_enabled { get; set; }
    public bool wiki_enabled { get; set; }
    public bool snippets_enabled { get; set; }
    public string created_at { get; set; }
    public string last_activity_at { get; set; }
    public Namespace @namespace { get; set; }
    public bool archived { get; set; }
  }



}
