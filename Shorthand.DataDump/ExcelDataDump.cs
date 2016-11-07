using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Shorthand
{


  public class ExcelDataDump : IDataDumper
  {
    public event Action<object, RecordProgressEventArgs> OnRecordProgress;
    public event Action<object, RecordsetProgressEventArgs> OnRecordsetProgress;

    private string[] _excelColumnNames = new string[78] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" 
                                                        , "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" 
                                                        , "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ" };

    public void Dump(DataSet dataSet, string fileNameFormat = "")
    {
      Excel.Application excel = null;
      try
      {
        excel = new Excel.Application();

        int cntRecordset = dataSet.Tables.Count;
        for (int i = 0; i < cntRecordset; i++)
        {
          this.ExportToExcel(dataSet.Tables[i], excel);

          //if (this.OnRecordsetProgress != null)
            this.OnRecordsetProgress?.Invoke(this, new RecordsetProgressEventArgs {  NumberOfRecordsets = cntRecordset, Current = i});

          System.Windows.Forms.Application.DoEvents();
        }
      }
      finally
      {
        if ( excel != null )
          Marshal.ReleaseComObject(excel);

        excel = null;
      }
    }

    public void Dump2(DataSet dataSet, string fileNameFormat = "")
    {
      Excel.Application excel = null;
      try
      {
        //excel = new Excel.Application();

        int cntRecordset = dataSet.Tables.Count;
        for (int i = 0; i < cntRecordset; i++)
        {
          this.ExportToExcel(dataSet.Tables[i], null);

          //if (this.OnRecordsetProgress != null)
            this.OnRecordsetProgress?.Invoke(this, new RecordsetProgressEventArgs { NumberOfRecordsets = cntRecordset, Current = i });

          System.Windows.Forms.Application.DoEvents();
        }
      }
      finally
      {
        if (excel != null)
          Marshal.ReleaseComObject(excel);

        excel = null;
      }
    }

    private void ExportToExcel(System.Data.DataTable table, Excel.Application excel = null)
    {
      if ( table.Columns.Count - 1 > _excelColumnNames.Count() )
        throw new ArgumentOutOfRangeException("Grid column count", "Number of columns exceeds the internal map capacity.");

      if ( table.Rows.Count < 1 )
        return;

      //Excel.Application excel = null;
      Excel.Workbook workbook = null;
      Excel.Worksheet worksheet = null;
      try
      {
        if ( excel == null )
          excel = new Excel.Application();

        if ( excel.ActiveWorkbook == null )
          excel.Workbooks.Add(System.Reflection.Missing.Value);

        workbook = excel.ActiveWorkbook;
        //worksheet = ( Excel.Worksheet ) workbook.ActiveSheet;        
        worksheet = ( Excel.Worksheet ) workbook.Worksheets.Add();
        worksheet.Name = table.TableName;

        string bottomRight = string.Format("{0}{1}", _excelColumnNames[table.Columns.Count - 1], table.Rows.Count + 1);
        string range = string.Format("A1:{0}", bottomRight);
        worksheet.get_Range(range, System.Type.Missing).Value2 = CreateTwoDimensionalObject(table);
        worksheet.get_Range("A1", bottomRight).EntireColumn.AutoFit();
        worksheet.get_Range("A1", bottomRight).RowHeight = 16;
        worksheet.get_Range("A1", bottomRight).Rows.VerticalAlignment = 2;

        excel.Visible = true;
      }
      catch
      {
        if ( excel != null )
          excel.Quit();

        throw;
      }
      finally
      {
        if ( worksheet != null )
          Marshal.ReleaseComObject(worksheet);

        if ( workbook != null )
          Marshal.ReleaseComObject(workbook);

        //if ( excel != null )
        //  Marshal.ReleaseComObject(excel);

        worksheet = null;
        workbook = null;
        //excel = null;
      }
    }

    private object[,] CreateTwoDimensionalObject(System.Data.DataTable table)
    {
      object[,] data = new object[table.Rows.Count + 1, table.Rows[0].ItemArray.Length];

      //add the first row(the column headers) to the array
      for ( int col = 0; col < table.Columns.Count; col++ )
        data[0, col] = table.Columns[col].ColumnName;

      //copy the actual data
      for (int row = 0; row < table.Rows.Count; row++)
      {
        for (int col = 0; col < table.Rows[0].ItemArray.Length; col++)
          if (table.Rows[row].ItemArray[col] != null)
            data[row + 1, col] = table.Rows[row].ItemArray[col].ToString();

        //if (this.OnRecordProgress != null)
          this.OnRecordProgress?.Invoke(this, new RecordProgressEventArgs { NumberOfRecords = table.Rows.Count, Current = row });
      }

      return data;
    }

  }


}
