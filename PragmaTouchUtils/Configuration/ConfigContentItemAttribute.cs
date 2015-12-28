using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PragmaTouchUtils
{
  [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
  public sealed class ConfigContentItemAttribute : Attribute
  {
    public ConfigContentItemAttribute()
    {
    }

  }


}
