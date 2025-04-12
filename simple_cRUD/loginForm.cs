using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simple_cRUD
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        static string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=students_crud;Data Source=Mohamed-Kiyas\SQLEXPRESS";
        static SqlConnection connectedConnection = new SqlConnection(connectionString);

        private void formClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            userName.Clear();
            password.Clear();
        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {
            registerForm redirectRegisterForm = new registerForm();
            this.Hide();
            redirectRegisterForm.Show();
        }

        private void checkedPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkedPassword.Checked)
            {
                password.PasswordChar = '\0';
            }
            else
            {
                password.PasswordChar = '*';
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try 
            {
                if (connectedConnection.State == ConnectionState.Closed)
                {
                    connectedConnection.Open();
                }

                if (string.IsNullOrEmpty(userName.Text) || (string.IsNullOrEmpty(password.Text)))
                {
                    MessageBox.Show("Both of these fields should not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else 
                {
                    string query = "SELECT user_Name, password FROM login WHERE user_Name = @UserName";
                    SqlCommand cmd = new SqlCommand(query, connectedConnection);

                    cmd.Parameters.AddWithValue("@UserName", userName.Text);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        if (reader["password"].ToString() == password.Text)
                        {
                            MessageBox.Show($"{userName.Text} you are Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password. Provide the correct password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect username. Provide the correct username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connectedConnection.Close();
                }
            }
            catch (Exception ex)
            {
                connectedConnection.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
