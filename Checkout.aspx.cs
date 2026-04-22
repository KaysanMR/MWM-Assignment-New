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
            if (Session["UserID"] == null) Response.Redirect("~/Login.aspx");
            if (Session["Cart"] == null || ((DataTable)Session["Cart"]).Rows.Count == 0)
                Response.Redirect("~/Products.aspx");

            if (!IsPostBack)
            {
                LoadShippingAddress();
                LoadOrderSummary();
            }
        }

        private void LoadShippingAddress()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Address FROM Users WHERE UserID = @UID", con);
                cmd.Parameters.AddWithValue("@UID", Session["UserID"]);
                con.Open();
                object address = cmd.ExecuteScalar();
                AddressParts parts = AddressFormatter.Split(address?.ToString());
                BindAddressParts(parts);
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
            if (!Page.IsValid)
            {
                return;
            }

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
                    SaveShippingAddress(con, trans, userId);

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

                        if (MockCatalog.IsMockProductId(prodId))
                        {
                            MockCatalog.EnsureProductExists(con, trans, prodId);
                        }

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
                    Response.Redirect("~/OrderSuccess.aspx?id=" + newOrderId);
                }
                catch (Exception)
                {
                    trans.Rollback();
                    lblError.Text = "We could not place your order. Please review your cart and shipping details, then try again.";
                }
            }
        }

        private void SaveShippingAddress(SqlConnection con, SqlTransaction trans, int userId)
        {
            string combinedAddress = AddressFormatter.Combine(ReadAddressParts());
            SqlCommand cmd = new SqlCommand("UPDATE Users SET Address = @Address WHERE UserID = @UID", con, trans);
            cmd.Parameters.AddWithValue("@Address", combinedAddress);
            cmd.Parameters.AddWithValue("@UID", userId);
            cmd.ExecuteNonQuery();
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
    }
}
