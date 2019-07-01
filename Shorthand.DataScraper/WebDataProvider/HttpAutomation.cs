using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;


namespace Shorthand.DataScraper.WebDataProvider
{

  public enum HttpRequestMethod
  {
    GET,
    POST,
    PUT,
    DELETE
  }

  public static class HttpAutomation
  {
    public static string BaseUrl { get; set; }
    public static string LoginPageUrl { get; set; }
    public static bool RequireLogin { get; set; }
    public static Dictionary<string, string> LoginPostData { get; set; }

    private static ICookieManager _cm = new CustomCookieManager();


    public static HttpRequestResult HttpLogin()
    {
      if (HttpAutomation.LoginPostData == null)
        throw new ArgumentNullException("HttpAutomation.LoginPostData", "value can not be null");

      if ( string.IsNullOrEmpty(HttpAutomation.LoginPageUrl) )
        throw new ArgumentNullException("HttpAutomation.LoginPageUrl", "value can not be null or empty");

      //Initialise the request
      HttpWebRequest request = CreateWebRequest(HttpAutomation.LoginPageUrl);
      request.AllowAutoRedirect = false;
      request.KeepAlive = false;
      request.ContentType = "application/x-www-form-urlencoded";

      string postData = HttpAutomation.BuildPostData(HttpAutomation.LoginPostData);
      UTF8Encoding encoding = new UTF8Encoding();
      Byte[] data = encoding.GetBytes(postData);
      request.ContentLength = data.Length;

      request.Method = "POST";
      request.CookieContainer = new CookieContainer();

      Stream SendReq = request.GetRequestStream();
      SendReq.Write(data, 0, data.Length);
      SendReq.Close();

      using (var response = (HttpWebResponse)request.GetResponse())
      {
        _cm.StoreCookies(response);

        string responseHtml = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(responseHtml);

        return new HttpRequestResult { Response = responseHtml, ResponseUrl = response.ResponseUri, Document = doc };
      }

    }

    private static string BuildPostData(Dictionary<string, string> pairs)
    {
      var x = (from pd in pairs
               select String.Format("{0}={1}", pd.Key, pd.Value)).ToArray<string>();

      return String.Join("&", x);
    }
    
    private static HttpWebRequest CreateWebRequest(string url)
    {
      Uri uri = new Uri(url);
      HttpWebRequest result = (HttpWebRequest) WebRequest.Create(uri);

      ServicePointManager.Expect100Continue = false;      
      IWebProxy proxy = WebRequest.DefaultWebProxy;
      if (proxy != null)
        result.Proxy = proxy;

      result.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1";
      result.KeepAlive = false;
      return result;
    }

    public static HttpWebRequest BuildRequest(string url)
    {
      HttpWebRequest result = HttpAutomation.CreateWebRequest(url);
      SetHttpHeaders(result);
      result.Method = "GET";

      return result;
    }

    public static HttpWebRequest BuildRequest(string url, Dictionary<string, string> postData)
    {
      HttpWebRequest result = HttpAutomation.BuildRequest(url);
      result.Method = "POST";
      result.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

      string postDataStr = HttpAutomation.BuildPostData(postData);

      //System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
      Byte[] data = encoding.GetBytes(postDataStr);
      result.ContentLength = data.Length;

      // Add the POST data
      Stream SendReq = result.GetRequestStream();
      SendReq.Write(data, 0, data.Length);
      SendReq.Close();

      return result;
    }

    public static HttpRequestResult DoPost(string url, Dictionary<string, string> postData)
    {
      return HttpAutomation.DoPost(url, postData, string.Empty);
    }

    public static HttpRequestResult DoPost(string url, Dictionary<string, string> postData, string referer)
    {
      if (HttpAutomation.RequireLogin && _cm.CookieCount == 0)
        HttpAutomation.HttpLogin();

      HttpWebRequest req = BuildRequest(url, postData);
      req.Referer = string.IsNullOrEmpty(referer) ? HttpAutomation.BaseUrl : referer;
      using (var response = (HttpWebResponse)req.GetResponse())
      {
        _cm.StoreCookies(response);
        
        string responseHtml = new StreamReader(response.GetResponseStream(), Encoding.UTF8 ).ReadToEnd();
        
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(responseHtml);

        return new HttpRequestResult{ Response = responseHtml, ResponseUrl = response.ResponseUri, Document = doc};
      }
    }

    public static HttpRequestResult DoGet(string url)
    {
      return HttpAutomation.DoGet(url, string.Empty);
    }

    public static HttpRequestResult DoGet(string url, string referer)
    {
      if (HttpAutomation.RequireLogin && _cm.CookieCount == 0)
        HttpAutomation.HttpLogin();

      HttpWebRequest req = BuildRequest(url);
      req.Referer = string.IsNullOrEmpty(referer) ? HttpAutomation.BaseUrl : referer;
      using (var response = (HttpWebResponse)req.GetResponse())
      {
        _cm.StoreCookies(response);

        string responseHtml = new StreamReader(response.GetResponseStream()).ReadToEnd();        

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(responseHtml);

        return new HttpRequestResult{ Response = responseHtml, ResponseUrl = response.ResponseUri, Document = doc};
      }
    }

    private static void SetHttpHeaders(HttpWebRequest req)
    {
      req.Accept = "text/html,application/xhtml+xml,application/xml,application/pdf;q=0.9,*/*;q=0.8";
      req.Headers.Add(HttpRequestHeader.AcceptLanguage, "tr-TR,tr;q=0.8,en-us;q=0.5,en;q=0.3");
      req.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-9,utf-8;q=0.7,*;q=0.7");
      //req.Headers.Add("X-Atlassian-Token", "no-check");

      _cm.PublishCookies(req);
    }






    public static int DownloadFile(string remoteFilename, string localFilename)
    {
      return FileDownload.DownloadFile(_cm, remoteFilename, localFilename);
    }

    public static void StoreCookies(HttpWebResponse webResponse)
    {
      _cm.StoreCookies(webResponse);
    }

    public static void ClearCookies()
    {
      _cm.ClearCookies();
    }




    public static HtmlDocument LoadHtmlDoc(Stream s)
    {
      return LoadHtmlDoc(s, Encoding.UTF8);
    }

    public static HtmlDocument LoadHtmlDoc(Stream s, Encoding enc)
    {
      HtmlDocument HD = HttpAutomation.GiveMeHtmlDoc();
      HD.Load(s, enc == null ? Encoding.UTF8 : enc);
      return HD;
    }

    private static HtmlDocument GiveMeHtmlDoc()
    {
      HtmlDocument HD = new HtmlDocument();
      HD.OptionOutputOriginalCase = true;
      HD.OptionWriteEmptyNodes = false;
      return HD;
    }

    






  }
}
