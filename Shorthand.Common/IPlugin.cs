using PragmaTouchUtils;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shorthand.Common
{

  public interface IAsyncPlugin
  {
    Task<Form> InitializeAsync(IPluginContext context);
    void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e);
  }



}
