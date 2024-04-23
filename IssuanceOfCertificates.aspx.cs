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
