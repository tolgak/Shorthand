using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PragmaTouchUtils;
using Shorthand.Common;
using Shorthand.Common.Sql;

namespace Shorthand.Queries
{
  [Export(typeof(IAsyncPlugin))]
  public partial class frmQueryProcessor : Form, IAsyncPlugin
  {
    private IPluginContext _context;
    private string _connectionString;

    public frmQueryProcessor()
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
      return await Task.Run(() => 
      {

        return true;
      });
    }
    private async Task<bool> InitializeUI()
    {
      return await Task.Run(() =>
      {
        _connectionString = SqlUtility.GetConnectionString("stockOffice");
        //this.FillTreeList();

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


    private async void FillTreeList()
    {
      lblStatus.Text = "Populating";
      tvQuery.Nodes.Clear();

      try
      {
        var connectionString = _connectionString;
        var commandText = "select * from Query order by ParentId, Id";

        var dataSet = await SqlUtility.BuildDataSet(connectionString, commandText);
        foreach (DataTable table in dataSet.Tables)
        {
          foreach (DataRow dr in table.Rows)
          {
            var parent = tvQuery.Nodes.Find(dr["ParentID"].ToString(), true).FirstOrDefault();
            if (parent != null)
            {
              parent.Nodes.Add(dr["ID"].ToString(), dr["Name"].ToString()).Tag = dr["ID"].ToString();
            }
            else
              tvQuery.Nodes.Add(dr["ID"].ToString(), dr["Name"].ToString()).Tag = dr["ID"].ToString();
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

    private void btnRun_Click(object sender, EventArgs e)
    {
      
    }
  }
}
