using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shorthand
{
  public enum DeliverTo { None, Production, Test }

  public static class Extensions
  {  
    public static StringBuilder AppendFormattedLine(this StringBuilder sb, string format, params object[] args)
    {
      return sb.AppendFormat(format + "\r\n", args);
    }

    public delegate void InvokeIfRequiredDelegate<T>(T obj) where T : ISynchronizeInvoke;

    public static void InvokeIfRequired<T>(this T obj, InvokeIfRequiredDelegate<T> action) where T : ISynchronizeInvoke
    {
      if (obj.InvokeRequired)      
        obj.Invoke(action, new object[] { obj });      
      else      
        action(obj);      
    } 

  }


}
