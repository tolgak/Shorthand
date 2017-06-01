using Microsoft.Web.Administration;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Shorthand
{
  public partial class frmIISAdminHelper : Form
  {
    public frmIISAdminHelper()
    {
      InitializeComponent();

      //var identity = WindowsIdentity.GetCurrent();

      //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
      //ManagementObjectCollection collection = searcher.Get();
      //string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
      //txtUserName.Text = username;

      
    }








    private void btnDumpPoolProperties_Click(object sender, EventArgs e)
    {
      txtDetails.Clear();
      this.TraverseApplicationPools(x => this.DumpPoolProperties(x));
    }

    private void btnSetPoolPassword_Click(object sender, EventArgs e)
    {
      txtDetails.Clear();
      this.TraverseApplicationPools(x => this.SetPoolPassword(x));
    }

    private void TraverseApplicationPools(Action<Microsoft.Web.Administration.ApplicationPool> task)
    {
      using (var serverManager = new ServerManager())
      {
        var pools = serverManager.ApplicationPools;
        foreach (Microsoft.Web.Administration.ApplicationPool pool in pools)
        {
          task(pool);
        }

        serverManager.CommitChanges();
      }

    }

    private void DumpPoolProperties(ApplicationPool pool)
    {
      ConfigurationAttributeCollection attributes = pool.Attributes;
      foreach (ConfigurationAttribute attribute in attributes)
      {
        //put code here to work with each attribute
        txtDetails.Dump($"{attribute.Name} : {attribute.Value}");
      }
      txtDetails.Dump("");
    }

    private void SetPoolPassword(ApplicationPool pool)
    {
      pool.ProcessModel.UserName = txtUserName.Text;
      pool.ProcessModel.Password = txtPassword.Text;
    }




    private void btnDumpAppProperties_Click(object sender, EventArgs e)
    {
      txtDetails.Clear();
      this.TraverseApplications(app => this.DumpVirtualDirectoryAttributes(app));
    }

    private void btnSetApplicationPassword_Click(object sender, EventArgs e)
    {
      txtDetails.Clear();
      this.TraverseApplications(app => this.SetVirtualDirectoryPassword(app));
    }


    private void TraverseApplications(Action<Microsoft.Web.Administration.Application> task)
    {
      using (var serverManager = new ServerManager())
      {
        var mySite = serverManager.Sites.FirstOrDefault();
        if (mySite == null)
          return;

        var applications = mySite.Applications;
        foreach (Microsoft.Web.Administration.Application application in applications)
        {
          txtDetails.Dump($"AppPool : {application.ApplicationPoolName}");
          task(application);          
        }

        serverManager.CommitChanges();
      }
    }

    private void DumpVirtualDirectoryAttributes(Microsoft.Web.Administration.Application application)
    {
      var directories = application.VirtualDirectories;

      foreach (VirtualDirectory directory in directories)
      {
        string path = directory.Path;
        string physicalPath = directory.PhysicalPath;

        ConfigurationAttributeCollection attributes = directory.Attributes;
        foreach (ConfigurationAttribute attribute in attributes)
        {
          //put code here to work with each attribute
          txtDetails.Dump($"{attribute.Name} : {attribute.Value}");
        }

        //var childElements = directory.ChildElements;
        //foreach (ConfigurationElement element in childElements)
        //{
        //  //put code here to work with each ConfigurationElement
        //}
        txtDetails.Dump("");
      }
    }

    private void SetVirtualDirectoryPassword(Microsoft.Web.Administration.Application application)
    {
      var directories = application.VirtualDirectories;
      foreach (VirtualDirectory directory in directories)
      {        
        directory.UserName = txtUserName.Text;
        directory.Password = txtPassword.Text;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      using (X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
      {
        store.Open(OpenFlags.ReadOnly);
        
        var certificatesInPersonalStoreOnLocalMachine = store.Certificates.Find(X509FindType.FindByIssuerName, Environment.MachineName, true);
        if (certificatesInPersonalStoreOnLocalMachine != null && certificatesInPersonalStoreOnLocalMachine.Count > 0)
        {
          foreach (var item in certificatesInPersonalStoreOnLocalMachine)
          {
            txtDetails.Dump(item.Subject.ToString());
            txtDetails.Dump(item.FriendlyName);
            txtDetails.Dump(item.GetNameInfo(X509NameType.EmailName, true));
            txtDetails.Dump(item.Issuer);
            txtDetails.Dump(item.IssuerName.Name);
            txtDetails.Dump(item.ToString());

          }


        }
        

        
        
      }


    }
  }
}
