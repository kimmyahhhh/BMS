using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace bms
{
    public partial class user_blotter : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usercomplainGV.Visible = true;
                historycomplainGV.Visible = false;
                DisplayUserComplainGV();
                DisplayHistoryComplainGV();
                if (Session["res_userID"] != null)
                {
                    int resuserID = Convert.ToInt32(Session["res_userID"]);
                    LoadResidentData();                  
                }
                else
                {
                    // Redirect to login page if the user is not logged in
                    Response.Redirect("residentlogin.aspx");
                }
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Add your logout logic here, for example:
            Session.Abandon();
            Response.Redirect("residentlogin.aspx");
        }

        protected void complain_btn_Click(object sender, EventArgs e)
        {
            string script = @"<script type='text/javascript'>
                 document.getElementById('complaining').style.display = 'block'; 
             </script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "openModal", script, false);

        }

        protected void reqModalbtn_Click(object sender, EventArgs e)
        {
            usercomplainGV.Visible = true;
            historycomplainGV.Visible = false;
        }

        protected void hisModalbtn_Click(object sender, EventArgs e)
        {
            usercomplainGV.Visible = false;
            historycomplainGV.Visible = true;
        }

        protected void save_edited_btn_Click(object sender, EventArgs e)
        {
            int userId = GetLoggedInUserId();
            string complain = complain_tb.Text;
            string suspect = suspect_tb.Text;
            DateTime incidentDate;
            DateTime incidentTime;
            DateTime started = DateTime.Now;

            bool isDateValid = DateTime.TryParse(date_tb.Text, out incidentDate);
            bool isTimeValid = DateTime.TryParse(time_tb.Text, out incidentTime);

            if (isDateValid && isTimeValid)
            {
                // Combine the date and time into a single DateTime variable if needed
                DateTime incidentDateTime = incidentDate.Date.Add(incidentTime.TimeOfDay);

                if (incidentDateTime > DateTime.Now)
                {
                    string cript = @"<script type='text/javascript'>
                alert('Incident time cannot be in the future.');
                </script>";
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalidInput", cript, false);
                    return;
                }

                string connectionString = "server=localhost;database=bms;uid=root;pwd=";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO blotter (res_id, complainant, compliance, started, suspect, incident_date, incident_time) 
                             VALUES (@ResId, @Complainant, @Compliance, @Started, @Suspect, @IncidentDate, @IncidentTime)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ResId", userId);
                    cmd.Parameters.AddWithValue("@Complainant", complainant_tb.Text);
                    cmd.Parameters.AddWithValue("@Compliance", complain);
                    cmd.Parameters.AddWithValue("@Started", started);
                    cmd.Parameters.AddWithValue("@Suspect", suspect);
                    cmd.Parameters.AddWithValue("@IncidentDate", incidentDateTime.Date);
                    cmd.Parameters.AddWithValue("@IncidentTime", incidentDateTime.TimeOfDay);

                    cmd.ExecuteNonQuery();
                }
                DisplayHistoryComplainGV();
                DisplayUserComplainGV();

                // Optionally, close the modal and clear the fields
                complainant_tb.Text = string.Empty;
                complain_tb.Text = string.Empty;
                suspect_tb.Text = string.Empty;
                date_tb.Text = string.Empty; // Clear incident date field
                time_tb.Text = string.Empty; // Clear incident time field

                string script = @"<script type='text/javascript'>
                document.getElementById('complaining').style.display = 'none'; 
                </script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "closeModal", script, false);
            }
            else
            {
                // Handle invalid date/time input
                string script = @"<script type='text/javascript'>
                alert('Please enter a valid date and time for the incident.');
                </script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "invalidInput", script, false);
            }
        }

        protected void clear_edited_btn_Click(object sender, EventArgs e)
        {
            complain_tb.Text = string.Empty;
            suspect_tb.Text = string.Empty;
        }

        protected void usercomplainGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private void DisplayUserComplainGV()
        {
            if (Session["res_userID"] != null)
            {
                int resuserID = Convert.ToInt32(Session["res_userID"]);
                MySqlConnection connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                    // Define the query to retrieve data (modify as needed)
                    string query = "SELECT blotter_id, res_id, complainant, compliance, suspect, started, status, incident_date, incident_time FROM blotter WHERE status = 'Scheduled' and res_id = @res_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@res_id", resuserID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        // Create a DataTable to hold the data
                        DataTable dt = new DataTable();
                        // Fill the DataTable with data from the database
                        adapter.Fill(dt);


                        // Bind the modified DataTable to the GridView
                        usercomplainGV.DataSource = dt;
                        usercomplainGV.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    mbox.Text = "Error: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                Response.Redirect("residentlogin.aspx");
            }
        }

        private void DisplayHistoryComplainGV()
        {
            if (Session["res_userID"] != null)
            {
                int resuserID = Convert.ToInt32(Session["res_userID"]);
                MySqlConnection connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                    // Define the query to retrieve data (modify as needed)
                    string query = "SELECT blotter_id, res_id, complainant, compliance, suspect, incident_date, incident_time, started, ended, status FROM blotter WHERE status = 'Settled' and res_id = @res_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@res_id", resuserID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        // Create a DataTable to hold the data
                        DataTable dt = new DataTable();
                        // Fill the DataTable with data from the database
                        adapter.Fill(dt);


                        // Bind the modified DataTable to the GridView
                        historycomplainGV.DataSource = dt;
                        historycomplainGV.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    mbox.Text = "Error: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 300);", true);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                Response.Redirect("residentlogin.aspx");
            }
        }

        private void LoadResidentData()
        {
            int userId = GetLoggedInUserId();
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT l_name, f_name, m_name FROM resident WHERE res_id = @UserId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string fullName = $"{reader["f_name"]} {reader["m_name"]} {reader["l_name"]}";
                    complainant_tb.Text = fullName;
                    complainant_tb.Enabled = false;
                }
            }
        }
        private int GetLoggedInUserId()
        {
            return Convert.ToInt32(Session["res_userID"]);
        }

        protected void date_tb_TextChanged(object sender, EventArgs e)
        {
            DateTime dob;
            if (DateTime.TryParse(date_tb.Text, out dob))
            {
                // Check if the entered date of birth is in the future
                if (dob > DateTime.Now)
                {
                    // If the date of birth is in the future, show an error message
                    date_tb.Text = "";
                    mbox.Text = "Incident date cannot be in the future.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                }
                else
                {
                    mbox.Text = "";
                }
            }
            else
            {
                // If the entered date is not valid, clear the age textbox and show an error message
                mbox.Text = "Please enter a valid date of the incident.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
            }
        }
    }
}