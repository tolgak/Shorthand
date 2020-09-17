using PragmaTouchUtils;
using Shorthand.GitLabEntity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shorthand
{
  public class DeliveryToProduction : IDelivery
  {
    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
    private DeploymentOptions _dplyOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
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
      this.CreateMergeRequest(ctx);
      this.PrepareJira(ctx);
      this.DeployExecutables(ctx);
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
      var sourceBranch = $"feature/{ctx.InternalIssue}";
      var title = $"feature/{ctx.InternalIssue}";
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
      if (string.IsNullOrEmpty(ctx.RequestIssue))
        throw new ArgumentNullException("RequestIssueKey", "Context does not contain a request issue key.");

      if (string.IsNullOrEmpty(ctx.InternalIssue))
        throw new ArgumentNullException("InternalIssueKey", "Context does not contain an internal issue key.");

      var jira = new Jira();
      // check if deployment has sql script file
      var sqlFilePath = Directory.GetFiles($"{ctx.LocalBinFolder}\\sql", $"{ctx.InternalIssue}.sql").FirstOrDefault();
      ctx.HasSqlScript = !string.IsNullOrEmpty(sqlFilePath);

      // create deployment issue if it does not exist
      if (string.IsNullOrEmpty(ctx.DeploymentIssue))
      {
        var summary = $"Deploy {ctx.InternalIssue}";
        ctx.DeploymentIssue = jira.CreateIssue(_jiraOptions.DPLY_ProjectKey, summary, "", "Task");

        var description = this.BuilDeploymentDescription(ctx);
        jira.SetDescription(ctx.DeploymentIssue, description);

        // link internal issue to deployment issue
        jira.CreateLink("Production", ctx.DeploymentIssue, ctx.InternalIssue, "Deployment oluşturuldu");
        jira.CreateLink("Production", ctx.DeploymentIssue, ctx.RequestIssue, "Deployment oluşturuldu");
        jira.CreateLink("UAT", ctx.DeploymentIssue, ctx.UatIssue, "Deployment oluşturuldu");

        this.Log($"Deployment created : {ctx.DeploymentIssue}");
      }

      // attach sql script file to deployment issue
      if (ctx.HasSqlScript)
        jira.AddAttachment(ctx.DeploymentIssue, sqlFilePath);

      // advance workflow for internal issue
      var q1 = jira.GetTransitionsForIssue(ctx.InternalIssue).FirstOrDefault(x => x.name == "Waiting for Production");
      if (q1 != null)
        jira.SetTransitionForIssue(ctx.InternalIssue, q1.id);

      // advance workflow for deployment issue
      var q2 = jira.GetTransitionsForIssue(ctx.DeploymentIssue).FirstOrDefault(x => x.name == "Waiting for Production");
      if (q2 != null)
        jira.SetTransitionForIssue(ctx.DeploymentIssue, q2.id);
    }

    public void DeployExecutables(DeliveryContext ctx)
    {
      if (!ctx.CopyExecutables)
      {
        this.Log("Skipped copying executables");
        return;
      }

      var files = Directory.GetFiles($"{ctx.LocalBinFolder}\\exe", "*.exe").ToList();
      if (files.Count() == 0)
        return;

      this.Log("Copying executables");

      var tempFolder = Path.GetTempPath();
      var destinationFolder = Directory.CreateDirectory($"{tempFolder}\\shorthand_{DateTime.Now.ToString("yyyyMMddHHmm")}");
      var qualifiedZipFileName = $"{tempFolder}{ctx.DeploymentIssue}.zip";

      try
      {
        files.ForEach(x => File.Copy(x, Path.Combine(destinationFolder.FullName, Path.GetFileName(x)), true));
        
        ZipFile.CreateFromDirectory(destinationFolder.FullName, qualifiedZipFileName);
        File.Copy(qualifiedZipFileName, $"{ctx.DeliveryProductionFolder}\\{ctx.DeploymentIssue}.zip", true);
      }
      finally
      {
        File.Delete(qualifiedZipFileName);
        Directory.Delete(destinationFolder.FullName, true);
      }
    }

    private string BuilDeploymentDescription(DeliveryContext ctx)
    {     
      return new StringBuilder().AppendLine(ctx.InternalIssue)
                                .AppendLine(ctx.RequestIssue)
                                .AppendConditionally(ctx.HasSoxWorkflow, ctx.UatIssue)
                                .AppendConditionally(!ctx.HasSoxWorkflow, "{color:red}SOX kapsamı dışında{color} olduğu için UAT *YOK*")

                                .AppendConditionally(ctx.CopyExecutables, $"merge request {ctx.GitProjectWebUrl}/merge_requests/{ctx.GitMergeRequestNo}")
                                .AppendConditionally(ctx.CopyExecutables, $"{{noformat}}{ctx.DeliveryProductionFolder}\\{ctx.DeploymentIssue}.zip{{noformat}}")
                                .AppendConditionally(ctx.CopyExecutables, "Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                .AppendConditionally(!ctx.CopyExecutables, "Bu iş için uygulama kopyalamaya gerek *YOK*")

                                .AppendConditionally(ctx.HasSqlScript, $"İşe ekli sql script dosyaları {ctx.Database} veritabanında çalıştırılacak.")
                                .AppendConditionally(!ctx.HasSqlScript, "İşe ekli sql script dosyası *YOK*")

                                .ToString();
    }

    private string BuildGitDescription(DeliveryContext ctx)
    {
      var description = new StringBuilder().AppendLine($"* **Internal Issue :** {ctx.InternalIssue}")
                                           .AppendLine($"* **Request Issue :** {ctx.RequestIssue}")
                                           .AppendLine($"* **Uat Issue :** {ctx.UatIssue}")
                                           .AppendConditionally(ctx.HasSoxWorkflow, $"* **Deployment Issue :** {ctx.DeploymentIssue}")
                                           .ToString();
      _logger?.Invoke(description);
      return description;
    }

    private void Log(string line)
    {
      _logger?.Invoke(line);
    }





  }
}
