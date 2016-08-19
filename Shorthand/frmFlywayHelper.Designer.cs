namespace Shorthand
{
  partial class frmFlywayHelper
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnSearch = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.cmbProjects = new System.Windows.Forms.ComboBox();
      this.grdIssue = new System.Windows.Forms.DataGridView();
      this.colIssueKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.coldueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colAssignee = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colReporter = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdIssue)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnSearch);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.cmbProjects);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(5, 5);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(774, 67);
      this.panel1.TabIndex = 2;
      // 
      // btnSearch
      // 
      this.btnSearch.Location = new System.Drawing.Point(324, 14);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new System.Drawing.Size(75, 25);
      this.btnSearch.TabIndex = 4;
      this.btnSearch.Text = "Search";
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 17);
      this.label1.TabIndex = 3;
      this.label1.Text = "Jira Projects";
      // 
      // cmbProjects
      // 
      this.cmbProjects.FormattingEnabled = true;
      this.cmbProjects.Location = new System.Drawing.Point(96, 14);
      this.cmbProjects.Name = "cmbProjects";
      this.cmbProjects.Size = new System.Drawing.Size(222, 25);
      this.cmbProjects.TabIndex = 2;
      // 
      // grdIssue
      // 
      this.grdIssue.AllowUserToAddRows = false;
      this.grdIssue.AllowUserToDeleteRows = false;
      this.grdIssue.AllowUserToResizeRows = false;
      this.grdIssue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdIssue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIssueKey,
            this.coldueDate,
            this.colAssignee,
            this.colReporter});
      this.grdIssue.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdIssue.EnableHeadersVisualStyles = false;
      this.grdIssue.Location = new System.Drawing.Point(5, 72);
      this.grdIssue.Name = "grdIssue";
      this.grdIssue.ReadOnly = true;
      this.grdIssue.RowHeadersVisible = false;
      this.grdIssue.Size = new System.Drawing.Size(774, 484);
      this.grdIssue.TabIndex = 3;
      this.grdIssue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdIssue_CellContentClick);
      // 
      // colIssueKey
      // 
      this.colIssueKey.HeaderText = "Issue";
      this.colIssueKey.Name = "colIssueKey";
      this.colIssueKey.ReadOnly = true;
      // 
      // coldueDate
      // 
      this.coldueDate.HeaderText = "Due Date";
      this.coldueDate.Name = "coldueDate";
      this.coldueDate.ReadOnly = true;
      // 
      // colAssignee
      // 
      this.colAssignee.HeaderText = "Assignee";
      this.colAssignee.Name = "colAssignee";
      this.colAssignee.ReadOnly = true;
      // 
      // colReporter
      // 
      this.colReporter.HeaderText = "Reporter";
      this.colReporter.Name = "colReporter";
      this.colReporter.ReadOnly = true;
      // 
      // frmFlywayHelper
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 561);
      this.Controls.Add(this.grdIssue);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmFlywayHelper";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.Text = "Flyway Helper";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdIssue)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbProjects;
    private System.Windows.Forms.DataGridView grdIssue;
    private System.Windows.Forms.DataGridViewTextBoxColumn colIssueKey;
    private System.Windows.Forms.DataGridViewTextBoxColumn coldueDate;
    private System.Windows.Forms.DataGridViewTextBoxColumn colAssignee;
    private System.Windows.Forms.DataGridViewTextBoxColumn colReporter;
  }
}