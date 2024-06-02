using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace bms
{
    public partial class user_res_info : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                age_tb.Enabled = false;
                contact_tb.TextChanged += contact_tb_TextChanged;
                income_tb.TextChanged += income_tb_TextChanged;
                employment.SelectedIndexChanged += employment_SelectedIndexChanged;
                if (Session["res_userID"] != null)
                {
                    int resuserID = Convert.ToInt32(Session["res_userID"]);
                    LoadUserData(resuserID);
                    SetBirthdayLabel();
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
                                dob_lbl.Text = birthday.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                dob_lbl.Text = "Date of Birth not found.";
                            }
                        }
                    }
                }
            }
            else
            {
                // Handle the case when the user is not logged in
                dob_lbl.Text = "User not logged in.";
                Response.Redirect("residentlogin.aspx");
            }
        }

        private void LoadUserData(int resuserID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT r.f_name, r.m_name, r.l_name, r.dob, r.age, r.sex, r.contact_no, r.pob, r.religion, r.civil_status, r.educ_attainment, o.employment, r.emp_status, o.job, o.incomelevel, (SELECT household_name FROM household WHERE household_id = r.household_id) AS household_name FROM resident AS r JOIN occupation AS o ON r.res_id = o.emp_id WHERE r.res_id = @resUserID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@resUserID", resuserID);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        fname_lbl.Text = reader["f_name"].ToString();
                        mname_lbl.Text = reader["m_name"].ToString();
                        lname_lbl.Text = reader["l_name"].ToString();
                        dob_lbl.Text = reader["dob"].ToString();
                        age_lbl.Text = reader["age"].ToString();
                        sex_lbl.Text = reader["sex"].ToString();
                        contact_lbl.Text = reader["contact_no"].ToString();
                        pob_lbl.Text = reader["pob"].ToString();
                        religion_lbl.Text = reader["religion"].ToString();
                        CS_lbl.Text = reader["civil_status"].ToString();
                        EA_lbl.Text = reader["educ_attainment"].ToString();
                        employment_lbl.Text = reader["employment"].ToString();
                        Emp_lbl.Text = reader["emp_status"].ToString();
                        occupation_lbl.Text = reader["job"].ToString();
                        income_lbl.Text = reader["incomelevel"].ToString();
                        HH_lbl.Text = reader["household_name"].ToString();

                        // Populate TextBoxes and Controls for Editing
                        fname_tb.Text = reader["f_name"].ToString();
                        mname_tb.Text = reader["m_name"].ToString();
                        lname_tb.Text = reader["l_name"].ToString();
                        dob_tb.Text = Convert.ToDateTime(reader["dob"]).ToString("yyyy-MM-dd");
                        age_tb.Text = reader["age"].ToString();
                        sexlist.SelectedValue = reader["sex"].ToString();
                        contact_tb.Text = reader["contact_no"].ToString();
                        pob_db.Text = reader["pob"].ToString();
                        religion_tb.Text = reader["religion"].ToString();
                        CSList.SelectedValue = reader["civil_status"].ToString();
                        EAList.SelectedValue = reader["educ_attainment"].ToString();
                        employment.SelectedValue = reader["employment"].ToString();
                        EmpList.SelectedValue = reader["emp_status"].ToString();
                        occupation_tb.Text = reader["job"].ToString();
                        income_tb.Text = reader["incomelevel"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        
        private bool IsValidName(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z\s]*$");
        }
       
        protected void user_res_edit_btn_Click(object sender, EventArgs e)
        {
            string script = @"<script type='text/javascript'>
                 document.getElementById('infoModal').style.display = 'block'; 
             </script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "openModal", script, false);
        }

        protected void save_edited_btn_Click(object sender, EventArgs e)
        {
            if (Session["res_userID"] != null)
            {
                int res_userID = Convert.ToInt32(Session["res_userID"]);

                if (!IsValidName(fname_tb.Text) || !IsValidName(mname_tb.Text) || !IsValidName(lname_tb.Text) || !IsValidName(religion_tb.Text) || !IsValidName(occupation_tb.Text))
                {
                    mbox.Text = "Please enter valid details.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"UPDATE resident AS r 
                        JOIN occupation AS o ON r.res_id = o.emp_id 
                        SET r.f_name = @f_name, r.m_name = @m_name, r.l_name = @l_name, r.dob = @dob, 
                            r.age = @age, r.sex = @sex, r.contact_no = @contact_no, r.pob = @pob, 
                            r.religion = @religion, r.civil_status = @civil_status, r.educ_attainment = @educ_attainment, 
                            o.employment = @employment, r.emp_status = @emp_status, o.job = @job, o.incomelevel = @incomelevel WHERE r.res_id = @resUserID";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@resUserID", res_userID); // Assuming you have a way to get the current user ID
                    command.Parameters.AddWithValue("@f_name", fname_tb.Text);
                    command.Parameters.AddWithValue("@m_name", mname_tb.Text);
                    command.Parameters.AddWithValue("@l_name", lname_tb.Text);
                    command.Parameters.AddWithValue("@dob", dob_tb.Text);
                    command.Parameters.AddWithValue("@age", age_tb.Text);
                    command.Parameters.AddWithValue("@sex", sexlist.SelectedValue);
                    command.Parameters.AddWithValue("@contact_no", contact_tb.Text);
                    command.Parameters.AddWithValue("@pob", pob_db.Text);
                    command.Parameters.AddWithValue("@religion", religion_tb.Text);
                    command.Parameters.AddWithValue("@civil_status", CSList.SelectedValue);
                    command.Parameters.AddWithValue("@educ_attainment", EAList.SelectedValue);
                    command.Parameters.AddWithValue("@employment", employment.SelectedValue);
                    command.Parameters.AddWithValue("@emp_status", EmpList.SelectedValue);
                    command.Parameters.AddWithValue("@job", occupation_tb.Text);
                    command.Parameters.AddWithValue("@incomelevel", income_tb.Text);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        // Optionally show a success message
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        // Optionally show an error message
                    }
                }
                LoadUserData(res_userID);
                SetBirthdayLabel();
            }
            else
            {
                // Handle the case where the session variable is null
                Response.Redirect("residentlogin.aspx");
            }
        }

        protected void contact_tb_TextChanged(object sender, EventArgs e)
        {
            string contactNumber = contact_tb.Text;

            // Remove any non-numeric characters from the input
            contactNumber = System.Text.RegularExpressions.Regex.Replace(contactNumber, "[^0-9]", "");

            // Limit the length of the contact number to 11 characters
            if (contactNumber.Length > 11)
            {
                contactNumber = contactNumber.Substring(0, 11);
            }

            // Check if the contact number starts with "09"
            if (!contactNumber.StartsWith("09"))
            {
                // If not, clear the textbox
                contactNumber = "";
                Console.WriteLine("Contact number must start with '09'.");
            }

            // Update the textbox with the cleaned contact number
            contact_tb.Text = contactNumber;
        }

        protected void income_tb_TextChanged(object sender, EventArgs e)
        {
            string income = income_tb.Text;

            // Remove any non-numeric characters from the input
            income = System.Text.RegularExpressions.Regex.Replace(income, "[^0-9]", "");

            // Format the income by inserting commas every 3 digits from the left side
            if (!string.IsNullOrEmpty(income) && income.Length > 3)
            {
                income = Convert.ToDecimal(income).ToString("#,##0");
            }

            // Update the textbox with the formatted income
            income_tb.Text = income;
        }

        protected void dob_tb_TextChanged(object sender, EventArgs e)
        {
            DateTime dob;
            if (DateTime.TryParse(dob_tb.Text, out dob))
            {
                // Check if the entered date of birth is in the future
                if (dob > DateTime.Now)
                {
                    // If the date of birth is in the future, show an error message
                    age_tb.Text = "";
                    dob_tb.Text = "";
                    age_tb.Enabled = false;
                    Console.WriteLine("Date of birth cannot be in the future.");
                }
                else
                {
                    // Calculate age based on the entered date of birth
                    TimeSpan ageTimeSpan = DateTime.Now - dob;
                    int ageInYears = (int)Math.Floor(ageTimeSpan.TotalDays / 365.25);
                    age_tb.Text = ageInYears.ToString();
                    age_tb.Enabled = false;
                    Console.WriteLine("");
                }
            }
            else
            {
                // If the entered date is not valid, clear the age textbox and show an error message
                age_tb.Text = "";
                age_tb.Enabled = false;
                Console.WriteLine("Please enter a valid date of birth.");
            }
        }

        protected void clear_edited_btn_Click(object sender, EventArgs e)
        {

        }

        protected void employment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employment.SelectedValue == "Unemployed")
            {
                // When the employment status is "Unemployed"

                // Enable EmpList, occupation_tb, and income_tb
                occupation_tb.Enabled = false;
                income_tb.Enabled = false;

                // Set the values of EmpList, occupation_tb, and income_tb to "Unemployed"
                EmpList.SelectedValue = "Unemployed";
                occupation_tb.Text = "Unemployed";
                income_tb.Text = "Unemployed";
            }
            else
            {
                // When the employment status is not "Unemployed"

                // Enable EmpList, occupation_tb, and income_tb
                EmpList.Enabled = true;
                occupation_tb.Enabled = true;
                income_tb.Enabled = true;

                // Find the ListItem with the value "Unemployed" in EmpList and disable it
                ListItem unemployedItem = EmpList.Items.FindByValue("Unemployed");
                if (unemployedItem != null)
                {
                    unemployedItem.Enabled = false;
                    unemployedItem.Attributes.Add("style", "display:none;"); // Hide the item using CSS
                }
            }


            /*if (employment.SelectedValue == "Unemployed")
            {               
                EmpList.Enabled = false;
                occupation_tb.Enabled = false;
                income_tb.Enabled = false;

                EmpList.SelectedValue = "Unemployed";
                occupation_tb.Text = "Unemployed";
                income_tb.Text = "Unemployed";
            }
            else
            {
                EmpList.Enabled = true;
                occupation_tb.Enabled = true;
                income_tb.Enabled = true;

                
            }*/
        }
    }
}