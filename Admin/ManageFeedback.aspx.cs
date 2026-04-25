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
                BindContactMessages();
            }
        }

        private void BindFeedback()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                ContactMessageService.EnsureSchema(con);

                // Joining Feedbacks with Users based on your schema
                string query = @"SELECT f.*, u.FullName 
                         FROM Feedbacks f 
                         INNER JOIN Users u ON f.UserID = u.UserID 
                         ORDER BY f.DateSubmitted DESC";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvFeedback.DataSource = dt;
                gvFeedback.DataBind();
            }
        }

        private void BindContactMessages()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                ContactMessageService.EnsureSchema(con);

                using (SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT ContactMessageID, FullName, Email, Subject, Message, DateSubmitted
FROM ContactMessages
ORDER BY DateSubmitted DESC", con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    gvContactMessages.DataSource = dt;
                    gvContactMessages.DataBind();
                }
            }
        }

        // Helper to show stars for the rating
        protected string ShowStars(object rating)
        {
            int r = Convert.ToInt32(rating);
            return new string('★', r) + new string('☆', 5 - r);
        }

        protected void gvFeedback_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int feedbackId = Convert.ToInt32(gvFeedback.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Feedbacks WHERE FeedbackID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", feedbackId);
                con.Open();
                cmd.ExecuteNonQuery();
                BindFeedback();
            }
        }

        protected void gvContactMessages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int messageId = Convert.ToInt32(gvContactMessages.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM ContactMessages WHERE ContactMessageID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", messageId);
                con.Open();
                ContactMessageService.EnsureSchema(con);
                cmd.ExecuteNonQuery();
                BindContactMessages();
            }
        }
    }
}
