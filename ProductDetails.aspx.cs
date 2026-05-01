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
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string prodId = Request.QueryString["id"];
                if (string.IsNullOrEmpty(prodId)) Response.Redirect("~/Products.aspx");

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
                ProductBadgeService.EnsureSchema(con);
                MockCatalog.EnsurePreviewCatalogExists(con);
                int recommendedProductId = 0;
                int recommendedCategoryId = 0;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblProductName.Text = dr["ProductName"].ToString();
                        litCrumb.Text = dr["ProductName"].ToString();
                        lblPrice.Text = string.Format("{0:N2}", dr["Price"]);
                        lblCategory.Text = dr["CategoryName"].ToString();
                        lblDescription.Text = dr["Description"].ToString();
                        imgProduct.ImageUrl = ResolveUrl(dr["ImagePath"].ToString());

                        recommendedProductId = Convert.ToInt32(dr["ProductID"]);
                        recommendedCategoryId = Convert.ToInt32(dr["CategoryID"]);
                        int stock = Convert.ToInt32(dr["StockQuantity"]);
                        lblStock.Text = stock.ToString();
                        litProductBadges.Text = ProductBadgeService.RenderBadges(dr["ProductID"], dr["ProductName"], dr["CategoryName"], dr["StockQuantity"], dr["Badges"]);
                        SetStockBadge(stock);

                        // UI logic for out-of-stock items
                        if (stock <= 0)
                        {
                            btnAddToCart.Enabled = false;
                            btnAddToCart.Text = "Out of Stock";
                            btnAddToCart.CssClass = "btn btn-secondary w-100";
                        }
                    }
                }

                if (recommendedProductId != 0)
                {
                    BindRecommendations(con, recommendedProductId, recommendedCategoryId);
                    return;
                }

                DataRow mockProduct = MockCatalog.FindProduct(id);
                if (mockProduct == null)
                {
                    Response.Redirect("~/Products.aspx");
                    return;
                }

                BindMockDetails(mockProduct);
                BindMockRecommendations(Convert.ToInt32(mockProduct["ProductID"]), Convert.ToInt32(mockProduct["CategoryID"]));
            }
        }

        private void BindMockDetails(DataRow product)
        {
            lblProductName.Text = product["ProductName"].ToString();
            litCrumb.Text = product["ProductName"].ToString();
            lblPrice.Text = string.Format("{0:N2}", product["Price"]);
            lblCategory.Text = product["CategoryName"].ToString();
            lblDescription.Text = product["Description"].ToString();
            imgProduct.ImageUrl = ResolveUrl(product["ImagePath"].ToString());
            lblStock.Text = product["StockQuantity"].ToString();
            litProductBadges.Text = ProductBadgeService.RenderBadges(product["ProductID"], product["ProductName"], product["CategoryName"], product["StockQuantity"], product["Badges"]);
            SetStockBadge(Convert.ToInt32(product["StockQuantity"]));
        }

        protected string RenderProductBadges(object productId, object productName, object categoryName, object stockQuantity, object storedBadges)
        {
            return ProductBadgeService.RenderBadges(productId, productName, categoryName, stockQuantity, storedBadges);
        }

        private void BindRecommendations(SqlConnection con, int productId, int categoryId)
        {
            string query = @"SELECT TOP 3 *
FROM (
    SELECT p.ProductID, p.ProductName, p.CategoryID, p.Price, p.StockQuantity, p.Description, p.ImagePath, p.Badges, c.CategoryName,
        CASE WHEN p.CategoryID = @CategoryID THEN 0 ELSE 1 END AS SortGroup
    FROM Products p
    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
    WHERE p.ProductID <> @ProductID AND p.StockQuantity > 0
) AS picks
ORDER BY SortGroup, ProductName";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                DataTable recommendations = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(recommendations);
                }

                BindRecommendationTable(recommendations);
            }
        }

        private void BindMockRecommendations(int productId, int categoryId)
        {
            DataTable source = MockCatalog.CreateProductTable();
            DataTable recommendations = source.Clone();

            foreach (DataRow row in source.Select("ProductID <> " + productId + " AND CategoryID = " + categoryId, "ProductName ASC"))
            {
                recommendations.ImportRow(row);
                if (recommendations.Rows.Count == 3) break;
            }

            if (recommendations.Rows.Count < 3)
            {
                foreach (DataRow row in source.Select("ProductID <> " + productId + " AND CategoryID <> " + categoryId, "ProductName ASC"))
                {
                    recommendations.ImportRow(row);
                    if (recommendations.Rows.Count == 3) break;
                }
            }

            BindRecommendationTable(recommendations);
        }

        private void BindRecommendationTable(DataTable recommendations)
        {
            rptRecommendations.DataSource = recommendations;
            rptRecommendations.DataBind();
            pnlRecommendations.Visible = recommendations.Rows.Count > 0;
        }

        private void SetStockBadge(int stock)
        {
            if (stock <= 0)
            {
                lblStockBadge.Text = "Out of stock";
                lblStockBadge.CssClass = "badge bg-danger";
            }
            else if (stock <= 5)
            {
                lblStockBadge.Text = "Low stock";
                lblStockBadge.CssClass = "badge bg-warning";
            }
            else if (stock <= 15)
            {
                lblStockBadge.Text = "Only " + stock + " left";
                lblStockBadge.CssClass = "badge bg-info";
            }
            else
            {
                lblStockBadge.Text = "In stock";
                lblStockBadge.CssClass = "badge bg-success";
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
            Response.Redirect("~/Cart.aspx"); // Send them to the cart page to see their items
        }
    }
}
