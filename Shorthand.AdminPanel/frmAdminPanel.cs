using PragmaTouchUtils;
using Shorthand.Common;
using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Shorthand.AdminPanel
{
  [Export(typeof(IPlugin))]
  public partial class frmAdminPanel : Form, IPlugin
  {
    private string _liveIp;
    private string _liveMac;

    private IPluginContext _context;
    private const string EmptyMAC = "000000000000";

    public frmAdminPanel()
    {
      InitializeComponent();
    }

    public void Initialize(IPluginContext context)
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

      var host = _context.Host as IPluginHost;
      if (host != null)
        host.onSettingsChanged += this.OnSettingsChangedEventHandler;

      this.FormClosing += (object sender, FormClosingEventArgs e) =>
      {
        e.Cancel = true;
        this.Hide();
      };

      this.InitializePlugin();
      this.InitializeUI();
    }

    public void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    {
      var shouldRefresh = e.ChangedOptions.Contains("AdminPanelOptions");
      if (!shouldRefresh)
        return;

      this.InitializePlugin();
      this.InitializeUI();
    }

    private void InitializePlugin()
    {
    }

    private void InitializeUI()
    {
      _liveMac = frmAdminPanel.EmptyMAC;
      lblMac.Text = _liveMac;
      lblMac.BackColor = Color.Maroon;
      btnSubscribe.Enabled = false;

    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      var missing = string.IsNullOrEmpty(txtServerName.Text)
                 || string.IsNullOrEmpty(txtDatabaseName.Text)
                 || string.IsNullOrEmpty(txtUserName.Text)
                 || string.IsNullOrEmpty(txtPassword.Text);

      if (missing)
      {
        MessageBox.Show(this, "Missing connection info", "Can not connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        try
        {
          var connectionString = $"Server={txtServerName.Text}; Database={txtDatabaseName.Text}; Uid={txtUserName.Text}; Pwd = {txtPassword.Text}";
          var commandText = "exec dbo.spAdminPanel_Ping";
          using (DataSet dataSet = this.BuildDataSet(connectionString, commandText))
          {
            var tblPingReply = dataSet.Tables[0];
            txtPingReply.Clear();
            txtPingReply.AppendText("Ping reply\r\n");
            txtPingReply.AppendText("-------------\r\n");

            _liveIp = tblPingReply.Rows[0].Field<string>("client_net_address");
            _liveMac = this.IpToMac(_liveIp);

            txtPingReply.AppendText($"MAC address for {_liveIp} is {_liveMac}\r\n");
            foreach (DataColumn column in tblPingReply.Columns)
              txtPingReply.AppendText($"{column.ColumnName} : {tblPingReply.Rows[0].Field<string>(column.Ordinal)}\r\n");
          }
        }
        catch (Exception exception)
        {

          MessageBox.Show(this, exception.Message, "Can not ping", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      finally
      {
        lblMac.Text = _liveMac;
        lblMac.BackColor = _liveMac == frmAdminPanel.EmptyMAC ? Color.Maroon : Color.DarkGreen;
        btnSubscribe.Enabled = _liveMac != frmAdminPanel.EmptyMAC;
      }

    }

    private DataSet BuildDataSet(string connectionString, string commandText)
    {
      DataSet dataSet = new DataSet("AdminTool_PingReply");
      using (var connection = new SqlConnection())
      {
        connection.ConnectionString = connectionString;
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

    public string IpToMac(string ipAddress)
    {
      NetworkInterface[] adapters = (from nic in NetworkInterface.GetAllNetworkInterfaces()
                                     where nic.OperationalStatus == OperationalStatus.Up
                                        && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback
                                        && nic.NetworkInterfaceType != NetworkInterfaceType.Unknown
                                     select nic).ToArray();

      foreach (var adapter in adapters)
      {
        var addresses = adapter.GetIPProperties().UnicastAddresses;
        if (addresses.Count > 0)
          foreach (UnicastIPAddressInformation address in addresses)
            if (address.Address.ToString() == ipAddress)
              return adapter.GetPhysicalAddress().ToString();
      }

      return string.Empty;
    }

    private void btnSubscribe_Click(object sender, EventArgs e)
    {

    }

 





  }

}
