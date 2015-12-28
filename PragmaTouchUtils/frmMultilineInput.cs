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
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace PragmaTouchUtils
{
  public partial class frmMultilineInput : Form
  {
    public frmMultilineInput()
    {
      InitializeComponent();
    }
    
    public static bool ShowInput(string caption, ref string value)
    {
      return ShowInput(caption, false, ref value);
    }
    
    public static bool ShowInput(string caption, bool isReadOnly, ref string value)
    {
      string inValue = value;
      using ( frmMultilineInput frm = new frmMultilineInput() )
      {
        frm.Text = String.IsNullOrWhiteSpace(caption) ? "Entry" : caption;
        frm.memoEdit1.ReadOnly = isReadOnly;
        frm.btnOK.Visible = !isReadOnly;
        frm.memoEdit1.Text = value;

        if ( frm.ShowDialog() == DialogResult.OK )
        {
          value = frm.memoEdit1.Text;
          return true;
        }
        else
        {
          value = inValue;
          return false;
        }
      }
    }


  }


}
