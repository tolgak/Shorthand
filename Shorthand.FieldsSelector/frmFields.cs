using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using PragmaTouchUtils;
using Shorthand.Common;
using Shorthand.Common.Sql;

namespace Shorthand.FieldSelector
{
  [Export(typeof(IAsyncPlugin))]
  public partial class frmFields : Form, IAsyncPlugin
  {
    private IPluginContext _context;

    public frmFields()
    {
      InitializeComponent();
    }

    public async Task<Form> InitializeAsync(IPluginContext context)
    {
      return await Task.Run(async () =>
      {
        _context = context;

        this.FormClosing += (object sender, FormClosingEventArgs e) =>
        {
          e.Cancel = true;
          this.Hide();
        };

        await this.InitializePlugin();
        await this.InitializeUI();

        return this;
      });
    }
    private async Task<bool> InitializePlugin()
    {
      return await Task.Run(() => { return true; });
    }
    private async Task<bool> InitializeUI()
    {
      return await Task.Run(() =>
      {
        txtConnection.Text = SqlUtility.GetConnectionString("pandoradev");
        return true;
      });
    }
    public async void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    {
      var shouldRefresh = e.ChangedOptions.Contains("FieldSelectOptions");
      if (!shouldRefresh)
        return;

      await this.InitializePlugin();
      await this.InitializeUI();
    }


    //private Task<DataSet> BuildDataSet(string connectionString, string commandText)
    //{
    //  return Task.Run(() =>
    //  {
    //    DataSet dataSet = new DataSet("DataDump");
    //    using (var connection = new SqlConnection(connectionString))
    //    {
    //      using (var command = connection.CreateCommand())
    //      {
    //        command.CommandType = CommandType.Text;
    //        command.CommandText = commandText;

    //        command.Connection.ConnectionString = connectionString;
    //        command.Connection.Open();
    //        using (var adapter = new SqlDataAdapter(command))
    //        {
    //          adapter.SelectCommand.CommandTimeout = 15000;
    //          adapter.Fill(dataSet);
    //        }

    //        return dataSet;
    //      }
    //    }
    //  }
    //  );
    //}

    private async void FillTreeList()
    {
      lblStatus.Text = "Populating";
      tvFieldList.Nodes.Clear();

      try
      {
        var connectionString = txtConnection.Text;
        var commandText = "exec spFields_GetTreeList";

        var dataSet = await SqlUtility.BuildDataSet(connectionString, commandText);
        foreach (DataTable table in dataSet.Tables)
        {
          foreach (DataRow dr in table.Rows)
          {
            var parent = tvFieldList.Nodes.Find(dr["ParentID"].ToString(), true).FirstOrDefault();
            if (parent != null)
            {
              parent.Nodes.Add(dr["ID"].ToString(), dr["ItemName"].ToString()).Tag = dr["ID"].ToString();
            }
            else
              tvFieldList.Nodes.Add(dr["ID"].ToString(), dr["ItemName"].ToString()).Tag = dr["ID"].ToString();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        lblStatus.Text = "";
      }
    }

    private void btnFill_Click(object sender, EventArgs e)
    {
      this.FillTreeList();
    }

    private void tvFieldList_DoubleClick(object sender, EventArgs e)
    {
      if (tvFieldList.SelectedNode.Nodes != null && tvFieldList.SelectedNode.Nodes.Count > 0)
        return;

      tvFieldList.SelectedNode.Checked = !tvFieldList.SelectedNode.Checked;
    }


    private void btnTraverse_Click(object sender, EventArgs e)
    {
      var nodes = this.Collect(tvFieldList.Nodes).Where(x => x.Checked).Select(x => x.Tag.ToString()).ToList();
      var selectedFields = string.Join(",", nodes);

      txtLog.Clear();
      txtLog.Text = selectedFields;

      lblStatus.Text = "Running";
      try
      {
        var connectionString = txtConnection.Text;
        var commandText = $"exec spFields_BuildSql '{selectedFields}'";

        var c = new SqlConnection(connectionString); // Your Connection String here
        var dataAdapter = new SqlDataAdapter(commandText, c);
        var commandBuilder = new SqlCommandBuilder(dataAdapter);
        var ds = new DataSet();
        dataAdapter.Fill(ds);

        var script = ds.Tables[0].Rows[0]["sqlScript"].ToString();
        txtLog.AppendText(Environment.NewLine + script);

        grd.ReadOnly = true;
        grd.DataSource = ds.Tables[1];
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        lblStatus.Text = "";
      }




    }

    private IEnumerable<TreeNode> Collect(TreeNodeCollection nodes)
    {
      foreach (TreeNode node in nodes)
      {
        yield return node;

        foreach (var child in Collect(node.Nodes))
          yield return child;
      }
    }


  }



}
