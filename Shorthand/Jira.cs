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
      var url = string.Format("{0}/rest/api/2/issue/{1}/comment", options.JiraBaseUrl, issueKey);
      var json = JsonConvert.SerializeObject( new {body = comment});

      var response = this.PostToJira(url, json, "POST");
    }

    public string CreateIssue(string projectKey, string summary, string description, string issueType, string assignee = "")
    {
      // create new
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      
      var url = string.Format("{0}/rest/api/2/issue", options.JiraBaseUrl);
      var data = new { fields = new { project = new { key = projectKey}
                                    , summary = summary
                                    , description = description
                                    , issuetype = new {name = issueType} }  };
      var json= JsonConvert.SerializeObject(data);
      var response = this.PostToJira(url, json, "POST");

      // update 
      var responseObj = new { id="", key="", self=""};
      var issueKey = JsonConvert.DeserializeAnonymousType(response, responseObj).key ;
      var url2 = string.Format("{0}/rest/api/2/issue/{1}", options.JiraBaseUrl, issueKey);
      var data2 = new { fields = new { assignee = new { name = string.IsNullOrEmpty(assignee) ? options.Username : assignee } } };
      var json2= JsonConvert.SerializeObject(data2);
      var response2 = this.PostToJira(url2, json2, "PUT");

      return issueKey;
    }

    public void CreateLink(string fromIssue, string toIssue, string relationship)
    {
      var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      var url = string.Format("{0}/rest/api/2/issue/{1}/remotelink", options.JiraBaseUrl, fromIssue);

      /*
      { 
          "object": { 
              "url":"http://www.mycompany.com/support?id=1", 
              "title":"Crazy customer support issue"
          } 
      } 
       */


      /*
      {
          "globalId": "system=http://www.mycompany.com/support&id=1",
          "application": {                                            
               "type":"com.acme.tracker",                      
               "name":"My Acme Tracker"
          },
          "relationship":"causes",                           
          "object": {                                            
              "url":"http://www.mycompany.com/support?id=1",     
              "title":"TSTSUP-111",                             
              "summary":"Crazy customer support issue",        
              "icon": {                                         
                  "url16x16":"http://www.openwebgraphics.com/resources/data/3321/16x16_voice-support.png",    
                  "title":"Support Ticket"     
              },
              "status": {                                           
                  "resolved": true,                                          
                  "icon": {                                                       
                      "url16x16":"http://www.openwebgraphics.com/resources/data/47/accept.png",
                      "title":"Case Closed",                                     
                      "link":"http://www.mycompany.com/support?id=1&details=closed"
                  }
              }
          }
      }
      */


    
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
