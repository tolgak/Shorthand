using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{

  public class RecordsetProgressEventArgs : EventArgs
  {
    public int NumberOfRecordsets { get; set; }
    public int Current { get; set; }
  }

  public class RecordProgressEventArgs : EventArgs
  {
    public int NumberOfRecords { get; set; }
    public int Current { get; set; }
  }

  public interface IDataDumper
  {
    event Action<object, RecordProgressEventArgs> OnRecordProgress;
    event Action<object, RecordsetProgressEventArgs> OnRecordsetProgress;

    void Dump(DataSet dataSet, string fileNameFormat = "");
    void Dump2(DataSet dataSet, string fileNameFormat = "");
  }




}
