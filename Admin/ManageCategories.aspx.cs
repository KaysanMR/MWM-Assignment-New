using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New.Admin
{
    public partial class ManageCategories : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindCategories();
            }
        }

        private void BindCategories()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Categories", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewCategory.Text))
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Categories (CategoryName) VALUES (@Name)", con);
                    cmd.Parameters.AddWithValue("@Name", txtNewCategory.Text.Trim());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    txtNewCategory.Text = "";
                    BindCategories();
                    lblCatMsg.Text = "Category added!";
                    lblCatMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void gvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;
            BindCategories();
        }

        protected void gvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;
            BindCategories();
        }

        protected void gvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int catId = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            string newName = ((TextBox)gvCategories.Rows[e.RowIndex].FindControl("txtEditCatName")).Text.Trim();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Categories SET CategoryName=@Name WHERE CategoryID=@ID", con);
                cmd.Parameters.AddWithValue("@Name", newName);
                cmd.Parameters.AddWithValue("@ID", catId);
                con.Open();
                cmd.ExecuteNonQuery();
                gvCategories.EditIndex = -1;
                BindCategories();
            }
        }

        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int catId = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connString))
            {
                // Note: This might fail if products are still linked to this category
                SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", catId);
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    BindCategories();
                }
                catch
                {
                    lblCatMsg.Text = "Cannot delete: Products are using this category.";
                    lblCatMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}