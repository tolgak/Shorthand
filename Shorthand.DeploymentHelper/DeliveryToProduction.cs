using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using PragmaTouchUtils;
using System.Reflection;

namespace Shorthand
{
  public class DeliveryToProduction : IDelivery
  {
    private JiraOptions       _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
    private DeploymentOptions _dplyOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
    private Action<string>    _logger;

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
      this.CreateMergeRequest(ctx);
    }

    private void CreateMergeRequest(DeliveryContext ctx)
    {
      if (!ctx.CreateMergeRequest)
      {
        this.Log("Skipped creating merge request");
        return;
      }

      this.Log("Creating merge request");
      var git = new GitLab();
            
      int projectId = ctx.GitProjectId;
      var sourceBranch = $"feature/{ctx.InternalIssueKey}";
      var title = $"feature/{ctx.InternalIssueKey}";
      var description = this.BuildGitDescription(ctx);
      var assigneeId = "";

      ctx.GitMergeRequestNo = git.CreateMergeRequest(projectId, sourceBranch, "master", title, description, assigneeId);
    }

    private void PrepareJira(DeliveryContext ctx)
    {
      if (!ctx.CreateDeploymentIssue)
      {
        this.Log("Skipped creating deployment issue");
        return;
      }

      this.Log("Creating deployment issue");
      if (string.IsNullOrEmpty(ctx.RequestIssueKey))
        throw new ArgumentNullException("RequestIssueKey", "Context does not contain a request issue key.");

      if (string.IsNullOrEmpty(ctx.InternalIssueKey))
        throw new ArgumentNullException("InternalIssueKey", "Context does not contain an internal issue key.");

      var jira = new Jira();
      
      // create deployment issue if it does not exist
      if (string.IsNullOrEmpty(ctx.DeploymentIssueKey))
      {
        var summary = $"Deploy {ctx.InternalIssueKey}";
        ctx.DeploymentIssueKey = jira.CreateIssue(_jiraOptions.DPLY_ProjectKey, summary, "", "Task");

        var description = this.BuilDeploymentDescription(ctx);
        jira.SetDescription(ctx.DeploymentIssueKey, description);

        // link internal issue to deployment issue
        jira.CreateLink("Production", ctx.DeploymentIssueKey, ctx.InternalIssueKey, "Deployment oluşturuldu");

        this.Log($"Deployment created : {ctx.DeploymentIssueKey}");
      }

      // attach sql script file to deployment issue
      var sqlFilePath = Directory.GetFiles(_dplyOptions.LocalBinPath + @"\sql", ctx.InternalIssueKey + ".sql").FirstOrDefault();
      if ( !string.IsNullOrEmpty(sqlFilePath) )
        jira.AddAttachment(ctx.DeploymentIssueKey, sqlFilePath);

      // advance workflow for internal issue
      var q1 = jira.GetTransitionsForIssue(ctx.InternalIssueKey).FirstOrDefault(x => x.name == "Waiting for Production");
      if (q1 != null)
        jira.SetTransitionForIssue(ctx.InternalIssueKey, q1.id);

      // advance workflow for deployment issue
      var q2 = jira.GetTransitionsForIssue(ctx.DeploymentIssueKey).FirstOrDefault(x => x.name == "Waiting for Production");
      if (q2 != null)
        jira.SetTransitionForIssue(ctx.DeploymentIssueKey, q2.id);
    }

    private void DeployExecutables(DeliveryContext ctx)
    {
      if (!ctx.CopyExecutables)
      {
        this.Log("Skipped copying executables");
        return;
      }

      this.Log("Copying executables");

      var tempFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var nf = Directory.CreateDirectory(tempFolder + @"\shorthand_" + DateTime.Now.ToString("yyyyMMddHHmm"));
      var destinationFolder = Path.Combine(tempFolder, nf.Name);

      try
      {
        var files = new List<string>();
        //files.AddRange(Directory.GetFiles($"{_dplyOptions.LocalBinPath}\\sql", ctx.InternalIssueKey + ".sql"));
        if (ctx.CopyExecutables)
          files.AddRange(Directory.GetFiles($"{_dplyOptions.LocalBinPath}\\exe", "*.exe"));
        else
          this.Log("will not include executables");

        if (files.Count() == 0)
          return;

        foreach (string file in files)
        {
          var destinationfile = $"{destinationFolder}\\{Path.GetFileName(file)}";
          File.Copy(file, destinationfile, true);
        }

        var zipFileName = $"{ctx.DeploymentIssueKey}.zip";
        var qualifiedZipFileName = destinationFolder + zipFileName;

        var startInfo = new ProcessStartInfo(_dplyOptions.ArchiveToolPath);
        startInfo.WorkingDirectory = destinationFolder;
        startInfo.Arguments = $"{_dplyOptions.ArchiveToolSwitches} {qualifiedZipFileName} *.*";
        var p = Process.Start(startInfo);
        p.WaitForExit();

        File.Copy(qualifiedZipFileName, $"{_dplyOptions.ProductionDeliveryFolder}\\{zipFileName}", true);
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
                                .AppendConditionally (ctx.CopyExecutables, $"merge request {ctx.GitProjectWebUrl}/sofdev/{ctx.GitProjectName}/merge_requests/{ctx.GitMergeRequestNo}")
                                .AppendLine("{noformat}")
                                .AppendConditionally( !ctx.CopyExecutables, "Bu iş için exe kopyalanmasına gerek yok.")
                                .AppendConditionally( ctx.CopyExecutables, $"{options.ProductionDeliveryFolder}\\{deploymentIssueKey}.zip")
                                .AppendConditionally( ctx.CopyExecutables, "Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                .AppendLine("İşe ekli sql script dosyaları varsa pandora.ibu veritabanında çalıştırılacak.")
                                .AppendLine("{noformat}")
                                .ToString();
    }

    private string BuildGitDescription(DeliveryContext ctx)
    {
      _logger?.Invoke($"\n* **Internal Issue :** {ctx.InternalIssueKey}\n* **Request Issue :** {ctx.RequestIssueKey}\n* **Deployment Issue :** {ctx.DeploymentIssueKey}\n* **Uat Issue :** {ctx.UatIssueKey}"
              .Replace("\n", Environment.NewLine));

      return new StringBuilder().AppendLine($"* **Internal Issue :** {ctx.InternalIssueKey}" )
                                .AppendLine($"* **Request Issue :** {ctx.RequestIssueKey}" )
                                .AppendLine($"* **Deployment Issue :** {ctx.DeploymentIssueKey}" )
                                .AppendLine($"* **Uat Issue :** {ctx.UatIssueKey}" )
                                .ToString();
    }

    private void Log(string line)
    {
      _logger?.Invoke(line);
    }





  }
}
