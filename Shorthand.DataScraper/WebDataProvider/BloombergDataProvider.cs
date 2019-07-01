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
    private const string BIST100 = "bist-100";
    private const string BIST50  = "bist-50";
    private const string BIST30  = "bist-30";

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
      var eq = new List<Equity>();
      string url = pageIndex > 1 ? $"{this.BaseUrl}/{pageIndex}" : this.BaseUrl;

      var result = HttpAutomation.DoGet(url, "");
      var hDoc = result?.Document;
      if (hDoc != null)
      {
        eq.AddRange(this.ParseEquities(hDoc));
      }

      return eq.ToArray();
    }

    public Task<Equity[]> GetDataAsync(int pageIndex = 1)
    {
      return Task.Run(() =>
      {
        return this.GetData(pageIndex);
      });
    }

    private List<Equity> ParseEquities(HtmlDocument document)
    {
      HtmlNode root = document.DocumentNode;
      var equityNodes = root.SelectNodes("descendant::div[contains(@class,'marketsData')]//table//tbody//tr");

      if (equityNodes == null)
        return new List<Equity> { };

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
          VolumeInTL = Convert.ToInt32(cells[7].InnerText.ToTidyString().Replace(".", ""), formatProvider),
          Type = 0
        });
      };

      return equities.ToList();
    }



    public Equity[] GetIndex()
    {
      var eq = new List<Equity>();

      eq.Add(this.GetBIST(BloombergDataProvider.BIST100));
      eq.Add(this.GetBIST(BloombergDataProvider.BIST50));
      eq.Add(this.GetBIST(BloombergDataProvider.BIST30));

      return eq.ToArray();
    }


    private Equity GetBIST(string indexName)
    {
      var url = $"https://www.bloomberght.com/borsa/endeks/{indexName}";
      var result = HttpAutomation.DoGet(url, "");
      var hDoc = result?.Document;
      if (hDoc != null)
        return this.ParseBIST(hDoc);

      return null;
    }


    private Equity ParseBIST(HtmlDocument document)
    {
      if (document == null)
        return null;

      HtmlNode root = document.DocumentNode;
      var detailTitle = root.SelectNodes("descendant::div[contains(@class,'piyasaDetayTitle')]//h1").FirstOrDefault();
      var name = detailTitle?.InnerText.ToTidyString();

      var detail1 = root.SelectNodes("descendant::div[contains(@class,'piyasaDetayTitle')]//div").FirstOrDefault();
      var lastValue = detail1?.InnerText.Split('%')?[0].ToTidyString();

      var detail2 = root.SelectNodes("descendant::div[contains(@class,'piyasaDetayTitle')]//div//span").ToArray();
      var percentage = detail2[1].InnerText.Replace("%", string.Empty).ToTidyString();

      var detailDate = root.SelectNodes("descendant::div[contains(@class,'piyasaDetayDate')]").FirstOrDefault();
      var dateOfValue = detailDate?.InnerText.ToTidyString();

      var dataValues = root.SelectNodes("descendant::span[contains(@class,'piyasaDataValues')]").ToArray();
      var yesterday = dataValues[0].InnerText.Split(':')?[1].ToTidyString();
      var high = dataValues[1].InnerText.Split(':')?[1].ToTidyString();
      var low = dataValues[2].InnerText.Split(':')?[1].ToTidyString();

      var formatProvider = CultureInfo.GetCultureInfo("tr-TR");
      return new Equity
      {
        Name = name,
        Last = Convert.ToDouble(lastValue, formatProvider),
        Yesterday = Convert.ToDouble(yesterday, formatProvider),
        Percentage = Convert.ToDouble(percentage, formatProvider),
        High = Convert.ToDouble(high, formatProvider),
        Low = Convert.ToDouble(low, formatProvider),
        DateOfValue = Convert.ToDateTime(dateOfValue, formatProvider),
        Type = 1
      };

    }











  }
}
