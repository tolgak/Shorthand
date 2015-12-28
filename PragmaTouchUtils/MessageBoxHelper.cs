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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PragmaTouchUtils
{
  public static class MessageBoxHelper
  {

    public static void ShowOptimisticConcurrencyExceptionForDelete()
    {
      ShowError("Proje başka bir kullanıcı tarafından silinmiş.");
    }

    public static DialogResult ShowYesNoCancel(string msg)
    {
      return MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
    }

    public static DialogResult ShowYesNo(string msg)
    {
      return MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
    }

    public static DialogResult ShowYesNoCancelError(string msg)
    {
      return MessageBox.Show(msg, "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
    }

    public static DialogResult ShowYesNoError(string msg)
    {
      return MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
    }

    public static void ShowError(string msg)
    {
      MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

   

    public static void ShowError(Exception ex)
    {
      ShowError(ex.Message);
    }

    public static void ShowWarning(string msg)
    {
      MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static DialogResult ShowWarningYesNo(string msg)
    {
      return MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
    }

    public static void ShowWarning(Exception ex)
    {
      ShowWarning(ex.Message);
    }

    public static void ShowInfo(string msg)
    {
      MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}
