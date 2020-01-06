using PragmaTouchUtils;
using Shorthand.Common;
using Shorthand.Common.Sql;
using Shorthand.DataScraper.WebDataProvider;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shorthand.DataScraper
{
  [Export(typeof(IAsyncPlugin))]
  public partial class frmScraper : Form, IAsyncPlugin
  {
    private IPluginContext _context;
    private IWebDataProvider _provider;

    private const string Equity_SaveOrUpdate = @"execute spEquity_SaveOrUpdate @Name, @Last, @Yesterday, @Percentage, @High, @Low, @VolumeInLots, @VolumeInTL, @DateOfValue, @Type";

    public frmScraper()
    {
      InitializeComponent();
    }

    public async Task<Form> InitializeAsync(IPluginContext context)
    {
      return await Task.Run(async () =>
      {
        _context = context;

        this.FormClosing += (object sender, FormClosingEventArgs e) =>
        {
          e.Cancel = true;
          this.Hide();
        };

        await this.InitializePlugin();
        await this.InitializeUI();

        return this;
      });
    }

    private async Task<bool> InitializePlugin()
    {
      return await Task.Run(() =>
      {
        //_provider = new AtaOnlineDataProvider();
        _provider = new BloombergDataProvider();
        return true;
      });
    }

    private async Task<bool> InitializeUI()
    {
      return await Task.Run(() =>
      {
        return true;
      });
    }

    public async void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    {
      var shouldRefresh = e.ChangedOptions.Contains("DataScraperOptions");
      if (!shouldRefresh)
        return;

      await this.InitializePlugin();
      await this.InitializeUI();
    }

    //private async void btnGetData_Click(object sender, EventArgs e)
    //{      
    //  txtLog.Log("Scraping ...");
    //  var date = dateQuote.Value;

    //  this.Cursor = Cursors.WaitCursor;
    //  try
    //  {
    //    (await _provider.GetDataAsync(date)).ForEach(eq => {
    //      txtLog.Log(eq.Name);
    //      eq.DateOfValue = date;

    //      this.SaveOrUpdate(eq);
    //    }); 
    //  }
    //  catch (Exception ex)
    //  {
    //    txtLog.Log(ex.Message);
    //  }
    //  finally
    //  {
    //    txtLog.Log("Done.");
    //    this.Cursor = Cursors.Default;
    //  }
    //}


    private void btnGetData_Click(object sender, EventArgs e)
    {
      txtLog.Log("Scraping ...");
      var date = dateQuote.Value;

      this.Cursor = Cursors.WaitCursor;
      try
      {

        var equities = new List<Equity>();
        List<Equity> items = new List<Equity>();
        // Async 
        //var tasks = Enumerable.Range(1, 33).Select(i => _provider.GetDataAsync(date, i)).ToArray();
        //Task.WaitAll(tasks);

        //items = tasks.SelectMany(t => t.Result).ToList();
        //equities.AddRange(items);

        //foreach (var item in equities)
        //{
        //  txtLog.Log($"{item.Name}");

        //  item.DateOfValue = date;
        //  this.SaveOrUpdate(item);
        //}

        // Parallel
        var bag = new ConcurrentDictionary<int, List<Equity>>();
        var pages = Enumerable.Range(1, 33).ToArray();

        var partitioner = Partitioner.Create<int>(pages, true);
        Parallel.ForEach(partitioner, i =>
        {
          try
          {
            var data = _provider.GetData(date, i);
            bag.TryAdd(i, data);
          }
          catch (Exception exception) { txtLog.Log($"{exception.Message}"); }
        });

        items.Clear();
        items = bag.SelectMany(kvp => kvp.Value).ToList();
        equities.AddRange(items);

        foreach (var item in equities)
        {
          txtLog.Log($"{item.Name}");

          item.DateOfValue = date.Date;
          this.SaveOrUpdate(item);
        }



      }
      catch (Exception ex)
      {
        txtLog.Log(this.GetInnerMostException(ex));
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    public string GetInnerMostException(Exception ex)
    {
      var result = "";
      if (ex.InnerException != null)
        result = GetInnerMostException(ex.InnerException);

      return result;

    }





    private int SaveOrUpdate(object entity)
    {
      //@"Data Source=localhost\SQLEXPRESS;Initial Catalog=StockExchange;Integrated Security=True;MultipleActiveResultSets=True;";
      //var connectionString = SqlUtility.GetConnectionString("stockOffice");
      var connectionString = SqlUtility.GetConnectionString("stockHome");

      var sqlParams = entity.ToSqlParamsArray();
      using (var connection = new SqlConnection(connectionString))
      {
        using (var command = connection.CreateCommand())
        {
          command.CommandType = CommandType.Text;
          command.CommandText = frmScraper.Equity_SaveOrUpdate;
          command.Parameters.AddRange(sqlParams);
          command.Connection.Open();
          var retVal = command.ExecuteNonQuery();

          return retVal;
        }
      }
    }





  }


}
