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

using iText;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text;

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






    private void btnRunningProcess_Click(object sender, EventArgs e)
    {
      // var processes = Process.GetProcesses();
      var processes = Process.GetProcessesByName("IBU");
      var users = new List<string>();
      processes.ToList()
               .ForEach(p => users.Add($"{p.ProcessName} {GetProcessOwner(p.Id)}"));

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
                            .Select(mo => {
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

    private void btnReadPdf_Click(object sender, EventArgs e)
    {
      var src = @"C:\tmp\sandbox\object.pdf";
      var dest = @"C:\tmp\sandbox\split";

      PdfDocument sourcePdfDoc = new PdfDocument(new PdfReader(src));
      var cntPage = sourcePdfDoc.GetNumberOfPages();

      for (int i = 1; i <= cntPage; i++)
      {
        var page = sourcePdfDoc.GetPage(i);
        string textFromPage = PdfTextExtractor.GetTextFromPage(page, new LocationTextExtractionStrategy());

        var id = this.ExtractId(textFromPage);
        var fileName = this.Validate(id) ? id : $"Not Valid - {id}";

        using (var writer = new PdfWriter($"{dest}\\{fileName}.pdf"))
        using (var destinationPdfDoc = new PdfDocument(writer))
        {
          new PdfMerger(destinationPdfDoc).Merge(sourcePdfDoc, i, i);          
        }

      }
    }

    private string ExtractId(string input)
    {
      var regex = new Regex("\\(T.C.KİMLİK NUMARASI\\)\\n(?<TCKNo>[\\d ]{21,22})", RegexOptions.IgnoreCase| RegexOptions.Singleline| RegexOptions.Compiled);
      var match = regex.Match(input);

      var value = match.Groups["TCKNo"].Value.Replace(" ", "");
      return value;
    }

    private bool Validate(string id)
    {
      if (id.Length != 11)
        return false;

      var digits = id.Select(x => Convert.ToInt32(x.ToString())).ToArray();
      if (digits[10] % 2 != 0)
        return false;
      
      return digits[10] == digits.Take(10).Sum() % 10;
    }



  }


}
