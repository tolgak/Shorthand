using AppBuilder.DTO;
using Newtonsoft.Json;
using PragmaTouchUtils;
using Shorthand.Common;
using Shorthand.GitLabEntity;
using Shorthand.JiraEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    private Application _appSetting;
    private DelphiBuilderService _builderService;


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

      //cmbGitProjectName.SelectedValueChanged += this.SelectedProjectChanged;
      cmbGitProjectName.SelectedItem = items?.FirstOrDefault(x => x.Text == _gitLabOptions.DefaultGitProjectName);

      lblMergeRequestLink.LinkClicked += (x, y) =>
      {
        if (y.Link.LinkData != null)
        { Process.Start(y.Link.LinkData as string); }
        else
        { this.Dump("LinkData not found"); }
      };

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


    private bool EnsureAppSettings()
    {
      try
      {
        try
        {
          string json = string.Empty;
          if (!File.Exists("config.json"))
          {
            json = _gitLab.GetSnippet(_gitLabOptions.ConfigSnippetId);
            File.WriteAllText("config.json", json);
          }
          else
            json = File.ReadAllText("config.json");

          var projectId = (int)cmbGitProjectName.SelectedValue;
          var config = CodebaseConfig.FromJson(json);
          _appSetting = config?.Applications.FirstOrDefault(x => x.GitProperties.ProjectId == projectId);
          _builderService = config?.DelphiBuilderService;

          return _appSetting != null && _builderService != null;
        }
        catch (Exception ex)
        {
          this.Dump(ex.AggregateExceptionMessages());
          return false;
        }
      }
      finally
      {
        this.Dump($"{_appSetting?.Codebase ?? "no codebase"} set for gitLab");
        this.Dump($"{_builderService?.HostName ?? "no host"} : {_builderService?.Port ?? "no port"} set for builder service");
      }
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

      var builderServiceEnabled = this.EnsureAppSettings();
      btnMakeExecutable.Enabled = builderServiceEnabled;
      btnPing.Enabled = builderServiceEnabled;
      if (!builderServiceEnabled)
      {
        this.Dump("Disabled builder service.");
      }

      if (!_jiraOptions.IsValid)
      {
        this.Dump("ERROR: Please configure jira options");
        return false;
      }

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
        this.Dump(ex.AggregateExceptionMessages());
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

      var projectId = (int)(cmbGitProjectName.SelectedValue ?? 0);
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
      var projectId = (int)cmbGitProjectName.SelectedValue;
      var project = await _gitLab.GetProjectByIdAsync(projectId);
      var mergeReq = await _gitLab.GetMergeRequestByInternalIssueKeyAsync(projectId, ctx.InternalIssue);

      ctx.GitProjectName = project.name;
      ctx.GitProjectId = project.id;
      ctx.GitProjectWebUrl = project.web_url;
      ctx.GitMergeRequestNo = mergeReq?.iid ?? 0;
      ctx.GitMergeRequestState = mergeReq?.state ?? "unknown";

      //var requests = await _gitLab.GetMergeRequestsAsync(projectId);
      //var r = requests.FirstOrDefault(x => x.source_branch.Contains(ctx.InternalIssue));
      //ctx.GitMergeRequestNo = r == null ? 0 : r.id;
      //ctx.GitMergeRequestState = r == null ? string.Empty : r.state;

      if (_appSetting != null)
      {
        ctx.HasSoxWorkflow = _appSetting.hasSoxWorkflow;
        ctx.Database = _appSetting.Database;
        ctx.LocalBinFolder = _appSetting.LocalBinFolder;
        ctx.DeliveryTestFolder = _appSetting.DeliveryTestFolder;
        ctx.DeliveryProductionFolder = _appSetting.DeliveryProductionFolder;
      }

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

    public void HandleResponse(MessageWrapper message)
    {
      this.Invoke(new Action(() => DumpMessage(message)));
    }

    private void DumpMessage(MessageWrapper message)
    {
      var pageKey = $"Request_{message.RequestId}";
      var page = tabMonitor.TabPages[pageKey];
      if (page == null)
      {
        page = new TabPage($"Request {message.RequestId}") { Name = pageKey, Padding = new Padding(3) };
        tabMonitor.TabPages.Add(page);
        page.Enter += (object sender, EventArgs e) => { page.ImageIndex = -1; };
        var font = new System.Drawing.Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(162)));
        var box = new TextBox() { Name = $"box_{message.RequestId}", Dock = DockStyle.Fill, ForeColor = Color.Lime, BackColor = Color.Black, Multiline = true, Font = font, ScrollBars = ScrollBars.Vertical };
        tabMonitor.TabPages[pageKey].Controls.Add(box);
        box.BringToFront();
      }

      page.ImageIndex = tabMonitor.SelectedTab == page ? -1 : 1;
      var destinationBox = this.Controls.Find($"box_{message.RequestId}", true).FirstOrDefault();
      (destinationBox as TextBox)?.Log(message.Message);
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
      tabMonitor.TabPages.OfType<TabPage>().Where(x => x.ImageIndex == -1).ToList().ForEach(x => tabMonitor.TabPages.Remove(x));
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



    private void btnOpenLocal_Click(object sender, EventArgs e)
    {
      this.OpenFolder(_appSetting?.LocalBinFolder);
    }

    private void btnOpenDeployment_Click(object sender, EventArgs e)
    {
      this.OpenFolder(_appSetting?.DeliveryProductionFolder);
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




    private void btnPing_Click(object sender, EventArgs e)
    {
      var remoteBuilder = this.GetBuilder();

      this.Dump($"ping");
      Task.Run(() => remoteBuilder.Ping());
    }

    private void btnBrew_Click(object sender, EventArgs e)
    {
      var remoteBuilder = this.GetBuilder();
      this.Dump($"brew");
      Task.Run(() => remoteBuilder.Brew());
    }

    private void btnMakeExecutable_Click(object sender, EventArgs e)
    {
      var remoteBuilder = this.GetBuilder();
      var args = new string[1] { cmbGitProjectName.Text };
      Task.Run(() => remoteBuilder.Build(args));
    }

    private RemoteBuilder GetBuilder()
    {
      //var remoteHost = "eoyuktas-ESPRIMO-p900";
      //var remoteHost = "localhost";
      //var remotePort = 3390;

      var remoteHost = _builderService.HostName;
      Int32.TryParse(_builderService.Port, out int remotePort);

      return new RemoteBuilder(remoteHost, remotePort, this.HandleResponse);
    }


  }
}
