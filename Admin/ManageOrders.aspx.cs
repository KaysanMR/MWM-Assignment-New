using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New.Admin
{
    public partial class ManageOrders : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"]?.ToString() != "Admin") Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                BindOrders();
            }
        }

        private void BindOrders()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"SELECT o.OrderID, o.OrderDate, o.TotalAmount, o.Status, u.FullName 
                         FROM Orders o 
                         INNER JOIN Users u ON o.UserID = u.UserID 
                         ORDER BY o.OrderDate DESC";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected string GetStatusClass(string status)
        {
            switch (status)
            {
                case "Pending": return "status-pending";
                case "Processing": return "status-processing";
                case "Shipped": return "status-shipped";
                case "Completed": return "status-completed";
                case "Cancelled": return "status-cancelled";
                default: return "badge bg-secondary";
            }
        }

        protected void gvOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrders.EditIndex = e.NewEditIndex;
            BindOrders();
        }

        protected void gvOrders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrders.EditIndex = -1;
            BindOrders();
        }

        protected void gvOrders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int orderId = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);
            DropDownList ddl = (DropDownList)gvOrders.Rows[e.RowIndex].FindControl("ddlStatus");
            string newStatus = ddl.SelectedValue;

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Orders SET Status = @Status WHERE OrderID = @ID", con);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@ID", orderId);
                con.Open();
                cmd.ExecuteNonQuery();
                gvOrders.EditIndex = -1;
                BindOrders();
            }
        }
    }
}
