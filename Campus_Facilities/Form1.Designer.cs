namespace Campus_Facilities
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Top Header
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppSubtitle;

        // Left Details GroupBox
        private System.Windows.Forms.GroupBox grpIssueDetails;
        private System.Windows.Forms.Label lblIssueId;
        private System.Windows.Forms.TextBox txtIssueId;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ComboBox cboPriority;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblReportedBy;
        private System.Windows.Forms.TextBox txtReportedBy;
        private System.Windows.Forms.Label lblReportDate;
        private System.Windows.Forms.DateTimePicker dtpReportDate;
        private System.Windows.Forms.Label lblEstimatedCost;
        private System.Windows.Forms.TextBox txtEstimatedCost;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;

        // Action Buttons
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;

        // Filter / Search Panel
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFilterCategory;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.Button btnResetFilter;

        // Data Display
        private System.Windows.Forms.DataGridView dgvIssues;

        // Bottom Status Bar
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblDbStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            
            pnlHeader = new System.Windows.Forms.Panel();
            lblAppSubtitle = new System.Windows.Forms.Label();
            lblAppTitle = new System.Windows.Forms.Label();
            grpIssueDetails = new System.Windows.Forms.GroupBox();
            btnClear = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            txtDescription = new System.Windows.Forms.TextBox();
            lblDescription = new System.Windows.Forms.Label();
            txtEstimatedCost = new System.Windows.Forms.TextBox();
            lblEstimatedCost = new System.Windows.Forms.Label();
            dtpReportDate = new System.Windows.Forms.DateTimePicker();
            lblReportDate = new System.Windows.Forms.Label();
            txtReportedBy = new System.Windows.Forms.TextBox();
            lblReportedBy = new System.Windows.Forms.Label();
            cboStatus = new System.Windows.Forms.ComboBox();
            lblStatus = new System.Windows.Forms.Label();
            cboPriority = new System.Windows.Forms.ComboBox();
            lblPriority = new System.Windows.Forms.Label();
            cboCategory = new System.Windows.Forms.ComboBox();
            lblCategory = new System.Windows.Forms.Label();
            txtLocation = new System.Windows.Forms.TextBox();
            lblLocation = new System.Windows.Forms.Label();
            txtTitle = new System.Windows.Forms.TextBox();
            lblTitle = new System.Windows.Forms.Label();
            txtIssueId = new System.Windows.Forms.TextBox();
            lblIssueId = new System.Windows.Forms.Label();
            grpFilter = new System.Windows.Forms.GroupBox();
            btnResetFilter = new System.Windows.Forms.Button();
            cboFilterStatus = new System.Windows.Forms.ComboBox();
            lblFilterStatus = new System.Windows.Forms.Label();
            cboFilterCategory = new System.Windows.Forms.ComboBox();
            lblFilterCategory = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            lblSearch = new System.Windows.Forms.Label();
            dgvIssues = new System.Windows.Forms.DataGridView();
            pnlStatusBar = new System.Windows.Forms.Panel();
            lblDbStatus = new System.Windows.Forms.Label();
            lblStats = new System.Windows.Forms.Label();

            pnlHeader.SuspendLayout();
            grpIssueDetails.SuspendLayout();
            grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIssues).BeginInit();
            pnlStatusBar.SuspendLayout();
            SuspendLayout();

            // -------------------------------------------------------------
            // pnlHeader
            // -------------------------------------------------------------
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(lblAppSubtitle);
            pnlHeader.Controls.Add(lblAppTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(1260, 68);
            pnlHeader.TabIndex = 0;

            // lblAppTitle
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblAppTitle.ForeColor = System.Drawing.Color.White;
            lblAppTitle.Location = new System.Drawing.Point(18, 11);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new System.Drawing.Size(425, 30);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "Campus Facilities Issue Register";

            // lblAppSubtitle
            lblAppSubtitle.AutoSize = true;
            lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            lblAppSubtitle.Location = new System.Drawing.Point(21, 43);
            lblAppSubtitle.Name = "lblAppSubtitle";
            lblAppSubtitle.Size = new System.Drawing.Size(400, 15);
            lblAppSubtitle.TabIndex = 1;
            lblAppSubtitle.Text = "ICL Campus Maintenance, Repair Tracking & Issue Management System";

            // -------------------------------------------------------------
            // grpIssueDetails
            // -------------------------------------------------------------
            grpIssueDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            grpIssueDetails.BackColor = System.Drawing.Color.White;
            grpIssueDetails.Controls.Add(btnClear);
            grpIssueDetails.Controls.Add(btnDelete);
            grpIssueDetails.Controls.Add(btnUpdate);
            grpIssueDetails.Controls.Add(btnAdd);
            grpIssueDetails.Controls.Add(txtDescription);
            grpIssueDetails.Controls.Add(lblDescription);
            grpIssueDetails.Controls.Add(txtEstimatedCost);
            grpIssueDetails.Controls.Add(lblEstimatedCost);
            grpIssueDetails.Controls.Add(dtpReportDate);
            grpIssueDetails.Controls.Add(lblReportDate);
            grpIssueDetails.Controls.Add(txtReportedBy);
            grpIssueDetails.Controls.Add(lblReportedBy);
            grpIssueDetails.Controls.Add(cboStatus);
            grpIssueDetails.Controls.Add(lblStatus);
            grpIssueDetails.Controls.Add(cboPriority);
            grpIssueDetails.Controls.Add(lblPriority);
            grpIssueDetails.Controls.Add(cboCategory);
            grpIssueDetails.Controls.Add(lblCategory);
            grpIssueDetails.Controls.Add(txtLocation);
            grpIssueDetails.Controls.Add(lblLocation);
            grpIssueDetails.Controls.Add(txtTitle);
            grpIssueDetails.Controls.Add(lblTitle);
            grpIssueDetails.Controls.Add(txtIssueId);
            grpIssueDetails.Controls.Add(lblIssueId);
            grpIssueDetails.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            grpIssueDetails.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            grpIssueDetails.Location = new System.Drawing.Point(12, 77);
            grpIssueDetails.Name = "grpIssueDetails";
            grpIssueDetails.Size = new System.Drawing.Size(415, 625);
            grpIssueDetails.TabIndex = 1;
            grpIssueDetails.TabStop = false;
            grpIssueDetails.Text = " Issue Details ";

            // lblIssueId & txtIssueId
            lblIssueId.AutoSize = true;
            lblIssueId.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblIssueId.Location = new System.Drawing.Point(16, 24);
            lblIssueId.Name = "lblIssueId";
            lblIssueId.Size = new System.Drawing.Size(51, 15);
            lblIssueId.TabIndex = 0;
            lblIssueId.Text = "Issue ID:";

            txtIssueId.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            txtIssueId.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            txtIssueId.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            txtIssueId.Location = new System.Drawing.Point(115, 21);
            txtIssueId.Name = "txtIssueId";
            txtIssueId.ReadOnly = true;
            txtIssueId.Size = new System.Drawing.Size(280, 23);
            txtIssueId.TabIndex = 1;

            // lblTitle & txtTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblTitle.Location = new System.Drawing.Point(16, 52);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(65, 15);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Issue Title:*";

            txtTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            txtTitle.Location = new System.Drawing.Point(115, 49);
            txtTitle.MaxLength = 100;
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new System.Drawing.Size(280, 23);
            txtTitle.TabIndex = 3;

            // lblLocation & txtLocation
            lblLocation.AutoSize = true;
            lblLocation.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblLocation.Location = new System.Drawing.Point(16, 80);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new System.Drawing.Size(61, 15);
            lblLocation.TabIndex = 4;
            lblLocation.Text = "Location:*";

            txtLocation.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            txtLocation.Location = new System.Drawing.Point(115, 77);
            txtLocation.MaxLength = 60;
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new System.Drawing.Size(280, 23);
            txtLocation.TabIndex = 5;

            // lblCategory & cboCategory
            lblCategory.AutoSize = true;
            lblCategory.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblCategory.Location = new System.Drawing.Point(16, 108);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new System.Drawing.Size(63, 15);
            lblCategory.TabIndex = 6;
            lblCategory.Text = "Category:*";

            cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboCategory.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new System.Drawing.Point(115, 105);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new System.Drawing.Size(280, 21);
            cboCategory.TabIndex = 7;

            // lblPriority & cboPriority
            lblPriority.AutoSize = true;
            lblPriority.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblPriority.Location = new System.Drawing.Point(16, 136);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new System.Drawing.Size(53, 15);
            lblPriority.TabIndex = 8;
            lblPriority.Text = "Priority:*";

            cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboPriority.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            cboPriority.FormattingEnabled = true;
            cboPriority.Location = new System.Drawing.Point(115, 133);
            cboPriority.Name = "cboPriority";
            cboPriority.Size = new System.Drawing.Size(280, 21);
            cboPriority.TabIndex = 9;

            // lblStatus & cboStatus
            lblStatus.AutoSize = true;
            lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblStatus.Location = new System.Drawing.Point(16, 164);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(47, 15);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "Status:*";

            cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new System.Drawing.Point(115, 161);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new System.Drawing.Size(280, 21);
            cboStatus.TabIndex = 11;

            // lblReportedBy & txtReportedBy
            lblReportedBy.AutoSize = true;
            lblReportedBy.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblReportedBy.Location = new System.Drawing.Point(16, 192);
            lblReportedBy.Name = "lblReportedBy";
            lblReportedBy.Size = new System.Drawing.Size(79, 15);
            lblReportedBy.TabIndex = 12;
            lblReportedBy.Text = "Reported By:*";

            txtReportedBy.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            txtReportedBy.Location = new System.Drawing.Point(115, 189);
            txtReportedBy.MaxLength = 60;
            txtReportedBy.Name = "txtReportedBy";
            txtReportedBy.Size = new System.Drawing.Size(280, 23);
            txtReportedBy.TabIndex = 13;

            // lblReportDate & dtpReportDate
            lblReportDate.AutoSize = true;
            lblReportDate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblReportDate.Location = new System.Drawing.Point(16, 220);
            lblReportDate.Name = "lblReportDate";
            lblReportDate.Size = new System.Drawing.Size(77, 15);
            lblReportDate.TabIndex = 14;
            lblReportDate.Text = "Report Date:*";

            dtpReportDate.CustomFormat = "yyyy-MM-dd";
            dtpReportDate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpReportDate.Location = new System.Drawing.Point(115, 217);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new System.Drawing.Size(280, 23);
            dtpReportDate.TabIndex = 15;

            // lblEstimatedCost & txtEstimatedCost
            lblEstimatedCost.AutoSize = true;
            lblEstimatedCost.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblEstimatedCost.Location = new System.Drawing.Point(16, 248);
            lblEstimatedCost.Name = "lblEstimatedCost";
            lblEstimatedCost.Size = new System.Drawing.Size(81, 15);
            lblEstimatedCost.TabIndex = 16;
            lblEstimatedCost.Text = "Est. Cost ($):*";

            txtEstimatedCost.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            txtEstimatedCost.Location = new System.Drawing.Point(115, 245);
            txtEstimatedCost.MaxLength = 12;
            txtEstimatedCost.Name = "txtEstimatedCost";
            txtEstimatedCost.Size = new System.Drawing.Size(280, 23);
            txtEstimatedCost.TabIndex = 17;

            // lblDescription & txtDescription
            lblDescription.AutoSize = true;
            lblDescription.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblDescription.Location = new System.Drawing.Point(16, 276);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(70, 15);
            lblDescription.TabIndex = 18;
            lblDescription.Text = "Description:";

            txtDescription.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            txtDescription.Location = new System.Drawing.Point(115, 273);
            txtDescription.MaxLength = 500;
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDescription.Size = new System.Drawing.Size(280, 52);
            txtDescription.TabIndex = 19;

            // btnAdd (Submit / Add Issue)
            btnAdd.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Location = new System.Drawing.Point(16, 335);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(185, 38);
            btnAdd.TabIndex = 20;
            btnAdd.Text = "+ Submit / Add Issue";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnUpdate
            btnUpdate.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnUpdate.ForeColor = System.Drawing.Color.White;
            btnUpdate.Location = new System.Drawing.Point(210, 335);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(185, 38);
            btnUpdate.TabIndex = 21;
            btnUpdate.Text = "Save / Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            btnDelete.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Location = new System.Drawing.Point(16, 380);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(185, 35);
            btnDelete.TabIndex = 22;
            btnDelete.Text = "Delete Selected";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClear
            btnClear.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClear.ForeColor = System.Drawing.Color.White;
            btnClear.Location = new System.Drawing.Point(210, 380);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(185, 35);
            btnClear.TabIndex = 23;
            btnClear.Text = "Clear Fields";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // -------------------------------------------------------------
            // grpFilter
            // -------------------------------------------------------------
            grpFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grpFilter.BackColor = System.Drawing.Color.White;
            grpFilter.Controls.Add(btnResetFilter);
            grpFilter.Controls.Add(cboFilterStatus);
            grpFilter.Controls.Add(lblFilterStatus);
            grpFilter.Controls.Add(cboFilterCategory);
            grpFilter.Controls.Add(lblFilterCategory);
            grpFilter.Controls.Add(txtSearch);
            grpFilter.Controls.Add(lblSearch);
            grpFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            grpFilter.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            grpFilter.Location = new System.Drawing.Point(438, 77);
            grpFilter.Name = "grpFilter";
            grpFilter.Size = new System.Drawing.Size(810, 75);
            grpFilter.TabIndex = 2;
            grpFilter.TabStop = false;
            grpFilter.Text = " Search & Filter ";

            // lblSearch & txtSearch
            lblSearch.AutoSize = true;
            lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblSearch.Location = new System.Drawing.Point(14, 33);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(45, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";

            txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            txtSearch.Location = new System.Drawing.Point(64, 30);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += new System.EventHandler(this.FilterCriteria_Changed);

            // lblFilterCategory & cboFilterCategory
            lblFilterCategory.AutoSize = true;
            lblFilterCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblFilterCategory.Location = new System.Drawing.Point(275, 33);
            lblFilterCategory.Name = "lblFilterCategory";
            lblFilterCategory.Size = new System.Drawing.Size(58, 15);
            lblFilterCategory.TabIndex = 2;
            lblFilterCategory.Text = "Category:";

            cboFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboFilterCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            cboFilterCategory.FormattingEnabled = true;
            cboFilterCategory.Location = new System.Drawing.Point(337, 30);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new System.Drawing.Size(150, 23);
            cboFilterCategory.TabIndex = 3;
            cboFilterCategory.SelectedIndexChanged += new System.EventHandler(this.FilterCriteria_Changed);

            // lblFilterStatus & cboFilterStatus
            lblFilterStatus.AutoSize = true;
            lblFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblFilterStatus.Location = new System.Drawing.Point(498, 33);
            lblFilterStatus.Name = "lblFilterStatus";
            lblFilterStatus.Size = new System.Drawing.Size(42, 15);
            lblFilterStatus.TabIndex = 4;
            lblFilterStatus.Text = "Status:";

            cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            cboFilterStatus.FormattingEnabled = true;
            cboFilterStatus.Location = new System.Drawing.Point(544, 30);
            cboFilterStatus.Name = "cboFilterStatus";
            cboFilterStatus.Size = new System.Drawing.Size(130, 23);
            cboFilterStatus.TabIndex = 5;
            cboFilterStatus.SelectedIndexChanged += new System.EventHandler(this.FilterCriteria_Changed);

            // btnResetFilter
            btnResetFilter.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnResetFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            btnResetFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnResetFilter.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            btnResetFilter.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnResetFilter.Location = new System.Drawing.Point(688, 27);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new System.Drawing.Size(105, 30);
            btnResetFilter.TabIndex = 6;
            btnResetFilter.Text = "Reset Filters";
            btnResetFilter.UseVisualStyleBackColor = false;
            btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);

            // -------------------------------------------------------------
            // dgvIssues
            // -------------------------------------------------------------
            dgvIssues.AllowUserToAddRows = false;
            dgvIssues.AllowUserToDeleteRows = false;
            dgvIssues.AllowUserToResizeRows = false;
            dgvIssues.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvIssues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvIssues.BackgroundColor = System.Drawing.Color.White;
            dgvIssues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dgvIssues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvIssues.ColumnHeadersHeight = 34;
            dgvIssues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(191, 219, 254);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dgvIssues.DefaultCellStyle = dataGridViewCellStyle2;

            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvIssues.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;

            dgvIssues.EnableHeadersVisualStyles = false;
            dgvIssues.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvIssues.Location = new System.Drawing.Point(438, 160);
            dgvIssues.MultiSelect = false;
            dgvIssues.Name = "dgvIssues";
            dgvIssues.ReadOnly = true;
            dgvIssues.RowHeadersVisible = false;
            dgvIssues.RowTemplate.Height = 28;
            dgvIssues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvIssues.Size = new System.Drawing.Size(810, 547);
            dgvIssues.TabIndex = 3;
            dgvIssues.SelectionChanged += new System.EventHandler(this.dgvIssues_SelectionChanged);
            dgvIssues.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIssues_CellClick);

            // -------------------------------------------------------------
            // pnlStatusBar
            // -------------------------------------------------------------
            pnlStatusBar.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            pnlStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlStatusBar.Controls.Add(lblDbStatus);
            pnlStatusBar.Controls.Add(lblStats);
            pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlStatusBar.Location = new System.Drawing.Point(0, 715);
            pnlStatusBar.Name = "pnlStatusBar";
            pnlStatusBar.Size = new System.Drawing.Size(1260, 32);
            pnlStatusBar.TabIndex = 4;

            // lblStats
            lblStats.AutoSize = true;
            lblStats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblStats.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblStats.Location = new System.Drawing.Point(12, 8);
            lblStats.Name = "lblStats";
            lblStats.Size = new System.Drawing.Size(420, 15);
            lblStats.TabIndex = 0;
            lblStats.Text = "Total Issues: 0 | Open / In Progress: 0 | Total Estimated Repair Cost: $0.00";

            // lblDbStatus
            lblDbStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblDbStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblDbStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblDbStatus.Location = new System.Drawing.Point(650, 8);
            lblDbStatus.Name = "lblDbStatus";
            lblDbStatus.Size = new System.Drawing.Size(595, 15);
            lblDbStatus.TabIndex = 1;
            lblDbStatus.Text = "Database: Connected to LocalDB";
            lblDbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // -------------------------------------------------------------
            // Form1
            // -------------------------------------------------------------
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            ClientSize = new System.Drawing.Size(1260, 755);
            Controls.Add(pnlStatusBar);
            Controls.Add(dgvIssues);
            Controls.Add(grpFilter);
            Controls.Add(grpIssueDetails);
            Controls.Add(pnlHeader);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            MinimumSize = new System.Drawing.Size(960, 540);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Campus Facilities Issue Register - 5101 Assignment 3";
            Load += new System.EventHandler(this.Form1_Load);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpIssueDetails.ResumeLayout(false);
            grpIssueDetails.PerformLayout();
            grpFilter.ResumeLayout(false);
            grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIssues).EndInit();
            pnlStatusBar.ResumeLayout(false);
            pnlStatusBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
