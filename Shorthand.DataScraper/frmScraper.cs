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
    private IWebDataProvider _bloomberg;

    private const string Equity_SaveOrUpdate = @"execute spEquity_SaveOrUpdate @Name, @Last, @Yesterday, @Percentage, @High, @Low, @VolumeInLots, @VolumeInTL, @DateOfValue";

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
        _bloomberg = new BloombergDataProvider();
        return true;
      });
    }

    private async Task<bool> InitializeUI()
    {
      return await Task.Run(() =>
      {
        //txtUserName.Text = UserPrincipal.Current.SamAccountName;
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
      //int.TryParse(txtPage.Text, out int pageIndex);
      txtLog.Log("getting data");
      this.Cursor = Cursors.WaitCursor;
      var equities = new List<Equity>();

      try
      {
        //var tasks = Enumerable.Range(1, 27)
        //                      .Select(pageIndex => new TaskFactory().StartNew(() => _webDataProvider.GetData(pageIndex)))
        //                      .ToArray();
        //Task.WhenAll(tasks)
        //    .ContinueWith(t => equities.OrderBy(x => x.Name).ToList().ForEach(x => txtLog.Log(x.Name)));
        
        Parallel.For(1, 30, i => equities.AddRange(_bloomberg.GetData(i)));
        var eq = equities.OrderBy(x => x.Name).ToArray();
        foreach (var item in eq)
        {
          txtLog.Log(item.Name);
          item.DateOfValue = Convert.ToDateTime("2019-06-28");
          //await this.SaveOrUpdate(item);
        }
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

    private async Task<int> SaveOrUpdate(object entity)
    {
      //@"Data Source=localhost\SQLEXPRESS;Initial Catalog=StockExchange;Integrated Security=True;MultipleActiveResultSets=True;";
      var connectionString = SqlUtility.GetConnectionString("home");
      var commandText = frmScraper.Equity_SaveOrUpdate;

      return await this.ExecuteCommand(connectionString, commandText, entity);      
    }

    private Task<int> ExecuteCommand(string connectionString, string commandText, object entity)
    {
      return Task.Run(() =>
      {
        var sqlParams = entity.ToSqlParamsArray();
        using (var connection = new SqlConnection(connectionString))
        {
          using (var command = connection.CreateCommand())
          {
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
            command.Parameters.AddRange(sqlParams);

            command.Connection.ConnectionString = connectionString;
            command.Connection.Open();
            var retVal = command.ExecuteNonQuery();

            return retVal;
          }
        }
      }
      );

    }




  }


}
