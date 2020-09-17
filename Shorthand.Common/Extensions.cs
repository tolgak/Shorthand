using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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

  public enum RestMethod
  {
    POST,
    GET,
    PUT,
    DELETE
  }

  public static class Extensions
  {
    public static StringBuilder AppendConditionally(this StringBuilder sb, bool predicate, string text)
    {
      return predicate ? sb.Append($"{text} {Environment.NewLine}") : sb;
    }

    public static string AggregateExceptionMessages(this Exception ex)
    {
      var result = $"{ex.Source} {ex.Message}";
      if (ex.InnerException != null)
        result = $"{result}\r\n{ex.InnerException.AggregateExceptionMessages()}";

      return result;
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

    public static bool IsJson(this string str)
    {
      return (str.StartsWith("{") && str.EndsWith("}"))
          || (str.StartsWith("[") && str.EndsWith("]"));
    }

    public static string ToTidyString(this string str)
    {
      if (string.IsNullOrWhiteSpace(str))
        return string.Empty;

      var x = str.Replace("\n", string.Empty);
      return Regex.Replace(x, "\\s+", " ").Trim();
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
