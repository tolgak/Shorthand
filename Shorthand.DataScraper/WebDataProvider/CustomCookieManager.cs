using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Globalization;
//using System.Web.Security;

//using JiraTouch.Utils;

namespace Shorthand.DataScraper.WebDataProvider
{

  
  public class CustomCookieManager : ICookieManager
  {
    private Dictionary<string, string> _cookieValues = new Dictionary<string, string>();


    public int CookieCount
    {
      get { return _cookieValues.Count; }
    }

    private string GetAsFullHeaderValue()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Cookie: ");
      string sep = String.Empty;
      foreach (string key in _cookieValues.Keys)
      {
        sb.Append(sep);
        sb.Append(key);
        sb.Append("=");
        sb.Append(_cookieValues[key]);
        sep = "; ";
      }

      return sb.ToString();
    }


    public void PublishCookies(HttpWebRequest webRequest)
    {     
      webRequest.Headers.Add(GetAsFullHeaderValue());

      
    }


    public void StoreCookies(HttpWebResponse webResponse)
    {

      if (!String.IsNullOrEmpty(webResponse.Headers[HttpResponseHeader.SetCookie]))
      {
        if (webResponse.Cookies.Count > 0)
        {
          foreach (Cookie c in webResponse.Cookies)
          {

            if (_cookieValues.ContainsKey(c.Name))
              _cookieValues[c.Name] = c.Value;
            else
              _cookieValues.Add(c.Name, c.Value);
          }
        }
        else
        {
          this.AddRawCookie(webResponse.Headers[HttpResponseHeader.SetCookie]);
        }
      }   
    }
   

    private void AddRawCookie(string rawCookieData)
    {
      string key = null;
      string value = null;

      string[] entries = null;

      if (rawCookieData.IndexOf(",") > 0)
      {
        entries = rawCookieData.Split(',');
      }
      else
      {
        entries = new string[] { rawCookieData };
      }

      foreach (string entry in entries)
      {
        string cookieData = entry.Trim();

        if (cookieData.IndexOf(';') > 0)
        {
          string[] temp = cookieData.Split(';');
          cookieData = temp[0];
        }

        int index = cookieData.IndexOf('=');
        if (index > 0)
        {
          key = cookieData.Substring(0, index);
          value = cookieData.Substring(index + 1);
        }

        if (key != null && value != null)
        {
          _cookieValues[key] = value;
        }

        cookieData = null;
      }

      rawCookieData = null;
      entries = null;
      key = null;
      value = null;
    }
    
    public void ClearCookies()
    {
      _cookieValues.Clear();
    }
  
    public string DumpCookies()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("[");
      foreach (string key in _cookieValues.Keys)
      {
        sb.Append("{");
        sb.Append(key);
        sb.Append(",");
        sb.Append(_cookieValues[key]);
        sb.Append("}, ");
      }
      if (_cookieValues.Keys.Count > 0)
      {
        sb.Remove(sb.Length - 2, 2);
      }
      sb.Append("]");

      return sb.ToString();
    }
  }

  public class DefaultCookieManager : ICookieManager
  {
    private CookieContainer _cc = new CookieContainer();
    public int CookieCount
    {
      get { return _cc.Count; }
    }

    public void PublishCookies(HttpWebRequest webRequest)
    {
      webRequest.CookieContainer = _cc;
    }


    public void StoreCookies(HttpWebResponse webResponse)
    {
      _cc.Add(webResponse.Cookies);
    }

    public void ClearCookies()
    {
      _cc = new CookieContainer();
    }
  }
}
