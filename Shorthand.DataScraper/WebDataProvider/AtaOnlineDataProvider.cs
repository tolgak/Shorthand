using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand.DataScraper.WebDataProvider
{
  public class AtaOnlineDataProvider : IWebDataProvider
  {
    public bool RequiresAuthentication { get; set; }
    public string BaseUrl { get; set; }
    public string LoginPageUrl { get; set; }



    public AtaOnlineDataProvider()
    {
      this.BaseUrl = "https://www.ataonline.com.tr/equity/getequities";
      this.RequiresAuthentication = false;

      //HttpAutomation.LoginPostData = new Dictionary<string, string>();
    }

    public void SetAuthenticationParameters(string loginPageUrl, string userName, string password)
    {
      throw new NotImplementedException();
    }

    public bool Authenticate(string loginPageUrl, string userName, string password)
    {
      throw new NotImplementedException();
    }

    public Equity[] GetData(int pageIndex = 1)
    {
      var eq = new List<Equity>();
      string url = this.BaseUrl;

      var result = HttpAutomation.DoGet(url);
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
      var equityNodes = root.SelectNodes("descendant::table[@id='table-equities']//tr");

      if (equityNodes == null)
        return new List<Equity> { };

      var formatProvider = CultureInfo.GetCultureInfo("tr-TR");
      var equities = new List<Equity>();
      foreach (var tr in equityNodes)
      {
        var cells = tr.ChildNodes.Where(c => c.Name == "td").ToArray();
        if (cells.Length == 0)
          continue;

        equities.Add(new Equity
        {
          Name = cells[0].InnerText.ToTidyString(),
          Last = Convert.ToDouble(cells[2].InnerText.ToTidyString(), formatProvider),
          Low = Convert.ToDouble(cells[3].InnerText.ToTidyString(), formatProvider),
          High = Convert.ToDouble(cells[4].InnerText.ToTidyString(), formatProvider),

          //Yesterday = Convert.ToDouble(cells[2].InnerText.ToTidyString(), formatProvider),
          Percentage = Convert.ToDouble(cells[6].InnerText.ToTidyString(), formatProvider),
          
          
          //VolumeInLots = Convert.ToInt32(cells[6].InnerText.ToTidyString().Replace(".", ""), formatProvider),
          VolumeInTL = Convert.ToInt32(cells[7].InnerText.ToTidyString().Replace(".", "").Replace(" ", "").Replace("TL", ""), formatProvider),
          Type = 0
        });
      };

      return equities.ToList();
    }




    public Equity[] GetIndex()
    {
      throw new NotImplementedException();
    }








  }
}
