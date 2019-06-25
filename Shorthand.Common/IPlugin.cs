using PragmaTouchUtils;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shorthand.Common
{
  public interface IPluginMarker
  {

  }

  public interface IPlugin : IPluginMarker
  {
    Form Initialize(IPluginContext context);
    void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e);
  }

  public interface IAsyncPlugin : IPluginMarker
  {
    Task<Form> InitializeAsync(IPluginContext context);
    void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e);
  }



}
