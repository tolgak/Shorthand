using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using PragmaTouchUtils;
using Shorthand.Common;
using Shorthand.Common.Sql;
using Shorthand.DataScraper.WebDataProvider;

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
        _provider = new AtaOnlineDataProvider();
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

    private async void btnGetData_Click(object sender, EventArgs e)
    {      
      txtLog.Log("Scraping ...");
      this.Cursor = Cursors.WaitCursor;
      try
      {
        (await _provider.GetDataAsync()).ForEach(eq => {
          txtLog.Log(eq.Name);
          eq.DateOfValue = DateTime.Now.Date;

          this.SaveOrUpdate(eq);
        }); 
      }
      catch (Exception ex)
      {
        txtLog.Log(ex.Message);
      }
      finally
      {
        txtLog.Log("Done.");
        this.Cursor = Cursors.Default;
      }

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
