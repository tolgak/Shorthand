using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using PragmaTouchUtils;
using System.Net.Mail;

using Shorthand.Common;
using System.ComponentModel.Composition;

namespace Shorthand
{

  [Export(typeof(IPlugin))]
  public partial class frmDeployment : Form, IPlugin
  {

    private IPluginContext _context;

    private Jira _jira;
    private JiraOptions _jiraOptions;
    private DeploymentOptions _deplyOptions;

    private GitLab _gitLab;
    private GitLabOptions _gitLabOptions;

    public frmDeployment()
    {
      InitializeComponent();
    }

    private void InitializePlugin()
    {
      _deplyOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;

      _jira = new Jira(x => this.Dump(x));
      _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;

      _gitLab = new GitLab(x => this.Dump(x));
      _gitLabOptions = ConfigContent.Current.GetConfigContentItem("GitLabOptions") as GitLabOptions;
    }

    private void InitializeUI()
    {
      if (!_jiraOptions.IsValid)
        this.Dump("jira options are invalid");
      else
      {
        lblREQ_IssueKey.Text = _jiraOptions.REQ_ProjectKey;
        lblDPLY_IssueKey.Text = _jiraOptions.DPLY_ProjectKey;
        lblUAT_IssueKey.Text = _jiraOptions.UAT_ProjectKey;

        lblInternal_Status.Text = string.Empty;
        lblREQ_Status.Text = string.Empty;
        lblDPLY_Status.Text = string.Empty;
        lblUAT_Status.Text = string.Empty;
      }

      if (!_gitLabOptions.IsValid)
        this.Dump("gitLab options are invalid");
      else
      {
        var projects = _gitLab.GetProjects(true);
        var items = (from x in projects
                     orderby x.name
                     select new LookupItem { Text = x.name, Value = x.id }).ToArray();

        cmbGitProjectName.DisplayMember = "Text";
        cmbGitProjectName.ValueMember = "Value";
        cmbGitProjectName.Items.AddRange(items);
        cmbGitProjectName.SelectedItem = items.FirstOrDefault(x => x.Text == _gitLabOptions.DefaultGitProjectName);

        lblMergeRequestLink.LinkClicked += (x, y) => { Process.Start(y.Link.LinkData as string); };
      }
    }

    public void PerformAction(IPluginContext context)
    {
      _context = context;
      this.MdiParent = _context.Host;
      _context.Configuration.LoadConfiguration();

      var strips = _context.Host.MainMenuStrip.Items.Find("mnuTools", true);
      if (strips.Length == 0)
        return;
      
      var subItem = new ToolStripMenuItem("Deployment Helper");
      subItem.Click += HandleClickEvent; 

      (strips[0] as ToolStripMenuItem).DropDownItems.Add(subItem);

      this.InitializePlugin();
      this.InitializeUI();
    }

    private void HandleClickEvent(object sender, EventArgs e)
    {
      this.Show();
    }

    private void frmDeployment_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      this.Hide();
    }





    private bool SanityCheck()
    {
      this.Dump("Sanity Check");

      if (!_jiraOptions.IsValid)
      {
        this.Dump("ERROR: Please configure jira options");
        return false;
      }

      if (!_gitLabOptions.IsValid)
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

    private void btnDeploy_Click(object sender, EventArgs e)
    {
      if (!this.SanityCheck())
        return;

      if (rdbProduction.Checked)
        this.Dump("Delivering to production");
      if (rdbTest.Checked)
        this.Dump("Delivering to test");

      try
      {
        var projectId = cmbGitProjectName.GetSelectedValue();
        var ctx = this.BuildDeliveryContext(txtInternal.Text, projectId);
        if (string.IsNullOrEmpty(ctx.RequestIssueKey))
        {
          this.Dump("ERROR: Can not locate request issue");
          return;
        }

        var deployment = BuildDelivery();
        deployment.Deliver(ctx);
        this.SendMail(ctx);
        
        this.RefreshUI();
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

      var projectId = cmbGitProjectName.GetSelectedValue();
      var ctx = this.BuildDeliveryContext(txtInternal.Text, projectId);
      if (string.IsNullOrEmpty(ctx.RequestIssueKey))
        this.Dump("WARNING: Can not locate request issue");

      txtREQ.Text = ctx.RequestIssueKey;
      txtDPLY.Text = ctx.DeploymentIssueKey;
      txtUAT.Text = ctx.UatIssueKey;
      txtGitMergeRequestNo.Text = ctx.GitMergeRequestNo.ToString();

      lblInternal_Status.Text = string.IsNullOrEmpty(ctx.InternalIssueKey) ? "N/A" : _jira.GetStatusOfIssue(ctx.InternalIssueKey);
      lblREQ_Status.Text = string.IsNullOrEmpty(ctx.RequestIssueKey) ? "N/A" : _jira.GetStatusOfIssue(ctx.RequestIssueKey);
      lblDPLY_Status.Text = string.IsNullOrEmpty(ctx.DeploymentIssueKey) ? "N/A" : _jira.GetStatusOfIssue(ctx.DeploymentIssueKey);
      lblUAT_Status.Text = string.IsNullOrEmpty(ctx.UatIssueKey) ? "N/A" : _jira.GetStatusOfIssue(ctx.UatIssueKey);
      lblMergeRequestLink.Links.Clear();
      lblMergeRequestLink.Text = $"merge request state {ctx.GitMergeRequestState}";

      var projectWebUrl = $"{ctx.GitProjectWebUrl}/merge_requests/{ctx.GitMergeRequestNo}";
      lblMergeRequestLink.Links.Add(0, lblMergeRequestLink.Text.Length, projectWebUrl);
    }

    private IDelivery BuildDelivery()
    {
      if (rdbProduction.Checked)
        return new DeliveryToProduction(x => this.Dump(x));
      else if (rdbTest.Checked)
        return new DeliveryToTest(x => this.Dump(x));

      return null;
    }

    private DeliveryContext BuildDeliveryContext(string issueKey, int projectId)
    {
      var ctx = new DeliveryContext();
      if (rdbProduction.Checked)
        ctx.DeliveryTo = DeliveryContext.ToProduction;
      else if (rdbTest.Checked)
        ctx.DeliveryTo = DeliveryContext.ToTest;

      ctx.CreateDeploymentIssue = chkCreateDPLY.Checked;
      ctx.CreateUatIssue = chkCreateUAT.Checked;

      // Jira
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

    public void SendMail(DeliveryContext ctx)
    {
      this.Dump("Sending notification mail...");
      
      var mail = new MailMessage("tolga.kurkcuoglu@gmail.com", "tolgak@bilgi.edu.tr");
      mail.Subject = $"Deployment and merge request notification ({ctx.DeliveryTo}) ";

      var sb = new StringBuilder();
      sb.AppendLine($"{ctx.DeliveryTo} Deployment ready.")
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

    private void Dump(string line)
    {
      txtDump.InvokeIfRequired((x) => { x.Text = x.Text + $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {line}" + "\r\n"; });
    }

    private string[] GetLinksOfIssue(string issueKey)
    {
      var issueLinks = _jira.GetLinksOfIssue(issueKey);
      var q = (from x in issueLinks where x.inwardIssue != null select x.inwardIssue.key)
            .Union(from x in issueLinks where x.outwardIssue != null select x.outwardIssue.key)
            .ToArray();

      return q;
    }


    private void btnClearLog_Click(object sender, EventArgs e)
    {
      txtDump.Clear();
    }

    private void btnTest_Click(object sender, EventArgs e)
    {

    }

    private void frmDeployment_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F2:
          this.FireLinks();
          break;
        case Keys.F5:
        case Keys.Return:
          this.RefreshUI();
          break;
        default:
          break;
      }
    }

    private void FireLinks()
    {      
      int projectId = cmbGitProjectName.GetSelectedValue();
      var ctx = this.BuildDeliveryContext(txtInternal.Text, projectId);

      if ( !string.IsNullOrEmpty(ctx.InternalIssueKey) )
        Process.Start( $"{_jiraOptions.JiraBaseUrl}/browse/{ctx.InternalIssueKey}");
      if (!string.IsNullOrEmpty(ctx.DeploymentIssueKey))
        Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.DeploymentIssueKey}");
      if (!string.IsNullOrEmpty(ctx.RequestIssueKey))
        Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.RequestIssueKey}");
      if (!string.IsNullOrEmpty(ctx.UatIssueKey))
        Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.UatIssueKey}");
      
      if (!string.IsNullOrEmpty(ctx.GitProjectWebUrl) && ctx.GitMergeRequestNo > 0)
        Process.Start($"{ctx.GitProjectWebUrl}/merge_requests/{ctx.GitMergeRequestNo}");
      else if (!string.IsNullOrEmpty(ctx.GitProjectWebUrl))
        Process.Start(ctx.GitProjectWebUrl);
    }






  }
}
