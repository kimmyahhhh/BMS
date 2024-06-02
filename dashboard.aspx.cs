using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms
{
    public partial class dashboard : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                totalresCount.Text = GetTotalResCount().ToString();
                totalblotterCount.Text = GetTotalBlotterCount().ToString();
                totalhouseholdCount.Text = GetTotalHouseholdCount().ToString();
                totalcertificateCount.Text = GetTotalCertificateCount().ToString();
                UpdateNotificationCount();
                LoadNotifications();
            }
            
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Add your logout logic here, for example:
            // Session.Abandon();
            Response.Redirect("login.aspx");
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

        private int GetTotalResCount()
        {
            int totalresCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM resident";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        totalresCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return totalresCount;
        }

        private int GetTotalCertificateCount()
        {
            int totalcertificateCount = 0;
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
                        totalcertificateCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return totalcertificateCount;
        }

        private int GetTotalHouseholdCount()
        {
            int totalhouseholdCount = 0;
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
                        totalhouseholdCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return totalhouseholdCount;
        }

        private int GetTotalBlotterCount()
        {
            int totalblotterCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM blotter";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        totalblotterCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return totalblotterCount;
        }
    }
}
