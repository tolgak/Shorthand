using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace Shorthand
{
    public class Jenkins
    {
        private Action<string> _logger;
        private JenkinsOptions _options;

        public Jenkins()
        {
            //_options = ConfigContent.Current.GetConfigContentItem("JenkinsOptions") as JenkinsOptions;
            _options = new JenkinsOptions();
            _options.JenkinsBaseUrl = "http://testbuild.bilgi.edu.tr:8080";
            _options.Username = "tolgak";
            _options.Password = "Sakura3x";

            // http://testbuild.bilgi.edu.tr:8080/user/tolgak/my-views/view/All/api/json
        }

        public Jenkins(Action<string> logger) : this()
        {
            _logger = logger;
        }




        public async Task<List<JenkinsJob>> GetJobsAsync()
        {
            var response = await this.json_GetJobsAsync();

            var regex = new Regex("\"jobs\":\\[(?<Jobs>.*?)\\]", RegexOptions.Multiline | RegexOptions.CultureInvariant);
            var m = regex.Match(response.Result);
            if (!m.Success)
                return null;

            var jobs = $"[{m.Groups["Jobs"].Value}]";
            var jobObjects = JsonConvert.DeserializeObject<JenkinsJob[]>(jobs);

            return jobObjects.Where( x => x.Color != "disabled" ).ToList();
        }


        private async Task<JsonResponse> json_GetJobsAsync()
        {
            var resource = $"user/{_options.Username}/my-views/view/All/api/json";
            return await this.CallRestApiAsync(_options.JenkinsBaseUrl, resource, null, RestMethod.GET);
        }

        private async Task<JsonResponse> CallRestApiAsync(string baseUrl, string resource, string data, RestMethod method)
        {
            if (_options == null)
                throw new ArgumentNullException("JenkinsOptions", "No config content for Jenkins Options");

            if (string.IsNullOrEmpty(_options.JenkinsBaseUrl))
                throw new ArgumentNullException("JenkinsBaseUrl", "Jenkins Base url is not configured");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + _options.EncodedCredentials);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                FormUrlEncodedContent content = null;
                if (!string.IsNullOrEmpty(data))
                {
                    var payload = new KeyValuePair<string, string>[1] { new KeyValuePair<string, string>("", data) };
                    content = new FormUrlEncodedContent(payload);
                }

                HttpResponseMessage response = null;
                switch (method)
                {
                    case RestMethod.POST:
                        response = await client.PostAsync(resource, content);
                        break;
                    case RestMethod.GET:
                        response = await client.GetAsync(resource);
                        break;
                    case RestMethod.PUT:
                        response = await client.PutAsync(resource, content);
                        break;
                    case RestMethod.DELETE:
                        response = await client.DeleteAsync(resource);
                        break;
                    default:
                        break;
                }

                if (response?.IsSuccessStatusCode ?? false)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return new JsonResponse { Success = true, Result = result, StatusCode = response.StatusCode, Description = response.ReasonPhrase };
                }

                var jsonResponse = new JsonResponse { Success = false, Result = string.Empty, Description = response?.ReasonPhrase ?? "Operation failed for some reason." };
                _logger?.Invoke(jsonResponse.Description);

                return jsonResponse;
            }

        }






        private JsonResponse SendApiRequest(string url, string data, string method)
        {
            if (_options == null)
                throw new ArgumentNullException("JenkinsOptions", "No config content for Jenkins Options");

            if (string.IsNullOrEmpty(_options.JenkinsBaseUrl))
                throw new ArgumentNullException("JenkinsOptions", "Jenkins Base url is not configured");

            var request = HttpWebRequest.CreateHttp(url);
            request.ContentType = "application/json";
            request.Method = method;

            if (data != null)
                using (var writer = new StreamWriter(request.GetRequestStream()))
                    writer.Write(data);
            
            request.Headers.Add("Authorization", "Basic " + _options.EncodedCredentials);

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
                var jsonResponse = new JsonResponse { Success = false, Result = string.Empty };
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    {
                        var reader = new StreamReader(errorResponse.GetResponseStream());
                        jsonResponse.StatusCode = errorResponse.StatusCode;
                        jsonResponse.Description = reader.ReadToEnd();
                    }
                }
                else
                {
                    jsonResponse.StatusCode = HttpStatusCode.InternalServerError;
                    jsonResponse.Description = "No response received";
                }

                if (request != null)
                    request.Abort();

                _logger?.Invoke(jsonResponse.Description);
                return jsonResponse;
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
                throw new ArgumentNullException("JenkinsOptions", "No config content for Jenkins Options");

            if (string.IsNullOrEmpty(_options.JenkinsBaseUrl))
                throw new ArgumentNullException("JenkinsBaseUrl", "Jenkins Base url is not configured");

            var request = HttpWebRequest.CreateHttp(url);
            request.ContentType = "application/json";
            request.Method = method;

            if (data != null)
                using (var writer = new StreamWriter(request.GetRequestStream()))
                    writer.Write(data);

            request.Headers.Add("Authorization", "Basic " + _options.EncodedCredentials);

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
                var jsonResponse = new JsonResponse { Success = false, Result = string.Empty };
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    {
                        var reader = new StreamReader(errorResponse.GetResponseStream());
                        jsonResponse.StatusCode = errorResponse.StatusCode;
                        jsonResponse.Description = reader.ReadToEnd();
                    }
                }
                else
                {
                    jsonResponse.StatusCode = HttpStatusCode.InternalServerError;
                    jsonResponse.Description = "No response received";
                }

                if (request != null)
                    request.Abort();

                _logger?.Invoke(jsonResponse.Description);
                return jsonResponse;
            }
            finally
            {
                //if (response != null)
                //  response.Close();
            }

        }

    }


}
