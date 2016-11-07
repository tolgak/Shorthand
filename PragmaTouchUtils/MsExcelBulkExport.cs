using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections;
using System.Data;
using xl = Microsoft.Office.Interop.Excel;

namespace PragmaTouchUtils
{
	/// <summary>
	/// Exports the Dataset(more than 1 dataset) to excel.
	/// </summary>
	public class MsExcelBulkExport : IDisposable
	{
		public delegate void BulkExportProgressDelegete(int iCurrentItem,int iTotalItems);

		/// <summary>
		/// Export progress indicator event for each DataSet.
		/// </summary>
		public event BulkExportProgressDelegete BulkExportProgress;

		/// <summary>
		/// Export progress indicator event for each record/row.
		/// </summary>
		public event BulkExportProgressDelegete BulkExportRowProgress;

		SortedList _listrow = null;
		int j = 0;
		int _totalItems = 1;
		int rowIndex = 1;
		int colIndex = 0;
		int startCol = 0;
		int newrow = 1;
		int i=0;
		ExportStyle _style = ExportStyle.ColumnWise;
		xl.Application excel;
    xl.Workbooks workbooks;
    xl.Workbook workbook;
    xl.Worksheet worksheet;
    xl.Sheets worksheets;
		ExcelStyle excelStyle;
		bool _header =true;

		/// <summary>
		/// Specifies the formatting style for the excel like color,font etc.
		/// </summary>
		public ExcelStyle ExcelFormattingStyle
		{
			set
			{
				excelStyle = value;
			}
			get
			{
				return excelStyle;
			}
		}

		/// <summary>
		/// Creates the instance.
		/// </summary>
		/// <param name="style">Style specifies Column wise,Row wise or Sheet wise</param>
		/// <param name="totalItems">Total number of DataSets to be exported</param>
		public MsExcelBulkExport(ExportStyle style, int totalItems)
		{
			//Create new excel instance and add workbook
			excel = new Microsoft.Office.Interop.Excel.Application();
			workbooks = excel.Workbooks;
			workbook = workbooks.Add(true);
			worksheets = (Microsoft.Office.Interop.Excel.Sheets) excel.Worksheets;
			this._totalItems = totalItems;
			_listrow = new SortedList();
			this._style = style;
		}

		/// <summary>
		/// Opens the exported excel file
		/// </summary>
		public void ShowExcel()
		{
			//Get the active sheet and make excel visible.
			worksheet = (xl.Worksheet) excel.ActiveSheet; 
			worksheet.Activate();
			excel.Visible = true;
		}

		/// <summary>
		/// Disposes and releases the com objects
		/// </summary>
		public void Dispose()
		{
			//Be careful while disposing excel objects.
			//################################################################ 
			// NOTE :Use ExitAndDispose() method instead of Dispose() method #
			//		 in case of any exception else excel application			     #	
			//		 may not be released from memory.						               #
			//################################################################
			if(worksheet!=null) 
        Marshal.ReleaseComObject(worksheet);

			if(worksheets != null)	
        Marshal.ReleaseComObject(worksheets);

			if(workbook != null) 
        Marshal.ReleaseComObject(workbook);

			if(workbooks != null) 
        Marshal.ReleaseComObject(workbooks);

			if(excel != null) 
        Marshal.ReleaseComObject(excel);

			worksheet = null;
			worksheets = null;
			workbook = null;
			workbooks = null;
			excel = null;
			
      GC.Collect();
		}

		/// <summary>
		/// Exit the excel application and disposes the object.
		/// </summary>
		public void ExitAndDispose()
		{
			//#################################################################
			// NOTE :Use ExitAndDispose() method instead of Dispose() method  #
			//		 incase of any exception else excel application			        #
			//		 may not be released from memory.						                #
			//#################################################################
			if(excel != null) 
        excel.Quit();

			this.Dispose();
		}
		
		/// <summary>
		/// Binds the DataSet to the excel
		/// </summary>
		/// <param name="ds">Dataset to be exported</param>
		public void BindDataToExcel(DataSet ds)
		{
			foreach(System.Data.DataTable dtb in ds.Tables)
			{
				startCol = colIndex;
				//Add this DataTable to the excel
				AddDataTableToExcel(excel,dtb,ref rowIndex,ref colIndex);
				switch(_style)
				{
					case ExportStyle.RowWise:
					{
						colIndex = 0;
						rowIndex += 2;
						break;
					}
					case ExportStyle.ColumnWise:
					{
						rowIndex = newrow;
						colIndex+=excelStyle.ColumnSpaceBetweenTables;
						break;
					}
					case ExportStyle.SheetWise:
					{
						colIndex = 0;
						rowIndex += excelStyle.RowSpaceBetweenTables;
						break;
					}
				}
				i++;
			}
				
			switch(_style)
			{
				case ExportStyle.RowWise:
				{
					colIndex = 0;
					rowIndex += excelStyle.RowSpaceBetweenTables -1 ;
					break;
				}
				case ExportStyle.ColumnWise:
				{
					_header = excelStyle.RepeatColumnHeader;
					if(_listrow.Count > 0)
					{
						rowIndex = (int)_listrow.GetByIndex(_listrow.Count- 1) + excelStyle.RowSpaceBetweenTables ;
						if(_header) rowIndex++; 
						newrow = rowIndex;
					}
					colIndex = 0;
					break;
				}
				case ExportStyle.SheetWise:
				{
					colIndex = 0;
					rowIndex = 1;
					if(worksheet != null)
					{
						//Release the com objects
						Marshal.ReleaseComObject(worksheet);
						Marshal.ReleaseComObject(worksheets);
						worksheet = null;
						worksheets = null;
					}
					if(j != _totalItems - 1)
					{
						//Add worksheet
						worksheets = (Microsoft.Office.Interop.Excel.Sheets) excel.Worksheets;
						worksheet = (Microsoft.Office.Interop.Excel.Worksheet) worksheets.Add(
							Type.Missing, Type.Missing, Type.Missing, Type.Missing);
					
					}
					break;
				}
			}
			j++;
			//Raise progress event
			//if(BulkExportProgress!=null)
				BulkExportProgress?.Invoke(j,_totalItems);
		}


		//Adds each datatable to the excel
		private void AddDataTableToExcel(Microsoft.Office.Interop.Excel.Application excel,System.Data.DataTable table,ref int rowIndex,ref int columnIndex)
		{
			int colstart = columnIndex;
			int colbak = colstart;
			//Check if column header needs to be added
			if(_header)
			{
				//Add column header
				foreach(DataColumn col in table.Columns) 
				{ 
					columnIndex+= excelStyle.ColumnSpace ; 
					Microsoft.Office.Interop.Excel.Range cel = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex,columnIndex];
					cel.Font.Bold = true;
					cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderBackColor);
					cel.Font.Color =  System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderForeColor);
					cel.Font.Name = excelStyle.FontName;
					cel.Font.Size = excelStyle.FontSize;
					cel.Font.Italic = excelStyle.HeaderItalic;
					cel.Font.Bold = excelStyle.HeaderFontBold;
					excel.Cells[rowIndex,columnIndex]=col.ColumnName; 
				}
			}
			else
			{
				columnIndex += table.Columns.Count * excelStyle.ColumnSpace;
			}

			int eventRow = 0;
			//Add the rows to the excel..
			foreach(DataRow row in table.Rows)
			{ 
				rowIndex+= excelStyle.RowSpace; 
				foreach(DataColumn col in table.Columns) 
				{ 
					//TO do
					colstart+=excelStyle.ColumnSpace ;
					//Set formatting for excel
					Microsoft.Office.Interop.Excel.Range cel = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex,colstart];
					
					if(rowIndex!= 0 && rowIndex%2 == 0)
					{
						if(excelStyle.ItemAlternateBackColor != Color.White)
							cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemAlternateBackColor);
					}
					else
					{
						if(excelStyle.ItemBackColor != Color.White)
							cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemBackColor);
					}

					cel.Font.Color =  System.Drawing.ColorTranslator.ToOle(excelStyle.ItemForeColor);
					cel.Font.Name = excelStyle.FontName;
					cel.Font.Size = excelStyle.FontSize;
					cel.Font.Italic = excelStyle.ItemItalic;
					cel.Font.Bold = excelStyle.ItemFontBold;
					excel.Cells[rowIndex,colstart]=row[col.ColumnName].ToString(); 
				} 
				colstart = colbak;
				//if(BulkExportRowProgress!=null)
					BulkExportRowProgress?.Invoke(eventRow++, table.Rows.Count);
			}
			if(!_listrow.ContainsKey(rowIndex))
			{
				_listrow.Add(rowIndex,rowIndex);
			}
		}
		}
}
