using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Campus_Facilities
{
    /// <summary>
    /// Manages data persistence, business workflows, and SQL Server LocalDB integration
    /// for FacilityIssue entities using ADO.NET.
    /// Interacts directly with the FacilityIssue class.
    /// </summary>
    public class IssueManager
    {
        // ---------------------------------------------------------
        // Private Fields
        // ---------------------------------------------------------
        private readonly string _databaseFilePath;
        private string _activeConnectionString = string.Empty;

        // ---------------------------------------------------------
        // Constructors
        // ---------------------------------------------------------
        public IssueManager()
        {
            _databaseFilePath = ResolveDatabasePath();
        }

        public IssueManager(string customConnectionString)
        {
            _activeConnectionString = customConnectionString;
            _databaseFilePath = string.Empty;
        }

        // ---------------------------------------------------------
        // Public Properties
        // ---------------------------------------------------------
        public string ConnectionString => _activeConnectionString;
        public string DatabaseFilePath => _databaseFilePath;

        // ---------------------------------------------------------
        // Database & Path Resolution
        // ---------------------------------------------------------
        private static string ResolveDatabasePath()
        {
            const string dbFileName = "CampusFacilitiesDB.mdf";
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // 1. Check current application output directory
            string primaryPath = Path.Combine(baseDir, dbFileName);
            if (File.Exists(primaryPath))
            {
                return primaryPath;
            }

            // 2. Check project source directory (in development: bin\Debug\net10.0-windows\..\..\..)
            try
            {
                string projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));
                string projectPath = Path.Combine(projectDir, dbFileName);
                if (File.Exists(projectPath))
                {
                    return projectPath;
                }
            }
            catch
            {
                // Fall back to primaryPath
            }

            // Default to primaryPath in execution directory
            return primaryPath;
        }

        /// <summary>
        /// Provides a resilient SQL connection.
        /// First attempts direct connection to registered 'CampusFacilitiesDB' database,
        /// then falls back to file attachment or master creation if required.
        /// </summary>
        public SqlConnection GetConnection()
        {
            // If we already established a working connection string, use it
            if (!string.IsNullOrEmpty(_activeConnectionString))
            {
                try
                {
                    var cachedConn = new SqlConnection(_activeConnectionString);
                    cachedConn.Open();
                    return cachedConn;
                }
                catch
                {
                    _activeConnectionString = string.Empty; // Reset and re-detect
                }
            }

            // Strategy 1: Connect directly to registered LocalDB database name
            string directConnStr = @"Server=(localdb)\MSSQLLocalDB;Database=CampusFacilitiesDB;Integrated Security=True;Connect Timeout=5;Encrypt=False;";
            try
            {
                var conn = new SqlConnection(directConnStr);
                conn.Open();
                _activeConnectionString = directConnStr;
                return conn;
            }
            catch
            {
                // Direct database connection failed; proceed to file attachment strategy
            }

            // Strategy 2: Attach via file path WITHOUT 'Database=' parameter (prevents name conflict)
            if (File.Exists(_databaseFilePath))
            {
                string attachConnStr = $@"Server=(localdb)\MSSQLLocalDB;AttachDbFilename={_databaseFilePath};Integrated Security=True;Connect Timeout=15;Encrypt=False;";
                try
                {
                    var conn = new SqlConnection(attachConnStr);
                    conn.Open();
                    _activeConnectionString = attachConnStr;
                    return conn;
                }
                catch
                {
                    // Fall back to master attachment strategy
                }
            }

            // Strategy 3: Connect to master to register/create the database
            EnsureDatabaseRegisteredViaMaster();

            // Retry direct connection now that it is registered
            var finalConn = new SqlConnection(directConnStr);
            finalConn.Open();
            _activeConnectionString = directConnStr;
            return finalConn;
        }

        private void EnsureDatabaseRegisteredViaMaster()
        {
            string masterConnStr = @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            using (SqlConnection masterConn = new SqlConnection(masterConnStr))
            {
                masterConn.Open();

                string dbDir = Path.GetDirectoryName(_databaseFilePath) ?? AppDomain.CurrentDomain.BaseDirectory;
                Directory.CreateDirectory(dbDir);
                string ldfPath = Path.Combine(dbDir, Path.GetFileNameWithoutExtension(_databaseFilePath) + "_log.ldf");

                // Check if database is already in master catalog
                bool dbExistsInMaster = false;
                using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM sys.databases WHERE name = 'CampusFacilitiesDB';", masterConn))
                {
                    dbExistsInMaster = ((int)checkCmd.ExecuteScalar()) > 0;
                }

                if (dbExistsInMaster)
                {
                    // If registered but points to non-existent file, detach it
                    string checkPathSql = "SELECT physical_name FROM sys.master_files WHERE database_id = DB_ID('CampusFacilitiesDB') AND file_id = 1;";
                    using (SqlCommand pathCmd = new SqlCommand(checkPathSql, masterConn))
                    {
                        object pathObj = pathCmd.ExecuteScalar();
                        string existingPath = pathObj?.ToString() ?? string.Empty;
                        if (!string.Equals(existingPath, _databaseFilePath, StringComparison.OrdinalIgnoreCase) || !File.Exists(existingPath))
                        {
                            using (SqlCommand detachCmd = new SqlCommand(
                                @"ALTER DATABASE [CampusFacilitiesDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                  EXEC sp_detach_db 'CampusFacilitiesDB';", masterConn))
                            {
                                detachCmd.ExecuteNonQuery();
                            }
                            dbExistsInMaster = false;
                        }
                    }
                }

                if (!dbExistsInMaster)
                {
                    if (File.Exists(_databaseFilePath))
                    {
                        // Attach existing MDF file
                        string attachSql = $@"
                            CREATE DATABASE [CampusFacilitiesDB] 
                            ON (FILENAME = '{_databaseFilePath}') 
                            FOR ATTACH;";
                        using (SqlCommand attachCmd = new SqlCommand(attachSql, masterConn))
                        {
                            attachCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Create brand new database files
                        string createSql = $@"
                            CREATE DATABASE [CampusFacilitiesDB]
                            ON PRIMARY (NAME = 'CampusFacilitiesDB_data', FILENAME = '{_databaseFilePath}')
                            LOG ON (NAME = 'CampusFacilitiesDB_log', FILENAME = '{ldfPath}');";
                        using (SqlCommand createCmd = new SqlCommand(createSql, masterConn))
                        {
                            createCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        // ---------------------------------------------------------
        // Database Initialization & Seed Data
        // ---------------------------------------------------------
        /// <summary>
        /// Ensures the LocalDB database file and FacilitiesIssues table exist,
        /// and seeds initial sample records if the table is empty.
        /// </summary>
        public bool InitializeDatabase(out string statusMessage)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    string createTableSql = @"
                        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FacilitiesIssues')
                        BEGIN
                            CREATE TABLE FacilitiesIssues (
                                IssueId INT IDENTITY(1,1) PRIMARY KEY,
                                Title NVARCHAR(100) NOT NULL,
                                Location NVARCHAR(60) NOT NULL,
                                Category NVARCHAR(40) NOT NULL,
                                Priority NVARCHAR(20) NOT NULL,
                                Status NVARCHAR(20) NOT NULL,
                                ReportedBy NVARCHAR(60) NOT NULL,
                                ReportDate DATE NOT NULL,
                                EstimatedCost DECIMAL(10,2) NOT NULL DEFAULT 0.00,
                                Description NVARCHAR(500) NULL
                            );
                        END";

                    using (SqlCommand cmd = new SqlCommand(createTableSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Check if table has records; if empty, insert realistic sample campus issues
                    string countSql = "SELECT COUNT(*) FROM FacilitiesIssues;";
                    using (SqlCommand countCmd = new SqlCommand(countSql, conn))
                    {
                        int count = (int)countCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            SeedInitialData(conn);
                        }
                    }
                }

                statusMessage = "Database and table verified successfully.";
                return true;
            }
            catch (Exception ex)
            {
                statusMessage = $"Database connection: {ex.Message}";
                return false;
            }
        }

        private void SeedInitialData(SqlConnection conn)
        {
            string seedSql = @"
                INSERT INTO FacilitiesIssues (Title, Location, Category, Priority, Status, ReportedBy, ReportDate, EstimatedCost, Description)
                VALUES 
                ('Ceiling Projector HDMI Port Damaged', 'Lecture Theatre 301, Block A', 'Electrical', 'High', 'Open', 'Dr. David Chen', '2026-10-14', 380.00, 'Main lecture theatre projector displays green screen artifact. HDMI female socket pin is broken.'),
                ('Water Leaking Under Restroom Sink', 'Level 2 Restroom, Block B', 'Plumbing', 'Critical', 'In Progress', 'Maria Santos', '2026-10-15', 250.00, 'Cold water pipe joint leaking steadily, creating slip hazard on floor tiles.'),
                ('Broken Ergonomic Swivel Chair', 'Faculty Office 214', 'Furniture', 'Low', 'Open', 'Prof. Angela Miller', '2026-10-16', 120.00, 'Hydraulic lift cylinder has failed; chair remains at lowest height.'),
                ('Air Conditioning Unit Blowing Warm Air', 'Computer Lab 105', 'HVAC / Heating', 'High', 'In Progress', 'Sam Patel (IT Support)', '2026-10-17', 450.00, 'Room temperature reached 28C, server rack and 30 student desktop computers at risk of overheating.'),
                ('Flickering Tube Lights Above Whiteboard', 'Tutorial Room 102', 'Electrical', 'Medium', 'Resolved', 'Jessica Taylor', '2026-10-12', 85.00, 'Two 40W fluorescent tubes flickering continuously, causing headache during morning classes.'),
                ('Stairwell Emergency Exit Door Handle Loose', 'Block C Fire Exit Stairwell', 'Safety & Security', 'Critical', 'Closed', 'Michael Wong (Campus Security)', '2026-10-10', 160.00, 'Panic push-bar latch was sticking. Repaired and safety inspection passed.');";

            using (SqlCommand cmd = new SqlCommand(seedSql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // ---------------------------------------------------------
        // CRUD Operations Interacting with FacilityIssue
        // ---------------------------------------------------------

        /// <summary>
        /// Reads all issue records from the database.
        /// </summary>
        public DataTable GetAllIssues()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                string sql = @"
                    SELECT 
                        IssueId AS [Issue ID],
                        Title AS [Title],
                        Location AS [Location],
                        Category AS [Category],
                        Priority AS [Priority],
                        Status AS [Status],
                        ReportedBy AS [Reported By],
                        ReportDate AS [Report Date],
                        EstimatedCost AS [Est. Cost ($)],
                        Description AS [Description]
                    FROM FacilitiesIssues
                    ORDER BY IssueId DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Creates (Inserts) a new facilities issue after validating business rules.
        /// </summary>
        public bool AddIssue(FacilityIssue issue, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (issue == null)
            {
                errorMessage = "Issue entity cannot be null.";
                return false;
            }

            // 1. Enforce business rules & validation logic
            if (!issue.Validate(out List<string> errors))
            {
                errorMessage = string.Join(Environment.NewLine, errors);
                return false;
            }

            // 2. Perform ADO.NET parameterized insert
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    string sql = @"
                        INSERT INTO FacilitiesIssues 
                        (Title, Location, Category, Priority, Status, ReportedBy, ReportDate, EstimatedCost, Description)
                        VALUES 
                        (@Title, @Location, @Category, @Priority, @Status, @ReportedBy, @ReportDate, @EstimatedCost, @Description);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = issue.Title;
                        cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 60).Value = issue.Location;
                        cmd.Parameters.Add("@Category", SqlDbType.NVarChar, 40).Value = issue.Category;
                        cmd.Parameters.Add("@Priority", SqlDbType.NVarChar, 20).Value = issue.Priority;
                        cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = issue.Status;
                        cmd.Parameters.Add("@ReportedBy", SqlDbType.NVarChar, 60).Value = issue.ReportedBy;
                        cmd.Parameters.Add("@ReportDate", SqlDbType.Date).Value = issue.ReportDate.Date;
                        cmd.Parameters.Add("@EstimatedCost", SqlDbType.Decimal).Value = issue.EstimatedCost;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500).Value =
                            string.IsNullOrEmpty(issue.Description) ? (object)DBNull.Value : issue.Description;

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int newId))
                        {
                            issue.IssueId = newId;
                            return true;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Database error adding record: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Updates an existing facilities issue after validating business rules.
        /// </summary>
        public bool UpdateIssue(FacilityIssue issue, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (issue == null)
            {
                errorMessage = "Issue entity cannot be null.";
                return false;
            }

            if (issue.IssueId <= 0)
            {
                errorMessage = "Please select a valid existing issue to update.";
                return false;
            }

            // 1. Enforce business rules & validation logic
            if (!issue.Validate(out List<string> errors))
            {
                errorMessage = string.Join(Environment.NewLine, errors);
                return false;
            }

            // 2. Perform ADO.NET parameterized update
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    string sql = @"
                        UPDATE FacilitiesIssues SET
                            Title = @Title,
                            Location = @Location,
                            Category = @Category,
                            Priority = @Priority,
                            Status = @Status,
                            ReportedBy = @ReportedBy,
                            ReportDate = @ReportDate,
                            EstimatedCost = @EstimatedCost,
                            Description = @Description
                        WHERE IssueId = @IssueId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@IssueId", SqlDbType.Int).Value = issue.IssueId;
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = issue.Title;
                        cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 60).Value = issue.Location;
                        cmd.Parameters.Add("@Category", SqlDbType.NVarChar, 40).Value = issue.Category;
                        cmd.Parameters.Add("@Priority", SqlDbType.NVarChar, 20).Value = issue.Priority;
                        cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = issue.Status;
                        cmd.Parameters.Add("@ReportedBy", SqlDbType.NVarChar, 60).Value = issue.ReportedBy;
                        cmd.Parameters.Add("@ReportDate", SqlDbType.Date).Value = issue.ReportDate.Date;
                        cmd.Parameters.Add("@EstimatedCost", SqlDbType.Decimal).Value = issue.EstimatedCost;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500).Value =
                            string.IsNullOrEmpty(issue.Description) ? (object)DBNull.Value : issue.Description;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = $"No issue record found with ID #{issue.IssueId}.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Database error updating record: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Deletes an issue record by primary key IssueId.
        /// </summary>
        public bool DeleteIssue(int issueId, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (issueId <= 0)
            {
                errorMessage = "Invalid Issue ID for deletion. Please select a record from the grid.";
                return false;
            }

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    string sql = "DELETE FROM FacilitiesIssues WHERE IssueId = @IssueId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@IssueId", SqlDbType.Int).Value = issueId;

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Record with ID #{issueId} could not be found or has already been deleted.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Database error deleting record: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Searches and filters issues by keyword (matching Title, Location, or ReportedBy)
        /// and category/status drop-downs.
        /// </summary>
        public DataTable SearchAndFilter(string searchKeyword, string categoryFilter, string statusFilter)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                List<string> whereClauses = new List<string>();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrWhiteSpace(searchKeyword))
                {
                    whereClauses.Add("(Title LIKE @Keyword OR Location LIKE @Keyword OR ReportedBy LIKE @Keyword OR Description LIKE @Keyword)");
                    cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = $"%{searchKeyword.Trim()}%";
                }

                if (!string.IsNullOrWhiteSpace(categoryFilter) && categoryFilter != "All Categories")
                {
                    whereClauses.Add("Category = @Category");
                    cmd.Parameters.Add("@Category", SqlDbType.NVarChar, 40).Value = categoryFilter;
                }

                if (!string.IsNullOrWhiteSpace(statusFilter) && statusFilter != "All Statuses")
                {
                    whereClauses.Add("Status = @Status");
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = statusFilter;
                }

                string whereString = whereClauses.Count > 0 ? " WHERE " + string.Join(" AND ", whereClauses) : string.Empty;
                string sql = $@"
                    SELECT 
                        IssueId AS [Issue ID],
                        Title AS [Title],
                        Location AS [Location],
                        Category AS [Category],
                        Priority AS [Priority],
                        Status AS [Status],
                        ReportedBy AS [Reported By],
                        ReportDate AS [Report Date],
                        EstimatedCost AS [Est. Cost ($)],
                        Description AS [Description]
                    FROM FacilitiesIssues
                    {whereString}
                    ORDER BY IssueId DESC;";

                cmd.CommandText = sql;
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Returns aggregate metrics for reporting and dashboard status bar.
        /// </summary>
        public (int totalCount, int openCount, decimal totalCost) GetStatistics()
        {
            int total = 0;
            int open = 0;
            decimal cost = 0m;

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    string sql = @"
                        SELECT 
                            COUNT(*) AS TotalCount,
                            ISNULL(SUM(CASE WHEN Status IN ('Open', 'In Progress') THEN 1 ELSE 0 END), 0) AS OpenCount,
                            ISNULL(SUM(EstimatedCost), 0) AS TotalCost
                        FROM FacilitiesIssues;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total = reader.GetInt32(0);
                            open = reader.GetInt32(1);
                            cost = reader.GetDecimal(2);
                        }
                    }
                }
            }
            catch
            {
                // Fallback to zeros if table query fails
            }

            return (total, open, cost);
        }
    }
}
