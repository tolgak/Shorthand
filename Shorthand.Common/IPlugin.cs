using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand.Common
{
  public interface IPlugin
  {

    //string PluginName { get; set; }
    void Initialize(IPluginContext context);


  }
}
