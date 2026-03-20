using System;
using System.Web;
using System.Web.Security;

namespace MWM_Assignment_New
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // We check authentication on every load
            if (Context.User.Identity.IsAuthenticated)
            {
                mvAuth.ActiveViewIndex = 1;

                // Set the display name. If you have a 'FullName' in Session, use that instead.
                litUsername.Text = Session["Username"]?.ToString() ?? Context.User.Identity.Name;

                // Admin Check
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
            Session.Clear(); // Clear all session data (including the Cart)
            Session.Abandon();

            // Clear authentication cookie and redirect
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            Response.Redirect("~/Default.aspx");
        }
    }
}