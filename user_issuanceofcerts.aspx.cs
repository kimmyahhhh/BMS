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
    public partial class user_issuanceofcerts : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Session["res_userID"] != null)
                {
                    int resuserID = Convert.ToInt32(Session["res_userID"]);
                    // Set the initial visibility of the modals
                    RequestGV.Visible = true;
                    HistoryGV.Visible = false;
                    DisplayRequestGV();
                    DisplayHistoryGV();

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

        private void DisplayRequestGV()
        {
            if (Session["res_userID"] != null)
            {
                int resuserID = Convert.ToInt32(Session["res_userID"]);
                MySqlConnection connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                    // Define the query to retrieve data (modify as needed)
                    string query = "SELECT cert_id, res_id, purpose, cert_status, certificate, date_issued  FROM certification WHERE cert_status = 'Pending' and res_id = @res_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@res_id", resuserID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        // Create a DataTable to hold the data
                        DataTable dt = new DataTable();
                        // Fill the DataTable with data from the database
                        adapter.Fill(dt);


                        // Bind the modified DataTable to the GridView
                        RequestGV.DataSource = dt;
                        RequestGV.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    mbox1.Text = "Error: " + ex.Message;
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

        private void DisplayHistoryGV()
        {
            if (Session["res_userID"] != null)
            {
                int resuserID = Convert.ToInt32(Session["res_userID"]);
                MySqlConnection connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                    // Define the query to retrieve data (modify as needed)
                    string query = "SELECT cert_id, res_id, purpose, cert_status, certificate, date_issued  FROM certification WHERE cert_status = 'Confirmed' and res_id = @res_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@res_id", resuserID);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        // Create a DataTable to hold the data
                        DataTable dt = new DataTable();
                        // Fill the DataTable with data from the database
                        adapter.Fill(dt);


                        // Bind the modified DataTable to the GridView
                        HistoryGV.DataSource = dt;
                        HistoryGV.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    mbox1.Text = "Error: " + ex.Message;
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

        protected void RequestGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void RequestGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pull")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = RequestGV.Rows[index];
                string certId = RequestGV.DataKeys[index].Value.ToString();

                string query = "UPDATE certification SET cert_status = 'Cancelled' WHERE cert_id = @CertId";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CertId", certId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                DisplayRequestGV();
                DisplayHistoryGV();
                mbox1.Text = "Request Cancelled!";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox1', 1000);", true);
            }
        }

        protected void HistoryGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void HistoryGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                string certificateType = e.CommandArgument.ToString();

                if (certificateType == "Certificate of Residency")
                {
                    Response.Redirect("CertOfRes.aspx?type=Residency");
                }

                if (certificateType == "Certificate of Indigency")
                {
                    Response.Redirect("CertOfIndi.aspx?type=Indigency");
                }

                if (certificateType == "Barangay Clearance")
                {
                    Response.Redirect("BarangayClearance.aspx?type=Clearance");
                }
                // Handle other certificate types as needed
            }
            else if (e.CommandName == "Archive")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = HistoryGV.Rows[index];
                string certId = HistoryGV.DataKeys[index].Value.ToString();

                string query = "UPDATE certification SET cert_status = 'Archived' WHERE cert_id = @CertId";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CertId", certId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                DisplayRequestGV();
                DisplayHistoryGV();
                mbox1.Text = "Certification Archived Successfully!";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox1', 1000);", true);
            }
        }

        protected void reqModalbtn_Click(object sender, EventArgs e)
        {
            RequestGV.Visible = true;
            HistoryGV.Visible = false;
        }

        protected void hisModalbtn_Click(object sender, EventArgs e)
        {
            RequestGV.Visible = false;
            HistoryGV.Visible = true;
        }

        protected void user_res_edit_btn_Click(object sender, EventArgs e)
        {
            string script = @"<script type='text/javascript'>
                 document.getElementById('addcertModal').style.display = 'block'; 
             </script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "openModal", script, false);
        }
        protected void save_edited_btn_Click(object sender, EventArgs e)
        {
            string purpose = purpose_tb.Text;
            string certificate = certList.SelectedValue;
            DateTime issuedDate = DateTime.Now;

            if (Session["res_userID"] != null)
            {
                int res_userID = Convert.ToInt32(Session["res_userID"]);

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        // Open the connection
                        connection.Open();

                        string insertQuery = "INSERT INTO certification (res_id, purpose, certificate, date_issued) VALUES (@res_id, @purpose, @certificate, @issued_date)";
                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@res_id", res_userID);
                            cmd.Parameters.AddWithValue("@purpose", purpose);
                            cmd.Parameters.AddWithValue("@certificate", certificate);
                            cmd.Parameters.AddWithValue("@issued_date", issuedDate);

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }

                        // Optionally close the connection (it will also be closed automatically by the 'using' statement)
                        connection.Close();
                    }
                    mbox1.Text = "Request Sent!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox1', 1000);", true);
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log the error, show a message to the user, etc.)
                    // For example, you might log the error and show a friendly message:
                    Console.WriteLine("An error occurred: " + ex.Message);
                    // You could also use a label on the page to display the error message:
                    // errorMessageLabel.Text = "An error occurred while saving the data. Please try again later.";
                }
                DisplayRequestGV();
                DisplayHistoryGV();
            }
            else
            {
                // Redirect to the login page if the user is not authenticated
                Response.Redirect("residentlogin.aspx");
            }
        }

   

        protected void clear_edited_btn_Click(object sender, EventArgs e)
        {
            purpose_tb.Text = string.Empty;
            certList.SelectedIndex = 0;
        }

        protected void HistoryGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the index of the current row
                int rowIndex = e.Row.RowIndex;

                // Find the Confirm button in the row
                Button archiveButton = (Button)e.Row.FindControl("archive_btn");

                // Set the CommandArgument to the row index
                archiveButton.CommandArgument = rowIndex.ToString();
            }
        }

        protected void RequestGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the index of the current row
                int rowIndex = e.Row.RowIndex;

                // Find the Confirm button in the row
                Button pullButton = (Button)e.Row.FindControl("pull_btn");

                // Set the CommandArgument to the row index
                pullButton.CommandArgument = rowIndex.ToString();
            }
        }
    }
}