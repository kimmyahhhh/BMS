using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace bms
{
    public partial class residentdashboard : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["res_userID"] != null)
                {
                    int resuserID = Convert.ToInt32(Session["res_userID"]);
                    LoadUserData(resuserID);
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

        private void LoadUserData(int resuserID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT f_name FROM resident WHERE res_id = @resUserID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@resUserID", resuserID);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string firstName = reader["f_name"].ToString();
                        resUser_lbl.Text = "Hi, " + firstName;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    resUser_lbl.Text = "Error loading user data.";
                }
            }
        }
    }
}
