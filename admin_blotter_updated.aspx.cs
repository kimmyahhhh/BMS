using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace blotter
{
    public partial class blotter : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FilterGrid("All");
            }

            adminblotterGV.Visible = true;
            //historyadminblotterGV.Visible = false;

            //DisplayUserComplainGV();

            DisplayHistoryComplainGV();
            scheduledCount.Text = GetScheduledCount().ToString();
            settledCount.Text = GetSettledCount().ToString();
            activeCount.Text = GetActiveCount().ToString();
            UpdateNotificationCount();
            LoadNotifications();
        }

        private void UpdateNotificationCount()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) FROM notifications WHERE isRead = 0";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    notificationCount.InnerText = count.ToString();
                }
            }
        }

        private void LoadNotifications()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT notif_id, message FROM notifications WHERE isRead = 0";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        NotificationList.DataSource = reader;
                        NotificationList.DataBind();
                    }
                }

                string updateSql = "UPDATE notifications SET isRead = 1 WHERE isRead = 0";
                using (MySqlCommand updateCmd = new MySqlCommand(updateSql, conn))
                {
                    updateCmd.ExecuteNonQuery();
                }
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Add your logout logic here, for example:
            // Session.Abandon();
            Response.Redirect("login.aspx");
        }

        private int GetScheduledCount()
        {
            int scheduledCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM blotter WHERE status = 'Scheduled'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        scheduledCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return scheduledCount;
        }

        private int GetActiveCount()
        {
            int activeCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM blotter WHERE status = 'Active'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        activeCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return activeCount;
        }

        private int GetSettledCount()
        {
            int settledCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM blotter WHERE status = 'Settled'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        settledCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return settledCount;
        }

        /*  private void DisplayUserComplainGV()
           {
               MySqlConnection connection = new MySqlConnection(connectionString);
               try
               {
                   connection.Open();
                   // Define the query to retrieve data (modify as needed)
                   string query = "SELECT blotter_id, res_id, complainant, compliance, suspect, started, status, incident_date, incident_time FROM blotter WHERE status = 'Active' OR status = 'Scheduled'";
                   MySqlCommand cmd = new MySqlCommand(query, connection);

                   using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                   {
                       // Create a DataTable to hold the data
                       DataTable dt = new DataTable();
                       // Fill the DataTable with data from the database
                       adapter.Fill(dt);

                       // Bind the modified DataTable to the GridView
                       adminblotterGV.DataSource = dt;
                       adminblotterGV.DataBind();
                   }
               }
               catch (Exception ex)
               {
                   mbox.Text = "Error: " + ex.Message;
                   ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
               }
               finally
               {
                   connection.Close();
               }
           }*/

        private void DisplayHistoryComplainGV()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                // Define the query to retrieve data (modify as needed)
                string query = "SELECT blotter_id, res_id, complainant, compliance, incident_date, incident_time, suspect, started, ended, status FROM blotter WHERE status = 'Settled'";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    // Create a DataTable to hold the data
                    DataTable dt = new DataTable();
                    // Fill the DataTable with data from the database
                    adapter.Fill(dt);

                    // Bind the modified DataTable to the GridView
                    //historyadminblotterGV.DataSource = dt;
                    //historyadminblotterGV.DataBind();
                }
            }
            catch (Exception ex)
            {
                mbox.Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
            }
            finally
            {
                connection.Close();
            }
        }


        private void FilterGrid(string status, string searchQuery = "")
        {
            ViewState["CurrentStatus"] = status;
            ViewState["CurrentSearchQuery"] = searchQuery;

            string query;

            if (status == "All")
            {
                query = "SELECT blotter_id, res_id, complainant, compliance, " +
                        "NULLIF(incident_date, '0000-00-00') AS incident_date, " +
                        "NULLIF(incident_time, '0000-00-00 00:00:00') AS incident_time, " +
                        "NULLIF(started, '0000-00-00') AS started, " +
                        "NULLIF(ended, '0000-00-00') AS ended, " +
                        "suspect, status " +
                        "FROM blotter";
            }
            else
            {
                query = "SELECT blotter_id, res_id, complainant, compliance, " +
                        "NULLIF(incident_date, '0000-00-00') AS incident_date, " +
                        "NULLIF(incident_time, '0000-00-00 00:00:00') AS incident_time, " +
                        "NULLIF(started, '0000-00-00') AS started, " +
                        "NULLIF(ended, '0000-00-00') AS ended, " +
                        "suspect, status " +
                        "FROM blotter WHERE status = @Status";
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += status == "All" ? " WHERE " : " AND ";
                query += "(complainant LIKE @SearchQuery OR suspect LIKE @SearchQuery)";
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (status != "All")
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                    }

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@SearchQuery", searchQuery + "%");
                    }

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            adminblotterGV.DataSource = dt;
                        }
                        else
                        {
                            adminblotterGV.DataSource = null;
                        }
                        adminblotterGV.DataBind();
                    }
                }
            }
        }
        protected void lnkPending_Click(object sender, EventArgs e)
        {
            adminblotterGV.PageIndex = 0;
            FilterGrid("Scheduled", ViewState["CurrentSearchQuery"] as string ?? "");
        }

        protected void lnkActive_Click(object sender, EventArgs e)
        {
            adminblotterGV.PageIndex = 0;
            FilterGrid("Active", ViewState["CurrentSearchQuery"] as string ?? "");
        }

        protected void lnkEnded_Click(object sender, EventArgs e)
        {
            adminblotterGV.PageIndex = 0;
            FilterGrid("Settled", ViewState["CurrentSearchQuery"] as string ?? "");
        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string currentStatus = ViewState["CurrentStatus"] as string ?? "All";
            string searchQuery = ViewState["CurrentSearchQuery"] as string ?? "";

            adminblotterGV.PageIndex = e.NewPageIndex;

            FilterGrid(currentStatus, searchQuery);
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string currentStatus = ViewState["CurrentStatus"] as string ?? "All";
            string searchQuery = search.Text;

            FilterGrid(currentStatus, searchQuery);
        }

        protected void save_edited_btn_Click(object sender, EventArgs e)
        {
            if (ViewState["blotter_id"] != null)
            {
                int blotterId = (int)ViewState["blotter_id"];
                string complainant = compliant_tb.Text;
                string compliance = compliance_tb.Text;
                string suspect = suspect_tb.Text;
                string status = blotterstatusList.SelectedValue;
                DateTime incidentDate;
                DateTime incidentTime;
                DateTime started;

                bool isDateParsed = DateTime.TryParseExact(started_tb.Text, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out started);
                bool isIncidentDateParsed = DateTime.TryParseExact(date_tb.Text, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out incidentDate);
                bool isIncidentTimeParsed = DateTime.TryParse(time_tb.Text, out incidentTime);

                if (!isDateParsed || !isIncidentDateParsed || !isIncidentTimeParsed)
                {
                    // Handle the case where any of the dates are not in the expected format
                    string script = @"<script type='text/javascript'>
            alert('Please enter valid date and time formats.');
            </script>";
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalidInput", script, false);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (status == "Settled")
                    {
                        DateTime ended = DateTime.Now;
                        query = "UPDATE blotter SET complainant = @complainant, compliance = @compliance, incident_date = @incidentdate, incident_time = @incidenttime, suspect = @suspect, started = @started, status = @status, ended = @ended WHERE blotter_id = @blotter_id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@complainant", complainant);
                        cmd.Parameters.AddWithValue("@compliance", compliance);
                        cmd.Parameters.AddWithValue("@incidentdate", incidentDate);
                        cmd.Parameters.AddWithValue("@incidenttime", incidentTime.TimeOfDay);
                        cmd.Parameters.AddWithValue("@suspect", suspect);
                        cmd.Parameters.AddWithValue("@started", started);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@ended", ended);
                        cmd.Parameters.AddWithValue("@blotter_id", blotterId);

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "UPDATE blotter SET complainant = @complainant, compliance = @compliance, incident_date = @incidentdate, incident_time = @incidenttime, suspect = @suspect, started = @started, status = @status WHERE blotter_id = @blotter_id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@complainant", complainant);
                        cmd.Parameters.AddWithValue("@compliance", compliance);
                        cmd.Parameters.AddWithValue("@incidentdate", incidentDate);
                        cmd.Parameters.AddWithValue("@incidenttime", incidentTime.TimeOfDay);
                        cmd.Parameters.AddWithValue("@suspect", suspect);
                        cmd.Parameters.AddWithValue("@started", started);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@blotter_id", blotterId);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Hide the modal
                ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "document.getElementById('viewdetailsModal').style.display = 'none';", true);


                string currentStatus = ViewState["CurrentStatus"] as string ?? "All";
                string currentSearchQuery = ViewState["CurrentSearchQuery"] as string ?? "";
                FilterGrid(currentStatus, currentSearchQuery);

                DisplayHistoryComplainGV();
                scheduledCount.Text = GetScheduledCount().ToString();
                settledCount.Text = GetSettledCount().ToString();
                activeCount.Text = GetActiveCount().ToString();
            }
            else
            {
                Console.WriteLine("blotter_id not found in ViewState.");
            }
        }


        protected void adminblotterGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int blotterId = Convert.ToInt32(e.CommandArgument);

                // Store the blotter_id in ViewState
                ViewState["blotter_id"] = blotterId;

                // Retrieve data for the selected blotter_id from the database
                string query = "SELECT complainant, compliance, suspect, started, status, incident_date, incident_time FROM blotter WHERE blotter_id = @blotterId";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@blotterId", blotterId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate modal fields with retrieved data
                            compliant_tb.Text = reader["complainant"].ToString();
                            compliance_tb.Text = reader["compliance"].ToString();
                            suspect_tb.Text = reader["suspect"].ToString();
                            started_tb.Text = ((DateTime)reader["started"]).ToString("MM/dd/yyyy");
                            date_tb.Text = ((DateTime)reader["incident_date"]).ToString("MM/dd/yyyy");
                            time_tb.Text = reader["incident_time"].ToString();
                            blotterstatusList.SelectedValue = reader["status"].ToString();
                        }

                    }
                }

                // Show the modal
                string script = @"<script type='text/javascript'>
                 document.getElementById('viewdetailsModal').style.display = 'block'; 
             </script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "openModal", script, false);
            }
        }
        protected string GetEndedText(string status, object ended)
        {
            if (status == "Settled")
            {
                DateTime endedDate;
                if (DateTime.TryParse(ended.ToString(), out endedDate))
                {
                    return endedDate.ToString("d");
                }
                return string.Empty;
            }
            return "Pending";
        }



        protected void reqModalbtn_Click(object sender, EventArgs e)
        {
            // adminblotterGV.Visible = true;
           // historyadminblotterGV.Visible = false;
            adminblotterGV.PageIndex = 0;
             FilterGrid("All", ViewState["CurrentSearchQuery"] as string ?? "");
        }

        protected void hisModalbtn_Click(object sender, EventArgs e)
        {
            //adminblotterGV.Visible = false;
            // historyadminblotterGV.Visible = true;
            FilterGrid("Settled", ViewState["CurrentSearchQuery"] as string ?? "");
        }

        protected void adminblotterGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

      



        protected void historyadminblotterGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void historyadminblotterGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
