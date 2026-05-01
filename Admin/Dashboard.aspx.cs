using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace MWM_Assignment_New.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
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

                    SqlCommand cmdProducts = new SqlCommand("SELECT COUNT(*) FROM Products", con);
                    lblTotalProducts.Text = cmdProducts.ExecuteScalar().ToString();

                    SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM Users", con);
                    lblTotalUsers.Text = cmdUsers.ExecuteScalar().ToString();

                    SqlCommand cmdOrders = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE Status = 'Pending'", con);
                    lblPendingOrders.Text = cmdOrders.ExecuteScalar().ToString();

                    SqlCommand cmdRevenue = new SqlCommand("SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders", con);
                    lblRevenue.Text = Convert.ToDecimal(cmdRevenue.ExecuteScalar()).ToString("N2");

                    SqlCommand cmdRating = new SqlCommand("SELECT AVG(CAST(Rating AS decimal(10,2))) FROM Feedbacks", con);
                    object rating = cmdRating.ExecuteScalar();
                    lblAverageRating.Text = rating == DBNull.Value ? "N/A" : Convert.ToDecimal(rating).ToString("N1") + " / 5";

                    ContactMessageService.EnsureSchema(con);
                    SqlCommand cmdFeedback = new SqlCommand("SELECT (SELECT COUNT(*) FROM Feedbacks) + (SELECT COUNT(*) FROM ContactMessages)", con);
                    lblTotalFeedback.Text = cmdFeedback.ExecuteScalar().ToString();

                    LoadChartData(con);
                    BindLowStockProducts(con);
                    BindRecentOrders(con);
                }
                catch (Exception)
                {
                    // Keep the dashboard available even if optional reporting data is unavailable.
                }
            }
        }

        private void LoadChartData(SqlConnection con)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            hfOrdersChartData.Value = serializer.Serialize(GetOrdersOverTime(con));
            hfTopProductsChartData.Value = serializer.Serialize(GetTopProducts(con));
            hfInventoryChartData.Value = serializer.Serialize(GetInventoryHealth(con));
        }

        private object GetOrdersOverTime(SqlConnection con)
        {
            List<string> labels = new List<string>();
            List<int> counts = new List<int>();
            List<decimal> revenue = new List<decimal>();

            using (SqlCommand cmd = new SqlCommand(@"SELECT TOP 7
    CAST(OrderDate AS date) AS OrderDay,
    COUNT(*) AS OrderCount,
    ISNULL(SUM(TotalAmount), 0) AS Revenue
FROM Orders
GROUP BY CAST(OrderDate AS date)
ORDER BY OrderDay ASC", con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    labels.Add(Convert.ToDateTime(reader["OrderDay"]).ToString("dd MMM"));
                    counts.Add(Convert.ToInt32(reader["OrderCount"]));
                    revenue.Add(Convert.ToDecimal(reader["Revenue"]));
                }
            }

            if (labels.Count == 0)
            {
                labels.Add("No orders");
                counts.Add(0);
                revenue.Add(0);
            }

            return new { labels, counts, revenue };
        }

        private object GetTopProducts(SqlConnection con)
        {
            List<string> labels = new List<string>();
            List<int> quantities = new List<int>();

            using (SqlCommand cmd = new SqlCommand(@"SELECT TOP 5
    p.ProductName,
    SUM(od.Quantity) AS QuantitySold
FROM OrderDetails od
INNER JOIN Products p ON od.ProductID = p.ProductID
GROUP BY p.ProductName
ORDER BY QuantitySold DESC, p.ProductName ASC", con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    labels.Add(reader["ProductName"].ToString());
                    quantities.Add(Convert.ToInt32(reader["QuantitySold"]));
                }
            }

            if (labels.Count == 0)
            {
                labels.Add("No sales yet");
                quantities.Add(0);
            }

            return new { labels, quantities };
        }

        private object GetInventoryHealth(SqlConnection con)
        {
            List<string> labels = new List<string> { "In Stock", "Low Stock", "Out of Stock" };
            List<int> counts = new List<int>();

            using (SqlCommand cmd = new SqlCommand(@"SELECT
    SUM(CASE WHEN StockQuantity > 5 THEN 1 ELSE 0 END) AS InStock,
    SUM(CASE WHEN StockQuantity BETWEEN 1 AND 5 THEN 1 ELSE 0 END) AS LowStock,
    SUM(CASE WHEN StockQuantity <= 0 THEN 1 ELSE 0 END) AS OutOfStock
FROM Products", con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    counts.Add(reader["InStock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["InStock"]));
                    counts.Add(reader["LowStock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LowStock"]));
                    counts.Add(reader["OutOfStock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["OutOfStock"]));
                }
            }

            return new { labels, counts };
        }

        private void BindLowStockProducts(SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT TOP 5 ProductName, StockQuantity
FROM Products
WHERE StockQuantity <= 5
ORDER BY StockQuantity ASC, ProductName ASC", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable lowStock = new DataTable();
                adapter.Fill(lowStock);
                rptLowStockProducts.DataSource = lowStock;
                rptLowStockProducts.DataBind();
                pnlNoLowStockProducts.Visible = lowStock.Rows.Count == 0;
            }
        }

        private void BindRecentOrders(SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT TOP 6
    o.OrderID,
    o.OrderDate,
    o.TotalAmount,
    o.Status,
    COALESCE(NULLIF(u.FullName, ''), u.Username, 'Customer') AS CustomerName
FROM Orders o
INNER JOIN Users u ON o.UserID = u.UserID
ORDER BY o.OrderDate DESC, o.OrderID DESC", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable recentOrders = new DataTable();
                adapter.Fill(recentOrders);
                rptRecentOrders.DataSource = recentOrders;
                rptRecentOrders.DataBind();
                pnlNoRecentOrders.Visible = recentOrders.Rows.Count == 0;
            }
        }
    }
}
