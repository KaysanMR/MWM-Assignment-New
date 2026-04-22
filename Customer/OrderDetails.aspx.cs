using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MWM_Assignment_New
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;
        private string currentStatus = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) Response.Redirect("~/Login.aspx");

            // Get OrderID from the URL query string
            if (Request.QueryString["id"] != null)
            {
                int orderId = Convert.ToInt32(Request.QueryString["id"]);
                litOrderID.Text = orderId.ToString();

                if (!IsPostBack)
                {
                    BindOrderData(orderId);
                    BindFeedbackState(orderId);
                }
            }
        }

        private void BindOrderData(int orderId)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // 1. Fetch Order General Info
                string orderQuery = "SELECT OrderDate, TotalAmount, Status FROM Orders WHERE OrderID = @OID AND UserID = @UID";
                SqlCommand cmdOrder = new SqlCommand(orderQuery, con);
                cmdOrder.Parameters.AddWithValue("@OID", orderId);
                cmdOrder.Parameters.AddWithValue("@UID", Session["UserID"]);

                SqlDataReader dr = cmdOrder.ExecuteReader();
                if (dr.Read())
                {
                    lblOrderDate.Text = Convert.ToDateTime(dr["OrderDate"]).ToString("dd MMM yyyy, hh:mm tt");
                    lblGrandTotal.Text = "RM " + Convert.ToDecimal(dr["TotalAmount"]).ToString("N2");
                    currentStatus = dr["Status"].ToString();
                    lblStatus.Text = currentStatus;
                    lblStatus.CssClass += GetStatusColor(currentStatus);
                }
                dr.Close();

                // 2. Fetch Items using a JOIN between OrderDetails and Products
                string itemsQuery = @"SELECT p.ProductName, p.ImagePath, od.Quantity, od.UnitPrice, 
                                     (od.Quantity * od.UnitPrice) as Subtotal 
                                     FROM OrderDetails od 
                                     INNER JOIN Products p ON od.ProductID = p.ProductID 
                                     WHERE od.OrderID = @OID";

                SqlCommand cmdItems = new SqlCommand(itemsQuery, con);
                cmdItems.Parameters.AddWithValue("@OID", orderId);

                SqlDataAdapter sda = new SqlDataAdapter(cmdItems);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                gvOrderItems.DataSource = dt;
                gvOrderItems.DataBind();
                DataBind();
            }
        }

        protected string GetTrackingClass(string step)
        {
            int current = GetStatusRank(currentStatus);
            int target = GetStatusRank(step);
            return current >= target ? "is-active" : "";
        }

        protected void btnSubmitFeedback_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            int orderId = Convert.ToInt32(Request.QueryString["id"]);
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"IF EXISTS (SELECT 1 FROM Feedbacks WHERE UserID = @UID AND OrderID = @OID)
UPDATE Feedbacks SET Rating = @Rating, Comment = @Comment, DateSubmitted = GETDATE() WHERE UserID = @UID AND OrderID = @OID
ELSE
INSERT INTO Feedbacks (UserID, OrderID, Rating, Comment, DateSubmitted) VALUES (@UID, @OID, @Rating, @Comment, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UID", userId);
                cmd.Parameters.AddWithValue("@OID", orderId);
                cmd.Parameters.AddWithValue("@Rating", ReadRating());
                cmd.Parameters.AddWithValue("@Comment", txtFeedbackComment.Text.Trim());

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    pnlFeedbackSaved.Visible = true;
                    lblFeedbackError.Text = "";
                }
                catch (SqlException)
                {
                    lblFeedbackError.Text = "Feedback could not be saved right now.";
                }
            }
        }

        private void BindFeedbackState(int orderId)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Rating, Comment FROM Feedbacks WHERE UserID = @UID AND OrderID = @OID", con);
                cmd.Parameters.AddWithValue("@UID", Session["UserID"]);
                cmd.Parameters.AddWithValue("@OID", orderId);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtRating.Text = dr["Rating"].ToString();
                        txtFeedbackComment.Text = dr["Comment"].ToString();
                        pnlFeedbackSaved.Visible = true;
                    }
                }
                catch (SqlException)
                {
                    pnlFeedback.Visible = false;
                }
            }
        }

        private string GetStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "pending": return " bg-warning text-dark";
                case "delivery":
                case "shipped": return " bg-info text-white";
                case "delivered": return " bg-success text-white";
                case "completed": return " bg-success text-white";
                case "cancelled": return " bg-danger text-white";
                default: return " bg-secondary text-white";
            }
        }

        private int ReadRating()
        {
            int rating;
            if (!int.TryParse(txtRating.Text, out rating))
            {
                return 1;
            }

            return Math.Max(1, Math.Min(5, rating));
        }

        private int GetStatusRank(string status)
        {
            switch ((status ?? "").ToLower())
            {
                case "pending": return 1;
                case "processing":
                case "delivery":
                case "shipped": return 2;
                case "delivered":
                case "completed": return 3;
                default: return 0;
            }
        }
    }
}
