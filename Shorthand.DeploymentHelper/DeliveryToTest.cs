using System;
using System.IO;
using System.Linq;
using System.Text;
using PragmaTouchUtils;

namespace Shorthand
{
  public class DeliveryToTest : IDelivery
  {

    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
    private DeploymentOptions _deploymentOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
    private Action<string> _logger;

    public DeliveryToTest()
    {

    }

    public DeliveryToTest(Action<string> logger) : this() 
    {
      _logger = logger;
    }

    public void Deliver(DeliveryContext ctx)
    {
      ctx.TestExecutableTargetName = this.BuildTargetName(ctx);

      this.PrepareJira(ctx);
      this.DeployExecutables(ctx);
    }

    private void PrepareJira(DeliveryContext ctx)
    {
      this.Log("Preparing Jira");

      if (string.IsNullOrEmpty(ctx.RequestIssue))      
        throw new ArgumentNullException("RequestIssueKey", "Context does not contain a request issue key.");

      if (string.IsNullOrEmpty(ctx.InternalIssue))
        throw new ArgumentNullException("InternalIssueKey", "Context does not contain an internal issue key.");

      var jira = new Jira();

      // announce that testers can test by adding comment to request issue
      var comment = this.BuilRequestComment(ctx);
      jira.AddCommentToIssue(ctx.RequestIssue, comment);
      
      // advance workflow for internal issue
      var transitions = jira.GetTransitionsForIssue(ctx.InternalIssue);
      var q = transitions.FirstOrDefault(x => x.name == "Deployed for Test");
      if ( q != null )
        jira.SetTransitionForIssue(ctx.InternalIssue, q.id);

      if (!ctx.CreateUatIssue)
      {
        this.Log("skipped creating uat issue...");
        return;
      }

      // create uat issue if it does not exist
      if (string.IsNullOrEmpty(ctx.UatIssue))
      {
        var summary = $"UAT for {ctx.RequestIssue}";
        var description = this.BuildUATDescription(ctx);
        ctx.UatIssue = jira.CreateIssue(_jiraOptions.UAT_ProjectKey, summary, description, "Task");
        // link uat issue to request issue
        jira.CreateLink("UAT", ctx.RequestIssue, ctx.UatIssue, "UAT oluşturuldu.");

        //jira.SetDescription(ctx.UatIssueKey, this.BuildUATDescription(ctx));
      }
             
    }

    private void DeployExecutables(DeliveryContext ctx)
    {
      if (!ctx.CopyExecutables)
      {
        this.Log("Skipped deploying executables ");
        return;
      }

      this.Log("Deploying executables");

      // copy executable to remote executable folder      
      if (string.IsNullOrEmpty(ctx.TestExecutableTargetName))
        ctx.TestExecutableTargetName = this.BuildTargetName(ctx);

      var qualifiedSourceName = Path.Combine(_deploymentOptions.LocalBinPath + @"\exe\", "IBU.exe");
      File.Copy(qualifiedSourceName, ctx.TestExecutableTargetName, true);
    }

    private string BuilRequestComment(DeliveryContext ctx)
    {

      if (string.IsNullOrEmpty (ctx.TestExecutableTargetName) )
        ctx.TestExecutableTargetName = this.BuildTargetName(ctx);

      return new StringBuilder().AppendLine($"İşlev *{ctx.TestExecutableTargetName}* uygulaması ile *ibu_test* veritabanında test edilebilir.")
                                .ToString();
    }

    private string BuildUATDescription(DeliveryContext ctx)
    {
      if (string.IsNullOrEmpty(ctx.TestExecutableTargetName))
        ctx.TestExecutableTargetName = this.BuildTargetName(ctx);

      return new StringBuilder().AppendLine("*Masaüstü uygulamasında test adımları*")
                                .AppendLine($"# *{ctx.TestExecutableTargetName}* uygulaması çalıştırılır.")
                                .AppendLine("# *ibu_test* veritabanına login olunur")
                                .AppendLine("# ...")
                                .AppendLine("# Yeni eklenen işlevin, diğer işlevleri bozmadığından emin olunur.")
                                .AppendLine("# Ekran görüntüsü bu işe eklenir")
                                .AppendLine("# Bu iş, *Done* ile kapatılır")
                                .AppendLine($"# {ctx.RequestIssue} *Passed* ile kapatılır")
                                .ToString();
    }

    public string BuildTargetName(DeliveryContext ctx)
    {
      if (!ctx.CopyExecutables)
        return "IBU.exe";

      var header = $"IBU_{ctx.RequestIssue}".Replace("-", " ").Replace(" ", "_");
      var versionNumber = Directory.GetFiles(_deploymentOptions.TestDeliveryFolder)
                                   .Where(x => x.Contains(header))
                                   .Count();
      var newFileName = versionNumber == 0 ? $"{header}.exe" : $"{header}.v{versionNumber++}.exe";
      var qualifiedNewName = Path.Combine(_deploymentOptions.TestDeliveryFolder, newFileName);

      return qualifiedNewName;
    }

    private void Log(string line)
    {
      _logger?.Invoke(line);
    }


  }
}
