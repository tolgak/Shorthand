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


namespace Shorthand
{
  public partial class frmDeployment : Form
  {
    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;
    private DeploymentOptions _deplyOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;

    public frmDeployment()
    {
      InitializeComponent();

      lblREQ_IssueKey.Text  = _jiraOptions.REQ_ProjectKey;
      lblDPLY_IssueKey.Text = _jiraOptions.DPLY_ProjectKey;
      lblUAT_IssueKey.Text  = _jiraOptions.UAT_ProjectKey;

      cmbGitProjectName.Items.Add("Bilgi.Els");
      cmbGitProjectName.Items.Add("Bilgi.Els.BackOffice");
      cmbGitProjectName.Items.Add("Bilgi.Scientia.Integration");
      cmbGitProjectName.Items.Add("Bilgi.Sis.BackOffice");
      cmbGitProjectName.Items.Add("BilgiCampus");
      cmbGitProjectName.SelectedItem = _deplyOptions.DefaultGitProjectName;

      this.RefreshUI();
    }

    private void btnBuild_Click(object sender, EventArgs e)
    {
      if ( string.IsNullOrEmpty(txtInternal.Text) )
      { 
        MessageBox.Show(this, "Please provide an internal issue key", "Error");
        return;
      }

      this.QueryLinkedIssues();
      if (string.IsNullOrEmpty(txtREQ.Text))
      {
        MessageBox.Show(this, "Can not locate request issue key", "Error");
        return;
      }

      var delivery = this.BuildDelivery();
      var references = this.BuildReferences();

      //delivery.Deliver(references);
      //txtDump.Text = delivery.BuilComment(references);
    }



    private void rdbProduction_CheckedChanged(object sender, EventArgs e)
    {
      this.RefreshUI();
    }

    private void btnJIRA_Click(object sender, EventArgs e)
    {

    }

    private void QueryLinkedIssues()
    {
      var linksOfInternalIssue = this.GetLinksOfIssue(txtInternal.Text);
      var reqIssueKey = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.REQ_ProjectKey));
      txtREQ.Text = reqIssueKey;
      txtDPLY.Text = linksOfInternalIssue.FirstOrDefault(x => x.Contains(_jiraOptions.DPLY_ProjectKey));

      if (!string.IsNullOrEmpty(reqIssueKey))
      {
        var linksOfReqIssue = this.GetLinksOfIssue(reqIssueKey);
        txtUAT.Text = linksOfReqIssue.FirstOrDefault(x => x.Contains(_jiraOptions.UAT_ProjectKey));
      }
    }

    private Dictionary<string, string> BuildReferences()
    {
      var references = new Dictionary<string, string>();
      references.Add("internalIssueKey", txtInternal.Text);
      references.Add("requestIssueKey", txtREQ.Text);

      var deploymentIssueKey = string.IsNullOrEmpty(txtDPLY.Text) ? "" : txtDPLY.Text;
      references.Add("deploymentIssueKey", deploymentIssueKey);

      var uatIssueKey = string.IsNullOrEmpty(txtUAT.Text) ? "" : txtUAT.Text;
      references.Add("uatIssueKey", uatIssueKey);

      references.Add("gitProjectName", cmbGitProjectName.SelectedText);
      references.Add("gitMergeRequestNo", txtGitMergeRequestNo.Text);

      return references;
    }

    private IDelivery BuildDelivery()
    {
      if ( rdbProduction.Checked )
        return new DeliveryToProduction();

      if ( rdbTest.Checked )
        return new DeliveryToTest();

      return null;
    }
                   
    private string[] GetLinksOfIssue(string issueKey)
    {
      var jira = new Jira();
      var issueLinks = jira.GetLinksOfIssue(issueKey);
      var q =     (from x in issueLinks where x.inwardIssue != null select x.inwardIssue.key)
            .Union(from x in issueLinks where x.outwardIssue != null select x.outwardIssue.key)
            .ToArray();

      return q;
    }

    private void Dump(string line)
    { 
      txtDump.Text = txtDump.Text + line + "\r\n";
    }

    private void RefreshUI()
    {
      txtDPLY.Enabled = rdbProduction.Checked;


      txtUAT.Enabled = rdbProduction.Checked;
      cmbGitProjectName.Enabled = rdbProduction.Checked;
      txtGitMergeRequestNo.Enabled = rdbProduction.Checked;
    }


  }
}
