using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
      this.PrepareJira(ctx);
      this.DeployExecutables(ctx);
    }

    private void PrepareJira(DeliveryContext ctx)
    {
      _logger?.Invoke("Preparing Jira");

      var jira = new Jira();

      var requestIssueKey = ctx.RequestIssueKey;
      if (string.IsNullOrEmpty(requestIssueKey))
        return;
      
      // announce that testers can test by adding comment to request issue
      var comment = this.BuilRequestComment(ctx);
      jira.AddCommentToIssue(requestIssueKey, comment);
      

      // advance workflow for internal issue
      var transitions = jira.GetTransitionsForIssue(ctx.InternalIssueKey);
      var q = transitions.FirstOrDefault(x => x.name == "Deployed for Test");
      if ( q != null )
        jira.SetTransitionForIssue(ctx.InternalIssueKey, q.id);

      // create uat issue if it does not exist
      if (string.IsNullOrEmpty(ctx.UatIssueKey))
      {
        var summary = string.Format("UAT for {0}", ctx.RequestIssueKey);
        var description = this.BuildUATDescription(ctx);
        ctx.UatIssueKey = jira.CreateIssue(_jiraOptions.UAT_ProjectKey, summary, description, "Task");

        // link uat issue to request issue
        jira.CreateLink("UAT", ctx.RequestIssueKey, ctx.UatIssueKey, "UAT oluşturuldu.");
      }
      else
        jira.SetDescription(ctx.UatIssueKey, this.BuildUATDescription(ctx));

    }

    private void DeployExecutables(DeliveryContext ctx)
    {
      _logger?.Invoke("Deploying executables");

      // copy executable to remote executable folder      
      var qualifiedTargetName = string.IsNullOrEmpty(ctx.TestExecutableTargetName) ? this.BuildTargetName(ctx) : ctx.TestExecutableTargetName;
      var qualifiedSourceName = Path.Combine(_deploymentOptions.LocalBinPath + @"\exe\", "IBU.exe");
      File.Copy(qualifiedSourceName, qualifiedTargetName, true);
    }

    private string BuilRequestComment(DeliveryContext ctx)
    {      
      ctx.TestExecutableTargetName = this.BuildTargetName(ctx);
      return new StringBuilder().AppendFormattedLine("İşlev *{0}* adresindeki *{1}* uygulaması ile  *ibu_test* veritabanında test edilebilir.", _deploymentOptions.TestDeliveryFolder, ctx.TestExecutableTargetName)
                                .AppendLine("")
                                .ToString();
    }

    private string BuildUATDescription(DeliveryContext ctx)
    {
      var fileName = $"IBU-{ctx.RequestIssueKey.Replace("-", " ")}.exe";
      return new StringBuilder().AppendLine("*Test Adımları*")
                                .AppendFormattedLine("# *{0}* adresindeki *{1}* uygulaması çalıştırılır.", _deploymentOptions.TestDeliveryFolder, fileName)
                                .AppendLine("# *ibu_test* veritabanına login olunur")
                                .AppendLine("# ...")
                                .AppendLine("# Ekran görüntüsü bu işe eklenir")
                                .AppendLine("# Bu iş, *Done* ile kapatılır")
                                .AppendFormattedLine("# {0} *Passed* ile kapatılır", ctx.RequestIssueKey)
                                .AppendLine("")
                                .ToString();
    }

    public string BuildTargetName(DeliveryContext ctx, int version = 1)
    {
      var newFileName = string.Format("IBU-{0}.v{1}.exe", ctx.RequestIssueKey.Replace("-", " "), version);
      var qualifiedNewName = Path.Combine(_deploymentOptions.TestDeliveryFolder, newFileName);
      if (File.Exists(qualifiedNewName))
        qualifiedNewName = this.BuildTargetName(ctx, ++version);

      return qualifiedNewName;
    }


  }
}
