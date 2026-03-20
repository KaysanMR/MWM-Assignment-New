using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace MWM_Assignment_New
{
    public partial class Checkout : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check: Redirect if not logged in or cart is empty
            if (Session["UserID"] == null) Response.Redirect("Login.aspx");
            if (Session["Cart"] == null || ((DataTable)Session["Cart"]).Rows.Count == 0)
                Response.Redirect("ProductGallery.aspx");

            if (!IsPostBack)
            {
                LoadOrderSummary();
            }
        }

        private void LoadOrderSummary()
        {
            DataTable dt = (DataTable)Session["Cart"];
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["Total"]);
            }

            // This fills the labels you created in the ASPX file
            lblSubtotal.Text = "RM " + total.ToString("N2");
            lblGrandTotal.Text = "RM " + total.ToString("N2");
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["Cart"];
            int userId = Convert.ToInt32(Session["UserID"]);
            decimal grandTotal = 0;

            foreach (DataRow dr in dt.Rows)
                grandTotal += Convert.ToDecimal(dr["Total"]);

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    string orderQuery = @"INSERT INTO Orders (UserID, OrderDate, TotalAmount, Status) 
                                         OUTPUT INSERTED.OrderID 
                                         VALUES (@UID, GETDATE(), @Total, 'Pending')";

                    SqlCommand cmdOrder = new SqlCommand(orderQuery, con, trans);
                    cmdOrder.Parameters.AddWithValue("@UID", userId);
                    cmdOrder.Parameters.AddWithValue("@Total", grandTotal);
                    int newOrderId = (int)cmdOrder.ExecuteScalar();

                    foreach (DataRow row in dt.Rows)
                    {
                        int prodId = Convert.ToInt32(row["ProductID"]);
                        int qtyPurchased = Convert.ToInt32(row["Quantity"]);

                        string detailQuery = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice) VALUES (@OID, @PID, @Qty, @Price)";
                        SqlCommand cmdDetail = new SqlCommand(detailQuery, con, trans);
                        cmdDetail.Parameters.AddWithValue("@OID", newOrderId);
                        cmdDetail.Parameters.AddWithValue("@PID", prodId);
                        cmdDetail.Parameters.AddWithValue("@Qty", qtyPurchased);
                        cmdDetail.Parameters.AddWithValue("@Price", row["Price"]);
                        cmdDetail.ExecuteNonQuery();

                        string stockQuery = "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductID = @PID";
                        SqlCommand cmdStock = new SqlCommand(stockQuery, con, trans);
                        cmdStock.Parameters.AddWithValue("@Qty", qtyPurchased);
                        cmdStock.Parameters.AddWithValue("@PID", prodId);
                        cmdStock.ExecuteNonQuery();
                    }

                    trans.Commit();
                    Session["Cart"] = null; // Important: Clear cart after success
                    Response.Redirect("OrderSuccess.aspx?id=" + newOrderId);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    lblError.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}