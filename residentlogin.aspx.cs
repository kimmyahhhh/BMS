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

    public partial class residentlogin : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void reslog_btn_Click(object sender, EventArgs e)
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

            (int resUserID, bool isChanged) = AuthenticateUser(usernames, passwords);
            if (resUserID != 0)
            {
                if (isChanged)
                {
                    Session["res_userID"] = resUserID;
                    mbox.Text = "Login successful!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                    Response.Redirect("residentdashboard.aspx");
                }
                else
                {
                    Response.Redirect("reset_account.aspx");
                }
            }
            else
            {
                mbox.Text = "Invalid username or password!";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
            }
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

        private (int, bool) AuthenticateUser(string usernames, string passwords)
        {
            int resUserID = 0;
            bool isChanged = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT res_id, isChanged FROM resident WHERE res_username = @username AND res_password = @password";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", usernames);
                cmd.Parameters.AddWithValue("@password", passwords);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resUserID = Convert.ToInt32(reader["res_id"]);
                            isChanged = Convert.ToBoolean(reader["isChanged"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    mbox.Text = "Error: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                }
            }

            return (resUserID, isChanged);
        }
    }
}
