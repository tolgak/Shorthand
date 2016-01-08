using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PragmaTouchUtils;
using System.Text.RegularExpressions;

namespace Shorthand
{
  public class Jira
  {

    private Action<string> _logger;

    public Jira()
    {

    }

    public Jira(Action<string> logger) : this() 
    {
      _logger = logger;
    }

    // https://docs.atlassian.com/jira/REST/latest
    // https://developer.atlassian.com/jiradev/jira-apis/jira-rest-apis/jira-rest-api-tutorials
    // https://developer.atlassian.com/jiradev/jira-apis/jira-rest-apis/jira-rest-api-tutorials/jira-rest-api-example-add-comment
    public void AddCommentToIssue(string issueKey, string comment)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url  = string.Format("{0}/rest/api/2/issue/{1}/comment", options.JiraBaseUrl, issueKey);
      var data = new { body = comment };
      var json = JsonConvert.SerializeObject( data );

      var response = this.PostToJira(url, json, "POST");
      this.Log("Comment added to issue");
    }

    public void AssignIssueTo(string issueKey, string assignee)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url  = string.Format("{0}/rest/api/2/issue/{1}/assignee", options.JiraBaseUrl, issueKey);
      var data = new { name = assignee };
      var json = JsonConvert.SerializeObject( data );
      var response = this.PostToJira(url, json, "PUT");
    }

    public string CreateIssue(string projectKey, string summary, string description, string issueType, string assignee = "")
    {
      // create new
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;      
      var url = string.Format("{0}/rest/api/2/issue", options.JiraBaseUrl);
      var data = new { fields = new { project = new { key = projectKey}
                                    , summary = summary
                                    , description = description
                                    , issuetype = new {name = issueType} } };
      var json= JsonConvert.SerializeObject(data);
      var response = this.PostToJira(url, json, "POST");

      // update assignee
      if (!response.Success)
        return string.Empty;

      var responseObj = new { id="", key="", self=""};
      var issueKey = JsonConvert.DeserializeAnonymousType(response.Result, responseObj).key ;
      this.AssignIssueTo(issueKey, string.IsNullOrEmpty(assignee) ? options.Username : assignee);

      return issueKey;
    }

    public void SetDescription(string issueKey, string description)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url = string.Format("{0}/rest/api/2/issue/{1}", options.JiraBaseUrl, issueKey);
      var data = new{ update = new{ description = new [] { new { set = description} } } };
      var json = JsonConvert.SerializeObject(data);
      var response = this.PostToJira(url, json, "PUT");
    }

    public Issuelink[] GetLinksOfIssue(string issueKey)
    {
      var response = this.json_GetIssue(issueKey);     
      if (!response.Success)
      {
        this.Log(string.Format("{0} : could not find issue", issueKey));
        return new Issuelink[0] { };
      }
      else
        this.Log(string.Format("{0} : found issue", issueKey));

      var regex = new Regex("\"issuelinks\":\\[(?<Links>.*?)\\]", RegexOptions.Multiline| RegexOptions.CultureInvariant);
      var m = regex.Match(response.Result);
      if (!m.Success)
        return new Issuelink[0]{};

      var issueLinks = "[" + m.Groups["Links"].Value + "]";
      var issueObjects = JsonConvert.DeserializeObject<Issuelink[]>(issueLinks);
      this.Log(string.Format("{0} : links queried, has {1}", issueKey, issueObjects.Count()));

      return issueObjects;
    }

    // https://docs.atlassian.com/jira/REST/latest
    // https://developer.atlassian.com/jiradev/jira-platform/guides/other/guide-jira-remote-issue-links/jira-rest-api-for-remote-issue-links
    public void CreateLink(string relationship, string inwardIssue, string outwardIssue )
    {
      //var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      //var url = string.Format("{0}/rest/api/2/issueLink", options.JiraBaseUrl, fromIssue);
      //var data = new { type = new { name = relationship }, inwardIsssue = new { key = fromIssue }, outwardIssue = new { key = toIssue }, comment = new { body=""} };
      //var json = JsonConvert.SerializeObject( data );
      //var response = this.PostToJira(url, json, "POST");

      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url = string.Format("{0}/rest/api/2/issue/{1}", options.JiraBaseUrl, inwardIssue);
      var data = new { update = new { issuelinks = new[] { new { add = new { type = new { name = relationship }, inwardIssue = new { key = outwardIssue } } } } } };
      var json = JsonConvert.SerializeObject(data);
      var response = this.PostToJira(url, json, "PUT");    
    }

    public IssueTransition[] GetTransitionsForIssue(string issueKey)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url = string.Format("{0}/rest/api/2/issue/{1}/transitions", options.JiraBaseUrl, issueKey);
      var response = this.PostToJira(url, null, "GET");
      
      var regex = new Regex("\"transitions\":\\[(?<Transitions>.*?)\\]", RegexOptions.Multiline | RegexOptions.CultureInvariant);
      var m = regex.Match(response.Result);
      if (!m.Success)
        return new IssueTransition[0] { };

      var transitions = "[" + m.Groups["Transitions"].Value + "]";
      var transitionObjects = JsonConvert.DeserializeObject<IssueTransition[]>(transitions);
            
      return transitionObjects;
    }


    public JsonResponse SetTransitionForIssue(string issueKey, string transitionId)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url = string.Format("{0}/rest/api/2/issue/{1}/transitions", options.JiraBaseUrl, issueKey);
      var data = new { transition = new { id = transitionId } };
      var json = JsonConvert.SerializeObject(data);

      return this.PostToJira(url, json, "POST");
    }


    private void Log(string line)
    {
      if (_logger != null)
        _logger(string.Format("{0} {1}\r\n", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), line));

    }

    private JsonResponse json_GetIssue(string issueKey)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url = string.Format("{0}/rest/api/2/issue/{1}", options.JiraBaseUrl, issueKey);
      return this.PostToJira(url, null, "GET");
    }



    private JsonResponse PostToJira(string url, string data, string method)
    {
      var request = WebRequest.Create(url) as HttpWebRequest;
      request.ContentType = "application/json";
      request.Method = method;

      if ( data != null )
        using ( var writer = new StreamWriter(request.GetRequestStream()) )
        {
          writer.Write(data);
        }

      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var base64Credentials = this.GetEncodedCredentials(options.Username, options.Password);
      request.Headers.Add("Authorization", "Basic " + base64Credentials);
      
      try
      {
        var response = request.GetResponse() as HttpWebResponse;
        using ( var reader = new StreamReader(response.GetResponseStream()) )
        {          
          return new JsonResponse { Success = true, Result = reader.ReadToEnd(), StatusCode = response.StatusCode, Description = response.StatusDescription};
        }
      }
      catch (WebException ex)
      {
        var r = ex.Response as HttpWebResponse;        
        return new JsonResponse { Success = false, Result = string.Empty, StatusCode = r.StatusCode, Description = r.StatusDescription };
      }

    }

    private string GetEncodedCredentials(string userName, string password)
    {
      string mergedCredentials = string.Format("{0}:{1}", userName, password);
      byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
      return Convert.ToBase64String(byteCredentials);
    }



  }


}
