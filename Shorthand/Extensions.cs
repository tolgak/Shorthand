using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  public enum DeliverTo { None, Production, Test }

  public static class Extensions
  {  
    public static StringBuilder AppendFormattedLine(this StringBuilder sb, string format, params object[] args)
    {
      return sb.AppendFormat(format + "\r\n", args);
    }

  }


}
