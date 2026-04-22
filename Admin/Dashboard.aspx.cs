using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace MWM_Assignment_New.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optional: Check if the session actually contains the Admin role 
                // as an extra layer of protection.
                if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
                {
                    Response.Redirect("~/Login.aspx");
                }

                LoadSummaryStats();
            }
        }

        private void LoadSummaryStats()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                try
                {
                    con.Open();

                    // Count Total Users
                    SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM Users", con);
                    lblTotalUsers.Text = cmdUsers.ExecuteScalar().ToString();

                    // Count Total Products
                    SqlCommand cmdProducts = new SqlCommand("SELECT COUNT(*) FROM Products", con);
                    lblTotalProducts.Text = cmdProducts.ExecuteScalar().ToString();

                    // Count Pending Orders (Requirement check)
                    SqlCommand cmdOrders = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE Status = 'Pending'", con);
                    lblPendingOrders.Text = cmdOrders.ExecuteScalar().ToString();

                    SqlCommand cmdRevenue = new SqlCommand("SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders", con);
                    lblRevenue.Text = Convert.ToDecimal(cmdRevenue.ExecuteScalar()).ToString("N2");

                    SqlCommand cmdLowStock = new SqlCommand("SELECT COUNT(*) FROM Products WHERE StockQuantity <= 5", con);
                    lblLowStock.Text = cmdLowStock.ExecuteScalar().ToString();

                    SqlCommand cmdRating = new SqlCommand("SELECT AVG(CAST(Rating AS decimal(10,2))) FROM Feedbacks", con);
                    object rating = cmdRating.ExecuteScalar();
                    lblAverageRating.Text = rating == DBNull.Value ? "N/A" : Convert.ToDecimal(rating).ToString("N1") + " / 5";
                }
                catch (Exception)
                {
                    // Log error or display message
                }
            }
        }
    }
}
