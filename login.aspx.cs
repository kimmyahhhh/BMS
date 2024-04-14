using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace bms
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void username_TextChanged(object sender, EventArgs e)
        {

        }

        protected void password_TextChanged(object sender, EventArgs e)
        {

        }

        protected void log_btn_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            string usernames = username.Text;
            string passwords = password.Text;

            if (string.IsNullOrWhiteSpace(usernames) || string.IsNullOrWhiteSpace(passwords))
            {
                mbox.Text = "Please fill in all fields.";
                return;
            }

            if (usernames.Length > 15)
            {
                mbox.Text = "Username must be 15 characters or less.";
                return;
            }

            try
            {
                if (IsValidLogin(connectionString, usernames, passwords))
                {
                    mbox.Text = "Login successful!";
                    Response.Redirect("dashboard.aspx");
                }
                else
                {
                    mbox.Text = "Invalid username or password!";
                }
            }
            catch (Exception ex)
            {
                mbox.Text = "An error occurred: " + ex.Message;
                // Handle other exceptions as needed
            }
        }

        private bool IsValidLogin(string connectionString, string usernames, string passwords)
        {
            string query = "SELECT COUNT(*) FROM signup WHERE su_username = @username AND su_password = @password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", usernames);
                    command.Parameters.AddWithValue("@password", passwords);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        protected void su_link_Click(object sender, EventArgs e)
        {

        }

        /*protected void login_but_Click(object sender, EventArgs e)
        {
           
        }*/
    }
}