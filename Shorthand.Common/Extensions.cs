using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Shorthand
{

  public enum DeliverTo { None, Production, Test }

  public class ApiMethod
  {
    public const string POST = "POST";
    public const string GET = "GET";
    public const string PUT = "PUT";
    public const string DELETE = "DELETE";
  }

  public static class Extensions
  {  
    public static StringBuilder AppendFormattedLine(this StringBuilder sb, string format, params object[] args)
    {
      return sb.AppendFormat(format + "\r\n", args);
    }

    public static StringBuilder AppendConditionally(this StringBuilder sb, bool predicate, string text)
    {
      return predicate ? sb.Append(text) : sb;
    }

    public delegate void InvokeIfRequiredDelegate<T>(T obj) where T : ISynchronizeInvoke;

    public static void InvokeIfRequired<T>(this T obj, InvokeIfRequiredDelegate<T> action) where T : ISynchronizeInvoke
    {
      if (obj.InvokeRequired)      
        obj.Invoke(action, new object[] { obj });      
      else      
        action(obj);      
    }

    public static string AsJson(this object data)
    {
      return JsonConvert.SerializeObject(data);
    }


    public static string GetMd5Hash(this object o)
    {
      if (o == null)
        return string.Empty;

      try
      {
        using (var ms = new MemoryStream())
        {
          var b = new BinaryFormatter();
          b.Serialize(ms, o);
          return GetMd5Sum(ms.ToArray());
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Cannot calculate hash of the object.", ex);
      }
    }

    private static string GetMd5Sum(byte[] buffer)
    {
      if (buffer == null)
        return string.Empty;

      byte[] result = MD5CryptoServiceProvider.Create().ComputeHash(buffer);

      var sb = new StringBuilder();
      for (int i = 0; i < result.Length; i++)
        sb.Append(result[i].ToString("X2"));

      return sb.ToString();
    }







  }

}
