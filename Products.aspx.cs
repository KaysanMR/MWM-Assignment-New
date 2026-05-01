using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New
{
    public partial class ProductGallery : System.Web.UI.Page
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
            ddlFilterCategory.Items.Insert(0, new ListItem("All Categories", "0"));
        }

        private void BindGallery(string categoryId = null)
        {
            DataTable dt = new DataTable();
            string selectedCategory = categoryId ?? ddlFilterCategory.SelectedValue ?? "0";
            string search = txtSearch.Text.Trim();
            string stockFilter = ddlStockFilter.SelectedValue;
            string badgeFilter = ddlBadgeFilter.SelectedValue;

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    MockCatalog.EnsurePreviewCatalogExists(con);

                    string query = @"SELECT p.*, c.CategoryName
FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID";
                    List<string> filters = new List<string>();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (selectedCategory != "0")
                    {
                        filters.Add("p.CategoryID = @CatID");
                        cmd.Parameters.AddWithValue("@CatID", selectedCategory);
                    }

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        filters.Add("(p.ProductName LIKE @Search OR p.Description LIKE @Search OR c.CategoryName LIKE @Search)");
                        cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
                    }

                    if (stockFilter == "available")
                    {
                        filters.Add("p.StockQuantity > 0");
                    }
                    else if (stockFilter == "low")
                    {
                        filters.Add("p.StockQuantity BETWEEN 1 AND 5");
                    }
                    else if (stockFilter == "out")
                    {
                        filters.Add("p.StockQuantity <= 0");
                    }

                    if (!string.IsNullOrWhiteSpace(badgeFilter))
                    {
                        filters.Add("p.Badges LIKE @Badge");
                        cmd.Parameters.AddWithValue("@Badge", "%" + badgeFilter + "%");
                    }

                    if (filters.Count > 0)
                    {
                        query += " WHERE " + string.Join(" AND ", filters);
                    }

                    query += " " + GetSortClause();
                    cmd.CommandText = query;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            catch (SqlException)
            {
                dt = MockCatalog.CreateProductTable(selectedCategory);
                ApplyInMemoryFilters(dt, search, stockFilter, badgeFilter);
            }

            if (!HasActiveFilters() && !MockCatalog.HasFullPreviewCatalog(dt))
            {
                dt = MockCatalog.CreateProductTable(selectedCategory);
            }

            ApplySort(dt);

            dlProducts.DataSource = dt;
            dlProducts.DataBind();

            lblNoProducts.Visible = (dt.Rows.Count == 0);
        }

        protected void ddlFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGallery(ddlFilterCategory.SelectedValue);
        }

        protected void FilterControl_Changed(object sender, EventArgs e)
        {
            BindGallery();
        }

        protected void btnApplyFilters_Click(object sender, EventArgs e)
        {
            BindGallery();
        }

        protected void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlFilterCategory.SelectedValue = "0";
            ddlStockFilter.SelectedValue = "all";
            ddlBadgeFilter.SelectedValue = "";
            ddlSort.SelectedValue = "featured";
            BindGallery();
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

        protected string RenderProductBadges(object productId, object productName, object categoryName, object stockQuantity, object storedBadges)
        {
            return ProductBadgeService.RenderBadges(productId, productName, categoryName, stockQuantity, storedBadges);
        }

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

                BindGallery(ddlFilterCategory.SelectedValue);
            }
        }

        private bool HasActiveFilters()
        {
            return (ddlFilterCategory.SelectedValue ?? "0") != "0"
                || !string.IsNullOrWhiteSpace(txtSearch.Text)
                || ddlStockFilter.SelectedValue != "all"
                || !string.IsNullOrWhiteSpace(ddlBadgeFilter.SelectedValue);
        }

        private string GetSortClause()
        {
            switch (ddlSort.SelectedValue)
            {
                case "name-asc": return "ORDER BY p.ProductName ASC";
                case "price-asc": return "ORDER BY p.Price ASC, p.ProductName ASC";
                case "price-desc": return "ORDER BY p.Price DESC, p.ProductName ASC";
                case "stock-desc": return "ORDER BY p.StockQuantity DESC, p.ProductName ASC";
                default: return "ORDER BY p.ProductID ASC";
            }
        }

        private void ApplyInMemoryFilters(DataTable table, string search, string stockFilter, string badgeFilter)
        {
            for (int index = table.Rows.Count - 1; index >= 0; index--)
            {
                DataRow row = table.Rows[index];
                int stock = Convert.ToInt32(row["StockQuantity"]);
                string haystack = (row["ProductName"] + " " + row["Description"] + " " + row["CategoryName"]).ToLowerInvariant();
                string badges = row["Badges"].ToString();

                bool keep = true;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    keep = haystack.Contains(search.ToLowerInvariant());
                }

                if (keep && stockFilter == "available") keep = stock > 0;
                if (keep && stockFilter == "low") keep = stock >= 1 && stock <= 5;
                if (keep && stockFilter == "out") keep = stock <= 0;
                if (keep && !string.IsNullOrWhiteSpace(badgeFilter)) keep = badges.IndexOf(badgeFilter, StringComparison.OrdinalIgnoreCase) >= 0;

                if (!keep)
                {
                    table.Rows.RemoveAt(index);
                }
            }
        }

        private void ApplySort(DataTable table)
        {
            string sort = "ProductID ASC";
            switch (ddlSort.SelectedValue)
            {
                case "name-asc": sort = "ProductName ASC"; break;
                case "price-asc": sort = "Price ASC, ProductName ASC"; break;
                case "price-desc": sort = "Price DESC, ProductName ASC"; break;
                case "stock-desc": sort = "StockQuantity DESC, ProductName ASC"; break;
            }

            DataView view = table.DefaultView;
            view.Sort = sort;
            DataTable sorted = view.ToTable();
            table.Clear();
            foreach (DataRow row in sorted.Rows)
            {
                table.ImportRow(row);
            }
        }
    }
}
