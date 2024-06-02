using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace bms
{
    public partial class res_record : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        public int residentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.Visible = true;
                GridView2.Visible = false;
                DisplayResInfoGV();
                DisplayArchiveInfoGV();
                femaleCount.Text = GetFemaleCount().ToString();
                maleCount.Text = GetMaleCount().ToString();
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

        private int GetFemaleCount()
        {
            int femaleCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM resident WHERE sex = 'F' AND archive = 0";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        femaleCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return femaleCount;
        }

        private int GetMaleCount()
        {
            int maleCount = 0;
            // Connect to your MySQL database
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Query to count the number of males in your database
                    string query = "SELECT COUNT(*) FROM resident WHERE sex = 'M' AND archive = 0";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and get the count
                        maleCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return maleCount;
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            string searchQuery = search.Text.Trim();
            SearchResidents(searchQuery);
        }

        protected void search_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = search.Text.Trim();
            SearchResidents(searchQuery); ;
        }

        private void DisplayResInfoGV()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                // Define the query to retrieve data (modify as needed)
                string query = "SELECT r.res_id, r.l_name, r.f_name, r.m_name, r.dob, r.pob, r.age, r.sex, r.purok, r.contact_no, o.emp_id, o.job FROM resident AS r JOIN occupation AS o ON r.res_id = o.emp_id WHERE r.archive = 0";
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
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                mbox.Text = "Error: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }

        private void DisplayArchiveInfoGV()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                // Define the query to retrieve data (modify as needed)
                string query = "SELECT r.res_id, r.l_name, r.f_name, r.m_name, r.dob, r.pob, r.age, r.sex, r.purok, r.contact_no, o.emp_id, o.job FROM resident AS r JOIN occupation AS o ON r.res_id = o.emp_id WHERE r.archive = 1";
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
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
            }
            catch (Exception ex)
            {
                mbox.Text = "Error: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }

        private void SearchResidents(string searchQuery)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT r.res_id, r.l_name, r.f_name, r.m_name, r.dob, r.pob, r.age, r.sex, r.purok, r.contact_no, o.emp_id, o.job FROM resident AS r JOIN occupation AS o ON r.res_id = o.emp_id WHERE r.archive = 0 AND l_name LIKE @search OR f_name LIKE @search OR m_name LIKE @search OR purok LIKE @search";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void addres_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("add_res_records.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            DisplayResInfoGV();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            DisplayArchiveInfoGV();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
             if (e.CommandName == "ArchiveRow")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
        
                    // Retrieve res_id and emp_id from DataKeys
                    string resId = GridView1.DataKeys[index].Values["res_id"].ToString();
                    string empId = GridView1.DataKeys[index].Values["emp_id"].ToString();

                    string query = "UPDATE resident SET archive = 1 WHERE res_id = @reseId";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@reseId", resId);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        DisplayResInfoGV();
                        DisplayArchiveInfoGV();
                        femaleCount.Text = GetFemaleCount().ToString();
                        maleCount.Text = GetMaleCount().ToString();
                        mbox.Text = "Archive Successfully!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                    }
            }               
        }

        protected void reqModalbtn_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            GridView2.Visible = false;
        }

        protected void hisModalbtn_Click(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            GridView2.Visible = true;

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the index of the current row
                int rowIndex = e.Row.RowIndex;

                // Find the Confirm button in the row
                Button archiveButton = (Button)e.Row.FindControl("ArchiveButton");

                // Set the CommandArgument to the row index
                archiveButton.CommandArgument = rowIndex.ToString();
            }
        }
    }
}
