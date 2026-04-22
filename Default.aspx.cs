using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

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
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
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

            if (dt.Rows.Count == 0)
            {
                dt = MockCatalog.CreateProductTable();
            }

            rptFeatured.DataSource = dt.Rows.Cast<DataRow>().Take(3).CopyToDataTable();
            rptFeatured.DataBind();
        }
    }
}
