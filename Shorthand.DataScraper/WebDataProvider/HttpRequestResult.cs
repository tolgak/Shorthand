using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Shorthand.DataScraper.WebDataProvider
{
  public class HttpRequestResult
  {
    public string Response { get; set; }
    public Uri ResponseUrl { get; set; }
    public HtmlDocument Document { get; set; }

    public HttpRequestResult(string response, string responseUrl)
    {
      Response = response;
      ResponseUrl = new Uri(responseUrl);
    }

    public HttpRequestResult()
    {

    }

  }
}
