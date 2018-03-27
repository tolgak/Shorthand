using System.Threading.Tasks;


namespace Shorthand.Common
{
  public interface IPluginMarker
  {

  }

  public interface IPlugin : IPluginMarker
  {
    void Initialize(IPluginContext context);
  }

  public interface IAsyncPlugin : IPluginMarker
  {
    Task InitializeAsync(IPluginContext context);
  }



}
