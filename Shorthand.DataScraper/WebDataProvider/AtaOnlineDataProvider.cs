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
    }

    public void SetAuthenticationParameters(string loginPageUrl, string userName, string password)
    {
      throw new NotImplementedException();
    }

    public bool Authenticate(string loginPageUrl, string userName, string password)
    {
      throw new NotImplementedException();
    }


    public List<Equity> GetData(DateTime date, int pageIndex = 1)
    {
      string url = this.BaseUrl;
      var result = HttpAutomation.DoGet(url);
      var hDoc = result?.Document;

      if (hDoc == null)
        return new List<Equity>();

      return this.ParseEquities(hDoc);
    }

    public Task<List<Equity>> GetDataAsync(DateTime date, int pageIndex = 1)
    {
      return Task.Run(() =>
      {
        return this.GetData(date, pageIndex);
      });
    }

    /*

    -- moving average
    select Name, DateOfValue, Last
         , avg(Last) over (order by Name, DateOfValue rows between 14 preceding and current row) as MovingAverageWindowSize15
    from Equity
    order by Name, DateOfValue;
    
    */

    private List<Equity> ParseEquities(HtmlDocument document)
    {
      HtmlNode root = document.DocumentNode;
      var equityNodes = root.SelectNodes("descendant::table[@id='table-equities']//tr").ToList();
      if (equityNodes == null || equityNodes.Count == 0)      
        return new List<Equity> { };

      var formatProvider = CultureInfo.GetCultureInfo("tr-TR");
      var equities = new List<Equity>();

      equityNodes.ForEach(  tr => {
        var cells = tr.ChildNodes.Where(c => c.Name == "td").ToArray();
        if (cells.Length == 0)
          return;

        equities.Add(new Equity {
          Name = cells[0].InnerText.ToTidyString(),
          Last = Convert.ToDouble(cells[2].InnerText.ToTidyString(), formatProvider),
          Low = Convert.ToDouble(cells[3].InnerText.ToTidyString(), formatProvider),
          High = Convert.ToDouble(cells[4].InnerText.ToTidyString(), formatProvider),
          //Yesterday = Convert.ToDouble(cells[2].InnerText.ToTidyString(), formatProvider),
          Percentage = Convert.ToDouble(cells[6].InnerText.ToTidyString(), formatProvider),
          //VolumeInLots = Convert.ToInt32(cells[6].InnerText.ToTidyString().Replace(".", ""), formatProvider),
          VolumeInTL = Convert.ToInt32(cells[7].InnerText.ToTidyString()
                                                         .Replace(".", "")
                                                         .Replace(" ", "")
                                                         .Replace("TL", ""), formatProvider),
          Type = 0
        });
      });

      return equities;
    }




    public Equity[] GetIndex()
    {
      throw new NotImplementedException();
    }








  }
}
