using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
    }
}