using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{

  public class CSVDataDump : IDataDumper
  {

    public event Action<object, RecordProgressEventArgs> OnRecordProgress;
    public event Action<object, RecordsetProgressEventArgs> OnRecordsetProgress;

    public void Dump(DataSet dataSet, string fileNameFormat = "")
    {
      try
      {
        var content = string.Empty;        
        int cntRecordset = dataSet.Tables.Count;

        for (int i = 0; i < cntRecordset; i++)
        {
          content = this.ExportToString(dataSet.Tables[i]);
          var fileName = string.IsNullOrEmpty(fileNameFormat) ? string.Format("dump{0}.txt", i) : string.Format(fileNameFormat, i);
          
          File.WriteAllText(fileName, content, Encoding.UTF8);          
          Process.Start(fileName);

          if (this.OnRecordsetProgress != null)
            this.OnRecordsetProgress(this, new RecordsetProgressEventArgs { NumberOfRecordsets = cntRecordset, Current = i });
        }
      }
      finally
      {

      }           
    }

    public void Dump2(DataSet dataSet, string fileNameFormat = "")
    {
      this.Dump(dataSet, fileNameFormat);
    }

    private string ExportToString(DataTable table)
    {
      var result = this.CreateTwoDimensionalObject(table);
      var sb = new StringBuilder();
      for ( int row = 0; row <= result.GetUpperBound(0); row++ )
      {
        var comma = "";
        for ( int col = 0; col <= result.GetUpperBound(1); col++ )
        {          
          if ( result[row, col] != null )
          {
            sb.AppendFormat("{0}{1}", comma, result[row, col].ToString());
            comma = ",";
          }
        }
        sb.Append("\r\n");
      }
      
      return sb.ToString();      
    }

    private object[,] CreateTwoDimensionalObject(DataTable table)
    {
      object[,] data = new object[table.Rows.Count + 1, table.Rows[0].ItemArray.Length];

      //add the first row(the column headers) to the array
      for ( int col = 0; col < table.Columns.Count; col++ )
        data[0, col] = table.Columns[col].ColumnName;

      //copy the actual data
      //for (int col = 0; col < table.Rows[0].ItemArray.Length; col++)
      //{
      //  for (int row = 0; row < table.Rows.Count; row++)
      //    if (table.Rows[row].ItemArray[col] != null)
      //      data[row + 1, col] = table.Rows[row].ItemArray[col].ToString();

      //  if (this.OnRecordProgress != null)
      //    this.OnRecordProgress(this, new RecordProgressEventArgs { NumberOfRecords = table.Rows.Count, Current = row });
      //}
      for (int row = 0; row < table.Rows.Count; row++)
      {
        for (int col = 0; col < table.Rows[0].ItemArray.Length; col++)
          if (table.Rows[row].ItemArray[col] != null)
            data[row + 1, col] = table.Rows[row].ItemArray[col].ToString();

        if (this.OnRecordProgress != null)
          this.OnRecordProgress(this, new RecordProgressEventArgs { NumberOfRecords = table.Rows.Count, Current = row });
      }


      return data;
    }

  }
}
