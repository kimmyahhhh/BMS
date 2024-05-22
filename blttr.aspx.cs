using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace blotter
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;Database = bms-1;username=root;passwoerd=; convert zero datetime= True");
        protected void Page_Load(object sender, EventArgs e)
        {
            filldview();
        }
        void filldview()
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
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
           
        }


            protected void btnShowModal_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.filldview();
        }

    }
}

       
       