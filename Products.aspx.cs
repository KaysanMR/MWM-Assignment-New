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
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories", con);
                con.Open();
                ddlFilterCategory.DataSource = cmd.ExecuteReader();
                ddlFilterCategory.DataTextField = "CategoryName";
                ddlFilterCategory.DataValueField = "CategoryID";
                ddlFilterCategory.DataBind();
                ddlFilterCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Categories", "0"));
            }
        }

        private void BindGallery(string categoryId = "0")
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
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
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dlProducts.DataSource = dt;
                dlProducts.DataBind();

                lblNoProducts.Visible = (dt.Rows.Count == 0);
            }
        }

        protected void ddlFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGallery(ddlFilterCategory.SelectedValue);
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
                    // Toggle Logic: If exists, delete. If not, insert.
                    string query = @"
                IF EXISTS (SELECT 1 FROM Wishlist WHERE UserID = @UID AND ProductID = @PID)
                    DELETE FROM Wishlist WHERE UserID = @UID AND ProductID = @PID
                ELSE
                    INSERT INTO Wishlist (UserID, ProductID) VALUES (@UID, @PID)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UID", userId);
                    cmd.Parameters.AddWithValue("@PID", prodId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // Refresh the list to update the heart icons
                BindGallery(ddlFilterCategory.SelectedValue);
            }
        }
    }
}