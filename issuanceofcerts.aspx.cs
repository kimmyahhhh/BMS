using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace bms
{
    public partial class issuanceofcerts : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the initial visibility of the modals
                RequestGV.Visible = true;
                HistoryGV.Visible = false;
                DisplayRequestGV();
                DisplayHistoryGV();
                certificateCount.Text = GetIssuedCertCount().ToString();
                pendingCount.Text = GetPendingCount().ToString();
                verifiedCount.Text = GetVerifiedCount().ToString();
                UpdateNotificationCount();
                LoadNotifications();
            }
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

        private int GetIssuedCertCount()
        {
            int certificateCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM certification WHERE cert_status != 'Cancelled'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        certificateCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return certificateCount;
        }

        private int GetPendingCount()
        {
            int pendingCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM certification WHERE cert_status = 'Pending'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        pendingCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return pendingCount;
        }

        private int GetVerifiedCount()
        {
            int verifiedCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM certification WHERE cert_status = 'Confirmed'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        verifiedCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return verifiedCount;
        }

        private void DisplayRequestGV()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                // Define the query to retrieve data (modify as needed)
                string query = "SELECT r.res_id, c.cert_id, r.l_name, r.f_name, r.m_name, r.purok, r.contact_no, c.certificate, c.purpose FROM resident AS r JOIN certification AS c ON r.res_id = c.res_id WHERE cert_status = 'Pending'";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    // Create a DataTable to hold the data
                    DataTable dt = new DataTable();
                    // Fill the DataTable with data from the database
                    adapter.Fill(dt);

                    // Mask certain information (e.g., contact number)
                    foreach (DataRow row in dt.Rows)
                    {
                        // Mask contact number (if it exists)
                        if (!string.IsNullOrEmpty(row["contact_no"]?.ToString()))
                        {
                            string contactNo = row["contact_no"].ToString();
                            // Mask all characters except the last four
                            row["contact_no"] = contactNo.Substring(0, 4) + new string('*', contactNo.Length - 4);
                        }
                    }

                    // Bind the modified DataTable to the GridView
                    RequestGV.DataSource = dt;
                    RequestGV.DataBind();
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

        private void DisplayHistoryGV()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                // Define the query to retrieve data (modify as needed)
                string query = "SELECT r.res_id, c.cert_id, r.l_name, r.f_name, r.m_name, r.purok, r.contact_no, c.certificate, c.purpose FROM resident AS r JOIN certification AS c ON r.res_id = c.res_id WHERE cert_status = 'Confirmed'";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    // Create a DataTable to hold the data
                    DataTable dt = new DataTable();
                    // Fill the DataTable with data from the database
                    adapter.Fill(dt);

                    // Mask certain information (e.g., contact number)
                    foreach (DataRow row in dt.Rows)
                    {
                        // Mask contact number (if it exists)
                        if (!string.IsNullOrEmpty(row["contact_no"]?.ToString()))
                        {
                            string contactNo = row["contact_no"].ToString();
                            // Mask all characters except the last four
                            row["contact_no"] = contactNo.Substring(0, 4) + new string('*', contactNo.Length - 4);
                        }
                    }

                    // Bind the modified DataTable to the GridView
                    HistoryGV.DataSource = dt;
                    HistoryGV.DataBind();
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

        protected void RequestGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void RequestGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = RequestGV.Rows[index];

                string resId = RequestGV.DataKeys[index].Values["res_id"].ToString();
                string certId = RequestGV.DataKeys[index].Values["cert_id"].ToString();

                string connectionString = "server = localhost; database = bms; uid = root; pwd = ";
                string query = "UPDATE certification SET cert_status = 'Confirmed' WHERE cert_id = @CertId";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CertId", certId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    DisplayRequestGV();
                    DisplayHistoryGV();
                    GetIssuedCertCount();
                    GetPendingCount();
                    GetVerifiedCount();
                    mbox.Text = "Request Confirmed!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                }
            }
        }

        protected void HistoryGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void HistoryGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void reqModalbtn_Click(object sender, EventArgs e)
        {
            RequestGV.Visible = true;
            HistoryGV.Visible = false;
        }

        protected void hisModalbtn_Click(object sender, EventArgs e)
        {
            RequestGV.Visible = false;
            HistoryGV.Visible = true;
        }

        protected void RequestGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the index of the current row
                int rowIndex = e.Row.RowIndex;

                // Find the Confirm button in the row
                Button confirmButton = (Button)e.Row.FindControl("confirm_btn");

                // Set the CommandArgument to the row index
                confirmButton.CommandArgument = rowIndex.ToString();
                
            }
        } 
    }
}