using PragmaTouchUtils;
using Shorthand.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Shorthand.AdminPanel
{
  [Export(typeof(IAsyncPlugin))]
  public partial class frmAdminPanel : Form, IAsyncPlugin
  {
    private IPluginContext _context;

    public frmAdminPanel()
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
        txtUserName.Text = UserPrincipal.Current.SamAccountName;
        return true;
      });
    }
    public async void OnSettingsChangedEventHandler(object sender, ConfigEventArgs e)
    {
      var shouldRefresh = e.ChangedOptions.Contains("AdminPanelOptions");
      if (!shouldRefresh)
        return;

      await this.InitializePlugin();
      await this.InitializeUI();
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

    private void btnCheckSolution_Click(object sender, EventArgs e)
    {
      var solutionBasePath = @"D:\Development\GitProjects\BilgiCampus\Bilgi.Sis.MobileWeb";
      var solutionFilePath = Path.Combine(solutionBasePath, "BilgiCampus.sln");

      var projects = new List<string>();
      Regex regex = new Regex("Project\\(.*\\) *= *\"(?<projectName>.*)\" *, *\"(?<projectFilePath>.*)\" *, *\"(?<solutionUID>.*)\""
                             , RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

      var inputText = File.ReadAllText(solutionFilePath);
      MatchCollection ms = regex.Matches(inputText);

      ms.OfType<Match>()
        .ToList()
        .ForEach(m =>
        {
          var p = m.Groups["projectFilePath"].Value;
          if (p != ".nuget")
          {
            var x = Path.Combine(solutionBasePath, p);
            CheckContentReferences(x);
          }
        }
        );
    }

    private void btnCheckFolder_Click(object sender, EventArgs e)
    {
      var projects = Directory.GetFiles(txtFolder.Text, "*.csproj", SearchOption.AllDirectories).ToList();
      projects.ForEach(p =>
      {
        if (p != ".nuget")
        {
          var x = Path.Combine(p);
          CheckContentReferences(x);
        }
      });

    }

    private void CheckContentReferences(string projectFilePath)
    {
      try
      {
        var projectBasePath = Path.GetDirectoryName(projectFilePath);
        var projectName = Path.GetFileName(projectFilePath);
        var xmlProject = XDocument.Load(projectFilePath);

        XNamespace ns = xmlProject.Root.GetDefaultNamespace();
        var result = xmlProject.Element(ns + "Project")
            .Elements(ns + "ItemGroup")
            .Elements(ns + "Content")
            .Select(x => $"{projectBasePath}\\{(string)x.Attribute("Include")}")
            .ToList();

        //var filesExist = result.TrueForAll(x => File.Exists(x));
        //var allUnique  = result.Distinct().Count() == result.Count();
        //var allUnique  = result.GroupBy(x => x).All(g => g.Count() == 1);

        // 1. duplications
        var duplicates = result.GroupBy(x => x)
                               .Where(g => g.Count() > 1)
                               .Select(y => y.Key)
                               .ToList();
        if (duplicates.Count() > 0)
        {
          txtLog.Log($"{projectName} Duplicates");
          duplicates.ForEach(x => txtLog.Log(x));
        }

        // 2. missing in file system
        var missing = result.Where(x => !File.Exists(x)).ToList();
        if (missing.Count() > 0)
        {
          txtLog.Log($"{projectName} Missing");
          missing.ForEach(x => txtLog.Log(x));
        }

      }
      catch (Exception)
      {
        throw;
      }
    }

    private void btnRunningProcess_Click(object sender, EventArgs e)
    {
      // var processes = Process.GetProcesses();
      var processes = Process.GetProcessesByName("IBU");
      var users = new List<string>();
      processes.ToList()
               .ForEach(p => users.Add($"{p.ProcessName} {GetProcessOwner(p.Id)}" ));

      txtLog.Lines = users.ToArray();
    }

    private string GetProcessOwner(int processId)
    {
      var query = "Select * From Win32_Process Where ProcessID = " + processId;
      using (var searcher = new ManagementObjectSearcher(query))
      {
        object[] args = { string.Empty, string.Empty };
        var owner = searcher.Get()
                            .OfType<ManagementObject>()
                            .Select( mo => {                                          
                                             mo.InvokeMethod("GetOwner", args);
                                             return $"{args[1]}\\{args[0]}" ?? "NO OWNER";
                                           })
                            .FirstOrDefault();

        return owner?.ToString();
      }
    }

    private void btnKill_Click(object sender, EventArgs e)
    {
      var processes = Process.GetProcessesByName("IBU");
      foreach (var p in processes)
      {
        txtLog.Log($"{p.ProcessName} bye!");
        p.Kill();        
      }
    }



  }

}
