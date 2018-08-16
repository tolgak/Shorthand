namespace Shorthand.TCPServer
{
    partial class frmListener
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
            this.btnStartListener = new System.Windows.Forms.Button();
            this.txtDump = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbPortNames = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnStartListener
            // 
            this.btnStartListener.Location = new System.Drawing.Point(375, 28);
            this.btnStartListener.Name = "btnStartListener";
            this.btnStartListener.Size = new System.Drawing.Size(75, 23);
            this.btnStartListener.TabIndex = 0;
            this.btnStartListener.Text = "button1";
            this.btnStartListener.UseVisualStyleBackColor = true;
            this.btnStartListener.Click += new System.EventHandler(this.btnStartListener_Click);
            // 
            // txtDump
            // 
            this.txtDump.Location = new System.Drawing.Point(16, 253);
            this.txtDump.Multiline = true;
            this.txtDump.Name = "txtDump";
            this.txtDump.Size = new System.Drawing.Size(780, 311);
            this.txtDump.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(375, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbPortNames
            // 
            this.cmbPortNames.FormattingEnabled = true;
            this.cmbPortNames.Location = new System.Drawing.Point(225, 86);
            this.cmbPortNames.Name = "cmbPortNames";
            this.cmbPortNames.Size = new System.Drawing.Size(121, 21);
            this.cmbPortNames.TabIndex = 3;
            // 
            // frmListener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 611);
            this.Controls.Add(this.cmbPortNames);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtDump);
            this.Controls.Add(this.btnStartListener);
            this.Name = "frmListener";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmListener";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartListener;
        private System.Windows.Forms.TextBox txtDump;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmbPortNames;
    }
}