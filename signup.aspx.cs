using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using System.Net;
using System.Net.Mail;

namespace bms
{
    public partial class signup : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            VerifyOTP.Visible = false;
            otpverify.Visible = false;
        }

        protected void back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void su_btn_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";

            string su_username = su_user.Text;
            string su_gmail = su_email.Text;
            string su_password = su_pass.Text;

            if (string.IsNullOrWhiteSpace(su_username) || string.IsNullOrWhiteSpace(su_gmail) || string.IsNullOrWhiteSpace(su_password))
            {
                mbox.Text = "Please fill in all fields.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            // Validate if username contains special characters
            if (!IsValidUsername(su_username))
            {
                mbox.Text = "Username cannot contain special characters.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            // Validate if username is not longer than 15 characters
            if (su_username.Length > 15)
            {
                mbox.Text = "Username must be 15 characters or less.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            // Check for duplication of data
            if (IsDuplicateUsername(su_username, su_gmail))
            {
                mbox.Text = "Username and Email already exist. Please choose a different one.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            // Hash the password using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(su_password);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO signup (su_username, su_email, su_password) VALUES (@su_Username, @su_Email, @su_Password)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@su_Username", su_username);
                command.Parameters.AddWithValue("@su_Password", hashedPassword);
                command.Parameters.AddWithValue("@su_Email", su_gmail);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        mbox.Text = "Registration successful!";
                        Response.Redirect("login.aspx");
                        ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            string sql = "INSERT INTO notifications (message) VALUES (@Message)";
                            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@Message", "A new admin has signed up. Please approve or reject the account.");
                                conn.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        mbox.Text = "Registration failed!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                    }
                }
                catch (Exception ex)
                {
                    mbox.Text = "An error occurred: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
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

        private bool IsDuplicateUsername(string username, string email)
        {
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM signup WHERE su_username = @su_Username AND su_email = @su_Email";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@su_Username", username);
                command.Parameters.AddWithValue("@su_Email", email);

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
        private void SendingOTP(string userEmail, string otp)
        {
            // Configure the SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587; // You may need to change this port based on your email provider
            smtpClient.EnableSsl = true; // Enable SSL if required
            smtpClient.Credentials = new System.Net.NetworkCredential("aujscmamaidjhunvonjave@gmail.com", "yqpgrotdqnsarizs");

            // Prepare the email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("bmsn2@gmail.com");
            mailMessage.To.Add(userEmail);
            mailMessage.Subject = "Email OTP";
            mailMessage.Body = "Your OTP is: " + otp;

            // Send the email
            smtpClient.Send(mailMessage);
        }

        protected void VerifyOTP_Click(object sender, EventArgs e)
        {
            // Get the OTP entered by the user
            string enteredOTP = otp_inp.Text;

            // Get the OTP stored in session or database
            string storedOTP = Session["OTP"] as string;

            if (storedOTP != null) // Check if storedOTP is not null
            {
                // Verify the entered OTP with the stored OTP
                if (enteredOTP == storedOTP)
                {
                    mbox.Text = "OTP verified successfully!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                    // Enable the signup button
                    su_btn.Enabled = true;
                    su_btn.Visible = true;
                }
                else
                {
                    mbox.Text = "Invalid OTP. Please try again.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                }
            }
            else
            {
                mbox.Text = "Session expired. Please request a new OTP.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
            }
        }

        protected void SendOTP_Click(object sender, EventArgs e)
        {
            // Generate a random OTP
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();

            // Get the email address entered by the user
            string userEmail = su_email.Text;

            // Send the OTP to the user's email address
            SendingOTP(userEmail, otp);

            // Store the OTP in session or database
            Session["OTP"] = otp;

            VerifyOTP.Visible = true;
            otpverify.Visible = true;

            // Show any relevant message to the user
            mbox.Text = "OTP has been sent to your email address.";
            ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
        }
    }
}
