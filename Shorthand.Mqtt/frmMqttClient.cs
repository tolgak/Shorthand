﻿using System;
using System.Net;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

using System.ComponentModel.Composition;
using PragmaTouchUtils;
using Shorthand.Common;

namespace Shorthand.Mqtt
{
  [Export(typeof(IPlugin))]
  public partial class frmMqttClient : Form, IPlugin
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

    private void _client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {      
      var message = System.Text.Encoding.Default.GetString(e.Message);
      _logger?.Invoke($"[{e.Topic}] {message}");
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

      if (_context.Host is IPluginHost host)
        host.onSettingsChanged += this.OnSettingsChangedEventHandler;

      this.FormClosing += (object sender, FormClosingEventArgs e) =>
      {
        e.Cancel = true;
        this.Hide();
      };

      this.InitializePlugin();
      this.InitializeUI();
    }

    private void InitializeUI()
    {
      //throw new NotImplementedException();
    }

    private void InitializePlugin()
    {
      //_mqttOptions = ConfigContent.Current.GetConfigContentItem("MqttOptions") as MqttOptions;

      _hostname = "mqtt.dioty.co";
      _userId = "tolga.kurkcuoglu@gmail.com";
      _password = "65bac065";
      _rootTopic = "/tolga.kurkcuoglu@gmail.com/#";

      //_hostname = "verbum.westeurope.cloudapp.azure.com";  //"52.178.115.89";
      //_userId = "tolgak";
      //_password = "Sakura3x_azure";
      //_rootTopic = "/tolgak/#";

    }

    private void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    {
      var shouldRefresh = e.ChangedOptions.Contains("MqttOptions");
      if (!shouldRefresh)
        return;

      this.InitializePlugin();
      this.InitializeUI();
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
