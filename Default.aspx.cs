using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Drawing;

namespace MWM_Assignment_New
{
    public partial class _Default : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFeaturedProducts();
                PrefillContactForm();
            }
        }

        private void LoadFeaturedProducts()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    MockCatalog.EnsurePreviewCatalogExists(con);

                    // Pulling the 3 newest products as "Featured"
                    string query = "SELECT TOP 3 ProductID, ProductName, Price, ImagePath FROM Products ORDER BY ProductID DESC";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.Fill(dt);
                }
            }
            catch (SqlException)
            {
                dt = MockCatalog.CreateProductTable();
            }

            if (dt.Rows.Count < 3)
            {
                dt = MockCatalog.CreateProductTable();
            }

            rptFeatured.DataSource = dt.Rows.Cast<DataRow>().Take(3).CopyToDataTable();
            rptFeatured.DataBind();
        }

        protected void btnSendInquiry_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    ContactMessageService.EnsureSchema(con);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO ContactMessages
(FullName, Email, Subject, Message, DateSubmitted)
VALUES (@FullName, @Email, @Subject, @Message, GETDATE())", con))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtContactName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtContactEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Subject", txtContactSubject.Text.Trim());
                        cmd.Parameters.AddWithValue("@Message", txtContactMessage.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                lblContactStatus.Text = "Inquiry saved. Golden Catch can review it from the admin inbox.";
                lblContactStatus.ForeColor = Color.Green;
                txtContactSubject.Text = "";
                txtContactMessage.Text = "";
            }
            catch (SqlException)
            {
                lblContactStatus.Text = "Your inquiry could not be saved right now. Please try again.";
                lblContactStatus.ForeColor = Color.Red;
            }
        }

        private void PrefillContactForm()
        {
            if (Session["UserID"] == null)
            {
                return;
            }

            using (SqlConnection con = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand("SELECT FullName, Email FROM Users WHERE UserID = @UserID", con))
            {
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtContactName.Text = reader["FullName"] == DBNull.Value ? "" : reader["FullName"].ToString();
                        txtContactEmail.Text = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                    }
                }
            }
        }
    }
}
