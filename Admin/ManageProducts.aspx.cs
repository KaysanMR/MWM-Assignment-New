using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New.Admin
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Fix for validation controls in modern ASP.NET
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (Session["UserRole"]?.ToString() != "Admin") Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                BindCategories();
                BindProducts();
            }
        }

        private void BindCategories()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories", con);
                con.Open();
                ddlCategories.DataSource = cmd.ExecuteReader();
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataBind();
                ddlCategories.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Category --", "0"));
            }
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            // Verify all Validation Controls passed before running SQL
            if (Page.IsValid)
            {
                if (fuProductImage.HasFile)
                {
                    try
                    {
                        // 1. Save Image to Folder
                        string fileName = Path.GetFileName(fuProductImage.FileName);
                        string folderPath = Server.MapPath("~/Images/Products/");

                        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                        string fullPath = folderPath + fileName;
                        fuProductImage.SaveAs(fullPath);

                        // 2. Save Data to SQL
                        string dbPath = "~/Images/Products/" + fileName;

                        using (SqlConnection con = new SqlConnection(connString))
                        {
                            string query = "INSERT INTO Products (ProductName, CategoryID, Price, StockQuantity, Description, ImagePath) " +
                                           "VALUES (@Name, @CatID, @Price, @StockQuantity, @Desc, @Img)";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@Name", txtProdName.Text.Trim());
                            cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);
                            cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                            cmd.Parameters.AddWithValue("@StockQuantity", int.Parse(txtStock.Text));
                            cmd.Parameters.AddWithValue("@Desc", txtDesc.Text.Trim());
                            cmd.Parameters.AddWithValue("@Img", dbPath);

                            con.Open();
                            cmd.ExecuteNonQuery();

                            lblUploadMsg.Text = "Product added successfully!";
                            lblUploadMsg.ForeColor = System.Drawing.Color.Green;

                            // Clear form after success
                            ClearForm();
                            BindProducts();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblUploadMsg.Text = "Error: " + ex.Message;
                        lblUploadMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtProdName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            txtDesc.Text = "";
            ddlCategories.SelectedIndex = 0;
        }

        private void BindProducts()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Joins with Categories to show the Name instead of just the ID in your grid
                string query = "SELECT p.*, c.CategoryName FROM Products p INNER JOIN Categories c ON p.CategoryID = c.CategoryID";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        protected void gvProducts_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int prodId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connString))
            {
                // Optional: Fetch image path to delete the physical file before removing the record
                SqlCommand cmdPath = new SqlCommand("SELECT ImagePath FROM Products WHERE ProductID = @ID", con);
                cmdPath.Parameters.AddWithValue("@ID", prodId);
                con.Open();
                object path = cmdPath.ExecuteScalar();

                if (path != null)
                {
                    string physicalPath = Server.MapPath(path.ToString());
                    if (File.Exists(physicalPath)) File.Delete(physicalPath);
                }

                // Delete the database record
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM Products WHERE ProductID = @ID", con);
                cmdDelete.Parameters.AddWithValue("@ID", prodId);
                cmdDelete.ExecuteNonQuery();

                BindProducts();
            }
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            BindProducts();

            // After binding, we need to populate the dropdown in the edit row
            GridViewRow row = gvProducts.Rows[e.NewEditIndex];
            DropDownList ddl = (DropDownList)row.FindControl("ddlEditCat");
            if (ddl != null)
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories", con);
                    con.Open();
                    ddl.DataSource = cmd.ExecuteReader();
                    ddl.DataTextField = "CategoryName";
                    ddl.DataValueField = "CategoryID";
                    ddl.DataBind();

                    // Set the selected value based on the hidden field
                    HiddenField hf = (HiddenField)row.FindControl("hfCatID");
                    ddl.SelectedValue = hf.Value;
                }
            }
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            BindProducts();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int prodId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvProducts.Rows[e.RowIndex];

            string name = ((TextBox)row.FindControl("txtEditName")).Text.Trim();
            decimal price = decimal.Parse(((TextBox)row.FindControl("txtEditPrice")).Text);
            int stock = int.Parse(((TextBox)row.FindControl("txtEditStock")).Text);
            int catId = int.Parse(((DropDownList)row.FindControl("ddlEditCat")).SelectedValue);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Products SET ProductName=@Name, Price=@Price, StockQuantity=@Stock, CategoryID=@CatID WHERE ProductID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@CatID", catId);
                cmd.Parameters.AddWithValue("@ID", prodId);

                con.Open();
                cmd.ExecuteNonQuery();
                gvProducts.EditIndex = -1;
                BindProducts();
                lblUploadMsg.Text = "Product updated!";
            }
        }
    }
}