using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PragmaTouchUtils;

namespace Shorthand
{
  public class Jira
  {
    // https://developer.atlassian.com/jiradev/jira-apis/jira-rest-apis/jira-rest-api-tutorials
    // https://developer.atlassian.com/jiradev/jira-apis/jira-rest-apis/jira-rest-api-tutorials/jira-rest-api-example-add-comment
    public void AddCommentToIssue(string issueKey, string comment)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url  = string.Format("{0}/rest/api/2/issue/{1}/comment", options.JiraBaseUrl, issueKey);
      var data = new { body = comment };
      var json = JsonConvert.SerializeObject( data );

      var response = this.PostToJira(url, json, "POST");
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
      var responseObj = new { id="", key="", self=""};
      var issueKey = JsonConvert.DeserializeAnonymousType(response, responseObj).key ;
      this.AssignIssueTo(issueKey, string.IsNullOrEmpty(assignee) ? options.Username : assignee);

      return issueKey;
    }

    // https://docs.atlassian.com/jira/REST/latest/#d2e4925
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

    private string PostToJira(string url, string data, string method)
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
      var response = request.GetResponse() as HttpWebResponse;
      using ( var reader = new StreamReader(response.GetResponseStream()) )
      {
        return reader.ReadToEnd();
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
