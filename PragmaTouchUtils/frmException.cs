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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace PragmaTouchUtils
{
  public partial class frmException : Form
  {
    Exception exception = null;
    public frmException( )
    {
      InitializeComponent();

    }

    #region Static methods

    private static frmException CreateExceptionForm(Exception ex )
    {
      frmException frm = new frmException();
      frm.exception = ex;
      if (ex != null)
      {
        frm.txtDetailMsg.Text = "EXCEPTION TYPE:\r\n" + ex.GetType().ToString();
        frm.txtDetailMsg.Text += "\r\n";
        frm.txtDetailMsg.Text += "\r\nMESSAGE:\r\n " + ex.Message;
        frm.txtDetailMsg.Text += "\r\n";
        if (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
        {
          frm.txtDetailMsg.Text += "\r\nINNER EXCEPTION MESSAGE:\r\n" + ex.InnerException.Message;
          frm.txtDetailMsg.Text += "\r\n";
        }
        frm.txtDetailMsg.Text += "\r\nSTACK TRACE:\r\n " + ex.StackTrace;
        frm.txtDetailMsg.SelectionStart = 0;
        frm.txtDetailMsg.SelectionLength = 0;

				frm.txtShortMsg.Text = ex.Message;
      }
      else
      {
        frm.txtDetailMsg.Text = "Unknwon error.";
				frm.txtShortMsg.Text = frm.txtDetailMsg.Text;
      }

      return frm;
    }

    public static void ShowAppError( string summary, Exception ex )
    {
      ShowAppError(String.Empty, summary, ex); 
    }

    public static void ShowAppError(string info, string summary, Exception ex)
    {
      using ( frmException frm = CreateExceptionForm(ex) )
      {
        frm.txtDetailMsg.Text = "Error Summary: " + summary + "\r\n" + "\r\n" + frm.txtDetailMsg.Text;
        if ( !String.IsNullOrEmpty(summary) )
          frm.txtShortMsg.Text = summary;

        if ( !String.IsNullOrEmpty(info) )
          frm.label1.Text = info;

        System.Media.SystemSounds.Exclamation.Play();
        frm.ShowDialog();
      }
    }


    public static void ShowAppError(Exception ex)
    {
      using ( frmException frm = CreateExceptionForm(ex) )
      {
        System.Media.SystemSounds.Exclamation.Play();
        frm.ShowDialog();
      }
    }
    
    #endregion

    private void btnReport_Click( object sender, EventArgs e )
    { 
      string msgBody = "EXCEPTION TYPE:%20" + exception.GetType().ToString()
        + "%0d%0d"
        + "MESSAGE:%20" +  exception.Message
        + "%0d%0d"
        + "SOURCE:%20" +  exception.Source
        + "%0d%0d"
        + "TARGET:%0d" 
        + " - TARGETSITE NAME%20:%20" + exception.TargetSite.Name
        + "%0d"
        + " - ASSEMBLYNAME%20:%20" + exception.TargetSite.DeclaringType.Assembly.FullName
        + "%0d"
        + " - TYPE NAME%20:%20" + exception.TargetSite.DeclaringType.Name
        + "%0d"
        + " - NAMESPACE%20:%20" + exception.TargetSite.DeclaringType.Namespace
        + "%0d";
      
      string  cmd = "mailto:tolga.kurkcuoglu@gmail.com?subject=OMBLOffice%20Unhandled%20Error&body=" + msgBody;
      
      try
      {
        System.Diagnostics.Process.Start(cmd);
        Close();
      }
      catch(Exception ex)
      {
        MessageBoxHelper.ShowError("Can not generate error message!\nReason: " + ex.Message);
      }
    }

    private void btnContinue_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void btnClose_Click( object sender, EventArgs e )
    {
      Application.Exit();
    }


  }
}