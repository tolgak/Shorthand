using System;
using System.Net;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

using System.ComponentModel.Composition;
using PragmaTouchUtils;
using Shorthand.Common;
using System.Threading.Tasks;

namespace Shorthand.Mqtt
{
  [Export(typeof(IPluginMarker))]
  public partial class frmMqttClient : Form, IAsyncPlugin
  {
    private MqttClient  _client;
    private IPluginContext _context;
    private Action<string> _logger;
    private string _userId;
    private string _password;
    private string _rootTopic;
    private string _hostname;

    public frmMqttClient()
    {
      InitializeComponent();

      _logger = (x) => txtLog.Log(x);
    }

    //public Form Initialize(IPluginContext context)
    //{
    //  _context = context;      
    //  _context.Configuration.LoadConfiguration();

    //  //this.MdiParent = _context.Host;
    //  //var strips = _context.Host.MainMenuStrip.Items.Find("mnuTools", true);
    //  //if (strips.Length == 0)
    //  //  return;

    //  //var subItem = new ToolStripMenuItem(this.Text);
    //  //if (this.Icon != null)
    //  //  subItem.Image = this.Icon.ToBitmap();
    //  //(strips[0] as ToolStripMenuItem).DropDownItems.Add(subItem);
    //  //subItem.Click += (object sender, EventArgs e) => { this.Show(); };

    //  //if (_context.Host is IPluginHost host)
    //  //  host.onSettingsChanged += this.OnSettingsChangedEventHandler;

    //  this.FormClosing += (object sender, FormClosingEventArgs e) =>
    //  {
    //    e.Cancel = true;
    //    this.Hide();
    //  };

    //  this.InitializePlugin();
    //  this.InitializeUI();

    //  return this;
    //}
    //private void InitializePlugin()
    //{
    //  //_mqttOptions = ConfigContent.Current.GetConfigContentItem("MqttOptions") as MqttOptions;

    //  _hostname = "mqtt.dioty.co";
    //  _userId = "tolga.kurkcuoglu@gmail.com";
    //  _password = "65bac065";
    //  _rootTopic = "/tolga.kurkcuoglu@gmail.com/#";

    //  //_hostname = "verbum.westeurope.cloudapp.azure.com";  //"52.178.115.89";
    //  //_userId = "tolgak";
    //  //_password = "Sakura3x_azure";
    //  //_rootTopic = "/tolgak/#";
    //}
    //private void InitializeUI()
    //{

    //}
    //public void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    //{
    //  var shouldRefresh = e.ChangedOptions.Contains("MqttOptions");
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
      return await Task.Run(() => {
        _hostname = "mqtt.dioty.co";
        _userId = "tolga.kurkcuoglu@gmail.com";
        _password = "65bac065";
        _rootTopic = "/tolga.kurkcuoglu@gmail.com/#";

        return true;
      });
    }
    private async Task<bool> InitializeUI()
    {
      return await Task.Run(() =>
      {
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






    private void _client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {      
      var message = System.Text.Encoding.Default.GetString(e.Message);
      _logger?.Invoke($"[{e.Topic}] {message}");
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      this.MqttClientDisengage();

      txtLog.Clear();
      _client = new MqttClient(_hostname);
      _client.MqttMsgPublishReceived += _client_MqttMsgPublishReceived;

      this.mqttConnect("Shorthand Mqtt Client", _userId, _password, _rootTopic);
    }

    private void mqttConnect(string clientId, string userName, string password, string rootTopic)
    {
      _client.Connect(clientId, userName, password, true, 120);
      _client.Subscribe(new string[] { rootTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
      _logger?.Invoke($"client protocol {_client.ProtocolVersion}");
    }

    private void frmMqttClient_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.MqttClientDisengage();
    }

    private void MqttClientDisengage()
    {
      if (_client == null)
        return;

      if (_client != null && _client.IsConnected)
        _client.Disconnect();

      _client.MqttMsgPublishReceived -= _client_MqttMsgPublishReceived;
      _client = null;
    }


  }
}
