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
    public partial class reset_account : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            modalState.Value = "visible";
            VerifyOTP.Visible = false;
            otpverify.Visible = false;
        }

        protected void su_btn_Click(object sender, EventArgs e)
        {
            if (Session["res_userID"] == null)
            {
                mbox.Text = "Session expired. Please log in again.";
                mbox.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                return;
            }

            int resUserID = (int)Session["res_userID"];
            string newUsername = res_user.Text.Trim();
            string newPassword = res_pass.Text.Trim();

            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                mbox.Text = "Please fill in all fields.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                return;
            }

            bool updateSuccess = UpdateResidentDetails(resUserID, newUsername, newPassword);

            if (updateSuccess)
            {
                Session["res_username"] = newUsername;
                Session["res_password"] = newPassword;

                mbox.Text = "Account updated successfully!";
                mbox.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                modalState.Value = "hidden";
                Response.Redirect("residentlogin.aspx");
            }
            else
            {
                mbox.Text = "Failed to update account!";
                mbox.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                modalState.Value = "hidden";
            }
        }

        private bool UpdateResidentDetails(int resUserID, string newUsername, string newPassword)
        {
            
            string updateQuery = "UPDATE resident SET res_username = @username, res_password = @password, isChanged = 1 WHERE res_id = @resUserID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(updateQuery, connection);
                cmd.Parameters.AddWithValue("@username", newUsername);
                cmd.Parameters.AddWithValue("@password", newPassword);
                cmd.Parameters.AddWithValue("@resUserID", resUserID);

                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    mbox.Text = "Error: " + ex.Message;
                    return false;
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
            mailMessage.Subject = "OTP for Changing of Account Details";
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                    // Enable the signup button
                    su_btn.Enabled = true;
                    su_btn.Visible = true;
                    modalState.Value = "hidden";
                }
                else
                {
                    mbox.Text = "Invalid OTP. Please try again.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                    modalState.Value = "hidden";
                }
            }
            else
            {
                mbox.Text = "Session expired. Please request a new OTP.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                modalState.Value = "hidden";
            }
        }

        protected void SendOTP_Click(object sender, EventArgs e)
        {
            // Generate a random OTP
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();

            // Get the email address entered by the user
            string userEmail = res_email.Text;

            // Send the OTP to the user's email address
            SendingOTP(userEmail, otp);

            // Store the OTP in session or database
            Session["OTP"] = otp;

            VerifyOTP.Visible = true;
            otpverify.Visible = true;

            // Show any relevant message to the user
            mbox.Text = "OTP has been sent to your email address.";
            ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
            modalState.Value = "hidden";
        }

        protected void save_edited_btn_Click(object sender, EventArgs e)
        {
            string resusername = username_tb.Text;
            string respassword = password_tb.Text;

            if (string.IsNullOrWhiteSpace(resusername) || string.IsNullOrWhiteSpace(respassword))
            {
                mbox.Text = "Please fill in all fields.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                return;
            }

            if (resusername.Length > 15)
            {
                mbox.Text = "Username must be 15 characters or less.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                return;
            }

            int resUserID = AuthenticateUser(resusername, respassword);
            if (resUserID != 0)
            {
                Session["res_userID"] = resUserID;
                Session["res_username"] = resusername;
                Session["res_password"] = respassword;

                mbox.Text = "User authenticated successfully!";
                mbox.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                modalState.Value = "hidden";
                ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", "closeModal();", true);
            }
            else
            {
                mbox.Text = "Invalid username or password!";
                mbox.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                modalState.Value = "visible";
            }
            
        }

        private int AuthenticateUser(string resusername, string respassword)
        {
            int resUserID = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT res_id FROM resident WHERE res_username = @username AND res_password = @password";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", resusername);
                cmd.Parameters.AddWithValue("@password", respassword);

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        resUserID = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    mbox.Text = "Error: " + ex.Message;
                }
            }

            return resUserID;
        }

        protected void clear_edited_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}