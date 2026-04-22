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
                    BindAddressParts(AddressFormatter.Split(dr["Address"]?.ToString()));
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
                cmd.Parameters.AddWithValue("@Addr", AddressFormatter.Combine(ReadAddressParts()));
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

        private AddressParts ReadAddressParts()
        {
            return new AddressParts
            {
                Line1 = txtAddressLine1.Text,
                Line2 = txtAddressLine2.Text,
                City = txtCity.Text,
                State = txtState.Text,
                Postcode = txtPostcode.Text,
                Country = txtCountry.Text
            };
        }

        private void BindAddressParts(AddressParts parts)
        {
            txtAddressLine1.Text = parts.Line1;
            txtAddressLine2.Text = parts.Line2;
            txtCity.Text = parts.City;
            txtState.Text = parts.State;
            txtPostcode.Text = parts.Postcode;
            txtCountry.Text = string.IsNullOrWhiteSpace(parts.Country) ? "Malaysia" : parts.Country;
        }
    } // This closes the Class
} // This closes the Namespace
