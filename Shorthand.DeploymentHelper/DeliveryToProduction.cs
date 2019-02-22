using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using PragmaTouchUtils;
using System.Reflection;
using Shorthand.GitLabEntity;

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
            var sqlFilePath = Directory.GetFiles($"{_dplyOptions.LocalBinPath}\\sql", $"{ctx.InternalIssue}.sql").FirstOrDefault();
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

                var zipFileName = $"{ctx.DeploymentIssue}.zip";
                var qualifiedZipFileName = destinationFolder + zipFileName;

                var startInfo = new ProcessStartInfo(_dplyOptions.ArchiveToolPath)
                {
                    WorkingDirectory = destinationFolder,
                    Arguments = $"{_dplyOptions.ArchiveToolSwitches} {qualifiedZipFileName} *.*"
                };
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
            return new StringBuilder().AppendLine(ctx.InternalIssue)
                                      .AppendLine(ctx.RequestIssue)
                                      .AppendLine(ctx.UatIssue)
                                      .AppendConditionally(ctx.CopyExecutables, $"merge request {ctx.GitProjectWebUrl}/merge_requests/{ctx.GitMergeRequestNo}")

                                      .AppendConditionally(ctx.CopyExecutables, $"{options.ProductionDeliveryFolder}\\{ctx.DeploymentIssue}.zip")
                                      .AppendConditionally(ctx.CopyExecutables, "Bu arşivdeki exe dosyalar uygulama dizinine kopyalanacak.")
                                      .AppendConditionally(!ctx.CopyExecutables, "Bu iş için exe kopyalanmasına gerek *YOK*")

                                      .AppendConditionally(ctx.HasSqlScript, "İşe ekli sql script dosyaları pandora.ibu veritabanında çalıştırılacak.")
                                      .AppendConditionally(!ctx.HasSqlScript, "İşe ekli sql script dosyası *YOK*")

                                      .ToString();
        }

        private string BuildGitDescription(DeliveryContext ctx)
        {
            var description = new StringBuilder().AppendLine($"* **Internal Issue :** {ctx.InternalIssue}")
                                                 .AppendLine($"* **Request Issue :** {ctx.RequestIssue}")
                                                 .AppendLine($"* **Deployment Issue :** {ctx.DeploymentIssue}")
                                                 .AppendLine($"* **Uat Issue :** {ctx.UatIssue}")
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
