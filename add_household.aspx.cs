using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace bms
{
    public partial class add_household : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayHouseholdGV();
            householdCount.Text = GetHouseholdCount().ToString();
            p1Count.Text = GetP1Count().ToString();
            p2Count.Text = GetP2Count().ToString();
            p3Count.Text = GetP3Count().ToString();
            p4Count.Text = GetP4Count().ToString();
            p5Count.Text = GetP5Count().ToString();
            p6Count.Text = GetP6Count().ToString();
            p7Count.Text = GetP7Count().ToString();
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

        private int GetHouseholdCount()
        {
            int householdCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        householdCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return householdCount;
        }

        private int GetP2Count()
        {
            int p2Count = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household WHERE purok = 2";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        p2Count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return p2Count;
        }

        private int GetP1Count()
        {
            int p1Count = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household WHERE purok = 1";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        p1Count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return p1Count;
        }

        private int GetP3Count()
        {
            int p3Count = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household WHERE purok = 3";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        p3Count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return p3Count;
        }

        private int GetP4Count()
        {
            int p4Count = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household WHERE purok = 4";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        p4Count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return p4Count;
        }

        private int GetP5Count()
        {
            int p5Count = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household WHERE purok = 5";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        p5Count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return p5Count;
        }

        private int GetP6Count()
        {
            int p6Count = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household WHERE purok = 6";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        p6Count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return p6Count;
        }

        private int GetP7Count()
        {
            int p7Count = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM household WHERE purok = 7";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        p7Count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return p7Count;
        }

        protected void add_HH_btn_Click(object sender, EventArgs e)
        {
            string script = @"<script type='text/javascript'>
                 document.getElementById('householdModal').style.display = 'block'; 
             </script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "openModal", script, false);
        }

        protected void MembersButton_Click(object sender, EventArgs e)
        {
            // Get the LinkButton that triggered the event
            LinkButton button = (LinkButton)sender;

            // Get the household ID from the CommandArgument
            int householdId = Convert.ToInt32(button.CommandArgument);

            // Retrieve the household name for the selected household
            string householdName = GetHouseholdNameById(householdId);

            // Set the household name in the modal header
            householdNameLabel.Text = householdName;

            // Retrieve the members data for the selected household
            DataTable membersData = GetMembersByHouseholdId(householdId);

            // Bind the members data to the membersGridView
            membersGridView.DataSource = membersData;
            membersGridView.DataBind();

            // Show the modal using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "showModal();", true);
        }

        protected void household_tb_TextChanged(object sender, EventArgs e)
        {

        }

        protected void houseID_tb_TextChanged(object sender, EventArgs e)
        {

        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            string searchQuery = search.Text.Trim();
            SearchHousehold(searchQuery);
        }

        protected void search_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = search.Text.Trim();
            SearchHousehold(searchQuery); ;
        }

        private void SearchHousehold(string searchQuery)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT household_id, house_id, household_name, purok, Members, lat, lng FROM household WHERE household_id LIKE @search OR house_id LIKE @search OR household_name LIKE @search OR purok LIKE @search OR Members LIKE @search";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                householdGV.DataSource = dt;
                householdGV.DataBind();
            }
        }

        protected void save_edited_btn_Click(object sender, EventArgs e)
        {
            string householdName = household_tb.Text;
            string houseID = houseID_tb.Text;
            string purok = PurokList.SelectedValue;
            string lat = locationLat.Value;
            string lng = locationLng.Value;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); // Open the connection

                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {

                        // Check if the head of household already exists in the database
                        using (MySqlCommand checkHouseholdCmd = new MySqlCommand("SELECT household_id FROM household WHERE household_name = @householdName", connection, transaction))
                        {
                            checkHouseholdCmd.Parameters.AddWithValue("@householdName", householdName);
                            object result = checkHouseholdCmd.ExecuteScalar();

                            if (result != null)
                            {
                                // If the head of household exists, retrieve its household ID
                                mbox.Text = "Household already exist";
                                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                            }
                            else
                            {
                                // If the head of household doesn't exist, insert it and retrieve the newly generated household ID
                                using (MySqlCommand insertHouseholdCmd = new MySqlCommand("INSERT INTO household (household_name, house_ID, purok, lat, lng) VALUES (@householdName, @houseID, @purok, @lat, @lng);", connection, transaction))
                                {
                                    insertHouseholdCmd.Parameters.AddWithValue("@householdName", householdName);
                                    insertHouseholdCmd.Parameters.AddWithValue("@houseID", Convert.ToInt32(houseID_tb.Text));
                                    insertHouseholdCmd.Parameters.AddWithValue("@purok", PurokList.SelectedValue);
                                    insertHouseholdCmd.Parameters.AddWithValue("@lat", lat);
                                    insertHouseholdCmd.Parameters.AddWithValue("@lng", lng);
                                    insertHouseholdCmd.ExecuteNonQuery();
                                }
                                mbox.Text = "Household added sucessfully!";
                                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                            }
                        }
                        transaction.Commit(); // Commit the transaction
                        DisplayHouseholdGV();
                        GetP1Count();
                        GetP2Count();
                        GetP3Count();
                        GetP4Count();
                        GetP5Count();
                        GetP6Count();
                        GetP7Count();
                    }
                }
            }
            catch (Exception ex)
            {
                mbox.Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
            }
        }

        private void DisplayHouseholdGV()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                // Define the query to retrieve data (modify as needed)
                string query = "SELECT household_id, house_id, household_name, purok, Members, lat, lng FROM household";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    // Create a DataTable to hold the data
                    DataTable dt = new DataTable();
                    // Fill the DataTable with data from the database
                    adapter.Fill(dt);
                   
                    // Bind the modified DataTable to the GridView
                    householdGV.DataSource = dt;
                    householdGV.DataBind();
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

        protected void clear_edited_btn_Click(object sender, EventArgs e)
        {
            household_tb.Text = "";
            houseID_tb.Text = "";
            PurokList.SelectedIndex = 0;
            locationLat.Value = "";
            locationLng.Value = "";
        }

        private DataTable GetMembersByHouseholdId(int householdId)
        {
            DataTable dt = new DataTable();
            string connectionString = "server=localhost;database=bms;uid=root;pwd="; // Replace with your connection string
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand("SELECT l_name, f_name, m_name, contact_no FROM resident WHERE household_id = @householdId", connection))
                {
                    command.Parameters.AddWithValue("@householdId", householdId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dt);

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
                    }
                }
            }
            return dt;
        }

        private string GetHouseholdNameById(int householdId)
        {
            string householdName = string.Empty;
            string connectionString = "server=localhost;database=bms;uid=root;pwd="; // Replace with your connection string

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT household_name FROM household WHERE household_id = @householdId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@householdId", householdId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        householdName = result.ToString();
                    }
                }
            }
            return householdName;
        }

        protected string GetMembersCount(object householdId)
        {
            int count = 0;

            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Members FROM household WHERE household_id = @householdId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@householdId", householdId);

                    connection.Open();

                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count.ToString();
        }

        protected void viewLocation_btn_Click(object sender, EventArgs e)
        {
            // Retrieve the household ID from the clicked button
            string householdId = ((LinkButton)sender).CommandArgument;

            // Query to fetch latitude and longitude from the database for the selected household
            string query = "SELECT lat, lng FROM household WHERE household_id = @householdId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@householdId", householdId);
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve latitude and longitude values
                            string latitude = reader["lat"].ToString();
                            string longitude = reader["lng"].ToString();

                            // Call JavaScript function to display location on Google Map
                            ScriptManager.RegisterStartupScript(this, GetType(), "ViewLocationScript", $"viewLocation('{latitude}', '{longitude}');", true);
                        }
                        else
                        {
                            // Handle case when household location is not found
                            ScriptManager.RegisterStartupScript(this, GetType(), "LocationNotFoundScript", "alert('Location not found for the selected household.');", true);
                        }
                    }
                }
            }
        }
       
        protected void householdGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }
    }
}