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

    public void Deliver(DeliveryContext ctx)
    {
      //this.PrepareJira(ctx);
      this.DeployExecutables(ctx);         
    }

    private void PrepareJira(DeliveryContext ctx)
    {
      var jira = new Jira();

      var internalIssueKey = ctx.InternalIssueKey;
      var requestIssueKey = ctx.RequestIssueKey;
      var deploymentIssueKey = ctx.DeploymentIssueKey;

      if (string.IsNullOrEmpty(deploymentIssueKey))
      {
        var summary = string.Format("Deploy {0}", internalIssueKey);
        deploymentIssueKey = jira.CreateIssue(_jiraOptions.DPLY_ProjectKey, summary, "", "Task");
        ctx.DeploymentIssueKey = deploymentIssueKey;

        var description = this.BuilDeploymentDescription(ctx);
        jira.SetDescription(deploymentIssueKey, description);
        jira.CreateLink("Production", internalIssueKey, deploymentIssueKey);
      }

      var uatIssueKey = ctx.UatIssueKey;
      if (string.IsNullOrEmpty(uatIssueKey))
      {
        var summary = string.Format("UAT for {0}", requestIssueKey);
        var description = this.BuildUATDescription(ctx);
        uatIssueKey = jira.CreateIssue(_jiraOptions.UAT_ProjectKey, summary, description, "Task");
        ctx.UatIssueKey = uatIssueKey;

        jira.CreateLink("UAT", uatIssueKey, requestIssueKey);
      }
      
    }

    private void DeployExecutables(DeliveryContext ctx)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      var tempFolder = Path.GetTempPath();
      var nf = Directory.CreateDirectory(tempFolder + @"\shorthand_" + DateTime.Now.ToString("yyyyMMddHHmm"));
      var destinationFolder = Path.Combine(tempFolder, nf.Name);

      try
      {
        var files = new List<string>();
        files.AddRange(Directory.GetFiles(options.LocalBinPath + @"\exe", "*.exe"));
        files.AddRange(Directory.GetFiles(options.LocalBinPath + @"\sql", ctx.InternalIssueKey + ".sql"));
        foreach (string file in files)
        {
          var destinationfile = destinationFolder + "\\" + Path.GetFileName(file);
          File.Copy(file, destinationfile, true);
        }

        var zipFileName = ctx.DeploymentIssueKey + ".zip";
        var qualifiedZipFileName = destinationFolder + zipFileName;

        var startInfo = new ProcessStartInfo(options.ArchiveToolPath);
        startInfo.WorkingDirectory = destinationFolder;
        var argumentToWinrar = string.Format("{0} {1} \"{2}\"", options.ArchiveToolSwitches, qualifiedZipFileName, "*.*");
        startInfo.Arguments = argumentToWinrar;
        var p = Process.Start(startInfo);
        p.WaitForExit();

        File.Copy(qualifiedZipFileName, options.ProductionDeliveryFolder + "\\" + zipFileName, true);
      }
      finally
      {
        Directory.Delete(destinationFolder, true);
      }
    }

    private string BuilDeploymentDescription(DeliveryContext ctx)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      var deploymentIssueKey = ctx.DeploymentIssueKey;
      return new StringBuilder().AppendLine(ctx.InternalIssueKey)
                                //.AppendFormattedLine("merge request http://sisgit.bilgi.networks/sofdev/{0}/merge_requests/{1}", ctx.GitProjectName, ctx.GitMergeRequestNo)
                                .AppendLine("{noformat}")
                                .AppendFormattedLine("{0}\\{1}.rar", options.ProductionDeliveryFolder, deploymentIssueKey)
                                .AppendLine("{noformat}")
                                .AppendLine("")
                                .AppendLine("Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                .AppendLine("Varsa sql script dosyaları pandora.ibu veritabanında çalıştırılacak.")
                                .ToString();
    }

    private string BuildUATDescription(DeliveryContext ctx)
    {
      return new StringBuilder().AppendLine("*Test Adımları*")
                                .AppendLine("")
                                .ToString();
    }

    private string BuildComment(DeliveryContext ctx)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      return new StringBuilder().AppendLine("GIT DESCRIPTION")
                                .AppendLine("----------------------------")
                                .AppendLine(ctx.InternalIssueKey)
                                .AppendLine(ctx.RequestIssueKey)
                                .AppendLine(ctx.UatIssueKey)
                                .AppendLine(ctx.DeploymentIssueKey)
                                .ToString();
    }







  }
}
