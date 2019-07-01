using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Shorthand.DataScraper.WebDataProvider
{
  internal class LoginResponse
  {
    public string retdata { get; set; }
    public string success { get; set; }
  }



  public class BloombergDataProvider : IWebDataProvider
  {

    private Dictionary<string, string> _propLookup;

    public string BaseUrl { get; set; }
    public bool RequiresAuthentication { get; set; }
    public string LoginPageUrl { get; set; }


    public BloombergDataProvider()
    {
      this.BaseUrl = "https://www.bloomberght.com/borsa/hisseler";
      this.RequiresAuthentication = false;

      HttpAutomation.LoginPostData = new Dictionary<string, string>();
    }

    public void SetAuthenticationParameters(string loginPageUrl, string userName, string password)
    {
      HttpAutomation.LoginPostData.Clear();

      HttpAutomation.LoginPageUrl = loginPageUrl;
      HttpAutomation.LoginPostData.Add("uname", userName);
      HttpAutomation.LoginPostData.Add("upass", password);
      HttpAutomation.RequireLogin = true;
    }

    public bool Authenticate(string loginPageUrl, string userName, string password)
    {
      this.SetAuthenticationParameters(loginPageUrl, userName, password);
      var result = HttpAutomation.HttpLogin();

      var response = JsonConvert.DeserializeObject<LoginResponse>(result.Response);
      return response.success == "1";
    }

    public Equity[] GetData(int pageIndex = 1)
    {
      string url = pageIndex > 1 ? $"{this.BaseUrl}/{pageIndex}" : this.BaseUrl;

      var result = HttpAutomation.DoGet(url, "");
      return this.ParseEquities(result.Document);
    }

    public Task<Equity[]> GetDataAsync(int pageIndex = 1)
    {
      return Task.Run(() =>
      {
        return this.GetData(pageIndex);
      });
    }

    private Equity[] ParseEquities(HtmlDocument document)
    {
      HtmlNode root = document.DocumentNode;
      var equityNodes = root.SelectNodes("descendant::div[contains(@class,'marketsData')]//table//tbody//tr");

      if (equityNodes == null)
        return new Equity[0] { };

      var formatProvider = CultureInfo.GetCultureInfo("tr-TR");
      var equities = new List<Equity>();
      foreach (var tr in equityNodes)
      {
        var cells = tr.ChildNodes.Where(c => c.Name == "td").ToArray();
        equities.Add(new Equity
        {
          Name = cells[0].InnerText.ToTidyString(),
          Last = Convert.ToDouble(cells[1].InnerText.ToTidyString(), formatProvider),
          Yesterday = Convert.ToDouble(cells[2].InnerText.ToTidyString(), formatProvider),
          Percentage = Convert.ToDouble(cells[3].InnerText.ToTidyString(), formatProvider),
          High = Convert.ToDouble(cells[4].InnerText.ToTidyString(), formatProvider),
          Low = Convert.ToDouble(cells[5].InnerText.ToTidyString(), formatProvider),
          VolumeInLots = Convert.ToInt32(cells[6].InnerText.ToTidyString().Replace(".", ""), formatProvider),
          VolumeInTL = Convert.ToInt32(cells[7].InnerText.ToTidyString().Replace(".", ""), formatProvider)
        });

      }

      return equities.ToArray();
    }







  }
}
