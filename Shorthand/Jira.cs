using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PragmaTouchUtils;
using System.Text.RegularExpressions;


namespace Shorthand
{

  //public class JiraResource
  //{
  //  public const string Issue = "{0}/rest/api/2/issue";
  //  public const string AssigneeOfIssue = "{0}/rest/api/2/issue/{1}/assignee";
  //}


  public class ApiMethod
  {
    public const string POST = "POST";
    public const string GET  = "GET";
    public const string PUT  = "PUT";
    public const string DELETE = "DELETE";
  }


  public class Jira
  {
    
    private Action<string> _logger;
    private JiraOptions _options;

    public Jira()
    {
      _options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
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
      var url  = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/comment";
      var data = new { body = comment };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);
      this.Log("Comment added to issue");
    }

    public void AssignIssueTo(string issueKey, string assignee)
    {      
      var url  = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/assignee";
      var data = new { name = assignee };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.PUT);
    }

    public string CreateIssue(string projectKey, string summary, string description, string issueType, string assignee = "")
    {      
      if (_options == null)
        throw new ArgumentNullException("JiraOptions", "No config content for Jira Options");
      if (string.IsNullOrEmpty(_options.JiraBaseUrl))
        throw new ArgumentNullException("JiraBaseUrl", "Jira Base url is not configured");

      // create new
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue";
      var data = new { fields = new { project = new { key = projectKey}
                                    , summary = summary
                                    , description = description
                                    , issuetype = new {name = issueType} } };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);

      // update assignee
      if (!response.Success)
        return string.Empty;

      var responseObj = new {id="", key="", self=""};
      var issueKey = JsonConvert.DeserializeAnonymousType(response.Result, responseObj).key ;
      this.AssignIssueTo(issueKey, string.IsNullOrEmpty(assignee) ? _options.Username : assignee);

      return issueKey;
    }

    public void SetDescription(string issueKey, string description)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      var data = new{ update = new{ description = new [] { new { set = description} } } };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.PUT);
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
        this.Log($"{issueKey} : found issue");

      var regex = new Regex("\"issuelinks\":\\[(?<Links>.*?)\\]", RegexOptions.Multiline| RegexOptions.CultureInvariant);
      var m = regex.Match(response.Result);
      if (!m.Success)
        return new Issuelink[0]{};

      var issueLinks = "[" + m.Groups["Links"].Value + "]";
      var issueObjects = JsonConvert.DeserializeObject<Issuelink[]>(issueLinks);
      this.Log($"{issueKey} : links queried, has {issueObjects.Count()}");

      return issueObjects;
    }

    // https://developer.atlassian.com/jiradev/jira-platform/guides/other/guide-jira-remote-issue-links/jira-rest-api-for-remote-issue-links
    public void CreateLink(string relationship, string inwardIssue, string outwardIssue )
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issueLink";
      var data = new { type = new { name = relationship }, inwardIssue = new { key = inwardIssue }, outwardIssue = new { key = outwardIssue }, comment = new { body = "UAT created" } };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);

      if (response.Success)
        this.Log(string.Format("RESULT: {0}", response.Result));
      else
        this.Log(string.Format("WEBEXCEPTION: {0}", response.Description));
      
      //var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      //var url = string.Format("{0}/rest/api/2/issue/{1}", options.JiraBaseUrl, inwardIssue);
      //var data = new { update = new { issueLinks = new[] { new { add = new { type = new { name = relationship }, inwardIssue = new { key = inwardIssue }, outwardIssue = new { key = outwardIssue }, comment = new { body = "UAT created" } } } } } };
      //var response = this.PostToJira(url, data.AsJson(), ApiMethod.PUT);    
    }

    public IssueTransition[] GetTransitionsForIssue(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/transitions";
      var response = this.SendApiRequest(url, null, ApiMethod.GET);
      
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
      var url = $"{ _options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/transitions";
      var data = new { transition = new { id = transitionId } };

      return this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);
    }

    private JsonResponse json_GetIssue(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      return this.SendApiRequest(url, null, ApiMethod.GET);
    }



    private JsonResponse SendApiRequest(string url, string data, string method)
    {
      var request = WebRequest.Create(url) as HttpWebRequest;
      request.ContentType = "application/json";
      request.Method = method;

      if ( data != null )
        using ( var writer = new StreamWriter(request.GetRequestStream()) )        
          writer.Write(data);
        
      var base64Credentials = this.EncodeCredentials(_options.Username, _options.Password);
      request.Headers.Add("Authorization", "Basic " + base64Credentials);
      
      try
      {
        var response = request.GetResponse() as HttpWebResponse;
        using ( var reader = new StreamReader(response.GetResponseStream()) )
        {          
          return new JsonResponse {Success = true, Result = reader.ReadToEnd(), StatusCode = response.StatusCode, Description = response.StatusDescription};
        }
      }
      catch (WebException ex)
      {
        var r = ex.Response as HttpWebResponse;                      
        return new JsonResponse { Success = false, Result = string.Empty, StatusCode = r.StatusCode, Description = r.StatusDescription };
      }
    }

    private string EncodeCredentials(string userName, string password)
    {
      string mergedCredentials = $"{userName}:{password}";
      byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
      return Convert.ToBase64String(byteCredentials);
    }

    private void Log(string line)
    {
      _logger?.Invoke($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {line}\n".Replace("\n", Environment.NewLine));
    }

  }


}
