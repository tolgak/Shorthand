using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PragmaTouchUtils;

namespace Shorthand
{
  public class DeliveryToProduction : IDelivery
  {
    public void Deliver(Dictionary<string, string> references)
    {
      var jira = new Jira();
      
      var internalIssueKey = references["internalIssueKey"];
      var description = this.BuilDeploymentDescription(references);
      var summary = string.Format("Deploy {0}", internalIssueKey);
      var deploymentIssueKey = jira.CreateIssue("ARG", summary, description, "Task");
      references.Add("deploymentIssueKey", deploymentIssueKey);

      jira.CreateLink("Production", internalIssueKey, deploymentIssueKey);


      
    
    
      //this.DeployExecutables(references);         
    }


    private void DeployExecutables(Dictionary<string, string> references)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      var localBinPath = options.LocalBinPath;
      var zipFileName =  references["deploymentIssueKey"] + ".zip";
      var qualifiedZipFileName = Path.Combine(options.DestinationFolder,zipFileName);

      var startInfo = new ProcessStartInfo(options.ArchiveToolPath);
      startInfo.WorkingDirectory = localBinPath;
      var argumentToWinrar = string.Format("{0} {1} \"{2}\"", options.ArchiveToolSwitches, qualifiedZipFileName, "IBU.exe");
      startInfo. Arguments = argumentToWinrar;
      var p = Process.Start(startInfo);
      p.WaitForExit();

    }

    private string BuilDeploymentDescription(Dictionary<string, string> references)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      return new StringBuilder().AppendFormattedLine("{0}", references["internalIssueKey"])
                                .AppendFormattedLine("merge request http://sisgit.bilgi.networks/sofdev/{0}/merge_requests/{1}", references["gitProjectName"], references["gitMergeRequestNo"])
                                .AppendLine("{noformat}")
                                .AppendFormattedLine("{0}\\IBU-{1}.rar", options.DestinationFolder, references["requestIssueKey"])
                                .AppendLine("{noformat}")
                                .AppendLine("")
                                .AppendLine("Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                .AppendLine("Varsa sql script dosyaları pandora.ibu veritabanında çalıştırılacak.")
                                .ToString();
    }




    private string BuilComment(Dictionary<string, string> references)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      return new StringBuilder().AppendLine("DEPLOYMENT DESCRIPTION")
                                .AppendLine("----------------------------")
                                .AppendFormattedLine("{0}", references["internalIssueKey"])
                                .AppendFormattedLine("merge request http://sisgit.bilgi.networks/sofdev/{0}/merge_requests/{1}", references["gitProjectName"], references["gitMergeRequestNo"])
                                .AppendLine("{noformat}")
                                .AppendFormattedLine("{0}\\IBU-{1}.rar", options.DestinationFolder, references["requestIssueKey"])
                                .AppendLine("{noformat}")
                                .AppendLine("")
                                .AppendLine("Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                .AppendLine("Varsa sql script dosyaları pandora.ibu veritabanında çalıştırılacak.")
                                .AppendLine("")
                                .AppendLine("")
                                .AppendLine("GIT DESCRIPTION")
                                .AppendLine("----------------------------")
                                .AppendLine(references["internalIssueKey"])
                                .AppendLine(references["requestIssueKey"])
                                .AppendLine(references["uatIssueKey"])
                                .AppendLine(references["deploymentIssueKey"])
                                .ToString();
    }



  }
}
