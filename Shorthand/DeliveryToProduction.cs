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
      this.DeployExecutables(ctx);
      this.CreateMR(ctx);
    }

    private void CreateMR(DeliveryContext ctx)
    {
      var git = new GitLab();

      int projectId = 53; // Bilgi.Sis.BackOffice
      var sourceBranch = $"feature/{ctx.InternalIssueKey}";
      var title = $"feature/{ctx.InternalIssueKey}";
      var description = this.BuildGitDescription(ctx);
      var assigneeId = "";

      var mr_id = git.CreateMergeRequest(projectId, sourceBranch, "master", title, description, assigneeId);
      ctx.GitMergeRequestNo = mr_id;
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
        jira.CreateLink("Production", ctx.InternalIssueKey, ctx.DeploymentIssueKey, "Deployment oluşturuldu");

        this.Log($"Deployment created : {ctx.DeploymentIssueKey}");
      }

      // advance workflow for internal issue
      var transitions = jira.GetTransitionsForIssue(ctx.InternalIssueKey);
      var q = transitions.FirstOrDefault(x => x.name == "Waiting for Production");
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

    private string BuildGitDescription(DeliveryContext ctx)
    {
      _logger?.Invoke($"\n* **Internal Issue :** {ctx.InternalIssueKey}\n* **Request Issue :** {ctx.RequestIssueKey}\n* **Deployment Issue :** {ctx.DeploymentIssueKey}\n* **Uat Issue :** {ctx.UatIssueKey}"
                       .Replace("\n", Environment.NewLine));

      return new StringBuilder().AppendFormattedLine("* **Internal Issue :** {0}", ctx.InternalIssueKey)
                                .AppendFormattedLine("* **Request Issue :** {0}", ctx.RequestIssueKey)
                                .AppendFormattedLine("* **Deployment Issue :** {0}", ctx.DeploymentIssueKey)
                                .AppendFormattedLine("* **Uat Issue :** {0}", ctx.UatIssueKey)
                                .ToString();
    }


    private void Log(string line)
    {
      _logger?.Invoke($"{DateTime.Now.ToString("dd.MM.yyyy HH: mm:ss")} {line}\n"
                       .Replace("\n", Environment.NewLine));
    }





  }
}
