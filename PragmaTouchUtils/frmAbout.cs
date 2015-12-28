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


namespace PragmaTouchUtils
{
  public partial class frmAbout : Form
  {
    public frmAbout( )
    {
      InitializeComponent();
    }

    public void PopulateSystemInfo( )
    {
      lv.Items.Clear();

      #region OS Information

      ListViewItem item = lv.Items.Add("Computer Name");
      item.SubItems.Add(SystemInformation.ComputerName);

      item = lv.Items.Add("Username");
      item.SubItems.Add(SystemInformation.UserName);

      item = lv.Items.Add("Domain");
      item.SubItems.Add(SystemInformation.UserDomainName);

      item = lv.Items.Add("Connected to network");
      item.SubItems.Add(SystemInformation.Network ? "Yes" : "No");

      item = lv.Items.Add("Current Directory");
      item.SubItems.Add(System.Environment.CurrentDirectory);

      item = lv.Items.Add("OS Name");
      item.SubItems.Add(System.Environment.OSVersion.VersionString);

      item = lv.Items.Add("OS Platform");
      item.SubItems.Add(System.Environment.OSVersion.Platform.ToString());

      item = lv.Items.Add("OS Version");
      item.SubItems.Add(System.Environment.OSVersion.Version.ToString());

      item = lv.Items.Add("OS Service Pack");
      item.SubItems.Add(System.Environment.OSVersion.ServicePack);

      #endregion

      #region Data Directories

      item = lv.Items.Add("");
      item = lv.Items.Add("Special Directories");
      item.SubItems.Add("Directory Path");

      item = lv.Items.Add("App Data");
      //item.SubItems.Add(SessionConsts.AppDataDir);

      item = lv.Items.Add("Common App Data");
      //item.SubItems.Add(SessionConsts.CommonAppDataDir);

      item = lv.Items.Add("Assembly Packages");
      //item.SubItems.Add(SessionConsts.AssemblyPackageDir);

      item = lv.Items.Add("Dashboard Dir");
      //item.SubItems.Add(SessionConsts.DashboardDir);

      item = lv.Items.Add("Widget Settings Dir");
      //item.SubItems.Add(SessionConsts.WidgetSettingsDir);

      item = lv.Items.Add("Temp Dir");
      //item.SubItems.Add(SessionConsts.TempDir);

      item = lv.Items.Add("SlideShowSettingsFile");
      //item.SubItems.Add(SessionConsts.SlideShowSettingsFile);

      #endregion

      #region Loaded Assembly Information


      item = lv.Items.Add("");
      item = lv.Items.Add("Loaded Assemblies");
      item.SubItems.Add("Assembly Path");

      System.Reflection.Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName).ToArray();

      foreach (System.Reflection.Assembly ass in loadedAssemblies)
      {
        if (ass.IsDynamic)
          continue;
        if (String.IsNullOrEmpty(ass.Location))
        {
          continue;
        }

        item = lv.Items.Add(ass.GetName().Name + " ( " + ass.GetName().Version.ToString() + " )");
        item.SubItems.Add(ass.Location);
      }

      #endregion

      lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

      System.Reflection.Assembly app = System.Reflection.Assembly.GetExecutingAssembly();
      Version v = app.GetName().Version;
      lblVersion.Text = String.Format("Version: {0}.{1}.{2} r{3}", v.Major, v.Minor,v.Build,v.Revision);

    }
    
   
    
    public static void ShowAbout( )
    {
      using ( frmAbout frm = new frmAbout() )
      {
        frm.PopulateSystemInfo();
        frm.ShowDialog();
      }
    }

    //private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
    //{
    //  System.Diagnostics.Process.Start("mailto:tolga.kurkcuoglu@gmail.com?Subject=OMBLOffice");
    //}



    private void lblThrowException_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
    {
      throw new Exception("This error is intentionally thrown for testing purpose.");
    }


    private void tabPage2_Click(object sender, EventArgs e)
    {

    }

    private void frmAbout_Load(object sender, EventArgs e)
    {
      lblCopyright.Text = String.Format("All rights reserved. ©2011-{0}", DateTime.Now.Year);
    }

    private void lblCopyright_DoubleClick(object sender, EventArgs e)
    {
      //MessageBoxHelper.ShowInfo(Petrotek.eXpressOto.WinForms.Properties.Resources.DeveloperInfo);
    }

  }
}