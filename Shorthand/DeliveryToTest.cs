using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PragmaTouchUtils;

namespace Shorthand
{
  public class DeliveryToTest : IDelivery
  {

    public void Deliver(Dictionary<string, string> references)
    {






    }

    public void Prepare(Dictionary<string, string> references)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      var newFileName = string.Format("IBU-{0}.exe", references["requestIssueKey"].Replace("-", " "));
      var qualifiedNewName = Path.Combine(options.TestDeliveryFolder, newFileName);
      var qualifiedOldName = Path.Combine(options.LocalBinPath, "IBU.exe");
      
      File.Copy(qualifiedOldName, qualifiedNewName, true);
    }

    public string BuilComment(Dictionary<string, string> references)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      var fileName = string.Format("IBU-{0}.exe", references["requestIssueKey"].Replace("-", " "));
      return new StringBuilder().AppendLine("TALEP COMMENT")
                                .AppendLine("----------------------------")
                                .AppendFormattedLine("İşlev *{0}\\{1}* uygulaması ile test edilebilir.", options.TestDeliveryFolder, fileName)
                                .AppendLine("")
                                .AppendLine("")
                                .ToString();
    }

  }
}
