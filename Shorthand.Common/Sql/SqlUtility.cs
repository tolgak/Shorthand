using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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

  }
}
