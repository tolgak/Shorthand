using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand.DataScraper.WebDataProvider
{
  public class Equity
  {
    public string   Name { get; set; }
    public double   Last { get; set; }
    public double   Yesterday { get; set; }
    public double   Percentage { get; set; }
    public double   High { get; set; }
    public double   Low { get; set; }
    public int      VolumeInLots { get; set; }
    public int      VolumeInTL { get; set; }
    public DateTime DateOfValue { get; set; }
    public int      Type { get; set; }


  }
}
