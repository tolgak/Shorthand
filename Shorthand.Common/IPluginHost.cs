using System;

using PragmaTouchUtils;

namespace Shorthand.Common
{
  public interface IPluginHost
  {
    event Action<object, ConfigEventArgs> onSettingsChanged;
  }
}
