using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New
{
    public partial class MyOrders : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security: Redirect to login if session expired
            if (Session["UserID"] == null) Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                BindOrders();
            }
        }

        private void BindOrders()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Filter by the current UserID from Session
                string query = "SELECT OrderID, OrderDate, TotalAmount, Status FROM Orders WHERE UserID = @UID ORDER BY OrderDate DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UID", Session["UserID"]);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        // Helper to change badge colors based on order status
        protected string GetStatusClass(string status)
        {
            switch (status.ToLower())
            {
                case "pending": return "badge bg-warning text-dark";
                case "shipped": return "badge bg-info text-white";
                case "delivered": return "badge bg-success text-white";
                case "cancelled": return "badge bg-danger text-white";
                default: return "badge bg-secondary";
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                // Redirect to a page that shows the specific items in that order
                Response.Redirect("OrderDetails.aspx?id=" + e.CommandArgument);
            }
        }
    }
}