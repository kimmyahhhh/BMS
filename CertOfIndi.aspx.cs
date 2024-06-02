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
    public partial class CertOfIndi : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string certificateType = Request.QueryString["type"];
                int resuserId = Convert.ToInt32(Session["res_userID"]);

                if (certificateType == "Indigency")
                {
                    LoadCertificateData(resuserId);
                }
            }
        }

        private void LoadCertificateData(int resuserId)
        {
            string query = "SELECT r.f_name, r.l_name, r.m_name, r.age, r.purok, c.purpose FROM resident r INNER JOIN certification c ON r.res_id = c.res_id WHERE r.res_id = @UserId"; // Adjust the query as needed

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", resuserId);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblName.Text = $"{reader["l_name"]} {reader["f_name"]}, {reader["m_name"]}";
                    lblName1.Text = reader["f_name"].ToString();
                    lblAge.Text = reader["age"].ToString();
                    lblZone.Text = reader["purok"].ToString();
                    lblDay.Text = DateTime.Now.Day.ToString();
                    lblMonth.Text = DateTime.Now.ToString("MMMM");
                    lblYear2.Text = DateTime.Now.Year.ToString();
                    lblPurpose.Text = reader["purpose"].ToString();
                    reader.Close();
                }
            }
        }
    }
}