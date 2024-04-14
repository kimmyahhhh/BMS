using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace bms
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void su_btn_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";

            string su_username = su_user.Text;
            string su_password = su_pass.Text;
            string su_copassword = su_copass.Text;

            if (string.IsNullOrWhiteSpace(su_username) || string.IsNullOrWhiteSpace(su_password) || string.IsNullOrWhiteSpace(su_copassword))
            {
                mbox.Text = "Please fill in all fields.";
                return;
            }

            if (su_password != su_copassword)
            {
                mbox.Text = "Passwords do not match.";
                su_copass.Text = ""; // Clear the confirmation password field
                return;
            }

            // Validate if username contains special characters
            if (!IsValidUsername(su_username))
            {
                mbox.Text = "Username cannot contain special characters.";
                return;
            }

            // Validate if username is not longer than 15 characters
            if (su_username.Length > 15)
            {
                mbox.Text = "Username must be 15 characters or less.";
                return;
            }

            // Check for duplication of data
            if (IsDuplicateUsername(su_username))
            {
                mbox.Text = "Username already exists. Please choose a different one.";
                return;
            }


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO signup (su_username, su_password) VALUES (@su_Username, @su_Password)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@su_Username", su_username);
                command.Parameters.AddWithValue("@su_Password", su_password);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        mbox.Text = "Registration successful!";
                    }
                    else
                    {
                        mbox.Text = "Registration failed!";
                    }
                }
                catch (Exception ex)
                {
                    mbox.Text = "An error occurred: " + ex.Message;
                }
            }
        }
        private bool IsValidUsername(string username)
        {
            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false; // Username contains a special character
                }
            }
            return true; // Username is valid
        }

        private bool IsDuplicateUsername(string username)
        {
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM signup WHERE su_username = @su_Username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@su_Username", username);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Returns true if count is greater than 0, indicating duplication
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it, display an error message)
                    Console.WriteLine("An error occurred while checking for duplicate username: " + ex.Message);
                    return false; // Return false in case of an error
                }
            }
        }
    }
}