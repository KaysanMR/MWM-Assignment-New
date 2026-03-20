using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MWM_Assignment_New
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string prodId = Request.QueryString["id"];
                if (string.IsNullOrEmpty(prodId)) Response.Redirect("Products.aspx");

                LoadDetails(prodId);
            }
        }

        private void LoadDetails(string id)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT p.*, c.CategoryName FROM Products p JOIN Categories c ON p.CategoryID = c.CategoryID WHERE p.ProductID = @ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblProductName.Text = dr["ProductName"].ToString();
                    litCrumb.Text = dr["ProductName"].ToString();
                    lblPrice.Text = string.Format("{0:N2}", dr["Price"]);
                    lblCategory.Text = dr["CategoryName"].ToString();
                    lblDescription.Text = dr["Description"].ToString();
                    imgProduct.ImageUrl = ResolveUrl(dr["ImagePath"].ToString());

                    int stock = Convert.ToInt32(dr["StockQuantity"]);
                    lblStock.Text = stock.ToString();

                    // UI logic for out-of-stock items
                    if (stock <= 0)
                    {
                        btnAddToCart.Enabled = false;
                        btnAddToCart.Text = "Out of Stock";
                        btnAddToCart.CssClass = "btn btn-secondary w-100";
                    }
                }
                else { Response.Redirect("Products.aspx"); }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int prodId = Convert.ToInt32(Request.QueryString["id"]);
            int qty = int.Parse(txtQty.Text);

            DataTable dt = (DataTable)Session["Cart"];
            bool isItemFound = false;

            // Check if the item is already in the cart to update quantity instead of adding a new row
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["ProductID"]) == prodId)
                {
                    row["Quantity"] = Convert.ToInt32(row["Quantity"]) + qty;
                    row["Total"] = Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);
                    isItemFound = true;
                    break;
                }
            }

            // If it's a new item, add a new row
            if (!isItemFound)
            {
                DataRow dr = dt.NewRow();
                dr["ProductID"] = prodId;
                dr["ProductName"] = lblProductName.Text;
                dr["Price"] = decimal.Parse(lblPrice.Text);
                dr["Quantity"] = qty;
                dr["Total"] = decimal.Parse(lblPrice.Text) * qty;
                dr["ImagePath"] = imgProduct.ImageUrl;
                dt.Rows.Add(dr);
            }

            Session["Cart"] = dt;
            Response.Redirect("Cart.aspx"); // Send them to the cart page to see their items
        }
    }
}