using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace bms
{
    public partial class add_res_records : System.Web.UI.Page
    {
        string connectionString = "server=localhost;database=bms;uid=root;pwd=";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                age_tb.Enabled = false;
                contact_tb.TextChanged += contact_tb_TextChanged;
                income_tb.TextChanged += income_tb_TextChanged;
                LoadComboBoxItems();
                BindHouseholdData();

            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            // Add your logout logic here, for example:
            // Session.Abandon();
            Response.Redirect("login.aspx");
        }

        private bool IsValidName(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z\s]*$");
        }

        private DataTable GetHouseholdData()
        {
            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT household_id, household_name, purok FROM household";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                throw new Exception("An error occurred while fetching household data: " + ex.Message);
            }
        }

        private void BindHouseholdData()
        {
            DataTable dt = GetHouseholdData();
            HHList.DataSource = dt;
            HHList.DataTextField = "household_name"; // The column name you want to display
            HHList.DataValueField = "household_id"; // The column name that will be used as the value
            HHList.DataBind();

            // Add the default item at the top of the list
            HHList.Items.Insert(0, new ListItem("Select Head", ""));
        }

        private void LoadComboBoxItems()
        {

        }

        private void ClearAllFields(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = string.Empty;
                }
                else if (c is DropDownList)
                {
                    ((DropDownList)c).SelectedIndex = -1;
                }
                else if (c is RadioButtonList)
                {
                    ((RadioButtonList)c).SelectedIndex = -1;
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }

                if (c.HasControls())
                {
                    ClearAllFields(c);
                }
            }
        }

        protected void saveres_btn_Click(object sender, EventArgs e)
        {
            if (!IsValidName(fname_tb.Text) || !IsValidName(mname_tb.Text) || !IsValidName(lname_tb.Text) || !IsValidName(religion_tb.Text) || !IsValidName(occupation_tb.Text))
            {
                mbox.Text = "Please enter valid details.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            string connectionString = "server=localhost;database=bms;uid=root;pwd=";
            // Retrieve data from form controls
            string firstName = fname_tb.Text;
            string middleName = mname_tb.Text;
            string lastName = lname_tb.Text;
            string birthday = dob_tb.Text; // Assuming this is a date string
            int age = Convert.ToInt32(age_tb.Text); // Assuming this is an integer
            string sex = sexlist.SelectedValue;
            string contactNo = contact_tb.Text;
            string placeOfBirth = pob_db.Text;
            string religion = religion_tb.Text;
            string civilStatus = CSList.SelectedValue;
            string educationalAttainment = EAList.SelectedValue;
            string employments = employment.SelectedValue;
            string employmentStatus = EmpList.SelectedValue;
            string occupation = occupation_tb.Text;
            string incomeLevel = income_tb.Text;
            string headOfHousehold = HHList.SelectedValue;

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(birthday) ||
                string.IsNullOrWhiteSpace(sex) ||
                string.IsNullOrWhiteSpace(contactNo) ||
                string.IsNullOrWhiteSpace(placeOfBirth) ||
                string.IsNullOrWhiteSpace(religion) ||
                CSList.SelectedIndex == -1 ||
                EAList.SelectedIndex == -1 ||
                HHList.SelectedIndex == -1)
            {
                mbox.Text = "Please fill in all fields.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); // Open the connection

                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        int householdId;
                        string purok;
                        using (MySqlCommand fetchHouseholdInfo = new MySqlCommand("SELECT household_id, purok FROM household WHERE household_id = @householdId", connection, transaction))
                        {
                            fetchHouseholdInfo.Parameters.AddWithValue("@householdId", HHList.SelectedValue);

                            using (MySqlDataReader reader = fetchHouseholdInfo.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    householdId = Convert.ToInt32(reader["household_id"]);
                                    purok = reader["purok"].ToString();
                                }
                                else
                                {
                                    throw new Exception("Selected household not found.");
                                }
                            }
                        }
                        string resusername = GenerateRandomUsername();
                        string respassword = GenerateRandomPassword();
                        // Insert resident information into the 'resident' table
                        using (MySqlCommand insertResInfo = new MySqlCommand("INSERT INTO resident (res_username, res_password, household_id, purok, l_name, f_name, m_name, dob, pob, age, sex, religion, civil_status, contact_no, educ_attainment, emp_status) VALUES (@resusername, @respassword, @householdId, @purok, @LastName, @FirstName, @MiddleName, @Birthday, @PlaceOfBirth, @Age, @Sex, @Religion, @CivilStatus, @ContactNo, @EducAtt, @EmpStats); SELECT LAST_INSERT_ID() AS LastID", connection, transaction))
                        {
                            insertResInfo.Parameters.AddWithValue("@resusername", resusername);
                            insertResInfo.Parameters.AddWithValue("@respassword", respassword);
                            insertResInfo.Parameters.AddWithValue("@householdId", householdId);
                            insertResInfo.Parameters.AddWithValue("@purok", purok);
                            insertResInfo.Parameters.AddWithValue("@LastName", lname_tb.Text);
                            insertResInfo.Parameters.AddWithValue("@FirstName", fname_tb.Text);
                            insertResInfo.Parameters.AddWithValue("@MiddleName", mname_tb.Text);
                            insertResInfo.Parameters.AddWithValue("@Birthday", dob_tb.Text);
                            insertResInfo.Parameters.AddWithValue("@PlaceOfBirth", pob_db.Text);
                            insertResInfo.Parameters.AddWithValue("@Age", Convert.ToInt32(age_tb.Text));
                            insertResInfo.Parameters.AddWithValue("@Sex", sexlist.SelectedValue);
                            insertResInfo.Parameters.AddWithValue("@Religion", religion_tb.Text);
                            insertResInfo.Parameters.AddWithValue("@ContactNo", contact_tb.Text);
                            insertResInfo.Parameters.AddWithValue("@CivilStatus", CSList.SelectedValue);
                            insertResInfo.Parameters.AddWithValue("@EducAtt", EAList.SelectedValue);
                            insertResInfo.Parameters.AddWithValue("@EmpStats", EmpList.SelectedValue);

                            using (MySqlDataReader reader = insertResInfo.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int residentId = Convert.ToInt32(reader["LastID"]);
                                    reader.Close();

                                    // Insert occupation information
                                    using (MySqlCommand insertEmpInfo = new MySqlCommand("INSERT INTO occupation (res_id, employment, job, incomelevel) VALUES (@ResidentId, @Employment, @Job, @Income)", connection, transaction))
                                    {
                                        insertEmpInfo.Parameters.AddWithValue("@ResidentId", residentId);
                                        insertEmpInfo.Parameters.AddWithValue("@Employment", employment.SelectedValue);
                                        insertEmpInfo.Parameters.AddWithValue("@Job", occupation_tb.Text);
                                        insertEmpInfo.Parameters.AddWithValue("@Income", income_tb.Text);
                                        insertEmpInfo.ExecuteNonQuery();
                                    }

                                    using (MySqlCommand updateMembersCount = new MySqlCommand("UPDATE household SET Members = (SELECT COUNT(*) FROM resident WHERE household_id = @householdId) WHERE household_id = @householdId", connection, transaction))
                                    {
                                        updateMembersCount.Parameters.AddWithValue("@householdId", householdId);
                                        updateMembersCount.ExecuteNonQuery();
                                    }
                                    mbox.Text = "Resident information added successfully.";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                                    ClearAllFields(this);
                                }
                            }
                        }

                        transaction.Commit(); // Commit the transaction
                    }
                    LoadComboBoxItems(); // Clear and reload dropdown lists
                }
            }
            catch (Exception ex)
            {
                mbox.Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
            }
        }

        private string GenerateRandomPassword()
        {
            // Generate a random password using a secure method (e.g., using RNGCryptoServiceProvider)
            // For simplicity, let's assume a random 8-character password here
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string respassword = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            return respassword;
        }

        private string GenerateRandomUsername()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string resusername = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            // You can add additional logic to handle duplicate usernames or ensure uniqueness
            return resusername;
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
                mbox.Text = "Contact number must start with '09'.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
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
                    mbox.Text = "Date of birth cannot be in the future.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
                }
                else
                {
                    // Calculate age based on the entered date of birth
                    TimeSpan ageTimeSpan = DateTime.Now - dob;
                    int ageInYears = (int)Math.Floor(ageTimeSpan.TotalDays / 365.25);
                    age_tb.Text = ageInYears.ToString();
                    age_tb.Enabled = false;
                    mbox.Text = "";
                }
            }
            else
            {
                // If the entered date is not valid, clear the age textbox and show an error message
                age_tb.Text = "";
                age_tb.Enabled = false;
                mbox.Text = "Please enter a valid date of birth.";
                ScriptManager.RegisterStartupScript(this, GetType(), "clearLabel", "clearLabelAfterDelay('mbox', 1000);", true);
            }
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

        }
    }
}
