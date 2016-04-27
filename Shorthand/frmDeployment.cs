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

namespace Shorthand
{
  public partial class frmDeployment : Form
  {
    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
    private DeploymentOptions _deplyOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
    private Jira _jira;

    public frmDeployment()
    {
      InitializeComponent();

      lblREQ_IssueKey.Text  = _jiraOptions.REQ_ProjectKey;
      lblDPLY_IssueKey.Text = _jiraOptions.DPLY_ProjectKey;
      lblUAT_IssueKey.Text  = _jiraOptions.UAT_ProjectKey;

      cmbGitProjectName.Items.Add("Bilgi.Scientia.Integration");
      cmbGitProjectName.Items.Add("Bilgi.Sis.BackOffice");
      cmbGitProjectName.Items.Add("BilgiCampus");

      _jira = new Jira( (x) => this.Dump(x) );
      
      this.RefreshUI();
    }

    private void btnBuild_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(txtInternal.Text))
      {
        this.Dump("ERROR: Please provide an internal issue key\r\n");
        return;
      }

      if (rdbProduction.Checked)
        this.Dump("Delivering to production\r\n");
      if (rdbTest.Checked)
        this.Dump("Delivering to test\r\n");

      txtREQ.Clear();
      txtDPLY.Clear();
      txtUAT.Clear();
      Application.DoEvents();

      var ctx = this.BuildDeliveryContext(txtInternal.Text);
      if (string.IsNullOrEmpty(ctx.RequestIssueKey))
      {
        this.Dump("ERROR: Can not locate request issue key\r\n");
        return;
      }

      ctx.GitProjectName = "";
      ctx.GitMergeRequestNo = "";

      txtREQ.Text  = ctx.RequestIssueKey;
      txtDPLY.Text = ctx.DeploymentIssueKey;
      txtUAT.Text  = ctx.UatIssueKey;
      Application.DoEvents();

      var delivery = this.BuildDelivery();
      delivery.Deliver(ctx);

      txtREQ.Text  = ctx.RequestIssueKey;
      txtDPLY.Text = ctx.DeploymentIssueKey;
      txtUAT.Text  = ctx.UatIssueKey;
      this.Dump("DONE.");

    }



    private void rdbProduction_CheckedChanged(object sender, EventArgs e)
    {
      this.RefreshUI();
    }

    private void btnJIRA_Click(object sender, EventArgs e)
    {
      SendMail();
      //var internalIssueKey = txtInternal.Text;
      //var transitions = _jira.GetTransitionsForIssue(internalIssueKey);
      //var q = transitions.FirstOrDefault(x => x.name == "Deployed for Test");
    }

    private DeliveryContext BuildDeliveryContext(string issueKey)
    {
      var ctx = new DeliveryContext();
      var linksOfInternalIssue = this.GetLinksOfIssue(issueKey);
      ctx.InternalIssueKey   = issueKey;
      ctx.RequestIssueKey    = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.REQ_ProjectKey));
      ctx.DeploymentIssueKey = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.DPLY_ProjectKey));


      if (!string.IsNullOrEmpty(ctx.RequestIssueKey))
      {
        var linksOfReqIssue = this.GetLinksOfIssue(ctx.RequestIssueKey);
        ctx.UatIssueKey = linksOfReqIssue.FirstOrDefault(x => x.Contains(_jiraOptions.UAT_ProjectKey));
      }

      return ctx;
    }


                   
    private string[] GetLinksOfIssue(string issueKey)
    {      
      var issueLinks = _jira.GetLinksOfIssue(issueKey);
      var q =     (from x in issueLinks where x.inwardIssue  != null select x.inwardIssue.key)
            .Union(from x in issueLinks where x.outwardIssue != null select x.outwardIssue.key)
            .ToArray();

      return q;
    }

    private IDelivery BuildDelivery()
    {
      if ( rdbProduction.Checked )
        return new DeliveryToProduction( x => this.Dump(x));

      if ( rdbTest.Checked )
        return new DeliveryToTest();

      return null;
    }

    private void Dump(string line)
    {       
      txtDump.InvokeIfRequired( (x) => { x.Text = x.Text + line;} );
    }



    private void RefreshUI()
    {
      //cmbGitProjectName.SelectedIndex = cmbGitProjectName.Items.IndexOf(_deplyOptions.DefaultGitProjectName);
      cmbGitProjectName.Enabled = rdbProduction.Checked;
      txtGitMergeRequestNo.Enabled = rdbProduction.Checked;
    }

    private void btnClearLog_Click(object sender, EventArgs e)
    {
      txtDump.Clear();
    }

    public void SendMail()
    {
      var mail = new MailMessage("tolgak@bilgi.edu.tr", "tolga.kurkcuoglu@gmail.com");
      var client = new SmtpClient();

      client.Port = 25;
      client.DeliveryMethod = SmtpDeliveryMethod.Network;
      client.UseDefaultCredentials = true;
      client.Host = "stone.bilgi.edu.tr";
      mail.Subject = "this is a test email.";
      mail.Body = "this is my test email body";
      client.Send(mail);


    }


  }
}
