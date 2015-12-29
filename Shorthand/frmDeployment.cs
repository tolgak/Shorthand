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
    public frmDeployment()
    {
      InitializeComponent();
    }

    private void btnBuild_Click(object sender, EventArgs e)
    {
      var delivery = this.BuildDelivery();

      var references = new Dictionary<string, string>();
      references.Add("requestIssueKey", string.Format("TALEP-{0}", txtTalep.Text));
      //references.Add("deploymentIssueKey", string.Format("DPLY-{0}", txtDPLY.Text));
      references.Add("uatIssueKey", string.Format("UAT-{0}", txtTalep.Text));
      references.Add("internalIssueKey", txtInternal.Text);
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
      txtDPLY.Enabled = rdbProduction.Checked;
      txtUAT.Enabled  = rdbProduction.Checked;
      cmbGitProjectName.Enabled = rdbProduction.Checked;
      txtGitMergeRequestNo.Enabled = rdbProduction.Checked;
    }


  }
}
