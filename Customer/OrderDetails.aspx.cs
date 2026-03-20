using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MWM_Assignment_New
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("~/Login.aspx");

            // Get OrderID from the URL query string
            if (Request.QueryString["id"] != null)
            {
                int orderId = Convert.ToInt32(Request.QueryString["id"]);
                litOrderID.Text = orderId.ToString();

                if (!IsPostBack)
                {
                    BindOrderData(orderId);
                }
            }
        }

        private void BindOrderData(int orderId)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // 1. Fetch Order General Info
                string orderQuery = "SELECT OrderDate, TotalAmount, Status FROM Orders WHERE OrderID = @OID AND UserID = @UID";
                SqlCommand cmdOrder = new SqlCommand(orderQuery, con);
                cmdOrder.Parameters.AddWithValue("@OID", orderId);
                cmdOrder.Parameters.AddWithValue("@UID", Session["UserID"]);

                SqlDataReader dr = cmdOrder.ExecuteReader();
                if (dr.Read())
                {
                    lblOrderDate.Text = Convert.ToDateTime(dr["OrderDate"]).ToString("dd MMM yyyy, hh:mm tt");
                    lblGrandTotal.Text = "RM " + Convert.ToDecimal(dr["TotalAmount"]).ToString("N2");
                    lblStatus.Text = dr["Status"].ToString();
                    lblStatus.CssClass += GetStatusColor(dr["Status"].ToString());
                }
                dr.Close();

                // 2. Fetch Items using a JOIN between OrderDetails and Products
                string itemsQuery = @"SELECT p.ProductName, p.ImagePath, od.Quantity, od.UnitPrice, 
                                     (od.Quantity * od.UnitPrice) as Subtotal 
                                     FROM OrderDetails od 
                                     INNER JOIN Products p ON od.ProductID = p.ProductID 
                                     WHERE od.OrderID = @OID";

                SqlCommand cmdItems = new SqlCommand(itemsQuery, con);
                cmdItems.Parameters.AddWithValue("@OID", orderId);

                SqlDataAdapter sda = new SqlDataAdapter(cmdItems);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                gvOrderItems.DataSource = dt;
                gvOrderItems.DataBind();
            }
        }

        private string GetStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "pending": return " bg-warning text-dark";
                case "delivered": return " bg-success text-white";
                default: return " bg-secondary text-white";
            }
        }
    }
}