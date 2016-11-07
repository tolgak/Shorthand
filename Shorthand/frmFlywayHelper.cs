using System;
using System.Windows.Forms;

namespace Shorthand
{
  public partial class frmFlywayHelper : Form
  {

    //private Jira _jira;
    //private JiraOptions _jiraOptions = ConfigContent.Current.GetConfigContentItem("JiraOptions") as JiraOptions;

    public frmFlywayHelper()
    {
      //InitializeComponent();

      //_jira = new Jira();
      //var projects = _jira.GetProjects();
      //var items = (from x in projects
      //             orderby x.key
      //             select new Tuple<string, string>(x.name, x.key)).ToArray();

      //cmbProjects.DisplayMember = "Item1";
      //cmbProjects.ValueMember = "Item2";
      //cmbProjects.Items.AddRange(items);
      
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      //var issues = _jira.GetIssuesOfAssignee("tolgak");

    }

    private void grdIssue_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
