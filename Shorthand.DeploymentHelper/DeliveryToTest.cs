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

      if (string.IsNullOrEmpty(ctx.RequestIssueKey))      
        throw new ArgumentNullException("RequestIssueKey", "Context does not contain a request issue key.");

      if (string.IsNullOrEmpty(ctx.InternalIssueKey))
        throw new ArgumentNullException("InternalIssueKey", "Context does not contain an internal issue key.");

      var jira = new Jira();

      // announce that testers can test by adding comment to request issue
      var comment = this.BuilRequestComment(ctx);
      jira.AddCommentToIssue(ctx.RequestIssueKey, comment);
      
      // advance workflow for internal issue
      var transitions = jira.GetTransitionsForIssue(ctx.InternalIssueKey);
      var q = transitions.FirstOrDefault(x => x.name == "Deployed for Test");
      if ( q != null )
        jira.SetTransitionForIssue(ctx.InternalIssueKey, q.id);

      if (!ctx.CreateUatIssue)
      {
        this.Log("skipped...");
        return;
      }

      // create uat issue if it does not exist
      if (string.IsNullOrEmpty(ctx.UatIssueKey))
      {
        var summary = string.Format("UAT for {0}", ctx.RequestIssueKey);
        var description = this.BuildUATDescription(ctx);
        ctx.UatIssueKey = jira.CreateIssue(_jiraOptions.UAT_ProjectKey, summary, description, "Task");

        // link uat issue to request issue
        jira.CreateLink("UAT", ctx.RequestIssueKey, ctx.UatIssueKey, "UAT oluşturuldu.");

        jira.SetDescription(ctx.UatIssueKey, this.BuildUATDescription(ctx));
      }
             
    }

    private void DeployExecutables(DeliveryContext ctx)
    {
      if (!ctx.CopyExecutables)
      {
        this.Log("Ignored Deploying executables ");
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

      return new StringBuilder().AppendFormattedLine("İşlev *{0}* uygulaması ile *ibu_test* veritabanında test edilebilir.", ctx.TestExecutableTargetName)
                                .AppendLine("")
                                .ToString();
    }

    private string BuildUATDescription(DeliveryContext ctx)
    {
      if (string.IsNullOrEmpty(ctx.TestExecutableTargetName))
        ctx.TestExecutableTargetName = this.BuildTargetName(ctx);

      return new StringBuilder().AppendLine("*Test Adımları*")
                                .AppendFormattedLine("# *{0}* uygulaması çalıştırılır.", ctx.TestExecutableTargetName)
                                .AppendLine("# *ibu_test* veritabanına login olunur")
                                .AppendLine("# ...")
                                .AppendLine("# Ekran görüntüsü bu işe eklenir")
                                .AppendLine("# Bu iş, *Done* ile kapatılır")
                                .AppendFormattedLine("# {0} *Passed* ile kapatılır", ctx.RequestIssueKey)
                                .AppendLine("")
                                .ToString();
    }

    public string BuildTargetName(DeliveryContext ctx)
    {
      if (!ctx.CopyExecutables)
        return "IBU.exe";

      var header = string.Format("IBU-{0}", ctx.RequestIssueKey.Replace("-", " "));
      var versionNumber = Directory.GetFiles(_deploymentOptions.TestDeliveryFolder)
                                   .Where(x => x.Contains(header))
                                   .Count();
      var newFileName = versionNumber == 0 ? string.Format("{0}.exe", header) : string.Format("{0}.v{1}.exe", header, 1 + versionNumber);                                                                                                                                              
      var qualifiedNewName = Path.Combine(_deploymentOptions.TestDeliveryFolder, newFileName);

      return qualifiedNewName;
    }

    private void Log(string line)
    {
      _logger?.Invoke(line);
    }


  }
}
