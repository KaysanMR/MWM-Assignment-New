using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
            }
        }

        private void LoadFeaturedProducts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Pulling the 3 newest products as "Featured"
                string query = "SELECT TOP 3 ProductID, ProductName, Price, ImagePath FROM Products ORDER BY ProductID DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                rptFeatured.DataSource = dt;
                rptFeatured.DataBind();
            }
        }
    }
}