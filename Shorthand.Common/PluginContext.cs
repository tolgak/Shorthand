using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PragmaTouchUtils;

namespace Shorthand.Common
{
  public class PluginContext : IPluginContext
  {
    //public MenuStrip MainMenu { get; set; }
    //public Form Host { get; set; }
    public ConfigContent Configuration { get; set; }
  }
}
