using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace bms
{
    public partial class add_res_records : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void saveres_btn_Click(object sender, EventArgs e)
        {
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
            string nationality = nationality_tb.Text;
            string religion = religion_tb.Text;
            string healthStatus = HSList.SelectedValue;
            string civilStatus = CSList.SelectedValue;
            string educationalAttainment = EAList.SelectedValue;
            string employments = employment.SelectedValue;
            string employmentStatus = EmpList.SelectedValue;
            string occupation = occupation_tb.Text;
            string incomeLevel = income_tb.Text;
            string purok = PurokList.SelectedValue;
            string headOfHousehold = HHList.SelectedValue;
            int houseID = Convert.ToInt32(House_id.SelectedValue); // Assuming this is an integer

            if (string.IsNullOrWhiteSpace(firstName) || 
                string.IsNullOrWhiteSpace(middleName) || 
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(birthday) ||
                string.IsNullOrWhiteSpace(sex) ||
                string.IsNullOrWhiteSpace(contactNo) ||
                string.IsNullOrWhiteSpace(placeOfBirth) ||
                string.IsNullOrWhiteSpace(nationality) ||
                string.IsNullOrWhiteSpace(religion) ||
                string.IsNullOrWhiteSpace(occupation) ||
                string.IsNullOrWhiteSpace(incomeLevel) ||
                HSList.SelectedIndex == -1 ||
                CSList.SelectedIndex == -1 ||
                EAList.SelectedIndex == -1 ||
                EmpList.SelectedIndex == -1 ||
                PurokList.SelectedIndex == -1 ||
                HHList.SelectedIndex == -1)
            {
                mbox.Text = "Please fill in all fields.";
                return;
            }

            try
            {
                // Establish connection to MySQL database
                
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Construct SQL INSERT statement
                    string query = @"INSERT INTO resident (FirstName, MiddleName, LastName, Birthday, Age, Sex, ContactNo, PlaceOfBirth, Nationality, Religion, HealthStatus, CivilStatus, EducationalAttainment, Employment, EmploymentStatus, Occupation, IncomeLevel, Purok, HeadOfHousehold, HouseID)
                             VALUES (@FirstName, @MiddleName, @LastName, @Birthday, @Age, @Sex, @ContactNo, @PlaceOfBirth, @Nationality, @Religion, @HealthStatus, @CivilStatus, @EducationalAttainment, @Employment, @EmploymentStatus, @Occupation, @IncomeLevel, @Purok, @HeadOfHousehold, @HouseID)";

                    // Create MySQL command and assign values to parameters
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@MiddleName", middleName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Birthday", birthday);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Sex", sex);
                        command.Parameters.AddWithValue("@ContactNo", contactNo);
                        command.Parameters.AddWithValue("@PlaceOfBirth", placeOfBirth);
                        command.Parameters.AddWithValue("@Nationality", nationality);
                        command.Parameters.AddWithValue("@Religion", religion);
                        command.Parameters.AddWithValue("@HealthStatus", healthStatus);
                        command.Parameters.AddWithValue("@CivilStatus", civilStatus);
                        command.Parameters.AddWithValue("@EducationalAttainment", educationalAttainment);
                        command.Parameters.AddWithValue("@Employment", employment);
                        command.Parameters.AddWithValue("@EmploymentStatus", employmentStatus);
                        command.Parameters.AddWithValue("@Occupation", occupation);
                        command.Parameters.AddWithValue("@IncomeLevel", incomeLevel);
                        command.Parameters.AddWithValue("@Purok", purok);
                        command.Parameters.AddWithValue("@HeadOfHousehold", headOfHousehold);
                        command.Parameters.AddWithValue("@HouseID", houseID);

                        // Open connection and execute INSERT command
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            // Insertion successful
                            Response.Write("Data inserted successfully.");
                        }
                        else
                        {
                            // Insertion failed
                            Response.Write("Failed to insert data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Response.Write("An error occurred: " + ex.Message);
            }
        }
    }
}