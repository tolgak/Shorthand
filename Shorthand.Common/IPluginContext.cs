using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PragmaTouchUtils;

namespace Shorthand.Common
{
  public interface IPluginContext
  {
    //MenuStrip MainMenu{ get; set; }
    Form Host { get; set; }
    ConfigContent Configuration { get; set; }
  }
}
