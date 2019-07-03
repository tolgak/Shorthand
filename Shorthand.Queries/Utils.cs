using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using System.Windows.Forms;

namespace Shorthand.Queries
{
  
  public class Utils
  {
    public static DataTable GetDataTable(string connectionString, string sqlText, SqlParameter[] parameters)
    {
      using ( SqlConnection connection = new SqlConnection(connectionString) )
      {
        connection.Open();
        using ( SqlCommand command = new SqlCommand(sqlText, connection) )
        {
          if ( parameters != null && parameters.Length != 0 )
            command.Parameters.AddRange(parameters);

          SqlDataAdapter adapter = new SqlDataAdapter();
          adapter.SelectCommand = command;

          DataTable table = new DataTable();
          adapter.Fill(table);

          return table;
        }
      }
    }

    public static List<T> FetchEntity<T>(string connectionString, string sqlText, SqlParameter[] parameters) where T : class, new()
    {
      List<T> result = new List<T>();
      using ( SqlConnection connection = new SqlConnection(connectionString) )
      {
        connection.Open();
        using ( SqlCommand command = new SqlCommand(sqlText, connection) )
        {
          if ( parameters != null && parameters.Length != 0 )
            command.Parameters.AddRange(parameters);

          using ( SqlDataReader reader = command.ExecuteReader() )
          {
            while ( reader.Read() )
              result.Add(Utils.MapToEntity<T>(reader));
          }
        }
      }

      return result;
    }

    private static T MapToEntity<T>(SqlDataReader reader) where T : class, new()
    {
      T dto = new T();
      if ( reader.IsClosed )
        return dto;

      PropertyInfo[] properties = typeof(T).GetProperties();
      for ( int i = 0; i < reader.FieldCount; i++ )
      {
        if ( reader.IsDBNull(i) )
          continue;

        string fieldName = reader.GetName(i);
        var matchingProperty = properties.FirstOrDefault(x => x.Name == fieldName);
        if ( matchingProperty != null )
        {
          object readerValue = reader.GetValue(i);
          matchingProperty.SetValue(dto, readerValue, null);
        }
      }

      return dto;    
    }

  }


}
