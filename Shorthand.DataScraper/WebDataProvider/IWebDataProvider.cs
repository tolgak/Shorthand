using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand.DataScraper.WebDataProvider
{
  public interface IWebDataProvider
  {
    bool RequiresAuthentication { get; set; }
    string BaseUrl { get; set; }
    string LoginPageUrl { get; set; }

    void SetAuthenticationParameters(string loginPageUrl, string userName, string password);
    bool Authenticate(string loginPageUrl, string userName, string password);

    Equity[] GetData(int pageIndex = 1);
    Task<Equity[]> GetDataAsync(int pageIndex = 1);

    Equity[] GetIndex();

  }

}
