using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

using PragmaTouchUtils;

namespace Shorthand
{

  internal static class Extensions
  {
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




  public partial class ucOptionEditorBase : UserControl
  {
    private object _underlyingOption;
    private string _cleanHash;

    public ucOptionEditorBase()
    {
      InitializeComponent();
    }

    protected ConfigContent _currentConfig;

    //public virtual bool Modified { get; set; }
    public virtual bool Modified => string.IsNullOrEmpty(_cleanHash) ? false : _cleanHash != _underlyingOption.GetMd5Hash();

    public bool ContentLoaded { get; set; }

    public string ItemClassName { get; set; }

    public string Caption { get; set; }

    public bool LoadContent()
    {
      _currentConfig = ConfigContent.Current;
      if ( _currentConfig == null )
        throw new Exception("Configuration content param is null!");

      _underlyingOption = this.LoadUnderlyingOption();
      _cleanHash = _underlyingOption.GetMd5Hash();

      this.ContentLoaded = true;
      return true;
    }

    protected virtual object LoadUnderlyingOption()
    {
      return null;      
    }

    public virtual bool SaveContent()
    {      
      _currentConfig?.SaveConfiguration(this.ItemClassName);
      _cleanHash = _underlyingOption.GetMd5Hash();
      return true;
    }

    public void ShowContent()
    {
      this.Show();
    }

    public void HideContent()
    {
      this.Hide();
    }

  }



}
