using System.Windows.Forms;
using System.ComponentModel.Composition;

using Shorthand.Common;
using System.Threading.Tasks;
using System;
using PragmaTouchUtils;
using DevLib.IO.Ports;
using System.Text;
using System.Threading;

namespace Shorthand.TCPServer
{
    [Export(typeof(IPluginMarker))]
    public partial class frmListener : Form, IAsyncPlugin
    {
        private IPluginContext _context;
        private AsyncListener _async;
        private SyncSerialPort _serial;
        private CancellationToken _cancellationToken;

        public frmListener()
        {
            InitializeComponent();
        }


        public async Task InitializeAsync(IPluginContext context)
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
            //await this.InitializeUIAsync();
        }

        private void InitializePlugin()
        {
            // _deployOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;

            cmbPortNames.Items.Clear();
            cmbPortNames.Items.AddRange(SyncSerialPort.PortNames);            
        }






        public void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
        {
            var shouldRefresh = e.ChangedOptions.Contains("SocketListenerOptions");
            if (!shouldRefresh)
                return;

            this.InitializePlugin();
            //this.InitializeUIAsync().ConfigureAwait(false);
        }


        private void Dump(string line)
        {
            txtDump.Log(line);
        }




        private void button2_Click(object sender, EventArgs e)
        {
            var selectedPort = cmbPortNames.SelectedItem.ToString();
            _serial = new SyncSerialPort(x => this.Dump(x), selectedPort);

            _serial.DataReceived += (s, arg) => { var result = _serial.ReadSync(); this.Dump(Encoding.UTF8.GetString(result)); };
            _serial.Open();
            _serial.Send(Encoding.UTF8.GetBytes("Hello"));
        }

        private void btnStartListener_Click(object sender, EventArgs e)
        {
            if (_async == null )
              _async = new AsyncListener(5151, x => this.Dump(x));

            _cancellationToken = new CancellationTokenSource().Token;
            _async.Start(_cancellationToken);
        }
    }
}
