using System;
using System.Web;
using System.Web.Security;

namespace MWM_Assignment_New
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                // Switch to LoggedIn view
                mvAuth.ActiveViewIndex = 1;
                litUsername.Text = Context.User.Identity.Name;

                // Requirement: Check for Admin Role
                // This assumes you stored the role in a Session during Login
                if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    phAdminLinks.Visible = true;
                }
            }
            else
            {
                mvAuth.ActiveViewIndex = 0;
                phAdminLinks.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}