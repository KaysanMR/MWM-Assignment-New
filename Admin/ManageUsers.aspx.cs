using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindUsers();
            }
        }

        private void BindUsers()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT UserID, Username, FullName, Email, Role FROM Users", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            BindUsers();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvUsers.Rows[e.RowIndex];

            // Finding the controls in the EditItemTemplate
            string fullName = ((TextBox)row.FindControl("txtEditName")).Text.Trim();
            string email = ((TextBox)row.FindControl("txtEditEmail")).Text.Trim();
            string role = ((DropDownList)row.FindControl("ddlRole")).SelectedValue;

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Users SET FullName=@Name, Email=@Email, Role=@Role WHERE UserID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@ID", userId);

                con.Open();
                cmd.ExecuteNonQuery();
                gvUsers.EditIndex = -1;
                BindUsers();
                ShowStatus("User updated successfully!", "bg-success");
            }
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", userId);
                con.Open();
                cmd.ExecuteNonQuery();
                BindUsers();
                ShowStatus("User deleted.", "bg-warning");
            }
        }

        private void ShowStatus(string msg, string cssClass)
        {
            lblStatus.Text = msg;
            lblStatus.CssClass = "badge p-2 " + cssClass;
        }
    }
}