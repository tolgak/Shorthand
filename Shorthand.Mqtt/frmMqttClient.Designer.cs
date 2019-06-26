namespace Shorthand.Mqtt
{
  partial class frmMqttClient
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMqttClient));
      this.txtLog = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.btnDisconnect = new System.Windows.Forms.Button();
      this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtHost = new System.Windows.Forms.TextBox();
      this.btnConnect = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.btnSubscribe = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.textBox5 = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnPublish = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtLog
      // 
      this.txtLog.BackColor = System.Drawing.Color.Black;
      this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtLog.Font = new System.Drawing.Font("Courier New", 11.25F);
      this.txtLog.ForeColor = System.Drawing.Color.Lime;
      this.txtLog.Location = new System.Drawing.Point(6, 215);
      this.txtLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtLog.Size = new System.Drawing.Size(939, 424);
      this.txtLog.TabIndex = 1;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.groupBox3);
      this.panel1.Controls.Add(this.groupBox2);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(6, 7);
      this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(939, 208);
      this.panel1.TabIndex = 0;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.btnDisconnect);
      this.groupBox3.Controls.Add(this.maskedTextBox1);
      this.groupBox3.Controls.Add(this.label3);
      this.groupBox3.Controls.Add(this.label1);
      this.groupBox3.Controls.Add(this.textBox1);
      this.groupBox3.Controls.Add(this.label2);
      this.groupBox3.Controls.Add(this.txtHost);
      this.groupBox3.Controls.Add(this.btnConnect);
      this.groupBox3.Location = new System.Drawing.Point(15, 13);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(413, 179);
      this.groupBox3.TabIndex = 0;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Connect";
      // 
      // btnDisconnect
      // 
      this.btnDisconnect.Location = new System.Drawing.Point(316, 131);
      this.btnDisconnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnDisconnect.Name = "btnDisconnect";
      this.btnDisconnect.Size = new System.Drawing.Size(87, 30);
      this.btnDisconnect.TabIndex = 27;
      this.btnDisconnect.Text = "Disconnect";
      this.btnDisconnect.UseVisualStyleBackColor = true;
      // 
      // maskedTextBox1
      // 
      this.maskedTextBox1.Location = new System.Drawing.Point(79, 97);
      this.maskedTextBox1.Name = "maskedTextBox1";
      this.maskedTextBox1.PasswordChar = '*';
      this.maskedTextBox1.Size = new System.Drawing.Size(194, 25);
      this.maskedTextBox1.TabIndex = 26;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 100);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(64, 17);
      this.label3.TabIndex = 25;
      this.label3.Text = "Password";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 67);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(67, 17);
      this.label1.TabIndex = 24;
      this.label1.Text = "Username";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(79, 60);
      this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(324, 25);
      this.textBox1.TabIndex = 23;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 30);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(67, 17);
      this.label2.TabIndex = 22;
      this.label2.Text = "Mqtt Host";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // txtHost
      // 
      this.txtHost.Location = new System.Drawing.Point(79, 27);
      this.txtHost.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.txtHost.Name = "txtHost";
      this.txtHost.Size = new System.Drawing.Size(324, 25);
      this.txtHost.TabIndex = 20;
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(316, 93);
      this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(87, 30);
      this.btnConnect.TabIndex = 21;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.btnSubscribe);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.textBox5);
      this.groupBox2.Location = new System.Drawing.Point(434, 129);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(485, 63);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Subscribe";
      // 
      // btnSubscribe
      // 
      this.btnSubscribe.Location = new System.Drawing.Point(382, 20);
      this.btnSubscribe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnSubscribe.Name = "btnSubscribe";
      this.btnSubscribe.Size = new System.Drawing.Size(87, 30);
      this.btnSubscribe.TabIndex = 29;
      this.btnSubscribe.Text = "Subscribe";
      this.btnSubscribe.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 27);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(39, 17);
      this.label7.TabIndex = 25;
      this.label7.Text = "Topic";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // textBox5
      // 
      this.textBox5.Location = new System.Drawing.Point(73, 24);
      this.textBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.textBox5.Name = "textBox5";
      this.textBox5.Size = new System.Drawing.Size(303, 25);
      this.textBox5.TabIndex = 24;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnPublish);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.textBox3);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.textBox2);
      this.groupBox1.Location = new System.Drawing.Point(434, 13);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(485, 110);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Publish";
      // 
      // btnPublish
      // 
      this.btnPublish.Location = new System.Drawing.Point(382, 59);
      this.btnPublish.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btnPublish.Name = "btnPublish";
      this.btnPublish.Size = new System.Drawing.Size(87, 30);
      this.btnPublish.TabIndex = 28;
      this.btnPublish.Text = "Publish";
      this.btnPublish.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 66);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(61, 17);
      this.label5.TabIndex = 27;
      this.label5.Text = "Message";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // textBox3
      // 
      this.textBox3.Location = new System.Drawing.Point(73, 60);
      this.textBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(226, 25);
      this.textBox3.TabIndex = 26;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 30);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(39, 17);
      this.label4.TabIndex = 25;
      this.label4.Text = "Topic";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(73, 27);
      this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(396, 25);
      this.textBox2.TabIndex = 24;
      // 
      // frmMqttClient
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(951, 646);
      this.Controls.Add(this.txtLog);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmMqttClient";
      this.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.Text = "Mqtt Client";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMqttClient_FormClosing);
      this.panel1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.MaskedTextBox maskedTextBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtHost;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnSubscribe;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox textBox5;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnPublish;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button btnDisconnect;
  }
}