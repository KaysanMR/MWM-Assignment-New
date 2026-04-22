using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;


namespace MWM_Assignment_New
{
    public partial class ProductGallery : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                BindGallery();
            }
        }

        private void BindCategories()
        {
            DataTable categories = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    MockCatalog.EnsurePreviewCatalogExists(con);

                    SqlDataAdapter sda = new SqlDataAdapter("SELECT CategoryID, CategoryName FROM Categories", con);
                    sda.Fill(categories);
                }
            }
            catch (SqlException)
            {
                categories = MockCatalog.CreateCategoryTable();
            }

            if (categories.Rows.Count < 6)
            {
                categories = MockCatalog.CreateCategoryTable();
            }

            ddlFilterCategory.DataSource = categories;
            ddlFilterCategory.DataTextField = "CategoryName";
            ddlFilterCategory.DataValueField = "CategoryID";
            ddlFilterCategory.DataBind();
            ddlFilterCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Categories", "0"));
        }

        private void BindGallery(string categoryId = "0")
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    MockCatalog.EnsurePreviewCatalogExists(con);

                    string query = @"SELECT p.*, c.CategoryName 
                                     FROM Products p 
                                     INNER JOIN Categories c ON p.CategoryID = c.CategoryID";

                    if (categoryId != "0")
                    {
                        query += " WHERE p.CategoryID = @CatID";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    if (categoryId != "0") cmd.Parameters.AddWithValue("@CatID", categoryId);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            catch (SqlException)
            {
                dt = MockCatalog.CreateProductTable(categoryId);
            }

            if (!MockCatalog.HasFullPreviewCatalog(dt))
            {
                dt = MockCatalog.CreateProductTable(categoryId);
            }

            dlProducts.DataSource = dt;
            dlProducts.DataBind();

            lblNoProducts.Visible = (dt.Rows.Count == 0);
        }

        protected void ddlFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGallery(ddlFilterCategory.SelectedValue);
        }

        protected string GetStockBadgeText(object stockQuantity)
        {
            int stock = Convert.ToInt32(stockQuantity);
            if (stock <= 0) return "Out of stock";
            if (stock <= 5) return "Low stock";
            if (stock <= 15) return "Only " + stock + " left";
            return "In stock";
        }

        protected string GetStockBadgeClass(object stockQuantity)
        {
            int stock = Convert.ToInt32(stockQuantity);
            if (stock <= 0) return "badge bg-danger align-self-start mb-3";
            if (stock <= 5) return "badge bg-warning align-self-start mb-3";
            if (stock <= 15) return "badge bg-info align-self-start mb-3";
            return "badge bg-success align-self-start mb-3";
        }

        // Helper to check wishlist status for the icon class
        protected bool IsInWishlist(object productID)
        {
            if (Session["UserID"] == null) return false;

            int userId = Convert.ToInt32(Session["UserID"]);
            int prodId = Convert.ToInt32(productID);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT COUNT(*) FROM Wishlist WHERE UserID = @UID AND ProductID = @PID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UID", userId);
                cmd.Parameters.AddWithValue("@PID", prodId);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Handle the heart click
        protected void dlProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ToggleWishlist")
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                int userId = Convert.ToInt32(Session["UserID"]);
                int prodId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    MockCatalog.EnsurePreviewCatalogExists(con);

                    // Toggle Logic: If exists, delete. If not, insert.
                    string query = @"
                IF EXISTS (SELECT 1 FROM Wishlist WHERE UserID = @UID AND ProductID = @PID)
                    DELETE FROM Wishlist WHERE UserID = @UID AND ProductID = @PID
                ELSE
                    INSERT INTO Wishlist (UserID, ProductID) VALUES (@UID, @PID)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UID", userId);
                    cmd.Parameters.AddWithValue("@PID", prodId);
                    cmd.ExecuteNonQuery();
                }

                // Refresh the list to update the heart icons
                BindGallery(ddlFilterCategory.SelectedValue);
            }
        }
    }
}
