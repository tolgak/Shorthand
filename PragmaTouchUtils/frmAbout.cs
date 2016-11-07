// Author: Ali Özgür (www.pragmatouch.com) 
// Contact: aliozgur79@gmail.com
// 
// Copyright (c) 2010 Ali Özgür
// 
//  PragmaTouch Licence
//  Version 1.0, September 2007  
// 
//  This licence, PragmaTouch Licence, applies to all PragmaTouch products published for
//  purchase or demonstration purposes over any media.
// 
//  This computer program is protected by copyright law and international
//  treaties.Unauthorized reproduction or distribution of this program, or any portion
//  of it, may result in severe civil and criminal penalties, and will be prosecuted 
//  to the maximum extent possible under the law.  

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PragmaTouchUtils
{
  public partial class frmAbout : Form
  {
    public static void ShowAbout()
    {
      using (frmAbout frm = new frmAbout())
      {
        frm.PopulateSystemInfo();
        frm.ShowDialog();
      }
    }

    public frmAbout( )
    {
      InitializeComponent();
    }

    private void AddListItem(string name, string value, ListViewGroup ownerGroup = null)
    {
      var item = lv.Items.Add(name);
      item.SubItems.Add(value);

      if (ownerGroup != null)
        item.Group = ownerGroup;
    }

    public void PopulateSystemInfo( )
    {
      lv.Items.Clear();
      lv.Groups.Clear();
      lv.ShowGroups = true;

      #region OS Information

      var g1 = new ListViewGroup("grpSystem", "System");
      lv.Groups.Add(g1);
      this.AddListItem("Computer", SystemInformation.ComputerName, g1);
      this.AddListItem("Username", SystemInformation.UserName, g1);
      this.AddListItem("Domain", SystemInformation.UserDomainName, g1);
      this.AddListItem("Connected to network", SystemInformation.Network ? "Yes" : "No", g1);

      var g2 = new ListViewGroup("grpEnvironment", "Environment");
      lv.Groups.Add(g2);
      this.AddListItem("Current Directory", Environment.CurrentDirectory, g2);
      this.AddListItem("OS Name", Environment.OSVersion.VersionString, g2);
      this.AddListItem("OS Platform", Environment.OSVersion.Platform.ToString(), g2);
      this.AddListItem("OS Version", Environment.OSVersion.Version.ToString(), g2);
      //this.AddListItem("OS Service Pack", Environment.OSVersion.ServicePack, g2);
      this.AddListItem("OS 64 Bit", Environment.Is64BitOperatingSystem ? "Yes" : "No", g2);
      this.AddListItem("App 64 Bit", Environment.Is64BitProcess? "Yes" : "No", g2);

      #endregion

      #region Data Directories

      //item = lv.Items.Add("");
      //item = lv.Items.Add("Special Directories");
      //item.SubItems.Add("Directory Path");

      //item = lv.Items.Add("App Data");
      //item.SubItems.Add(SessionConsts.AppDataDir);

      //item = lv.Items.Add("Common App Data");
      //item.SubItems.Add(SessionConsts.CommonAppDataDir);

      //item = lv.Items.Add("Assembly Packages");
      //item.SubItems.Add(SessionConsts.AssemblyPackageDir);

      //item = lv.Items.Add("Dashboard Dir");
      //item.SubItems.Add(SessionConsts.DashboardDir);

      //item = lv.Items.Add("Widget Settings Dir");
      //item.SubItems.Add(SessionConsts.WidgetSettingsDir);

      //item = lv.Items.Add("Temp Dir");
      //item.SubItems.Add(SessionConsts.TempDir);

      //item = lv.Items.Add("SlideShowSettingsFile");
      //item.SubItems.Add(SessionConsts.SlideShowSettingsFile);

      #endregion

      #region Loaded Assembly Information

      var g4 = new ListViewGroup("grpAssemblies", "Loaded Assemblies");
      lv.Groups.Add(g4);

      var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName).ToArray();
      foreach (System.Reflection.Assembly ass in loadedAssemblies)
      {
        if (ass.IsDynamic || string.IsNullOrEmpty(ass.Location))        
          continue;

        this.AddListItem($"{ass.GetName().Name} ({ass.GetName().Version.ToString() })", ass.Location, g4);
      }

      #endregion

      lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

      var app = Assembly.GetCallingAssembly();
      var v = app.GetName().Version;
      lblVersion.Text = string.Format("Version: {0}.{1}.{2} r{3}", v.Major, v.Minor, v.Build, v.Revision);
    }

    private void lblThrowException_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
    {
      throw new Exception("This error is intentionally thrown for testing purpose.");
    }

    private void frmAbout_Load(object sender, EventArgs e)
    {
      lblCopyright.Text = String.Format("All rights reserved. ©2011-{0}", DateTime.Now.Year);
    }



  }
}