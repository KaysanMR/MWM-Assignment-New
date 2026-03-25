using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MWM_Assignment_New.Customer
{
    public partial class Profile : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadUserProfile();
            }
        }

        private void LoadUserProfile()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT FullName, Email, Phone, Address FROM Users WHERE UserID = @UID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UID", userId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtName.Text = dr["FullName"]?.ToString() ?? "";
                    txtEmail.Text = dr["Email"]?.ToString() ?? "";
                    txtPhone.Text = dr["Phone"]?.ToString() ?? "";
                    txtAddress.Text = dr["Address"]?.ToString() ?? "";
                }
            } // This closes the 'using' block
        } // This closes the 'LoadUserProfile' method

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"UPDATE Users SET FullName = @Name, Phone = @Phone, Address = @Addr 
                                WHERE UserID = @UID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Addr", txtAddress.Text);
                cmd.Parameters.AddWithValue("@UID", userId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    pnlMessage.Visible = true;
                    Session["UserName"] = txtName.Text;
                }
            }
        }
    } // This closes the Class
} // This closes the Namespace