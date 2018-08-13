using System.Windows.Forms;
using System.ComponentModel.Composition;

using Shorthand.Common;
using System.Threading.Tasks;
using System;
using PragmaTouchUtils;

namespace Shorthand.TCPServer
{
    [Export(typeof(IPluginMarker))]
    public partial class frmListener : Form, IAsyncPlugin
    {
        private IPluginContext _context;

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
//            _deployOptions = ConfigContent.Current.GetConfigContentItem("DeploymentOptions") as DeploymentOptions;
        }






        public void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
        {
            var shouldRefresh = e.ChangedOptions.Contains("SocketListenerOptions");
            if (!shouldRefresh)
                return;

            this.InitializePlugin();
            //this.InitializeUIAsync().ConfigureAwait(false);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            AsyncEchoServer async = new AsyncEchoServer(51510, x => this.Dump(x));
            async.Start();
        }







        private void Dump(string line)
        {
            txtDump.Log(line);
        }

    }
}
