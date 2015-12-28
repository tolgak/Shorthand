namespace PragmaTouchUtils
{
	partial class ChangePwdDlg
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePwdDlg));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.ep = new System.Windows.Forms.ErrorProvider(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.txtCurrent = new System.Windows.Forms.TextBox();
			this.txtNew = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtReNew = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(128, 148);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 26);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(209, 148);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 26);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// ep
			// 
			this.ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep.ContainerControl = this;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(33, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Current Password";
			// 
			// txtCurrent
			// 
			this.txtCurrent.Location = new System.Drawing.Point(36, 31);
			this.txtCurrent.Name = "txtCurrent";
			this.txtCurrent.PasswordChar = '*';
			this.txtCurrent.Size = new System.Drawing.Size(242, 20);
			this.txtCurrent.TabIndex = 0;
			// 
			// txtNew
			// 
			this.txtNew.Location = new System.Drawing.Point(36, 69);
			this.txtNew.Name = "txtNew";
			this.txtNew.PasswordChar = '*';
			this.txtNew.Size = new System.Drawing.Size(242, 20);
			this.txtNew.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(33, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "New Password";
			// 
			// txtReNew
			// 
			this.txtReNew.Location = new System.Drawing.Point(36, 110);
			this.txtReNew.Name = "txtReNew";
			this.txtReNew.PasswordChar = '*';
			this.txtReNew.Size = new System.Drawing.Size(242, 20);
			this.txtReNew.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(33, 94);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Retype New Password ";
			// 
			// ChangePwdDlg
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(309, 183);
			this.Controls.Add(this.txtReNew);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtNew);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtCurrent);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChangePwdDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Change Password";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

    private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ErrorProvider ep;
		private System.Windows.Forms.TextBox txtReNew;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtNew;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCurrent;
		private System.Windows.Forms.Label label1;
	}
}