using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Shorthand.Common.Sql
{
  public class SqlUtility
  {

    public static string GetConnectionString(string name)
    {
      var result = string.Empty;

      foreach (var item in ConfigurationManager.ConnectionStrings)
      {
        var x = (item as ConnectionStringSettings).ElementInformation.Properties["name"];
        if (x.Value.ToString() != name)
          continue;

        var y = (item as ConnectionStringSettings).ElementInformation.Properties["connectionString"];
        result = y.Value.ToString();
      }

      return result;
    }

    public static Task<DataSet> BuildDataSet(string connectionString, string commandText)
    {
      return Task.Run(() =>
      {
        DataSet dataSet = new DataSet("DataDump");
        using (var connection = new SqlConnection(connectionString))
        {
          using (var command = connection.CreateCommand())
          {
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;

            command.Connection.ConnectionString = connectionString;
            command.Connection.Open();
            using (var adapter = new SqlDataAdapter(command))
            {
              adapter.SelectCommand.CommandTimeout = 15000;
              adapter.Fill(dataSet);
            }

            return dataSet;
          }
        }
      }
      );
    }


  }
}
