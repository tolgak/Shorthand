using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PragmaTouchUtils;
using Shorthand.JiraEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shorthand
{

  //public class JiraResource
  //{
  //  public const string Issue = "{0}/rest/api/2/issue";
  //  public const string AssigneeOfIssue = "{0}/rest/api/2/issue/{1}/assignee";
  //}

  public class Jira
  {

    private Action<string> _logger;
    private JiraOptions _options;
    public JiraOptions Options => _options;

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
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/comment";
      var data = new { body = comment };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);

      _logger?.Invoke($"Comment added to issue {issueKey}");
    }

    public void AssignIssueTo(string issueKey, string assignee)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/assignee";
      var data = new { name = assignee };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.PUT);
    }

    public string CreateIssue(string projectKey, string summary, string description, string issueType, string assignee = "")
    {
      // create new
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue";
      var data = new { fields = new { project = new { key = projectKey }, summary, description, issuetype = new { name = issueType } } };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);

      if (!response.Success)
        return string.Empty;

      var responseObj = new { id = "", key = "", self = "" };
      var issueKey = JsonConvert.DeserializeAnonymousType(response.Result, responseObj).key;
      this.AssignIssueTo(issueKey, string.IsNullOrEmpty(assignee) ? _options.Username : assignee);

      return issueKey;
    }

    public string AddLabelToIssue(string issueKey, string label)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      var data = "{ \"update\": { \"labels\": [ {\"add\" : \"test_Label\"} ] } }";
      var response = this.SendApiRequest(url, data, ApiMethod.PUT);

      if (!response.Success)
        return string.Empty;

      return issueKey;
    }

    public void SetDescription(string issueKey, string description)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      var data = new { update = new { description = new[] { new { set = description } } } };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.PUT);
    }



    public async Task<Issuelink[]> GetLinksOfIssueAsync(string issueKey)
    {
      var response = await this.json_GetIssueAsync(issueKey);

      var regex = new Regex("\"issuelinks\":\\[(?<Links>.*?)\\]", RegexOptions.Multiline | RegexOptions.CultureInvariant);
      var m = regex.Match(response.Result);
      if (!m.Success)
        return new Issuelink[0] { };

      var issueLinks = $"[{m.Groups["Links"].Value}]";
      var issueObjects = JsonConvert.DeserializeObject<Issuelink[]>(issueLinks);

      return issueObjects;
    }


    // https://developer.atlassian.com/jiradev/jira-platform/guides/other/guide-jira-remote-issue-links/jira-rest-api-for-remote-issue-links
    public void CreateLink(string relationship, string inwardIssue, string outwardIssue, string commentBody = "")
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issueLink";
      var data = new { type = new { name = relationship }, inwardIssue = new { key = inwardIssue }, outwardIssue = new { key = outwardIssue }, comment = new { body = commentBody } };
      var response = this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);

      //var options = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
      //var url = string.Format("{0}/rest/api/2/issue/{1}", options.JiraBaseUrl, inwardIssue);
      //var data = new { update = new { issueLinks = new[] { new { add = new { type = new { name = relationship }, inwardIssue = new { key = inwardIssue }, outwardIssue = new { key = outwardIssue }, comment = new { body = "UAT created" } } } } } };
      //var response = this.PostToJira(url, data.AsJson(), ApiMethod.PUT);    
    }

    public IssueTransition[] GetTransitionsForIssue(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/transitions";
      var response = this.SendApiRequest(url, null, ApiMethod.GET);

      JObject rss = JObject.Parse(response.Result);
      var tr = (JArray)rss["transitions"];
      var transitionObjects = JsonConvert.DeserializeObject<IssueTransition[]>(tr.ToString());

      return transitionObjects;
    }

    public JsonResponse SetTransitionForIssue(string issueKey, string transitionId)
    {
      var url = $"{ _options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/transitions";
      var data = new { transition = new { id = transitionId } };

      return this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);
    }











    public async Task<Issue> GetIssueAsync(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      var response = await this.SendApiRequestAsync(url, null, ApiMethod.GET);

      if (string.IsNullOrEmpty(response?.Result))
        return null;

      var settings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore
      };
      return JsonConvert.DeserializeObject<Issue>(response.Result, settings);
    }








    public string GetStatusOfIssue(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      var response = this.SendApiRequest(url, null, ApiMethod.GET);

      JObject rss = JObject.Parse(response.Result);
      return (string)rss["fields"]["status"]["name"];
    }

    public async Task<string> GetStatusOfIssueAsync(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      var response = await this.SendApiRequestAsync(url, null, ApiMethod.GET);

      if (string.IsNullOrEmpty(response?.Result))
        return string.Empty;

      JObject rss = JObject.Parse(response.Result);
      return (string)rss["fields"]["status"]["name"];
    }

    public string[] ListAttachmentsOfIssue(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      var response = this.SendApiRequest(url, null, ApiMethod.GET);

      JObject rss = JObject.Parse(response.Result);
      var attachments = (JArray)rss["fields"]["attachment"];

      return attachments.Select(x => (string)x["filename"]).ToArray();
    }






    public List<Project> GetProjects()
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/project";
      var response = this.SendApiRequest(url, null, ApiMethod.GET);

      return JsonConvert.DeserializeObject<List<Project>>(response.Result);
    }





    private JsonResponse json_GetIssue(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      return this.SendApiRequest(url, null, ApiMethod.GET);
    }

    private async Task<JsonResponse> json_GetIssueAsync(string issueKey)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}";
      return await this.SendApiRequestAsync(url, null, ApiMethod.GET);
    }


    private JsonResponse SendApiRequest(string url, string data, string method)
    {
      if (_options == null)
        throw new ArgumentNullException("JiraOptions", "No config content for Jira Options");
      if (string.IsNullOrEmpty(_options.JiraBaseUrl))
        throw new ArgumentNullException("JiraBaseUrl", "Jira Base url is not configured");

      var request = HttpWebRequest.CreateHttp(url);
      request.ContentType = "application/json";
      request.Method = method;

      if (data != null)
        using (var writer = new StreamWriter(request.GetRequestStream()))
          writer.Write(data);

      var base64Credentials = this.EncodeCredentials(_options.Username, _options.Password);
      request.Headers.Add("Authorization", "Basic " + base64Credentials);

      HttpWebResponse response = null;
      try
      {
        response = request.GetResponse() as HttpWebResponse;
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          return new JsonResponse { Success = true, Result = reader.ReadToEnd(), StatusCode = response.StatusCode, Description = response.StatusDescription };
        }
      }
      catch (WebException ex)
      {
        var jsonRespone = new JsonResponse { Success = false, Result = string.Empty };
        if (ex.Response != null)
        {
          using (var errorResponse = (HttpWebResponse)ex.Response)
          {
            var reader = new StreamReader(errorResponse.GetResponseStream());
            jsonRespone.StatusCode = errorResponse.StatusCode;
            jsonRespone.Description = reader.ReadToEnd();
          }
        }
        else
        {
          jsonRespone.StatusCode = HttpStatusCode.InternalServerError;
          jsonRespone.Description = "No response received";
        }

        if (request != null)
          request.Abort();

        _logger?.Invoke(jsonRespone.Description);
        return jsonRespone;
      }
      finally
      {
        if (response != null)
          response.Close();
      }

    }

    private async Task<JsonResponse> SendApiRequestAsync(string url, string data, string method)
    {
      if (_options == null)
        throw new ArgumentNullException("JiraOptions", "No config content for Jira Options");
      if (string.IsNullOrEmpty(_options.JiraBaseUrl))
        throw new ArgumentNullException("JiraBaseUrl", "Jira Base url is not configured");

      var request = HttpWebRequest.CreateHttp(url);
      request.ContentType = "application/json";
      request.Method = method;

      if (data != null)
        using (var writer = new StreamWriter(request.GetRequestStream()))
          writer.Write(data);

      var base64Credentials = this.EncodeCredentials(_options.Username, _options.Password);
      request.Headers.Add("Authorization", "Basic " + base64Credentials);

      HttpWebResponse response = null;
      try
      {
        response = await request.GetResponseAsync() as HttpWebResponse;
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          return new JsonResponse { Success = true, Result = reader.ReadToEnd(), StatusCode = response.StatusCode, Description = response.StatusDescription };
        }
      }
      catch (WebException ex)
      {
        var jsonRespone = new JsonResponse { Success = false, Result = string.Empty };
        if (ex.Response != null)
        {
          using (var errorResponse = (HttpWebResponse)ex.Response)
          {
            var reader = new StreamReader(errorResponse.GetResponseStream());
            jsonRespone.StatusCode = errorResponse.StatusCode;
            jsonRespone.Description = reader.ReadToEnd();
          }
        }
        else
        {
          jsonRespone.StatusCode = HttpStatusCode.InternalServerError;
          jsonRespone.Description = "No response received";
        }

        if (request != null)
          request.Abort();

        _logger?.Invoke(jsonRespone.Description);
        return jsonRespone;
      }
      finally
      {
        //if (response != null)
        //  response.Close();
      }

    }



    public bool AddAttachment(string issueKey, string filePath)
    {
      var url = $"{_options.JiraBaseUrl}/rest/api/2/issue/{issueKey}/attachments";
      if (!File.Exists(filePath))
      {
        _logger?.Invoke($"File {filePath} does not exist");
        return false;
      }

      var file = new FileInfo(filePath);
      if (file.Length > 10485760) // TODO Get Actual Limit
      {
        _logger?.Invoke("Attachment too large");
        return false;
      }

      return this.PostMultiPart(url, file);
    }

    private Boolean PostMultiPart(string url, FileInfo fileInfo)
    {
      HttpWebResponse response = null;
      HttpWebRequest request = null;

      try
      {
        var boundary = string.Format("----------{0:N}", Guid.NewGuid());
        var content = new MemoryStream();
        var writer = new StreamWriter(content);

        var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
        var data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        writer.WriteLine("--{0}", boundary);
        writer.WriteLine("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"", fileInfo.Name);
        writer.WriteLine("Content-Type: application/octet-stream");
        writer.WriteLine();
        writer.Flush();

        content.Write(data, 0, data.Length);
        writer.WriteLine();
        writer.WriteLine("--" + boundary + "--");
        writer.Flush();
        content.Seek(0, SeekOrigin.Begin);


        request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "POST";
        request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
        request.Accept = "application/json";

        var base64Credentials = this.EncodeCredentials(_options.Username, _options.Password);
        request.Headers.Add("Authorization", "Basic " + base64Credentials);

        request.Headers.Add("X-Atlassian-Token", "nocheck");
        request.ContentLength = content.Length;

        using (var requestStream = request.GetRequestStream())
          content.WriteTo(requestStream);

        using (response = request.GetResponse() as HttpWebResponse)
        {
          if (response.StatusCode != HttpStatusCode.OK)
          {
            var reader = new StreamReader(response.GetResponseStream());
            _logger?.Invoke($"The server returned {response.StatusCode}\n{reader.ReadToEnd()}");
            return false;
          }

          return true;
        }
      }

      catch (WebException wex)
      {
        if (wex.Response != null)
        {
          using (var errorResponse = (HttpWebResponse)wex.Response)
          {
            var reader = new StreamReader(errorResponse.GetResponseStream());
            _logger?.Invoke($"The server returned {errorResponse.StatusCode}\n{reader.ReadToEnd()}).");
          }
        }

        if (request != null)
          request.Abort();

        return false;
      }

    }

    private string EncodeCredentials(string userName, string password)
    {
      string mergedCredentials = $"{userName}:{password}";
      byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
      return Convert.ToBase64String(byteCredentials);
    }


  }


}
