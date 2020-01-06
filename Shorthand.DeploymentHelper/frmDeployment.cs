using PragmaTouchUtils;
using Shorthand.Common;
using Shorthand.GitLabEntity;
using Shorthand.JiraEntity;
using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shorthand
{

  [Export(typeof(IAsyncPlugin))]
  public partial class frmDeployment : Form, IAsyncPlugin
  {
    private IPluginContext _context;

    private Jira _jira;
    private JiraOptions _jiraOptions;
    private DeploymentOptions _deployOptions;

    private GitLab _gitLab;
    private GitLabOptions _gitLabOptions;
    

    public frmDeployment()
    {
      InitializeComponent();
    }

    public async Task<Form> InitializeAsync(IPluginContext context)
    {
      _context = context;
      context.Configuration.LoadConfiguration();

      this.FormClosing += (object sender, FormClosingEventArgs e) =>
      {
        e.Cancel = true;
        this.Hide();
      };

      await this.InitializePlugin();
      await this.InitializeUIAsync();

      return this;
    }

    private async Task<bool> InitializePlugin()
    {
      return await Task.Run(() =>
       {
         _deployOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;

         _jira = _jira ?? new Jira(x => this.Dump(x));
         _jiraOptions = _jira.Options; // ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;

         _gitLab = _gitLab ?? new GitLab(x => this.Dump(x));
         _gitLabOptions = _gitLab.Options; // ConfigContent.Current.GetConfigContentItem("GitLabOptions") as GitLabOptions;

         return true;
       });
    }

    private async Task InitializeUIAsync()
    {
      lblREQ_IssueKey.Text = _jiraOptions.REQ_ProjectKey;
      lblDPLY_IssueKey.Text = _jiraOptions.DPLY_ProjectKey;
      lblUAT_IssueKey.Text = _jiraOptions.UAT_ProjectKey;

      lblInternal_Status.Text = string.Empty;
      lblREQ_Status.Text = string.Empty;
      lblDPLY_Status.Text = string.Empty;

      var projects = await _gitLab.GetProjectsAsync(true);
      var items = projects?.OrderBy(x => x.name).Select(x => new { Text = x.name, Value = x.id }).ToArray();
      cmbGitProjectName.DisplayMember = "Text";
      cmbGitProjectName.ValueMember = "Value";
      cmbGitProjectName.DataSource = items;
      cmbGitProjectName.SelectedItem = items?.FirstOrDefault(x => x.Text == _gitLabOptions.DefaultGitProjectName);

      lblMergeRequestLink.LinkClicked += (x, y) => { Process.Start(y.Link.LinkData as string); };

      rdbProduction.CheckedChanged -= Rdb_CheckedChanged;
      rdbProduction.CheckedChanged += Rdb_CheckedChanged;

      rdbTest.CheckedChanged -= Rdb_CheckedChanged;
      rdbTest.CheckedChanged += Rdb_CheckedChanged;
    }

    public async void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    {
      if (e.action == ConfigAction.Cancel || e.action == ConfigAction.None)
        return;

      if (e.ChangedOptions.Count() == 0)
        return;

      var shouldRefresh = e.ChangedOptions.Contains("DeploymentOptions")
                        || e.ChangedOptions.Contains("JiraOptions")
                        || e.ChangedOptions.Contains("GitLabOptions");
      if (!shouldRefresh)
        return;

      await this.InitializePlugin();
      await this.InitializeUIAsync();
    }





    private void Rdb_CheckedChanged(object sender, EventArgs e)
    {
      chkCreateDPLY.Checked = rdbProduction.Checked;
      chkCreateUAT.Checked = rdbTest.Checked;
      chkCreateMergeRequest.Checked = rdbProduction.Checked;
      chkCopyExecutables.Checked = true;
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

    private async void btnDeploy_Click(object sender, EventArgs e)
    {
      if (!this.SanityCheck())
        return;

      if (rdbProduction.Checked)
        this.Dump("Delivering to production");
      if (rdbTest.Checked)
        this.Dump("Delivering to test");

      try
      {
        var projectId = (int)cmbGitProjectName.SelectedValue;
        var ctx = await this.BuildDeliveryContext();

        if (string.IsNullOrEmpty(ctx.RequestIssue))
        {
          this.Dump("ERROR: Can not locate request issue");
          return;
        }

        var delivery = this.BuildDelivery();
        delivery.Deliver(ctx);

        await this.RefreshUIAsync();
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

    private async void btnRefresh_Click(object sender, EventArgs e)
    {
      await this.RefreshUIAsync();
    }

    private async Task RefreshUIAsync()
    {
      if (!this.SanityCheck())
        return;

      this.ClearBaseData();

      var projectId = (int) (cmbGitProjectName.SelectedValue ?? 0);
      var baseData = await this.GetBaseData(txtInternal.Text, projectId);
      if (string.IsNullOrEmpty(baseData.RequestIssue?.key))
        this.Dump("WARNING: Can not locate request issue");

      this.DisplayBaseData(baseData);
    }

    private void ClearBaseData()
    {
      lblInternal_Status.Text = string.Empty;
      lblREQ_Status.Text = string.Empty;
      lblDPLY_Status.Text = string.Empty;

      txtREQ.Clear();
      txtDPLY.Clear();
      lbxUAT.DataSource = null;

      txtGitMergeRequestNo.Clear();

      lblMergeRequestLink.Text = $"merge request state unknown";
      lblMergeRequestLink.Links.Clear();

      //Application.DoEvents();
    }

    private async Task<BaseData> GetBaseData(string issueKey, int projectId)
    {
      var baseData = new BaseData();

      // Jira
      var internalIssue = await _jira.GetIssueAsync(issueKey);
      baseData.InternalIssue = new boIssue { key = internalIssue.key, status = internalIssue.fields.status.name, description = internalIssue.fields.description };

      var linksOfInternalIssue = this.ExtractLinksOfIssue(internalIssue);
      baseData.RequestIssue = linksOfInternalIssue.FirstOrDefault(x => x.key.Contains(_jiraOptions.REQ_ProjectKey));
      baseData.DeploymentIssue = linksOfInternalIssue.FirstOrDefault(x => x.key.Contains(_jiraOptions.DPLY_ProjectKey));

      if (baseData.RequestIssue != null)
      {
        var reqIssue = await _jira.GetIssueAsync(baseData.RequestIssue.key);
        var linksOfReqIssue = this.ExtractLinksOfIssue(reqIssue);
        baseData.UatIssues = linksOfReqIssue?.Where(x => x.key.Contains(_jiraOptions.UAT_ProjectKey)).ToArray();
      }

      // GitLab
      if (projectId > 0)
      {
        var project = await _gitLab.GetProjectByIdAsync(projectId);
        var mergeReq = await _gitLab.GetMergeRequestByInternalIssueKeyAsync(projectId, issueKey);
        var mergeReqNo = mergeReq?.iid ?? 0;
        var mergeReqState = mergeReq?.state ?? "unknown";

        baseData.GitProjectName = project.name;
        baseData.GitProjectId = project.id;
        baseData.GitProjectWebUrl = project.web_url;
        baseData.GitMergeRequestNo = mergeReqNo;
        baseData.GitMergeRequestState = mergeReqState;
      }

      return baseData;
    }

    private void DisplayBaseData(BaseData baseData)
    {
      txtREQ.Text = baseData.RequestIssue?.key;
      txtDPLY.Text = baseData.DeploymentIssue?.key;
      lbxUAT.SelectedItem = baseData.UatIssue?.key;

      lbxUAT.DataSource = baseData.UatIssues?.Select(x => new { Text = $"{x?.key} - {x?.status}", Value = x?.key }).ToArray();
      lbxUAT.DisplayMember = "Text";
      lbxUAT.ValueMember = "Value";

      lblInternal_Status.Text = baseData.InternalIssue?.status;
      lblREQ_Status.Text = baseData.RequestIssue?.status;
      lblDPLY_Status.Text = baseData.DeploymentIssue?.status;

      txtGitMergeRequestNo.Text = baseData.GitMergeRequestNo.ToString();
      var mergeReqUrl = $"{baseData.GitProjectWebUrl}/merge_requests/{baseData.GitMergeRequestNo}";
      lblMergeRequestLink.Text = $"merge request state {baseData.GitMergeRequestState}";
      lblMergeRequestLink.Links.Add(0, lblMergeRequestLink.Text.Length, mergeReqUrl);
    }



    private async Task<DeliveryContext> BuildDeliveryContext()
    {
      var ctx = new DeliveryContext();

      // Deployment Spec.
      if (rdbProduction.Checked)
        ctx.DeliveryTo = DeliveryContext.ToProduction;
      else if (rdbTest.Checked)
        ctx.DeliveryTo = DeliveryContext.ToTest;

      ctx.CreateDeploymentIssue = chkCreateDPLY.Checked;
      ctx.CreateUatIssue = chkCreateUAT.Checked;
      ctx.CreateMergeRequest = chkCreateMergeRequest.Checked;
      ctx.CopyExecutables = chkCopyExecutables.Checked;

      // Jira
      ctx.InternalIssue = txtInternal.Text;
      ctx.RequestIssue = txtREQ.Text;
      ctx.DeploymentIssue = txtDPLY.Text;
      ctx.UatIssue = lbxUAT.SelectedValue?.ToString();




      // GitLab

      var codebaseConfig = _gitLab.GetCodebaseConfig();

      var projectId = (int)cmbGitProjectName.SelectedValue;
      var project = await _gitLab.GetProjectByIdAsync(projectId);
      var mergeReq = await _gitLab.GetMergeRequestByInternalIssueKeyAsync(projectId, ctx.InternalIssue);
      var mergeReqNo = mergeReq?.iid ?? 0;
      var mergeReqState = mergeReq?.state ?? "unknown";

      ctx.GitProjectName = cmbGitProjectName.Text;
      ctx.GitProjectId = (int)(cmbGitProjectName.SelectedValue ?? 0);
      ctx.GitProjectWebUrl = project.web_url;
      ctx.GitMergeRequestNo = mergeReqNo;
      ctx.GitMergeRequestState = mergeReqState;

      return ctx;
    }

    private IDelivery BuildDelivery()
    {
      if (rdbProduction.Checked)
        return new DeliveryToProduction(x => this.Dump(x));
      else if (rdbTest.Checked)
        return new DeliveryToTest(x => this.Dump(x));

      return null;
    }

    public void SendMail(DeliveryContext ctx)
    {
      this.Dump("Sending notification mail...");

      var mail = new MailMessage("tolga.kurkcuoglu@gmail.com", "tolgak@bilgi.edu.tr")
      {
        Subject = $"Deployment and merge request notification ({ctx.DeliveryTo}) "
      };

      var sb = new StringBuilder();
      sb.AppendLine($"{ctx.DeliveryTo} Deployment ready.")
        .AppendLine("")
        .AppendConditionally(ctx.CreateMergeRequest, "GITLAB ÜZERİNDE İLGİLİ REQUEST İ MERGE ETMEYİ UNUTMAMAK GEREKLİ")
        .AppendLine("")
        .AppendLine("")
        .AppendLine("Jira Details")
        .AppendLine("------------")
        .AppendLine($"Internal Issue Key : {ctx.InternalIssue}")
        .AppendLine($"Request Issue Key : {ctx.RequestIssue}")
        .AppendLine($"Deployment Issue Key : {ctx.DeploymentIssue}")
        .AppendLine($"UAT Issue Key : {ctx.UatIssue}")
        .AppendLine("")
        .AppendLine("Git Details")
        .AppendLine("------------")
        .AppendLine($"Project : {ctx.GitProjectName}")
        .AppendLine($"Project Url : {ctx.GitProjectWebUrl}")
        .AppendLine($"Merge Req : {ctx.GitMergeRequestNo}");
      mail.Body = sb.ToString();

      var client = new SmtpClient("smtp.gmail.com", 587)
      {
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential("tolga.kurkcuoglu@gmail.com", "!tk1123581321tk!"),
        EnableSsl = true
      };
      client.Send(mail);
    }

    private void Dump(string line)
    {
      txtDump.Log(line);
    }

    private boIssue[] ExtractLinksOfIssue(Issue issue)
    {
      var q1 = issue.fields.issuelinks.Where(x => x.inwardIssue != null).Select(x => new boIssue { key = x.inwardIssue.key, status = x.inwardIssue.fields.status.name });
      var q2 = issue.fields.issuelinks.Where(x => x.outwardIssue != null).Select(x => new boIssue { key = x.outwardIssue.key, status = x.outwardIssue.fields.status.name });

      return q1.Union(q2).ToArray();
    }

    private void btnClearLog_Click(object sender, EventArgs e)
    {
      txtDump.Clear();
    }

    private async void frmDeployment_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F2:
          await this.FireLinksAsync();
          break;
        case Keys.F5:
        case Keys.Return:
          await this.RefreshUIAsync();
          break;
        default:
          break;
      }
    }

    private async Task FireLinksAsync()
    {
      int projectId = cmbGitProjectName.GetSelectedValue();
      var ctx = await this.BuildDeliveryContext();

      if (!string.IsNullOrEmpty(ctx.InternalIssue))
        Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.InternalIssue}");
      if (!string.IsNullOrEmpty(ctx.DeploymentIssue))
        Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.DeploymentIssue}");
      if (!string.IsNullOrEmpty(ctx.RequestIssue))
        Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.RequestIssue}");
      if (!string.IsNullOrEmpty(ctx.UatIssue))
        Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.UatIssue}");

      if (!string.IsNullOrEmpty(ctx.GitProjectWebUrl) && ctx.GitMergeRequestNo > 0)
        Process.Start($"{ctx.GitProjectWebUrl}/merge_requests/{ctx.GitMergeRequestNo}");
      else if (!string.IsNullOrEmpty(ctx.GitProjectWebUrl))
        Process.Start(ctx.GitProjectWebUrl);
    }

    private void btnMakeExecutable_Click(object sender, EventArgs e)
    {
      var remoteHost = "eoyuktas-ESPRIMO-p900";
      var remotePort = 6200;
      var remoteBuilder = new RemoteBuilder(remoteHost, remotePort, this.Dump);

      var args = new string[1] { cmbGitProjectName.Text };
      remoteBuilder.Build(args);
    }

    private void btnOpenLocal_Click(object sender, EventArgs e)
    {
      this.OpenFolder(_deployOptions.LocalBinPath);
    }

    private void btnOpenDeployment_Click(object sender, EventArgs e)
    {
      this.OpenFolder(_deployOptions.TestDeliveryFolder);
    }

    private void OpenFolder(string folder)
    {
      if (!Directory.Exists(folder))
      {
        MessageBox.Show(this, $"Folder does not exist.\n\r{folder}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        return;
      }

      var pi = new ProcessStartInfo()
      {
        FileName = folder,
        UseShellExecute = true,
        Verb = "open"
      };

      Process.Start(pi);
    }

    private async void btnJenkins_Click(object sender, EventArgs e)
    {
      var jenkins = new Jenkins(x => this.Dump(x));

      var jobs = await jenkins.GetJobsAsync();
      foreach (var job in jobs)
      {
        this.Dump($"{job.Name} - {job.Color}");
      }

    }



  }
}
