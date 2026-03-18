using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace MWM_Assignment_New
{
    public partial class Login : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Added UserID to the select for session storage
                string query = "SELECT UserID, Role FROM Users WHERE Username = @User AND [Password] = @Pass";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@User", txtLoginUser.Text.Trim());
                cmd.Parameters.AddWithValue("@Pass", txtLoginPass.Text);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // 1. Capture User Data
                        string userId = reader["UserID"].ToString();
                        string role = reader["Role"].ToString();

                        // 2. Store in Session for use across the site
                        Session["UserID"] = userId;
                        Session["UserRole"] = role;

                        // 3. Create the Authentication Cookie (false = non-persistent)
                        FormsAuthentication.SetAuthCookie(txtLoginUser.Text, false);

                        // 4. Role-Based Redirect Logic
                        if (role == "Admin")
                        {
                            Response.Redirect("~/Admin/Dashboard.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                    else
                    {
                        lblLoginMsg.Text = "Invalid username or password.";
                    }
                }
                catch (Exception ex)
                {
                    lblLoginMsg.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}