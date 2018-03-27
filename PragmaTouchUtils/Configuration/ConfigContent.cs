/********************************************************************
  Class      : ConfigurationContent
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;


namespace PragmaTouchUtils
{
  [Serializable]
  public class ConfigContent
  {
    public static string ApplicationName { get; set; }

    private static ConfigContent _current;
    public static ConfigContent Current
    { 
      get 
      {
        _current = _current ?? new ConfigContent();
        return _current;
      } 
    }



    private Dictionary<string, object> _preferences = new Dictionary<string, object>();
    
    public string DefaultFileName {get; set;}

    public string UserDataDirectory { get; set; }
  
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
      _preferences.TryGetValue(className, out var item);
      return item;
    }

    public void LoadConfiguration()
    {
      var items = AppDomain.CurrentDomain.GetAssemblies()
                           .SelectMany(s => s.GetTypes())
                           .Where(p => Attribute.IsDefined(p, typeof(ConfigContentItemAttribute)));

      foreach ( var e in items )
      {
        if (_preferences.ContainsKey(e?.Name))
          continue;
        
        string prefPath = $"{this.UserDataDirectory}\\{e.Name}.options";
        var value = File.Exists(prefPath) 
                  ? this.LoadFromDocumentFormat(e, prefPath)
                  : Activator.CreateInstance(e.Assembly.GetName().Name, e.FullName).Unwrap();

        _preferences.Add(e.Name, value);
      }
    }

    public void SaveConfiguration(string key)
    {
      if (!_preferences.TryGetValue(key, out object item))
        return;

      string prefPath = $"{this.UserDataDirectory}\\{key}.options";
      this.SaveToDocumentFormat(item, prefPath);
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
        var xmlSerializer = new XmlSerializer(type);
        return xmlSerializer.Deserialize(textReader);
      }
    }

  }


}
