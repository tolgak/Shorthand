using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PragmaTouchUtils;
using System.Reflection;
using System.Net.Mail;

namespace Shorthand
{
  public class DeliveryToProduction : IDelivery
  {
    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
    private Action<string> _logger;

    public DeliveryToProduction()
    {

    }

    public DeliveryToProduction(Action<string> logger) : this() 
    {
      _logger = logger;
    }


    public void Deliver(DeliveryContext ctx)
    {
      this.PrepareJira(ctx);
      _logger?.Invoke( $"\n* **Internal Issue :** {ctx.InternalIssueKey}\n* **Request Issue :** {ctx.RequestIssueKey}\n* **Deployment Issue :** {ctx.DeploymentIssueKey}\n* **Uat Issue :** {ctx.UatIssueKey}"
                       .Replace("\n", Environment.NewLine));

      this.DeployExecutables(ctx);
    }

    private void PrepareJira(DeliveryContext ctx)
    {
      var jira = new Jira();

      // create deployment issue if it does not exist
      if (string.IsNullOrEmpty(ctx.DeploymentIssueKey))
      {
        var summary = $"Deploy {ctx.InternalIssueKey}";
        ctx.DeploymentIssueKey = jira.CreateIssue(_jiraOptions.DPLY_ProjectKey, summary, "", "Task");

        var description = this.BuilDeploymentDescription(ctx);
        jira.SetDescription(ctx.DeploymentIssueKey, description);

        // link internal issue to deployment issue
        jira.CreateLink("Production", ctx.InternalIssueKey, ctx.DeploymentIssueKey);
      }

      // advance workflow for internal issue
      var transitions = jira.GetTransitionsForIssue(ctx.InternalIssueKey);
      var q = transitions.FirstOrDefault(x => x.name == "Waiting For Production");
      if (q != null)
        jira.SetTransitionForIssue(ctx.InternalIssueKey, q.id);          
    }

    private void DeployExecutables(DeliveryContext ctx)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      var tempFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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
        startInfo.Arguments = $"{options.ArchiveToolSwitches} {qualifiedZipFileName} *.*";
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
                                .AppendFormattedLine("merge request http://sisgit.bilgi.networks/sofdev/{0}/merge_requests", ctx.GitProjectName)
                                .AppendLine("{noformat}")
                                .AppendFormattedLine("{0}\\{1}.rar", options.ProductionDeliveryFolder, deploymentIssueKey)                                                                
                                .AppendLine("Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                .AppendLine("Varsa sql script dosyaları pandora.ibu veritabanında çalıştırılacak.")
                                .AppendLine("{noformat}")
                                .ToString();
    }

    private string BuildGitComment(DeliveryContext ctx)
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


    private void Log(string line)
    {
      _logger?.Invoke($"{DateTime.Now.ToString("dd.MM.yyyy HH: mm:ss")} {line}\n"
                       .Replace("\n", Environment.NewLine));
    }





  }
}
