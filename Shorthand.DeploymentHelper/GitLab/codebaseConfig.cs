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

    [JsonProperty("delphiBuilderService")]
    public DelphiBuilderService DelphiBuilderService { get; set; }


    [JsonProperty("applications")]
    public Application[] Applications { get; set; }


    public static CodebaseConfig FromJson(string json) => JsonConvert.DeserializeObject<CodebaseConfig>(json, Converter.Settings);
  }

  public partial class DelphiBuilderService
  {
    [JsonProperty("hostname")]
    public string HostName { get; set; }

    [JsonProperty("port")]
    public string Port { get; set; }
  }


  public partial class Application
  {
    [JsonProperty("codebase")]
    public string Codebase { get; set; }

    [JsonProperty("database")]
    public string Database { get; set; }

    [JsonProperty("gitOptions")]
    public GitOptions GitProperties { get; set; }

    [JsonProperty("hasSoxWorkflow")]
    public bool hasSoxWorkflow { get; set; }

    [JsonProperty("localBinFolder")]
    public string LocalBinFolder { get; set; }

    [JsonProperty("deliveryTestFolder")]
    public string DeliveryTestFolder { get; set; }

    [JsonProperty("deliveryProductionFolder")]
    public string DeliveryProductionFolder { get; set; }
  }

  public partial class GitOptions
  {
    [JsonProperty("projectName")]
    public string ProjectName { get; set; }

    [JsonProperty("projectId")]
    public int ProjectId { get; set; }
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





}



