using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PragmaTouchUtils
{
  public class ConfigEventArgs : EventArgs
  {
    public ConfigAction action = ConfigAction.None;
    public ConfigContent content = null;
    public IList<string> ChangedOptions = new List<string>();
  }

  public delegate void ConfigFinalSelectionEventHandler(object sender, ConfigEventArgs e);


}
