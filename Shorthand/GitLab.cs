using System;
using PragmaTouchUtils;
using System.Net;
using System.IO;

namespace Shorthand
{

  // http://sisgit.bilgi.networks/help/api/merge_requests.md
  // Create MR : POST to http://sisgit.bilgi.networks/api/v3/projects/53/merge_requests
  // include in header PRIVATE-TOKEN : Xwzn7JSDYgayTJrspdPF
  public class GitLab
  {
    private Action<string> _logger;
    private GitLabOptions _options;

    public GitLab()
    {
      _options = ConfigContent.Current.GetConfigContentItem("GitLabOptions") as GitLabOptions;
    }

    public GitLab(Action<string> logger) : this()
    {
      _logger = logger;
    }


    public void CreateMergeRequest(int projectId )
    {
      //int projectId = 53;
      var url = $"{_options.Url}/api/v3/projects/{projectId}/merge_requests";
      var data = new { };
      this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);

    }

    private JsonResponse SendApiRequest(string url, string data, string method)
    {
      var request = WebRequest.Create(url) as HttpWebRequest;
      request.ContentType = "application/json";
      request.Method = method;

      if (data != null)
        using (var writer = new StreamWriter(request.GetRequestStream()))
          writer.Write(data);
      
      request.Headers.Add("PRIVATE-TOKEN", _options.PrivateToken);

      try
      {
        var response = request.GetResponse() as HttpWebResponse;
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          return new JsonResponse { Success = true, Result = reader.ReadToEnd(), StatusCode = response.StatusCode, Description = response.StatusDescription };
        }
      }
      catch (WebException ex)
      {
        var r = ex.Response as HttpWebResponse;
        return new JsonResponse { Success = false, Result = string.Empty, StatusCode = r.StatusCode, Description = r.StatusDescription };
      }
    }



  }



}
