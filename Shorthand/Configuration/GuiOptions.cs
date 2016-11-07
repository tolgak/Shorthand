using PragmaTouchUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  [ConfigContentItem]
  [Serializable]

  public class GuiOptions
  {
    public int Width { get; set; }
    public int Height { get; set; }
  }

}
