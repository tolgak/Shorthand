/********************************************************************
  Class      : ConfigurationContent
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.Runtime.Remoting;


namespace PragmaTouchUtils
{
  [Serializable]
  public class ConfigContent
  {
    public static string ApplicationName { get; set; }
    private Dictionary<string, object> _preferences = new Dictionary<string, object>();
    
    public string DefaultFileName {get; set;}
    public string UserDataDirectory { get; set; }

    private static ConfigContent _current;
    public static ConfigContent Current 
    { 
      get 
      {
        if ( _current == null )
          _current = new ConfigContent();

        return _current;
      } 
    }
  
    public ConfigContent()
    {
      string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
      this.UserDataDirectory = Path.Combine(path, ConfigContent.ApplicationName);

      if ( !Directory.Exists(this.UserDataDirectory) )
        Directory.CreateDirectory(this.UserDataDirectory);

      this.DefaultFileName = $"{this.UserDataDirectory}\\{ConfigContent.ApplicationName}.options";
    }

    public object GetConfigContentItem(string className)
    {
      object item = null;
      _preferences.TryGetValue(className, out item);
      return item;
    }

    public void LoadConfiguration()
    {
      var items = AppDomain.CurrentDomain.GetAssemblies().ToList()
                 .SelectMany(s => s.GetTypes())
                 .Where(p => Attribute.IsDefined(p, typeof(ConfigContentItemAttribute)));

      foreach ( var e in items )
      {
        if ( e != null && !_preferences.ContainsKey(e.Name))
        {
          object value = null;
          string prefPath = $"{this.UserDataDirectory}\\{e.Name}.options";
          if ( File.Exists(prefPath) )
            value = this.LoadFromDocumentFormat(e, prefPath);
          else
            value = Activator.CreateInstance(e.Assembly.GetName().Name, e.FullName).Unwrap();

          _preferences.Add(e.Name, value);
        }
      }

    }




    public void SaveConfiguration()
    {
      foreach ( var item in _preferences )
      {
        string prefPath = string.Format("{0}\\{1}.options", this.UserDataDirectory, item.Key);
        this.SaveToDocumentFormat(item.Value, prefPath);                
      }
    }


    private void SaveToDocumentFormat(object serializableObject, string path)
    {
      using ( TextWriter textWriter = new StreamWriter(path) )
      {
        XmlSerializer xmlSerializer = new XmlSerializer(serializableObject.GetType());
        xmlSerializer.Serialize(textWriter, serializableObject);
      }
    }

    private object LoadFromDocumentFormat(Type type, string path)
    {
      using ( TextReader textReader = new StreamReader(path) )
      {
        XmlSerializer xmlSerializer = new XmlSerializer(type);
        return xmlSerializer.Deserialize(textReader);
      }
    }



  }
}
