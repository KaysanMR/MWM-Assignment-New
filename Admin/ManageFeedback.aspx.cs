using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New.Admin
{
    public partial class ManageFeedback : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"]?.ToString() != "Admin") Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                BindFeedback();
            }
        }

        private void BindFeedback()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Pulling latest messages first
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Feedback ORDER BY DateSubmitted DESC", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvFeedback.DataSource = dt;
                gvFeedback.DataBind();
            }
        }

        protected void gvFeedback_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int feedbackId = Convert.ToInt32(gvFeedback.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Feedback WHERE FeedbackID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", feedbackId);
                con.Open();
                cmd.ExecuteNonQuery();
                BindFeedback();
            }
        }
    }
}