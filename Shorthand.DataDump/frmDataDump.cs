using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

using System.ComponentModel.Composition;
using Shorthand.Common;
using System.Drawing;
using System.Threading.Tasks;
using System.Linq;
using PragmaTouchUtils;

namespace Shorthand
{

  [Export(typeof(IPluginMarker))]
  public partial class frmDataDump : Form, IAsyncPlugin
  {

    private IPluginContext _context;

    public frmDataDump()
    {
      InitializeComponent();      
    }

    //public Form Initialize(IPluginContext context)
    //{
    //  _context = context;

    //  //this.MdiParent = _context.Host;
    //  //var mnuTools = _context.Host.MainMenuStrip.Items.Find("mnuTools", true).FirstOrDefault();
    //  //if (mnuTools == null)
    //  //  return;

    //  //var subItem = new ToolStripMenuItem(this.Text);
    //  //if (this.Icon != null)
    //  //  subItem.Image = this.Icon.ToBitmap();
    //  //(mnuTools as ToolStripMenuItem).DropDownItems.Add(subItem);
    //  //subItem.Click += (object sender, EventArgs e) => { this.Show(); };

    //  this.FormClosing += (object sender, FormClosingEventArgs e) => { e.Cancel = true; this.Hide(); };

    //  this.InitializePlugin();
    //  this.InitializeUI();

    //  return this;
    //}
    //private void InitializePlugin()
    //{

    //}
    //private void InitializeUI()
    //{
    //  txtConnection.Text = this.GetDefaultConnectionString();
    //  dlgLoad.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
    //}
    //public void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    //{
    //  var shouldRefresh = e.ChangedOptions.Contains("FieldSelectOptions");
    //  if (!shouldRefresh)
    //    return;

    //  this.InitializePlugin();
    //  this.InitializeUI();
    //}


    public async Task<Form> InitializeAsync(IPluginContext context)
    {
      return await Task.Run(async () =>
      {
        _context = context;
        _context.Configuration.LoadConfiguration();

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
        txtConnection.Text = this.GetDefaultConnectionString();
        dlgLoad.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
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






    private void btnDump_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      IDataDumper dumper = null;

      if (rdExcel.Checked)
        dumper = new ExcelDataDump();
      else if (rdCSV.Checked)
        dumper = new CSVDataDump();

      pbRecordCounter.Value = 0;
      pbRecordsetCounter.Value = 0;

      try
      {
        var connectionString = txtConnection.Text;
        var commandText = txtEditor.Text;
        var fileNameFormat = txtFileNameFormat.Text;

        DataSet dataSet = this.BuildDataSet(connectionString, commandText);

        dumper.OnRecordsetProgress += dumper_OnRecordSetProgress;
        dumper.OnRecordProgress += dumper_OnRecordProgress;

        dumper.Dump2(dataSet, fileNameFormat);
      }
      finally
      {
        pbRecordCounter.Value = 0;
        pbRecordsetCounter.Value = 0;

        dumper.OnRecordsetProgress -= dumper_OnRecordSetProgress;
        dumper.OnRecordProgress -= dumper_OnRecordProgress;

        this.Cursor = Cursors.Default;
      }
    }

    private void dumper_OnRecordProgress(object sender, RecordProgressEventArgs e)
    {
      if (pbRecordCounter.Maximum == 0)
        pbRecordCounter.Maximum = e.NumberOfRecords;

      pbRecordCounter.Increment(1);
    }

    private void dumper_OnRecordSetProgress(object sender, RecordsetProgressEventArgs e)
    {
      pbRecordCounter.Value = 0;
      pbRecordCounter.Maximum = 0;

      if (pbRecordsetCounter.Maximum == 0)
        pbRecordsetCounter.Maximum = e.NumberOfRecordsets;

      pbRecordsetCounter.Increment(1);
    }

    private string GetDefaultConnectionString()
    {
      var result = string.Empty;
      
      foreach (var item in ConfigurationManager.ConnectionStrings)
      {
        var x = (item as ConnectionStringSettings).ElementInformation.Properties["name"];
        if (x.Value.ToString() != "Default")
          continue;

        var y = (item as ConnectionStringSettings).ElementInformation.Properties["connectionString"];
        result = y.Value.ToString();
      }

      return result;
    }

    private DataSet BuildDataSet(string connectionString, string commandText)
    {
      DataSet dataSet = new DataSet("DataDump");
      
      using (var connection = new SqlConnection(connectionString))
      {
        using (var command = connection.CreateCommand())
        {
          command.CommandType = CommandType.Text;
          command.CommandText = commandText;

          using (var adapter = new SqlDataAdapter(command))
          {
            try
            {
              adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                           
          }
        }
      }

      return dataSet;
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      var result = dlgLoad.ShowDialog();
      if (result != DialogResult.OK)
        return;

      txtFilePath.Text = dlgLoad.FileName;
      txtEditor.Text = File.ReadAllText(dlgLoad.FileName);
      this.Text = string.Format("{0} {1}", this.Text, txtFilePath.Text);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(txtFilePath.Text))
        dlgSave.FileName = txtFilePath.Text;

      var result = dlgSave.ShowDialog();
      if (result != DialogResult.OK)
        return;

      File.WriteAllText(dlgSave.FileName, txtEditor.Text);
    }


  }


}
