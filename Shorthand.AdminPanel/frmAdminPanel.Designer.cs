namespace Shorthand.AdminPanel
{
  partial class frmAdminPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminPanel));
      this.grpDatabase = new System.Windows.Forms.GroupBox();
      this.btnConnect = new System.Windows.Forms.Button();
      this.lblPassword = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.lblUserName = new System.Windows.Forms.Label();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.lblDatabaseName = new System.Windows.Forms.Label();
      this.txtDatabaseName = new System.Windows.Forms.TextBox();
      this.lblServer = new System.Windows.Forms.Label();
      this.txtServerName = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnSubscribe = new System.Windows.Forms.Button();
      this.txtPingReply = new System.Windows.Forms.TextBox();
      this.lblMac = new System.Windows.Forms.Label();
      this.grpDatabase.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpDatabase
      // 
      this.grpDatabase.Controls.Add(this.btnConnect);
      this.grpDatabase.Controls.Add(this.lblPassword);
      this.grpDatabase.Controls.Add(this.txtPassword);
      this.grpDatabase.Controls.Add(this.lblUserName);
      this.grpDatabase.Controls.Add(this.txtUserName);
      this.grpDatabase.Controls.Add(this.lblDatabaseName);
      this.grpDatabase.Controls.Add(this.txtDatabaseName);
      this.grpDatabase.Controls.Add(this.lblServer);
      this.grpDatabase.Controls.Add(this.txtServerName);
      this.grpDatabase.Location = new System.Drawing.Point(12, 12);
      this.grpDatabase.Name = "grpDatabase";
      this.grpDatabase.Size = new System.Drawing.Size(345, 198);
      this.grpDatabase.TabIndex = 12;
      this.grpDatabase.TabStop = false;
      this.grpDatabase.Text = "Configuration Database";
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(245, 160);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(75, 26);
      this.btnConnect.TabIndex = 20;
      this.btnConnect.Text = "Ping";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(20, 132);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(64, 17);
      this.lblPassword.TabIndex = 19;
      this.lblPassword.Text = "Password";
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(128, 129);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(192, 25);
      this.txtPassword.TabIndex = 18;
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point(20, 101);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(74, 17);
      this.lblUserName.TabIndex = 17;
      this.lblUserName.Text = "User Name";
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point(128, 98);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(192, 25);
      this.txtUserName.TabIndex = 16;
      // 
      // lblDatabaseName
      // 
      this.lblDatabaseName.AutoSize = true;
      this.lblDatabaseName.Location = new System.Drawing.Point(20, 70);
      this.lblDatabaseName.Name = "lblDatabaseName";
      this.lblDatabaseName.Size = new System.Drawing.Size(102, 17);
      this.lblDatabaseName.TabIndex = 15;
      this.lblDatabaseName.Text = "Database Name";
      // 
      // txtDatabaseName
      // 
      this.txtDatabaseName.Location = new System.Drawing.Point(128, 67);
      this.txtDatabaseName.Name = "txtDatabaseName";
      this.txtDatabaseName.Size = new System.Drawing.Size(192, 25);
      this.txtDatabaseName.TabIndex = 14;
      // 
      // lblServer
      // 
      this.lblServer.AutoSize = true;
      this.lblServer.Location = new System.Drawing.Point(20, 39);
      this.lblServer.Name = "lblServer";
      this.lblServer.Size = new System.Drawing.Size(84, 17);
      this.lblServer.TabIndex = 13;
      this.lblServer.Text = "Server Name";
      // 
      // txtServerName
      // 
      this.txtServerName.Location = new System.Drawing.Point(128, 36);
      this.txtServerName.Name = "txtServerName";
      this.txtServerName.Size = new System.Drawing.Size(192, 25);
      this.txtServerName.TabIndex = 12;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lblMac);
      this.groupBox1.Controls.Add(this.btnSubscribe);
      this.groupBox1.Location = new System.Drawing.Point(363, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(388, 198);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      // 
      // btnSubscribe
      // 
      this.btnSubscribe.Location = new System.Drawing.Point(264, 160);
      this.btnSubscribe.Name = "btnSubscribe";
      this.btnSubscribe.Size = new System.Drawing.Size(105, 26);
      this.btnSubscribe.TabIndex = 21;
      this.btnSubscribe.Text = "Subscribe";
      this.btnSubscribe.UseVisualStyleBackColor = true;
      this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
      // 
      // txtPingReply
      // 
      this.txtPingReply.BackColor = System.Drawing.Color.Black;
      this.txtPingReply.ForeColor = System.Drawing.Color.Lime;
      this.txtPingReply.Location = new System.Drawing.Point(12, 216);
      this.txtPingReply.Multiline = true;
      this.txtPingReply.Name = "txtPingReply";
      this.txtPingReply.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtPingReply.Size = new System.Drawing.Size(739, 256);
      this.txtPingReply.TabIndex = 23;
      this.txtPingReply.WordWrap = false;
      // 
      // lblMac
      // 
      this.lblMac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblMac.BackColor = System.Drawing.Color.Maroon;
      this.lblMac.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblMac.ForeColor = System.Drawing.Color.White;
      this.lblMac.Location = new System.Drawing.Point(15, 21);
      this.lblMac.Name = "lblMac";
      this.lblMac.Size = new System.Drawing.Size(354, 40);
      this.lblMac.TabIndex = 22;
      this.lblMac.Text = "MAC address";
      this.lblMac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // frmAdminPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(763, 510);
      this.Controls.Add(this.txtPingReply);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.grpDatabase);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmAdminPanel";
      this.Text = "Admin Panel";
      this.grpDatabase.ResumeLayout(false);
      this.grpDatabase.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox grpDatabase;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label lblDatabaseName;
    private System.Windows.Forms.TextBox txtDatabaseName;
    private System.Windows.Forms.Label lblServer;
    private System.Windows.Forms.TextBox txtServerName;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnSubscribe;
    private System.Windows.Forms.TextBox txtPingReply;
    private System.Windows.Forms.Label lblMac;
  }
}