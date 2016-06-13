using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PragmaTouchUtils;
using System.Reflection;
using System.Net.Mail;
using Shorthand.Properties;

namespace Shorthand
{
  public partial class frmDeployment : Form
  {
    private Jira _jira;
    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;

    private GitLab _gitLab;
    private GitLabOptions _gitLabOptions = ConfigContent.Current.GetConfigContentItem("GitLabOptions") as GitLabOptions;

    private DeploymentOptions _deplyOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;

    public frmDeployment()
    {
      InitializeComponent();

      _jira = new Jira(x => this.Dump(x));
      if (!_jiraOptions.IsActive)
        this.Dump("jira options are invalid");
      else
      {
        lblREQ_IssueKey.Text = _jiraOptions.REQ_ProjectKey;
        lblDPLY_IssueKey.Text = _jiraOptions.DPLY_ProjectKey;
        lblUAT_IssueKey.Text = _jiraOptions.UAT_ProjectKey;

        lblREQ_Status.Text = string.Empty;
        lblDPLY_Status.Text = string.Empty;
        lblUAT_Status.Text = string.Empty;
      }


      _gitLab = new GitLab(x => this.Dump(x));
      if ( !_gitLabOptions.IsActive )
        this.Dump("gitLab options are invalid");
      else
      {
        var projects = _gitLab.GetProjects();
        var items = (from x in projects
                     orderby x.name
                     select new Tuple<string, int>(x.name, x.id)).ToArray();

        cmbGitProjectName.DisplayMember = "Item1";
        cmbGitProjectName.ValueMember = "Item2";
        cmbGitProjectName.Items.AddRange(items);
        cmbGitProjectName.SelectedItem = items.FirstOrDefault(x => x.Item1 == _gitLabOptions.DefaultGitProjectName);

        lblMergeRequestLink.LinkClicked += (x, y) => { Process.Start(y.Link.LinkData as string); };
      }

    }


    private bool SanityCheck()
    {
      this.Dump("Sanity Check");

      if (!_jiraOptions.IsActive)
      {
        this.Dump("ERROR: Please configure jira options");
        return false;
      }

      if (!_gitLabOptions.IsActive)
      {
        this.Dump("ERROR: Please configure GitLab options");
        return false;
      }

      if (string.IsNullOrEmpty(txtInternal.Text))
      {
        this.Dump("ERROR: Please provide an internal issue key");
        return false;
      }

      return true;
    }

    private void btnBuild_Click(object sender, EventArgs e)
    {
      if (!this.SanityCheck())
        return;

      if (rdbProduction.Checked)
        this.Dump("Delivering to production");
      if (rdbTest.Checked)
        this.Dump("Delivering to test");

      try
      {
        var projectId = (cmbGitProjectName.SelectedItem as Tuple<string, int>).Item2;
        var ctx = this.BuildDeliveryContext(txtInternal.Text, projectId);
        if (string.IsNullOrEmpty(ctx.RequestIssueKey))
        {
          this.Dump("ERROR: Can not locate request issue");
          return;
        }

        var delivery = this.BuildDelivery();
        delivery.Deliver(ctx);

        txtREQ.Text = ctx.RequestIssueKey;
        txtDPLY.Text = ctx.DeploymentIssueKey;
        txtUAT.Text = ctx.UatIssueKey;
        txtGitMergeRequestNo.Text = ctx.GitMergeRequestNo.ToString();
        Application.DoEvents();

        this.SendMail(ctx);
      }
      catch (Exception ex)
      {
        this.Dump(ex.Message);
      }
      finally
      {        
        this.Dump("DONE.");
      }

    }




    private void btnRefresh_Click(object sender, EventArgs e)
    {
      this.RefreshUI();
    }


    private DeliveryContext BuildDeliveryContext(string issueKey, int projectId)
    {
      // Jira
      var ctx = new DeliveryContext();      
      var linksOfInternalIssue = this.GetLinksOfIssue(issueKey);
      ctx.InternalIssueKey = issueKey;
      ctx.RequestIssueKey = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.REQ_ProjectKey));
      ctx.DeploymentIssueKey = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.DPLY_ProjectKey));
      if (!string.IsNullOrEmpty(ctx.RequestIssueKey))
      {
        var linksOfReqIssue = this.GetLinksOfIssue(ctx.RequestIssueKey);
        ctx.UatIssueKey = linksOfReqIssue.FirstOrDefault(x => x.Contains(_jiraOptions.UAT_ProjectKey));
      }

      // GitLab      
      var project = _gitLab.GetProjectById(projectId);
      var mergeReq = _gitLab.GetMergeRequestByInternalIssueKey(projectId, issueKey);
      var mergeReqNo = mergeReq?.iid ?? 0;
      var mergeReqState = mergeReq?.state ?? "unknown";

      ctx.GitProjectName = project.name;
      ctx.GitProjectId = project.id;
      ctx.GitProjectWebUrl = project.web_url;
      ctx.GitMergeRequestNo = mergeReqNo;
      ctx.GitMergeRequestState = mergeReqState;
      return ctx;
    }



    private string[] GetLinksOfIssue(string issueKey)
    {
      var issueLinks = _jira.GetLinksOfIssue(issueKey);
      var q = (from x in issueLinks where x.inwardIssue != null select x.inwardIssue.key)
            .Union(from x in issueLinks where x.outwardIssue != null select x.outwardIssue.key)
            .ToArray();

      return q;
    }

    private IDelivery BuildDelivery()
    {
      if (rdbProduction.Checked)
        return new DeliveryToProduction(x => this.Dump(x));

      if (rdbTest.Checked)
        return new DeliveryToTest(x => this.Dump(x));

      return null;
    }

    private void Dump(string line)
    {
      txtDump.InvokeIfRequired((x) => { x.Text = x.Text + $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {line}" + "\r\n"; });
    }



    private void RefreshUI()
    {
      if (!this.SanityCheck())
        return;

      lblREQ_Status.Text = string.Empty;
      lblDPLY_Status.Text = string.Empty;
      lblUAT_Status.Text = string.Empty;

      txtREQ.Clear();
      txtDPLY.Clear();
      txtUAT.Clear();
      txtGitMergeRequestNo.Clear();
      lblMergeRequestLink.Text = $"merge request state unknown";
      Application.DoEvents();

      var projectId = (cmbGitProjectName.SelectedItem as Tuple<string, int>).Item2;
      var ctx = this.BuildDeliveryContext(txtInternal.Text, projectId);
      if (string.IsNullOrEmpty(ctx.RequestIssueKey))
      {
        this.Dump("ERROR: Can not locate request issue");
        return;
      }

      txtREQ.Text = ctx.RequestIssueKey;
      txtDPLY.Text = ctx.DeploymentIssueKey;
      txtUAT.Text = ctx.UatIssueKey;
      txtGitMergeRequestNo.Text = ctx.GitMergeRequestNo.ToString();

      lblREQ_Status.Text  = string.IsNullOrEmpty(ctx.InternalIssueKey) ? "N/A" : _jira.GetStatusOfIssue(ctx.InternalIssueKey);
      lblDPLY_Status.Text = string.IsNullOrEmpty(ctx.DeploymentIssueKey) ? "N/A" : _jira.GetStatusOfIssue(ctx.DeploymentIssueKey);
      lblUAT_Status.Text  = string.IsNullOrEmpty(ctx.UatIssueKey) ? "N/A" : _jira.GetStatusOfIssue(ctx.UatIssueKey);

      lblMergeRequestLink.Links.Clear();
      lblMergeRequestLink.Text = $"merge request state {ctx.GitMergeRequestState}";
      var projectWebUrl = $"{ctx.GitProjectWebUrl}/merge_requests/{ctx.GitMergeRequestNo}";
      lblMergeRequestLink.Links.Add(0, lblMergeRequestLink.Text.Length, projectWebUrl);
    }

    private void btnClearLog_Click(object sender, EventArgs e)
    {
      txtDump.Clear();
    }

    public void SendMail(DeliveryContext ctx)
    {
      this.Dump("Sending notification mail...");

      var mail = new MailMessage("tolga.kurkcuoglu@gmail.com", "tolgak@bilgi.edu.tr");
      mail.Subject = "Deployment and merge request notification";

      var sb = new StringBuilder();
      sb.AppendLine("Deployment done.")
        .AppendLine("")
        .AppendLine("GITLAB ÜZERİNDE İLGİLİ REQUEST İ MERGE ETMEYİ UNUTMAMAK GEREKLİ")
        .AppendLine("")
        .AppendLine("")
        .AppendLine("Jira Details")
        .AppendLine("------------")
        .AppendLine($"Internal Issue Key : {ctx.InternalIssueKey}")
        .AppendLine($"Request Issue Key : {ctx.RequestIssueKey}")
        .AppendLine($"Deyloyment Issue Key : {ctx.DeploymentIssueKey}")
        .AppendLine($"UAT Issue Key : {ctx.UatIssueKey}")
        .AppendLine("")
        .AppendLine("Git Details")
        .AppendLine("------------")
        .AppendLine($"Project : {ctx.GitProjectName}")
        .AppendLine($"Project Url : {ctx.GitProjectWebUrl}")
        .AppendLine($"Merge Req : {ctx.GitMergeRequestNo}");
      mail.Body = sb.ToString();

      var client = new SmtpClient("smtp.gmail.com", 587);
      client.DeliveryMethod = SmtpDeliveryMethod.Network;
      client.UseDefaultCredentials = false;
      client.Credentials = new NetworkCredential("tolga.kurkcuoglu@gmail.com", "31415926tk");
      client.EnableSsl = true;
      client.Send(mail);
    }

    private void btnTest_Click(object sender, EventArgs e)
    {

    }

  }
}
