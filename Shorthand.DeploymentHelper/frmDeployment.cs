using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

using System.ComponentModel.Composition;
using PragmaTouchUtils;
using Shorthand.Common;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shorthand
{

    [Export(typeof(IPluginMarker))]
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


        public async Task InitializeAsync(IPluginContext context)
        {
            _context = context;
            this.MdiParent = _context.Host;
            _context.Configuration.LoadConfiguration();

            var strips = _context.Host.MainMenuStrip.Items.Find("mnuTools", true);
            if (strips.Length == 0)
                return;

            var subItem = new ToolStripMenuItem(this.Text);
            if (this.Icon != null)
                subItem.Image = this.Icon.ToBitmap();
            (strips[0] as ToolStripMenuItem).DropDownItems.Add(subItem);
            subItem.Click += (object sender, EventArgs e) => { this.Show(); };

            if (_context.Host is IPluginHost host)
                host.onSettingsChanged += this.OnSettingsChangedEventHandler;

            this.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                e.Cancel = true;
                this.Hide();
            };

            this.InitializePlugin();
            await this.InitializeUIAsync();
        }




        private void InitializePlugin()
        {
            _deployOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;

            _jira = _jira ?? new Jira(x => this.Dump(x));
            _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;

            _gitLab = _gitLab ?? new GitLab(x => this.Dump(x));
            _gitLabOptions = ConfigContent.Current.GetConfigContentItem("GitLabOptions") as GitLabOptions;
        }

        private async Task InitializeUIAsync()
        {
            lblREQ_IssueKey.Text = _jiraOptions.REQ_ProjectKey;
            lblDPLY_IssueKey.Text = _jiraOptions.DPLY_ProjectKey;
            lblUAT_IssueKey.Text = _jiraOptions.UAT_ProjectKey;

            lblInternal_Status.Text = string.Empty;
            lblREQ_Status.Text = string.Empty;
            lblDPLY_Status.Text = string.Empty;
            lblUAT_Status.Text = string.Empty;

            cmbUAT.DisplayMember = "Text";
            cmbUAT.ValueMember = "Value";

            var projects = await _gitLab.GetProjectsAsync(true);
            var items = projects.OrderBy(x => x.name).Select(x => new LookupItem { Text = x.name, Value = x.id }).ToArray();

            cmbGitProjectName.DisplayMember = "Text";
            cmbGitProjectName.ValueMember = "Value";
            cmbGitProjectName.Items.AddRange(items);
            cmbGitProjectName.SelectedItem = items.FirstOrDefault(x => x.Text == _gitLabOptions.DefaultGitProjectName);

            lblMergeRequestLink.LinkClicked += (x, y) => { Process.Start(y.Link.LinkData as string); };

            rdbProduction.CheckedChanged -= Rdb_CheckedChanged;
            rdbProduction.CheckedChanged += Rdb_CheckedChanged;

            rdbTest.CheckedChanged -= Rdb_CheckedChanged;
            rdbTest.CheckedChanged += Rdb_CheckedChanged;
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
                var projectId = cmbGitProjectName.GetSelectedValue();
                var ctx = await this.BuildDeliveryContext(txtInternal.Text, projectId);

                if (string.IsNullOrEmpty(ctx.RequestIssueKey))
                {
                    this.Dump("ERROR: Can not locate request issue");
                    return;
                }

                var deployment = this.BuildDelivery();
                deployment.Deliver(ctx);
                this.SendMail(ctx);

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


        private void ClearDisplayedState()
        {
            lblInternal_Status.Text = string.Empty;
            lblREQ_Status.Text      = string.Empty;
            lblDPLY_Status.Text     = string.Empty;
            lblUAT_Status.Text      = string.Empty;

            txtREQ.Clear();
            txtDPLY.Clear();
            txtUAT.Clear();
            cmbUAT.Items.Clear();
            txtGitMergeRequestNo.Clear();

            lblMergeRequestLink.Text = $"merge request state unknown";
            lblMergeRequestLink.Links.Clear();

            Application.DoEvents();
        }

        private async Task DisplayState(DeliveryContext ctx)
        {
            var tasks = new List<Task<string>> { _jira.GetStatusOfIssueAsync(ctx.InternalIssueKey)
                                               , _jira.GetStatusOfIssueAsync(ctx.RequestIssueKey)
                                               , _jira.GetStatusOfIssueAsync(ctx.DeploymentIssueKey)
                                               , _jira.GetStatusOfIssueAsync(ctx.UatIssueKey) };
            string[] status = await Task.WhenAll(tasks);

            txtREQ.Text = ctx.RequestIssueKey;
            txtDPLY.Text = ctx.DeploymentIssueKey;
            txtUAT.Text = ctx.UatIssueKey;
            cmbUAT.Items.Clear();
            if (ctx.UatIssueKeys.Count() > 1)
            {
                cmbUAT.Items.AddRange(ctx.UatIssueKeys.Select(x => new { Text = x, Value = x }).ToArray());
                cmbUAT.SelectedIndex = 0;
            }
            txtGitMergeRequestNo.Text = ctx.GitMergeRequestNo.ToString();

            lblInternal_Status.Text = string.IsNullOrEmpty(status[0]) ? "N/A" : status[0];
            lblREQ_Status.Text      = string.IsNullOrEmpty(status[1]) ? "N/A" : status[1];
            lblDPLY_Status.Text     = string.IsNullOrEmpty(status[2]) ? "N/A" : status[2];
            lblUAT_Status.Text      = string.IsNullOrEmpty(status[3]) ? "N/A" : status[3];

            var mergeReqUrl = $"{ctx.GitProjectWebUrl}/merge_requests/{ctx.GitMergeRequestNo}";
            lblMergeRequestLink.Text = $"merge request state {ctx.GitMergeRequestState}";
            lblMergeRequestLink.Links.Add(0, lblMergeRequestLink.Text.Length, mergeReqUrl);
        }

        private async Task RefreshUIAsync()
        {
            if (!this.SanityCheck())
                return;

            this.ClearDisplayedState();

            var projectId = cmbGitProjectName.GetSelectedValue();
            var ctx = await this.BuildDeliveryContext(txtInternal.Text, projectId);
            if (string.IsNullOrEmpty(ctx.RequestIssueKey))
                this.Dump("WARNING: Can not locate request issue");

            await DisplayState(ctx);
        }


        private IDelivery BuildDelivery()
        {
            if (rdbProduction.Checked)
                return new DeliveryToProduction(x => this.Dump(x));
            else if (rdbTest.Checked)
                return new DeliveryToTest(x => this.Dump(x));

            return null;
        }

        private async Task<DeliveryContext> BuildDeliveryContext(string issueKey, int projectId)
        {
            var ctx = new DeliveryContext();
            if (rdbProduction.Checked)
                ctx.DeliveryTo = DeliveryContext.ToProduction;
            else if (rdbTest.Checked)
                ctx.DeliveryTo = DeliveryContext.ToTest;

            ctx.CreateDeploymentIssue = chkCreateDPLY.Checked;
            ctx.CreateUatIssue = chkCreateUAT.Checked;
            ctx.CreateMergeRequest = chkCreateMergeRequest.Checked;
            ctx.CopyExecutables = chkCopyExecutables.Checked;

            // Jira
            var linksOfInternalIssue = await this.GetLinksOfIssueAsync(issueKey);
            ctx.InternalIssueKey = issueKey;
            ctx.RequestIssueKey = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.REQ_ProjectKey));
            ctx.DeploymentIssueKey = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.DPLY_ProjectKey));
            if (!string.IsNullOrEmpty(ctx.RequestIssueKey))
            {
                var linksOfReqIssue = await this.GetLinksOfIssueAsync(ctx.RequestIssueKey);
                ctx.UatIssueKeys = linksOfReqIssue.Where(x => x.Contains(_jiraOptions.UAT_ProjectKey)).ToArray();
                
                if (cmbUAT.Items.Count > 1)
                    ctx.UatIssueKey = cmbUAT.SelectedText;
                else
                    ctx.UatIssueKey = linksOfReqIssue.FirstOrDefault(x => x.Contains(_jiraOptions.UAT_ProjectKey));
            }

            // GitLab      
            var project = await _gitLab.GetProjectByIdAsync(projectId);
            var mergeReq = await _gitLab.GetMergeRequestByInternalIssueKeyAsync(projectId, issueKey);
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
              .AppendLine($"Internal Issue Key : {ctx.InternalIssueKey}")
              .AppendLine($"Request Issue Key : {ctx.RequestIssueKey}")
              .AppendLine($"Deployment Issue Key : {ctx.DeploymentIssueKey}")
              .AppendLine($"UAT Issue Key : {ctx.UatIssueKey}")
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

        private async Task<string[]> GetLinksOfIssueAsync(string issueKey)
        {
            var issueLinks = await _jira.GetLinksOfIssueAsync(issueKey);
            var q1 = issueLinks.Where(x => x.inwardIssue != null).Select(x => x.inwardIssue.key);
            var q2 = issueLinks.Where(x => x.outwardIssue != null).Select(x => x.outwardIssue.key);

            return q1.Union(q2).ToArray();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtDump.Clear();
        }



        public void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
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

            this.InitializePlugin();
            this.InitializeUIAsync().ConfigureAwait(false);
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
            var ctx = await this.BuildDeliveryContext(txtInternal.Text, projectId);

            if (!string.IsNullOrEmpty(ctx.InternalIssueKey))
                Process.Start($"{_jiraOptions.JiraBaseUrl}/browse/{ctx.InternalIssueKey}");
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

        private void btnMakeExecutable_Click(object sender, EventArgs e)
        {
            var buildScriptFile = "D:\\Development\\GitProjects\\Bilgi.Sis.BackOffice\\build.cmd";
            var workingDirectory = "D:\\Development\\GitProjects\\Bilgi.Sis.BackOffice";

            var pi = new ProcessStartInfo
            {
                WorkingDirectory = workingDirectory,
                FileName = buildScriptFile,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            Process p = new Process { StartInfo = pi };
            p.EnableRaisingEvents = true;
            p.OutputDataReceived += (s, args) => { txtDump.Log(args.Data); };
            p.ErrorDataReceived += (s, args) => { txtDump.Log($"ERROR: {args.Data}"); };
            p.Exited += (s, args) => { txtDump.Log($"Exit Code {p.ExitCode}"); };
            p.Start();
            p.BeginOutputReadLine();

            p.WaitForExit(5000);
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

        private void cmbUAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUAT.Text = cmbUAT.SelectedText;
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
