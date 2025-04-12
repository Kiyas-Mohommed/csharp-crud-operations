using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simple_cRUD
{
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
        }

        static string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=students_crud;Data Source=Mohamed-Kiyas\SQLEXPRESS";
        static SqlConnection connectedConnection = new SqlConnection(connectionString);

        private void formClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutLabel_Click_1(object sender, EventArgs e)
        {
            loginForm redirectLoginForm = new loginForm();
            this.Hide();
            redirectLoginForm.Show();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            firstName.Clear();
            lastName.Clear();
            address.Clear();
            email.Clear();
            homeNumber.Clear();
            mobileNumber.Clear();
            parentName.Clear();
            nicNumber.Clear();
            parentNumber.Clear();
        }

        static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);

                if (mailAddress.Host.IndexOf("gmail") == -1 || mailAddress.Host.IndexOf('.') == -1 || mailAddress.Host.IndexOf("com") == -1)
                {
                    return false;
                }

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            try 
            {
                if (connectedConnection.State == ConnectionState.Closed)
                {
                    connectedConnection.Open();
                }

                if (string.IsNullOrEmpty(regNo.Text) || string.IsNullOrEmpty(firstName.Text) || string.IsNullOrEmpty(lastName.Text) || string.IsNullOrEmpty(dateOfBirth.Text) || string.IsNullOrEmpty(gender.Text) || string.IsNullOrEmpty(address.Text) || string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(homeNumber.Text) || string.IsNullOrEmpty(mobileNumber.Text) || string.IsNullOrEmpty(parentName.Text) || string.IsNullOrEmpty(nicNumber.Text) || string.IsNullOrEmpty(parentNumber.Text))
                {
                    MessageBox.Show("All fields should be filled out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!IsValidEmail(email.Text))
                {
                    MessageBox.Show("Invalid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else 
                {
                    string insertQuery = "INSERT INTO [dbo].[registration] " +
                        "(reg_no, first_name, last_name, date_of_birth, gender, address, email, home_number, mobile_number, parent_name, nic_number, parent_number) " +
                        "VALUES (@RegNo, @FirstName, @LastName, @DateOfBirth, @Gender, @Address, @Email, @HomeNumber, @MobileNumber, @ParentName, @NicNumber, @ParentNumber)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connectedConnection))
                    {
                        cmd.Parameters.AddWithValue("@RegNo", regNo.Text);

                        cmd.Parameters.AddWithValue("@FirstName", firstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", lastName.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth.Text.ToLower());
                        cmd.Parameters.AddWithValue("@Gender", gender.Text);

                        cmd.Parameters.AddWithValue("@Address", address.Text.ToLower());
                        cmd.Parameters.AddWithValue("@Email", email.Text.ToLower());
                        cmd.Parameters.AddWithValue("@HomeNumber", homeNumber.Text);
                        cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber.Text);

                        cmd.Parameters.AddWithValue("@ParentName", parentName.Text);
                        cmd.Parameters.AddWithValue("@NicNumber", nicNumber.Text);
                        cmd.Parameters.AddWithValue("@ParentNumber", parentNumber.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        connectedConnection.Close();
                        // regNo.Items.Add("@RegNo");

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{firstName.Text} {lastName.Text} you are registered successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            registerForm redirectRegisterForm = new registerForm();
                            this.Hide();
                            redirectRegisterForm.Show();

                            /* if (connectedConnection.State == ConnectionState.Closed)
                            {
                                connectedConnection.Open();
                            }

                            string selectQuery = "SELECT reg_no FROM [dbo].[registration]";

                            using (SqlCommand selectCmd = new SqlCommand(selectQuery, connectedConnection))
                            {
                                using (SqlDataReader reader = selectCmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        regNo.Items.Add(reader["reg_no"].ToString());
                                    }
                                }
                            } */
                        }
                        else
                        {
                            MessageBox.Show("Failed to register due to the following error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                connectedConnection.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void regNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                if (connectedConnection.State == ConnectionState.Closed)
                {
                    connectedConnection.Open();
                }

                // string selectedRegNo = regNo.SelectedItem.ToString();
                // string selectQuery = $"SELECT * FROM [dbo].[registration] WHERE reg_no = {selectedRegNo}";
            }
            catch (Exception ex)
            {
                connectedConnection.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            /* try
            {
                if (connectedConnection.State == ConnectionState.Closed)
                {
                    connectedConnection.Open();
                }

                if (string.IsNullOrEmpty(regNo.Text))
                {
                    MessageBox.Show("Please select a registration number to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string selectedRegNo = regNo.Text;

                    string query = "DELETE FROM [dbo].[registration] WHERE reg_no = @RegNo";

                    using (SqlCommand cmd = new SqlCommand(query, connectedConnection))
                    {
                        cmd.Parameters.AddWithValue("@RegNo", selectedRegNo);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        connectedConnection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Registration record with {selectedRegNo} has been deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            registerForm redirectRegisterForm = new registerForm();
                            this.Hide();
                            redirectRegisterForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the registration record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                connectedConnection.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }
    }
}
