using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  public class LookupItem<T>
  {
    public string Name { get; set; }
    public T Value { get; set; }
  }


}
