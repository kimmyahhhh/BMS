using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms
{
    public partial class res_record : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addres_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("add_res_records.aspx");
        }
    }
}