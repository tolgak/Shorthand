using System;
using System.Windows.Forms;
using PragmaTouchUtils;

namespace Shorthand.Common
{
  public interface IPluginHost
  {
    event Action<object, ConfigEventArgs> onSettingsChanged;
  }
}
