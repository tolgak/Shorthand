using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Shorthand.DataScraper.WebDataProvider
{
  public interface ICookieManager
  {
    int CookieCount { get; }
    void PublishCookies(HttpWebRequest webRequest);
    void StoreCookies(HttpWebResponse webResponse);
    void ClearCookies();
    
  }
}
