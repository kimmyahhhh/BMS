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
    public partial class CertOfRes : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string certificateType = Request.QueryString["type"];
                int resuserId = Convert.ToInt32(Session["res_userID"]);

                if (certificateType == "Residency")
                {
                    LoadCertificateData(resuserId);
                    SetBirthdayLabel();
                }
            }
        }

        private void LoadCertificateData(int resuserId)
        {
            string query = "SELECT * FROM resident WHERE res_id = @UserId"; // Adjust the query as needed

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", resuserId);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblName.Text = $"{reader["l_name"]} {reader["f_name"]}, {reader["m_name"]}";
                    lblAge.Text = reader["age"].ToString();
                    lblZone.Text = reader["purok"].ToString();
                    lblBirthday.Text = reader["dob"].ToString();
                    lblAge2.Text = reader["age"].ToString();
                    lblDay.Text = DateTime.Now.Day.ToString();
                    lblMonth.Text = DateTime.Now.ToString("MMMM");
                    lblYear2.Text = DateTime.Now.Year.ToString();
                }
                reader.Close();
            }
        }

        private void SetBirthdayLabel()
        {
            if (Session["res_userID"] != null)
            {
                int res_userID = Convert.ToInt32(Session["res_userID"]);
                string query = "SELECT dob FROM resident WHERE res_id = @res_id";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@res_id", res_userID);
                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime birthday = reader.GetDateTime("dob");
                                lblBirthday.Text = birthday.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                lblBirthday.Text = "Date of Birth not found.";
                            }
                        }
                    }
                }
            }
            else
            {
                // Handle the case when the user is not logged in
                lblBirthday.Text = "User not logged in.";
                Response.Redirect("residentlogin.aspx");
            }
        }

    }
}