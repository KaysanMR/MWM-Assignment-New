using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New.Customer // Added .Customer to the namespace
{
    public partial class Wishlist : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindWishlist();
            }
        }

        private void BindWishlist()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT p.*, c.CategoryName FROM Products p 
                                INNER JOIN Wishlist w ON p.ProductID = w.ProductID 
                                INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                                WHERE w.UserID = @UID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UID", userId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                gvWishlist.DataSource = (dt.Rows.Count > 0) ? dt : null;
                gvWishlist.DataBind();
                lblEmptyWishlist.Visible = (dt.Rows.Count == 0);
            }
        }

        protected void gvWishlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                int prodId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connString))
                {
                    string query = "DELETE FROM Wishlist WHERE UserID = @UID AND ProductID = @PID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UID", userId);
                    cmd.Parameters.AddWithValue("@PID", prodId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                BindWishlist();
            }
        }
    }
}