using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using PragmaTouchUtils;
using Shorthand.Common;

namespace Shorthand.AdminPanel
{
    [Export(typeof(IPluginMarker))]
    public partial class frmAdminPanel : Form, IPlugin
    {
        private IPluginContext _context;
        private PerformanceCounter _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", "D10067185");
        private int i = 0;

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

        public Task InitializeAsync(IPluginContext context)
        {
            throw new NotImplementedException();
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
            txtUserName.Text = UserPrincipal.Current.SamAccountName;

            _cpuCounter.BeginInit();
            _cpuCounter.NextValue();
            _cpuCounter.EndInit();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            var credentialsEmpty = string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtUserName.Text);
            if (credentialsEmpty)
            {
                MessageBox.Show(this, "Enter current username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var userName = txtUserName.Text;
            var originalPassword = txtPassword.Text;

            var oldPassword = originalPassword;
            string newPassword = string.Empty;
            string passwordFormat = oldPassword + "_{0}";

            try
            {
                for (int i = 0; i < 8; i++)
                {
                    newPassword = string.Format(passwordFormat, i);
                    this.ChangeADPassword(userName, oldPassword, newPassword);
                    txtLog.Log(string.Format("appended _{0} to original password", i));
                    oldPassword = newPassword;

                    txtLog.Log("going to sleep for 2s");
                    Thread.Sleep(2000);
                    txtLog.Log("woke up");
                }
                this.ChangeADPassword(userName, newPassword, originalPassword);
                txtLog.AppendText("original password successfuly set\n");
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void ChangeADPassword(string userName, string oldPassword, string newPassword)
        {
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName))
                {
                    if (user != null)
                    {
                        user.ChangePassword(oldPassword, newPassword);
                        user.Save(context);
                    }
                }
            }
        }

        private async void btnCheckCPU_Click(object sender, EventArgs e)
        {

            do {
              await this.getCPUUsageAsync();
              Application.DoEvents();
              
            } while (chkRepeat.Checked);

        }


        private async Task getCPUUsageAsync()
        {
            Thread.Sleep(1000);
            //int satiCPU  = await Task<int>.FromResult( (int)_cpuCounter.NextValue() );

            var finalCpuCounter = await new TaskFactory().StartNew( () => {
                //PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                CounterSample cs1 = _cpuCounter.NextSample();
                Thread.Sleep(500);
                CounterSample cs2 = _cpuCounter.NextSample();
                return CounterSample.Calculate(cs1, cs2);
            });

            txtLog.Log($"sati : {finalCpuCounter}");
            pbCPU.Value = (int)finalCpuCounter;
            lblCPU.Text = finalCpuCounter.ToString();
            chart1.Series["Series1"].Points.AddXY(i++, finalCpuCounter);


        }





    }

}
