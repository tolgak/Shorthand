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
using Newtonsoft.Json;
using PragmaTouchUtils;


namespace Shorthand
{
  public partial class frmDeployment : Form
  {
    private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;

    public frmDeployment()
    {
      InitializeComponent();

      lblREQ_IssueNumber.Text = string.Format("{0} #", _jiraOptions.REQ_ProjectKey);
      lblDPLY_IssueNumber.Text = string.Format("{0} #", _jiraOptions.DPLY_ProjectKey);
      lblUAT_IssueNumber.Text  = string.Format("{0} #", _jiraOptions.UAT_ProjectKey);

      this.RefreshUI();
    }

    private void btnBuild_Click(object sender, EventArgs e)
    {
      var delivery = this.BuildDelivery();

      var references = new Dictionary<string, string>();
      references.Add("internalIssueKey", txtInternal.Text);
      references.Add("requestIssueKey", string.Format("{0}-{1}", _jiraOptions.REQ_ProjectKey, txtREQ.Text));

      var deploymentIssueKey = string.IsNullOrEmpty(txtDPLY.Text) ? "" : string.Format("{0}-{1}", _jiraOptions.DPLY_ProjectKey, txtDPLY.Text);
      references.Add("deploymentIssueKey", deploymentIssueKey);

      var uatIssueKey = string.IsNullOrEmpty(txtUAT.Text) ? "" : string.Format("{0}-{1}", _jiraOptions.UAT_ProjectKey, txtUAT.Text);
      references.Add("uatIssueKey", uatIssueKey);

      references.Add("gitProjectName", cmbGitProjectName.SelectedText);
      references.Add("gitMergeRequestNo", txtGitMergeRequestNo.Text);

      delivery.Deliver(references);
//      txtDump.Text = delivery.BuilComment(references);
    }

    private IDelivery BuildDelivery()
    {
      if ( rdbProduction.Checked )
        return new DeliveryToProduction();

      if ( rdbTest.Checked )
        return new DeliveryToTest();

      return null;
    }
                   
    private void btnJIRA_Click(object sender, EventArgs e)
    {
      var jira = new Jira();
      jira.CreateLink( "Production", "ARG-47", "ARG-66");

    }

    private void rdbProduction_CheckedChanged(object sender, EventArgs e)
    {
      this.RefreshUI();
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
