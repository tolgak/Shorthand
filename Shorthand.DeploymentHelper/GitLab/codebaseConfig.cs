using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Shorthand
{

  internal static class Converter
  {
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
      MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
      DateParseHandling = DateParseHandling.None,
      Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
  }

  public partial class CodebaseConfig
  {
    [JsonProperty("archiveTool")]
    public ArchiveTool ArchiveTool { get; set; }

    [JsonProperty("applications")]
    public Application[] Applications { get; set; }

    public static CodebaseConfig FromJson(string json) => JsonConvert.DeserializeObject<CodebaseConfig>(json, Converter.Settings);
  }

  public partial class Application
  {
    [JsonProperty("codebase")]
    public string Codebase { get; set; }

    [JsonProperty("database")]
    public string Database { get; set; }

    [JsonProperty("git")]
    public Git Git { get; set; }

    [JsonProperty("localBinFolder")]
    public string LocalBinFolder { get; set; }

    [JsonProperty("deliveryTestFolder")]
    public string DeliveryTestFolder { get; set; }

    [JsonProperty("deliveryProductionFolder")]
    public string DeliveryProductionFolder { get; set; }
  }

  public partial class Git
  {
    [JsonProperty("projectName")]
    public string ProjectName { get; set; }

    [JsonProperty("projectId")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long ProjectId { get; set; }

    [JsonProperty("deploymentDescriptionTemplate")]
    public string DeploymentDescriptionTemplate { get; set; }
  }

  public partial class ArchiveTool
  {
    [JsonProperty("path")]
    public string Path { get; set; }

    [JsonProperty("switches")]
    public string Switches { get; set; }
  }


  public static class Serialize
  {
    public static string ToJson(this CodebaseConfig self) => JsonConvert.SerializeObject(self, Converter.Settings);
  }



  internal class ParseStringConverter : JsonConverter
  {
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null) return null;
      var value = serializer.Deserialize<string>(reader);
      long l;
      if (Int64.TryParse(value, out l))
      {
        return l;
      }
      throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
      if (untypedValue == null)
      {
        serializer.Serialize(writer, null);
        return;
      }
      var value = (long)untypedValue;
      serializer.Serialize(writer, value.ToString());
      return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
  }



}



