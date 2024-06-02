using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;

namespace bms
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        protected void username_TextChanged(object sender, EventArgs e)
        {

        }

        protected void password_TextChanged(object sender, EventArgs e)
        {

        }

        protected void back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void log_btn_Click(object sender, EventArgs e)
        {
            // Get the reCAPTCHA response from the client
            string captchaResponse = Request.Form["g-recaptcha-response"];

            // Verify reCAPTCHA
            bool isCaptchaValid = VerifyCaptcha(captchaResponse);

            if (!isCaptchaValid)
            {
                // CAPTCHA verification failed, display error message
                mbox.Text = "Please complete the CAPTCHA verification.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            string usernames = username.Text;
            string passwords = password.Text;

            if (string.IsNullOrWhiteSpace(usernames) || string.IsNullOrWhiteSpace(passwords))
            {
                mbox.Text = "Please fill in all fields.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            if (usernames.Length > 15)
            {
                mbox.Text = "Username must be 15 characters or less.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT su_password FROM signup WHERE su_username = @username AND isApproved = 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", usernames);
                        connection.Open();

                        string storedPasswordHash = cmd.ExecuteScalar() as string;

                        // Verify the input password against the stored hashed password
                        if (storedPasswordHash != null && BCrypt.Net.BCrypt.Verify(passwords, storedPasswordHash))
                        {
                            // Set the session variable
                            Session["username"] = usernames;
                            Response.Redirect("dashboard.aspx");
                        }
                        else
                        {
                            mbox.Text = "Invalid username or password! / Not Registered Admin Account";
                            ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mbox.Text = "An error occurred: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                // Handle other exceptions as needed
            }
        }

        /*private bool IsValidLogin(string connectionString, string usernames, string passwords)
        {
            string query = "SELECT su_password FROM signup WHERE su_username = @username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", usernames);

                    connection.Open();
                    string storedPasswordHash = command.ExecuteScalar() as string;

                    // Verify the input password against the stored hashed password
                    return BCrypt.Net.BCrypt.Verify(passwords, storedPasswordHash);
                }
            }
        }*/

        protected void su_link_Click(object sender, EventArgs e)
        {

        }

        protected void gsignin_Click(object sender, EventArgs e)
        {
            // Get the reCAPTCHA response from the client
            string captchaResponse = Request.Form["g-recaptcha-response"];

            // Verify reCAPTCHA
            bool isCaptchaValid = VerifyCaptcha(captchaResponse);

            if (!isCaptchaValid)
            {
                // CAPTCHA verification failed, display error message
                mbox.Text = "Please complete the CAPTCHA verification.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            Response.Redirect(GetGoogleAuthUrl());
        }

        private bool VerifyCaptcha(string response)
        {
            try
            {
                // Create a WebClient to send the verification request to Google
                using (WebClient client = new WebClient())
                {
                    // Build the verification request URL
                    string secretKey = "6Ld04MQpAAAAAMNe_CxfH0aSNzuAwztCn93QxaZ5"; // Replace with your reCAPTCHA Secret Key
                    string verificationUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={response}";

                    // Send the verification request to Google
                    string result = client.DownloadString(verificationUrl);

                    // Parse the JSON response from Google
                    JObject jsonResult = JObject.Parse(result);

                    // Check if the verification was successful
                    return (bool)jsonResult["success"];
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the verification process
                // For example, log the exception
                Console.WriteLine("An error occurred while verifying reCAPTCHA: " + ex.Message);
                return false; // Verification failed
            }
        }
    

        private string GetGoogleAuthUrl()
        {
            string clientId = "875169705399-t6lvdbcknjru46ljitul75f89hk9aqq3.apps.googleusercontent.com";
            string redirectUri = "http://localhost:64219/oauth2callback";
            string scope = "email profile openid";

            string url = $"https://accounts.google.com/o/oauth2/auth?" +
                         $"client_id={clientId}&" +
                         $"redirect_uri={redirectUri}&" +
                         $"scope={scope}&" +
                         $"response_type=code";

            return url;
        }
    }
}
