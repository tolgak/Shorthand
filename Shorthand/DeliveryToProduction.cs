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
    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;

    public void Deliver(Dictionary<string, string> references)
    {
      var jira = new Jira();

      var internalIssueKey = references["internalIssueKey"];
      var requestIssueKey = references["requestIssueKey"];
      var deploymentIssueKey = references["deploymentIssueKey"];

      if ( string.IsNullOrEmpty(deploymentIssueKey) )
      {        
        var description = this.BuilDeploymentDescription(references);
        var summary = string.Format("Deploy {0}", internalIssueKey);
        deploymentIssueKey = jira.CreateIssue(_jiraOptions.DPLY_ProjectKey, summary, description, "Task");
        references["deploymentIssueKey"] = deploymentIssueKey;
      }
      jira.CreateLink("Production", internalIssueKey, deploymentIssueKey);



      var uatIssueKey = references["uatIssueKey"];
      if ( string.IsNullOrEmpty(uatIssueKey) )
      {
        var description = this.BuildUATDescription(references);
        var summary = string.Format("UAT for {0}", requestIssueKey);
        uatIssueKey = jira.CreateIssue(_jiraOptions.UAT_ProjectKey, summary, description, "Task");
        references["uatIssueKey"] = uatIssueKey;
      }
      jira.CreateLink("UAT", uatIssueKey, requestIssueKey);
    
    
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
      return new StringBuilder().AppendLine(references["internalIssueKey"])
                                .AppendFormattedLine("merge request http://sisgit.bilgi.networks/sofdev/{0}/merge_requests/{1}", references["gitProjectName"], references["gitMergeRequestNo"])
                                .AppendLine("{noformat}")
                                .AppendFormattedLine("{0}\\IBU-{1}.rar", options.DestinationFolder, references["requestIssueKey"].Replace("-"," "))
                                .AppendLine("{noformat}")
                                .AppendLine("")
                                .AppendLine("Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                .AppendLine("Varsa sql script dosyaları pandora.ibu veritabanında çalıştırılacak.")
                                .ToString();
    }

    private string BuildUATDescription(Dictionary<string, string> references)
    {
      return new StringBuilder().AppendLine("*Test Adımları*")
                                .AppendLine("")
                                .ToString();
    }




    private string BuilComment(Dictionary<string, string> references)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      return new StringBuilder().AppendLine("GIT DESCRIPTION")
                                .AppendLine("----------------------------")
                                .AppendLine(references["internalIssueKey"])
                                .AppendLine(references["requestIssueKey"])
                                .AppendLine(references["uatIssueKey"])
                                .AppendLine(references["deploymentIssueKey"])
                                .ToString();
    }



  }
}
