using System;
using System.Collections.Generic;

namespace Campus_Facilities
{
    /// <summary>
    /// Represents a campus facilities maintenance or repair issue.
    /// Demonstrates encapsulation through private fields and public properties,
    /// along with business logic and validation methods.
    /// </summary>
    public class FacilityIssue
    {
        // ---------------------------------------------------------
        // Private Fields (Strict Encapsulation)
        // ---------------------------------------------------------
        private int _issueId;
        private string _title = string.Empty;
        private string _location = string.Empty;
        private string _category = string.Empty;
        private string _priority = string.Empty;
        private string _status = string.Empty;
        private string _reportedBy = string.Empty;
        private DateTime _reportDate = DateTime.Today;
        private decimal _estimatedCost;
        private string _description = string.Empty;

        // ---------------------------------------------------------
        // Constructors
        // ---------------------------------------------------------
        public FacilityIssue()
        {
            _reportDate = DateTime.Today;
            _status = "Open";
            _priority = "Medium";
            _category = "General Maintenance";
            _estimatedCost = 0.00m;
        }

        public FacilityIssue(
            int issueId,
            string title,
            string location,
            string category,
            string priority,
            string status,
            string reportedBy,
            DateTime reportDate,
            decimal estimatedCost,
            string description)
        {
            IssueId = issueId;
            Title = title;
            Location = location;
            Category = category;
            Priority = priority;
            Status = status;
            ReportedBy = reportedBy;
            ReportDate = reportDate;
            EstimatedCost = estimatedCost;
            Description = description;
        }

        // ---------------------------------------------------------
        // Public Properties with Encapsulation & Guard Logic
        // ---------------------------------------------------------
        public int IssueId
        {
            get => _issueId;
            set => _issueId = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value?.Trim() ?? string.Empty;
        }

        public string Location
        {
            get => _location;
            set => _location = value?.Trim() ?? string.Empty;
        }

        public string Category
        {
            get => _category;
            set => _category = value?.Trim() ?? string.Empty;
        }

        public string Priority
        {
            get => _priority;
            set => _priority = value?.Trim() ?? string.Empty;
        }

        public string Status
        {
            get => _status;
            set => _status = value?.Trim() ?? string.Empty;
        }

        public string ReportedBy
        {
            get => _reportedBy;
            set => _reportedBy = value?.Trim() ?? string.Empty;
        }

        public DateTime ReportDate
        {
            get => _reportDate;
            set => _reportDate = value;
        }

        public decimal EstimatedCost
        {
            get => _estimatedCost;
            set => _estimatedCost = value >= 0 ? value : 0.00m;
        }

        public string Description
        {
            get => _description;
            set => _description = value?.Trim() ?? string.Empty;
        }

        // ---------------------------------------------------------
        // Business Methods & Validation
        // ---------------------------------------------------------

        /// <summary>
        /// Validates all business rules for the issue before database persistence.
        /// Returns true if valid, or false with a list of error messages.
        /// </summary>
        public bool Validate(out List<string> validationErrors)
        {
            validationErrors = new List<string>();

            // Rule 1: Title validation (required, 5 to 100 characters)
            if (string.IsNullOrWhiteSpace(_title))
            {
                validationErrors.Add("Title is required. Please enter a brief headline of the issue.");
            }
            else if (_title.Length < 5 || _title.Length > 100)
            {
                validationErrors.Add("Issue title must be between 5 and 100 characters long.");
            }

            // Rule 2: Location validation (required, at least 2 characters)
            if (string.IsNullOrWhiteSpace(_location))
            {
                validationErrors.Add("Location is required. Please specify a campus room, building or area.");
            }
            else if (_location.Length < 2 || _location.Length > 60)
            {
                validationErrors.Add("Location must be between 2 and 60 characters long.");
            }

            // Rule 3: Category validation
            if (string.IsNullOrWhiteSpace(_category) || _category == "All")
            {
                validationErrors.Add("Please select a valid Category for the issue.");
            }

            // Rule 4: Priority validation
            if (string.IsNullOrWhiteSpace(_priority) || _priority == "All")
            {
                validationErrors.Add("Please select a valid Priority level (Low, Medium, High, or Critical).");
            }

            // Rule 5: Status validation
            if (string.IsNullOrWhiteSpace(_status) || _status == "All")
            {
                validationErrors.Add("Please select a valid Status (Open, In Progress, Resolved, or Closed).");
            }

            // Rule 6: ReportedBy validation (required)
            if (string.IsNullOrWhiteSpace(_reportedBy))
            {
                validationErrors.Add("Reported By is required. Please enter the name or ID of the reporting staff member.");
            }
            else if (_reportedBy.Length < 2 || _reportedBy.Length > 60)
            {
                validationErrors.Add("Reported By must be between 2 and 60 characters long.");
            }

            // Rule 7: Report date cannot be in the future
            if (_reportDate.Date > DateTime.Today)
            {
                validationErrors.Add("Report date cannot be a future date.");
            }

            // Rule 8: Estimated cost must be non-negative
            if (_estimatedCost < 0)
            {
                validationErrors.Add("Estimated cost must be a positive number or zero ($0.00 or greater).");
            }

            return validationErrors.Count == 0;
        }

        /// <summary>
        /// Calculates the recommended service target turnaround time (in business days)
        /// based on the priority level. Demonstrates OOP business calculations.
        /// </summary>
        public int CalculateTargetResolutionDays()
        {
            switch (_priority?.Trim().ToUpperInvariant())
            {
                case "CRITICAL":
                    return 1; // Immediate / same-day action
                case "HIGH":
                    return 3; // Within 3 business days
                case "MEDIUM":
                    return 7; // Within 1 week
                case "LOW":
                    return 14; // Within 2 weeks
                default:
                    return 7;
            }
        }

        /// <summary>
        /// Generates a readable single-line summary of the issue.
        /// </summary>
        public string GetSummary()
        {
            return $"[ID #{_issueId}] {_title} | Location: {_location} | Priority: {_priority} | Status: {_status} | Est: ${_estimatedCost:F2}";
        }

        public override string ToString()
        {
            return GetSummary();
        }
    }
}
