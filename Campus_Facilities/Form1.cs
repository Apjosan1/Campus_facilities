using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Campus_Facilities
{
    public partial class Form1 : Form
    {
        // ---------------------------------------------------------
        // Core Business Manager (Interacting Class)
        // ---------------------------------------------------------
        private readonly IssueManager _issueManager;
        private bool _isBindingData = false;

        public Form1()
        {
            InitializeComponent();
            _issueManager = new IssueManager();
        }

        // ---------------------------------------------------------
        // Form Load & Initialization
        // ---------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Populate DropDown Lists
                PopulateComboBoxes();

                // 2. Initialize Database & Seed Sample Records
                bool dbOk = _issueManager.InitializeDatabase(out string dbMessage);
                if (dbOk)
                {
                    lblDbStatus.Text = $"Database: {Path.GetFileName(_issueManager.DatabaseFilePath)} (Connected)";
                    lblDbStatus.ForeColor = Color.FromArgb(16, 185, 129); // Green

                    // 3. Load initial data into DataGridView
                    LoadGridData();
                }
                else
                {
                    lblDbStatus.Text = $"Database Status: {dbMessage}";
                    lblDbStatus.ForeColor = Color.FromArgb(239, 68, 68); // Red
                    MessageBox.Show($"Database connection message:\n{dbMessage}", "Database Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 4. Reset form fields to clean initial state
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application Initialization Error: {ex.Message}", "Startup Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateComboBoxes()
        {
            // Category options
            string[] categories = new[]
            {
                "Electrical",
                "Plumbing",
                "Furniture",
                "HVAC / Heating",
                "Safety & Security",
                "General Maintenance"
            };

            cboCategory.Items.Clear();
            cboCategory.Items.AddRange(categories);
            cboCategory.SelectedIndex = 0;

            // Priority options
            string[] priorities = new[] { "Low", "Medium", "High", "Critical" };
            cboPriority.Items.Clear();
            cboPriority.Items.AddRange(priorities);
            cboPriority.SelectedIndex = 1; // Default to Medium

            // Status options
            string[] statuses = new[] { "Open", "In Progress", "Resolved", "Closed" };
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(statuses);
            cboStatus.SelectedIndex = 0; // Default to Open

            // Filter Category (includes 'All Categories')
            cboFilterCategory.Items.Clear();
            cboFilterCategory.Items.Add("All Categories");
            cboFilterCategory.Items.AddRange(categories);
            cboFilterCategory.SelectedIndex = 0;

            // Filter Status (includes 'All Statuses')
            cboFilterStatus.Items.Clear();
            cboFilterStatus.Items.Add("All Statuses");
            cboFilterStatus.Items.AddRange(statuses);
            cboFilterStatus.SelectedIndex = 0;

            // Set max date for report date picker to today
            dtpReportDate.MaxDate = DateTime.Today;
            dtpReportDate.Value = DateTime.Today;
        }

        // ---------------------------------------------------------
        // Data Loading & Grid Display
        // ---------------------------------------------------------
        private void LoadGridData()
        {
            try
            {
                _isBindingData = true;

                string keyword = txtSearch.Text.Trim();
                string categoryFilter = cboFilterCategory.SelectedItem?.ToString() ?? "All Categories";
                string statusFilter = cboFilterStatus.SelectedItem?.ToString() ?? "All Statuses";

                DataTable dt;
                if (string.IsNullOrEmpty(keyword) && categoryFilter == "All Categories" && statusFilter == "All Statuses")
                {
                    dt = _issueManager.GetAllIssues();
                }
                else
                {
                    dt = _issueManager.SearchAndFilter(keyword, categoryFilter, statusFilter);
                }

                dgvIssues.DataSource = dt;
                ConfigureGridColumns();
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load issue records: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isBindingData = false;
            }
        }

        private void ConfigureGridColumns()
        {
            if (dgvIssues.Columns.Count == 0) return;

            dgvIssues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgvIssues.Columns.Contains("Issue ID"))
            {
                dgvIssues.Columns["Issue ID"].Width = 65;
            }

            if (dgvIssues.Columns.Contains("Title"))
            {
                dgvIssues.Columns["Title"].Width = 175;
            }

            if (dgvIssues.Columns.Contains("Location"))
            {
                dgvIssues.Columns["Location"].Width = 140;
            }

            if (dgvIssues.Columns.Contains("Category"))
            {
                dgvIssues.Columns["Category"].Width = 110;
            }

            if (dgvIssues.Columns.Contains("Priority"))
            {
                dgvIssues.Columns["Priority"].Width = 75;
            }

            if (dgvIssues.Columns.Contains("Status"))
            {
                dgvIssues.Columns["Status"].Width = 85;
            }

            if (dgvIssues.Columns.Contains("Reported By"))
            {
                dgvIssues.Columns["Reported By"].Width = 120;
            }

            if (dgvIssues.Columns.Contains("Report Date"))
            {
                dgvIssues.Columns["Report Date"].Width = 90;
                dgvIssues.Columns["Report Date"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }

            if (dgvIssues.Columns.Contains("Est. Cost ($)"))
            {
                dgvIssues.Columns["Est. Cost ($)"].Width = 95;
                dgvIssues.Columns["Est. Cost ($)"].DefaultCellStyle.Format = "C2";
                dgvIssues.Columns["Est. Cost ($)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvIssues.Columns.Contains("Description"))
            {
                dgvIssues.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvIssues.Columns["Description"].MinimumWidth = 160;
            }
        }

        private void UpdateStatusBar()
        {
            var (totalCount, openCount, totalCost) = _issueManager.GetStatistics();
            int currentVisible = dgvIssues.Rows.Count;

            lblStats.Text = $"Showing: {currentVisible} of {totalCount} total issues | Outstanding (Open/In Progress): {openCount} | Total Est. Repair Cost: {totalCost:C2}";
        }

        // ---------------------------------------------------------
        // Grid Selection to Form Binding
        // ---------------------------------------------------------
        private void dgvIssues_SelectionChanged(object sender, EventArgs e)
        {
            if (_isBindingData) return;
            PopulateFieldsFromSelectedRow();
        }

        private void dgvIssues_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PopulateFieldsFromSelectedRow();
            }
        }

        private void PopulateFieldsFromSelectedRow()
        {
            if (dgvIssues.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvIssues.SelectedRows[0];
            if (row.Cells["Issue ID"].Value == null || row.Cells["Issue ID"].Value == DBNull.Value) return;

            txtIssueId.Text = row.Cells["Issue ID"].Value.ToString();
            txtTitle.Text = row.Cells["Title"].Value?.ToString() ?? string.Empty;
            txtLocation.Text = row.Cells["Location"].Value?.ToString() ?? string.Empty;

            string category = row.Cells["Category"].Value?.ToString() ?? string.Empty;
            if (cboCategory.Items.Contains(category))
            {
                cboCategory.SelectedItem = category;
            }

            string priority = row.Cells["Priority"].Value?.ToString() ?? string.Empty;
            if (cboPriority.Items.Contains(priority))
            {
                cboPriority.SelectedItem = priority;
            }

            string status = row.Cells["Status"].Value?.ToString() ?? string.Empty;
            if (cboStatus.Items.Contains(status))
            {
                cboStatus.SelectedItem = status;
            }

            txtReportedBy.Text = row.Cells["Reported By"].Value?.ToString() ?? string.Empty;

            if (row.Cells["Report Date"].Value != null && DateTime.TryParse(row.Cells["Report Date"].Value.ToString(), out DateTime rDate))
            {
                dtpReportDate.Value = rDate <= dtpReportDate.MaxDate ? rDate : dtpReportDate.MaxDate;
            }

            if (row.Cells["Est. Cost ($)"].Value != null && decimal.TryParse(row.Cells["Est. Cost ($)"].Value.ToString(), out decimal cost))
            {
                txtEstimatedCost.Text = cost.ToString("F2");
            }
            else
            {
                txtEstimatedCost.Text = "0.00";
            }

            txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? string.Empty;
        }

        // ---------------------------------------------------------
        // CRUD Operations
        // ---------------------------------------------------------

        /// <summary>
        /// Handles Create (Add) operation.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Gather & parse input values
            if (!decimal.TryParse(txtEstimatedCost.Text.Trim().Replace("$", ""), out decimal estimatedCost))
            {
                MessageBox.Show("Estimated Cost must be a valid numeric amount (e.g., 150.00).",
                    "Input Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEstimatedCost.Focus();
                return;
            }

            // 2. Instantiate custom FacilityIssue business entity
            FacilityIssue newIssue = new FacilityIssue
            {
                Title = txtTitle.Text,
                Location = txtLocation.Text,
                Category = cboCategory.SelectedItem?.ToString() ?? string.Empty,
                Priority = cboPriority.SelectedItem?.ToString() ?? string.Empty,
                Status = cboStatus.SelectedItem?.ToString() ?? string.Empty,
                ReportedBy = txtReportedBy.Text,
                ReportDate = dtpReportDate.Value,
                EstimatedCost = estimatedCost,
                Description = txtDescription.Text
            };

            // 3. Delegate creation to IssueManager
            bool success = _issueManager.AddIssue(newIssue, out string errorMessage);
            if (success)
            {
                int targetDays = newIssue.CalculateTargetResolutionDays();
                MessageBox.Show(
                    $"Facilities issue successfully registered!\n\n" +
                    $"Assigned ID: #{newIssue.IssueId}\n" +
                    $"Title: {newIssue.Title}\n" +
                    $"Priority Target: Turnaround within {targetDays} business day(s).",
                    "Issue Created",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadGridData();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show(errorMessage, "Validation / Business Rule Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles Update operation.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Ensure an issue is selected
            if (!int.TryParse(txtIssueId.Text.Trim(), out int issueId) || issueId <= 0)
            {
                MessageBox.Show("Please select an existing facilities issue from the table to update.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Parse estimated cost
            if (!decimal.TryParse(txtEstimatedCost.Text.Trim().Replace("$", ""), out decimal estimatedCost))
            {
                MessageBox.Show("Estimated Cost must be a valid numeric amount (e.g., 150.00).",
                    "Input Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEstimatedCost.Focus();
                return;
            }

            // 3. Instantiate updated FacilityIssue entity
            FacilityIssue updatedIssue = new FacilityIssue
            {
                IssueId = issueId,
                Title = txtTitle.Text,
                Location = txtLocation.Text,
                Category = cboCategory.SelectedItem?.ToString() ?? string.Empty,
                Priority = cboPriority.SelectedItem?.ToString() ?? string.Empty,
                Status = cboStatus.SelectedItem?.ToString() ?? string.Empty,
                ReportedBy = txtReportedBy.Text,
                ReportDate = dtpReportDate.Value,
                EstimatedCost = estimatedCost,
                Description = txtDescription.Text
            };

            // 4. Delegate update to IssueManager
            bool success = _issueManager.UpdateIssue(updatedIssue, out string errorMessage);
            if (success)
            {
                MessageBox.Show($"Facilities issue #{issueId} updated successfully.",
                    "Issue Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadGridData();
            }
            else
            {
                MessageBox.Show(errorMessage, "Validation / Update Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles Delete operation.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Ensure an issue is selected
            if (!int.TryParse(txtIssueId.Text.Trim(), out int issueId) || issueId <= 0)
            {
                MessageBox.Show("Please select an issue from the table that you wish to delete.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string issueTitle = txtTitle.Text.Trim();

            // 2. Confirm deletion with the user
            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to permanently delete Issue #{issueId} ({issueTitle})?\n\nThis operation cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            // 3. Delegate deletion to IssueManager
            bool success = _issueManager.DeleteIssue(issueId, out string errorMessage);
            if (success)
            {
                MessageBox.Show($"Facilities issue #{issueId} has been successfully deleted.",
                    "Issue Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadGridData();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show(errorMessage, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clears all input fields and resets selections.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtIssueId.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtLocation.Text = string.Empty;

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            if (cboPriority.Items.Count > 1)
                cboPriority.SelectedIndex = 1; // Medium

            if (cboStatus.Items.Count > 0)
                cboStatus.SelectedIndex = 0; // Open

            txtReportedBy.Text = string.Empty;
            dtpReportDate.Value = DateTime.Today;
            txtEstimatedCost.Text = "0.00";
            txtDescription.Text = string.Empty;

            if (dgvIssues.SelectedRows.Count > 0)
            {
                dgvIssues.ClearSelection();
            }

            txtTitle.Focus();
        }

        // ---------------------------------------------------------
        // Search & Filter Event Handlers
        // ---------------------------------------------------------
        private void FilterCriteria_Changed(object sender, EventArgs e)
        {
            if (_isBindingData) return;
            LoadGridData();
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            if (cboFilterCategory.Items.Count > 0)
                cboFilterCategory.SelectedIndex = 0;
            if (cboFilterStatus.Items.Count > 0)
                cboFilterStatus.SelectedIndex = 0;

            LoadGridData();
        }
    }
}
