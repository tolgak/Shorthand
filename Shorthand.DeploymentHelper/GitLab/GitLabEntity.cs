﻿using Newtonsoft.Json;
using PragmaTouchUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shorthand.GitLabEntity
{

  // http://sisgit.bilgi.networks/help/api/merge_requests.md
  public class GitLab
  {
    private Action<string> _logger;
    private GitLabOptions _options;
    public GitLabOptions Options => _options;

    public GitLab()
    {
      _options = ConfigContent.Current.GetConfigContentItem("GitLabOptions") as GitLabOptions;
    }

    public GitLab(Action<string> logger) : this()
    {
      _logger = logger;
    }


    public CodebaseConfig GetCodebaseConfig(int configSnippetId)
    {
      var json = this.GetSnippet(configSnippetId);
      return CodebaseConfig.FromJson(json);
    }

    public string GetSnippet(int snippetId)
    {
      var url = $"{_options.Url}/snippets/{snippetId}/raw";
      var jsonResponse = this.SendApiRequest(url, null, ApiMethod.GET);

      return jsonResponse.Result;
    }


    public async Task<CodebaseConfig> GetCodebaseConfigAsync(int configSnippetId)
    {
      var url = $"{_options.Url}/snippets/{configSnippetId}/raw";
      var jsonResponse = await this.SendApiRequestAsync(url, null, ApiMethod.GET);

      return CodebaseConfig.FromJson(jsonResponse.Result);
    }


    public List<Project> GetProjects(bool onlyStarred = false)
    {
      var url = $"{_options.Url}/api/v3/projects";
      if (onlyStarred)
        url += "/starred";

      var jsonResponse = this.SendApiRequest(url, null, ApiMethod.GET);
      return JsonConvert.DeserializeObject<List<Project>>(jsonResponse.Result);
    }

    public async Task<List<Project>> GetProjectsAsync(bool onlyStarred = false)
    {
      var url = $"{_options.Url}/api/v3/projects";
      if (onlyStarred)
        url += "/starred";

      var jsonResponse = await this.SendApiRequestAsync(url, null, ApiMethod.GET);
      return JsonConvert.DeserializeObject<List<Project>>(jsonResponse.Result);
    }



    public Project GetProjectById(int projectId)
    {
      var url = $"{_options.Url}/api/v3/projects/{projectId}";
      var jsonResponse = this.SendApiRequest(url, null, ApiMethod.GET);

      return JsonConvert.DeserializeObject<Project>(jsonResponse.Result);
    }

    public async Task<Project> GetProjectByIdAsync(int projectId)
    {
      var url = $"{_options.Url}/api/v3/projects/{projectId}";
      var jsonResponse = await this.SendApiRequestAsync(url, null, ApiMethod.GET);

      return JsonConvert.DeserializeObject<Project>(jsonResponse.Result);
    }



    public List<MergeRequestResponse> GetMergeRequests(int projectId)
    {
      var url = $"{_options.Url}/api/v3/projects/{projectId}/merge_requests";
      var jsonResponse = this.SendApiRequest(url, null, ApiMethod.GET);

      return JsonConvert.DeserializeObject<List<MergeRequestResponse>>(jsonResponse.Result);
    }

    public async Task<List<MergeRequestResponse>> GetMergeRequestsAsync(int projectId)
    {
      var url = $"{_options.Url}/api/v3/projects/{projectId}/merge_requests";
      var jsonResponse = await this.SendApiRequestAsync(url, null, ApiMethod.GET);

      return JsonConvert.DeserializeObject<List<MergeRequestResponse>>(jsonResponse.Result);
    }



    public MergeRequestResponse GetMergeRequestByInternalIssueKey(int projectId, string internalIssueKey)
    {
      var mergeRequests = this.GetMergeRequests(projectId);
      return mergeRequests.FirstOrDefault(x => x.source_branch.Contains(internalIssueKey));
    }

    public async Task<MergeRequestResponse> GetMergeRequestByInternalIssueKeyAsync(int projectId, string internalIssueKey)
    {
      var mergeRequests = await this.GetMergeRequestsAsync(projectId);
      return mergeRequests.FirstOrDefault(x => x.source_branch.Contains(internalIssueKey));
    }



    public int GetMergeRequestById(int projectId, int mergeReqId)
    {
      var url = $"{_options.Url}/api/v3/projects/{projectId}/merge_request/{mergeReqId}";
      var data = new { id = projectId, merge_request_id = mergeReqId };
      var jsonResponse = this.SendApiRequest(url, data.AsJson(), ApiMethod.GET);

      var mrResponse = JsonConvert.DeserializeObject<MergeRequestResponse>(jsonResponse.Result);
      return mrResponse.id;
    }






    public int CreateMergeRequest(int projectId, string sourceBranch, string targetBranch, string title, string description = "", string assigneeId = "")
    {
      var url = $"{_options.Url}/api/v3/projects/{projectId}/merge_requests";
      var data = new { id = projectId, source_branch = sourceBranch, target_branch = targetBranch, title = title, description = description, assignee_id = assigneeId };
      var jsonResponse = this.SendApiRequest(url, data.AsJson(), ApiMethod.POST);

      var mrResponse = JsonConvert.DeserializeObject<MergeRequestResponse>(jsonResponse.Result);
      return mrResponse.iid;
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
      var request = WebRequest.Create(url) as HttpWebRequest;
      request.ContentType = "application/json";
      request.Method = method;

      if (data != null)
        using (var writer = new StreamWriter(request.GetRequestStream()))
          writer.Write(data);

      request.Headers.Add("PRIVATE-TOKEN", _options.PrivateToken);

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
        if (response != null)
          response.Close();
      }

    }


  }



}
