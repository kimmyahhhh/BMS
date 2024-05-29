using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Services;

namespace blotter
{
    public partial class admin_blttr : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;Database = bms-1;username=root;password=; convert zero datetime= True");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FilterGrid("All");
            }

           /* if (IsPostBack)
            {
                lblMessage.Text = "";
            }*/

            UpdateCounts();
        }

        /* public void DisplayByStatus(string status)
         {
             ViewState["CurrentStatus"] = status;  // Store the status in ViewState
             string connectionString = "datasource=localhost;port=3306;Database =bms-1;username=root;password=; convert zero datetime= True";
             string query = "SELECT blotter_id, res_id, complainant, compliance, date_data, time_data, started, ended, Suspect, status FROM blotter WHERE status = @Status";

             using (MySqlConnection con = new MySqlConnection(connectionString))
             {
                 using (MySqlCommand cmd = new MySqlCommand(query, con))
                 {
                     cmd.Parameters.AddWithValue("@Status", status);
                     con.Open();
                     using (MySqlDataReader reader = cmd.ExecuteReader())
                     {
                         if (reader.HasRows)
                         {
                             DataTable dt = new DataTable();
                             dt.Load(reader);
                             GridView1.DataSource = dt;
                             GridView1.DataBind();
                         }
                         else
                         {
                             if (status == "Pending")
                             {
                                 DisplayByStatus("Active");
                             }
                             else if (status == "Active")
                             {
                                 DisplayByStatus("Ended");
                             }
                         }
                     }
                 }
             }
         }*/
        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ShowDetails(id);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal2", "showModal2();", true);
            }
        }
        private void ShowDetails(int id)
        {
            string connectionString = "datasource=localhost;port=3306;Database=bms-1;username=root;password=;convert zero datetime=True;";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT blotter_id, res_id, complainant, compliance, date_data, time_data, started, ended, Suspect, status FROM blotter WHERE blotter_id = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            blott_tbv.Text = reader["blotter_id"].ToString();
                            res_tbv.Text = reader["res_id"].ToString();
                            compla_tbv.Text = reader["complainant"].ToString();
                            compli_tbv.Text = reader["compliance"].ToString();
                            txtDatev.Text = reader["date_data"].ToString();
                            txtTimev.Text = reader["time_data"].ToString();
                            start_tbv.Text = reader["started"] == DBNull.Value ? "Pending" : Convert.ToDateTime(reader["started"]).ToString("yyyy-MM-dd");
                            end_tbv.Text = reader["ended"] == DBNull.Value ? "Pending" : Convert.ToDateTime(reader["ended"]).ToString("yyyy-MM-dd");
                            susp_tbv.Text = reader["Suspect"].ToString();
                            string status = reader["status"].ToString();
                            status_tbv.Text = status;

                            if (status == "Active")
                            {
                                btnSetActive.Text = "End";
                                btnSetActive.Visible = true; 
                            else if (status == "Ended")
                            {
                                btnSetActive.Visible = false;
                            }
                            else
                            {
                                btnSetActive.Text = "Set Active";
                                btnSetActive.Visible = true; 
                            }
                        }
                    }
                }
            }
        }

        protected void btnSetActive_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(blott_tbv.Text);

            string connectionString = "datasource=localhost;port=3306;Database=bms-1;username=root;password=;convert zero datetime=True;";
            string query = "";

            if (btnSetActive.Text == "Set Active")
            {
                query = "UPDATE blotter SET status = @Status, started = IF(started IS NULL, NOW(), started) WHERE blotter_id = @Id";
            }
            else if (btnSetActive.Text == "End")
            {
                query = "UPDATE blotter SET status = @Status, ended = IF(ended IS NULL, NOW(), ended) WHERE blotter_id = @Id";
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", btnSetActive.Text == "Set Active" ? "Active" : "Ended");
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Data updated successfully
                        FilterGrid(ViewState["CurrentStatus"] as string ?? "All"); 
                    }
                    else
                    {
                        // Failed to update data
                    }
                }
            }
        }

        protected void lnkAll_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;  
            FilterGrid("All");
        }

        protected void lnkPending_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;  
            FilterGrid("Pending");
        }

        protected void lnkActive_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;  
            FilterGrid("Active");
        }

        protected void lnkEnded_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;  
            FilterGrid("Ended");
        }


        /*void filldview()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From blotter", con);
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            adap.Fill(dtb);
            con.Close();

            GridView1.DataSource = dtb;
            GridView1.DataBind();
        }*/

        protected void btnShowModal_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void res_tb_TextChanged(object sender, EventArgs e)
        {

        }

        protected void submitButton_Click(object sender, EventArgs e)
        {

        }


      /*  [WebMethod]
        public static void AddUser(string res, string compla, string compli, string date, string time, string susp)
        {
            string connectionString = "datasource=localhost;port=3306;Database = bms-1;username=root;password=";
            string query = "INSERT INTO blotter (res_id, complainant, compliance, date_data, time_data, Suspect) VALUES (@res_id, @complainant, @compliance, @DateData, @TimeData, @Suspect)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@res_id", Convert.ToInt32(res));
                    cmd.Parameters.AddWithValue("@complainant", compla);
                    cmd.Parameters.AddWithValue("@compliance", compli);
                    cmd.Parameters.AddWithValue("@DateData", DateTime.Parse(date));
                    cmd.Parameters.AddWithValue("@TimeData", TimeSpan.Parse(time));
                    cmd.Parameters.AddWithValue("@Suspect", susp);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }*/

        private void FilterGrid(string status, string searchQuery = "")
        {
            
            ViewState["CurrentStatus"] = status;

            string connectionString = "datasource=localhost;port=3306;Database=bms-1;username=root;password=;convert zero datetime=True;";
            string query;

            if (string.IsNullOrEmpty(status) || status == "All")
            {
                query = "SELECT blotter_id, res_id, complainant, compliance, date_data, time_data, started, ended, Suspect, status FROM blotter";
            }
            else
            {
                query = "SELECT blotter_id, res_id, complainant, compliance, date_data, time_data, started, ended, Suspect, status FROM blotter WHERE status = @Status";
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += string.IsNullOrEmpty(status) || status == "All" ? " WHERE " : " AND ";
                query += "complainant LIKE @SearchQuery";
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(status) && status != "All")
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                    }

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                        }
                        else
                        {
                            GridView1.DataSource = null;
                        }
                        GridView1.DataBind();
                    }
                }
            }

           
            UpdateCounts();
        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            string currentStatus = ViewState["CurrentStatus"] as string ?? "All";

          
            GridView1.PageIndex = e.NewPageIndex;

            
            FilterGrid(currentStatus);
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string currentStatus = ViewState["CurrentStatus"] as string ?? "All";
            string searchQuery = TextBox1.Text;
            FilterGrid(currentStatus, searchQuery);
        }

       
        private void UpdateCounts()
        {
            string connectionString = "datasource=localhost;port=3306;Database=bms-1;username=root;password=;convert zero datetime=True;";
            string queryTotal = "SELECT COUNT(*) FROM blotter";
            string queryPending = "SELECT COUNT(*) FROM blotter WHERE status IS NULL OR status = 'Pending'";
            string queryActive = "SELECT COUNT(*) FROM blotter WHERE status = 'Active'";
            string queryEnded = "SELECT COUNT(*) FROM blotter WHERE status = 'Ended'";

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                using (MySqlCommand cmdTotal = new MySqlCommand(queryTotal, con))
                using (MySqlCommand cmdPending = new MySqlCommand(queryPending, con))
                using (MySqlCommand cmdActive = new MySqlCommand(queryActive, con))
                using (MySqlCommand cmdEnded = new MySqlCommand(queryEnded, con))
                {
                    int totalCount = Convert.ToInt32(cmdTotal.ExecuteScalar());
                    int pendingCount = Convert.ToInt32(cmdPending.ExecuteScalar());
                    int activeCount = Convert.ToInt32(cmdActive.ExecuteScalar());
                    int endedCount = Convert.ToInt32(cmdEnded.ExecuteScalar());

                    lblTotalCount.Text = totalCount.ToString(); ;
                    lblPendingCount.Text = pendingCount.ToString(); ;
                    lblActiveCount.Text = activeCount.ToString(); ;
                    lblEndedCount.Text = endedCount.ToString(); ;
                }
            }
        }
    }
}
