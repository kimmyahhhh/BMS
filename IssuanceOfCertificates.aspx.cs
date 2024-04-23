using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms
{
    public partial class IssuanceOfCertificates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            MySqlConnection connection = new MySqlConnection(connectionString);

            string purpose = Request.Form["purpose"];
            string type = Request.Form["type"];

            if (string.IsNullOrEmpty(purpose) || string.IsNullOrEmpty(type))
            {
                Response.Write("Please fill in all fields.");
            return;
            }

            try
            {
            string query = "INSERT INTO certificates (purpose, type) VALUES (@purpose, @type)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@purpose", purpose);
            command.Parameters.AddWithValue("@type", type);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Response.Write("Certificate issued successfully.");
            }
            else
            {
                Response.Write("Error issuing certificate.");
            }
            }
            catch (Exception ex)
            {
            Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        { 
            string purpose = txtPurpose.Text.Trim();
            if (string.IsNullOrEmpty(purpose))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please enter a purpose.";
                return;
            }

            Response.Redirect("dashboard.aspx");
        }
    }
}
