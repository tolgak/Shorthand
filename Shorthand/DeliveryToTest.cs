﻿using System;
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

    public void Deliver(DeliveryContext ctx)
    {
      this.PrepareJira(ctx);
      this.DeployExecutables(ctx);
    }

    private void PrepareJira(DeliveryContext ctx)
    {
      var jira = new Jira();

      var requestIssueKey = ctx.RequestIssueKey;
      if (string.IsNullOrEmpty(requestIssueKey))
        return;

      var comment = this.BuilRequestComment(ctx);
      jira.AddCommentToIssue(requestIssueKey, comment);      
    }

    private void DeployExecutables(DeliveryContext ctx)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;

      var newFileName = string.Format("IBU-{0}.exe", ctx.RequestIssueKey.Replace("-", " "));
      var qualifiedNewName = Path.Combine(options.TestDeliveryFolder, newFileName);
      var qualifiedOldName = Path.Combine(options.LocalBinPath + @"\exe\", "IBU.exe");
      File.Copy(qualifiedOldName, qualifiedNewName, true);
    }

    private string BuilRequestComment(DeliveryContext ctx)
    {
      var options = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
      var fileName = string.Format("IBU-{0}.exe", ctx.RequestIssueKey.Replace("-", " "));

      return new StringBuilder().AppendFormattedLine("İşlev *{0}\\{1}* uygulaması ile test edilebilir.", options.TestDeliveryFolder, fileName)
                                .AppendLine("")
                                .ToString();
    }

  }
}
