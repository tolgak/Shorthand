using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace PragmaTouchUtils
{
	public partial class ChangePwdDlg : Form
	{
		private int _userId = 0;
		private string _dbConnectionString = String.Empty;
		private bool _removeForceChangePwd = false;

		public ChangePwdDlg()
		{
			InitializeComponent();
		}

		public static bool ShowDialog(string dbConnectionString, int userId)
		{
			return ShowDialog(dbConnectionString, userId, false);
		}

		public static bool ShowDialog(string dbConnectionString, int userId, bool removeForceChangePwd)
		{
      using ( ChangePwdDlg frm = new ChangePwdDlg() )
      {
        frm._userId = userId;
        frm._dbConnectionString = dbConnectionString;
        frm._removeForceChangePwd = removeForceChangePwd;

        if ( frm.ShowDialog() != DialogResult.OK )
          return false;
        else
          return true;
      }
		}

		private bool ValidateInput()
		{
			ep.Clear();
			bool result = true;
			if (txtNew.Text.Trim() != txtReNew.Text.Trim())
			{
				ep.SetError(txtReNew, "Please retype the new password correctly.");
				result = false;
			}
			return result;

		}
		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (!ValidateInput())
					return;

        //UserFacade.ChangePwd(_dbConnectionString, _userId, txtCurrent.Text, txtNew.Text, _removeForceChangePwd);
				DialogResult = DialogResult.OK;
			}
			catch(Exception ex)
			{
				MessageBoxHelper.ShowError("Can not change password.\r\nError:" + ex.Message);				
			}
		}
	}
}