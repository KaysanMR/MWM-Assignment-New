using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing; 

namespace MWM_Assignment_New
{
    public partial class SignUp : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) // Ensures all validators passed 
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    // 1. Check if Username already exists in myData
                    string checkUser = "SELECT COUNT(*) FROM Users WHERE Username = @User";
                    SqlCommand checkCmd = new SqlCommand(checkUser, con);
                    checkCmd.Parameters.AddWithValue("@User", txtUsername.Text.Trim());

                    try
                    {
                        con.Open();
                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            lblMessage.Text = "Username already taken. Please choose another.";
                            lblMessage.ForeColor = Color.Red;
                        }
                        else
                        {
                            // 2. Insert new member 
                            string insertQuery = "INSERT INTO Users (Username, [Password], Email, FullName, [Role]) " +
                                               "VALUES (@User, @Pass, @Email, @Name, 'Member')";

                            SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                            insertCmd.Parameters.AddWithValue("@User", txtUsername.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@Pass", txtPassword.Text);
                            insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());

                            insertCmd.ExecuteNonQuery();

                            lblMessage.Text = "Registration successful! You can now login.";
                            lblMessage.ForeColor = Color.Green;

                            // Clear form fields after success
                            txtUsername.Text = txtEmail.Text = txtFullName.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = Color.Red;
                    }
                }
            }
        }
    }
}