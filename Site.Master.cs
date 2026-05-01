using System;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace MWM_Assignment_New
{
    public partial class Site : System.Web.UI.MasterPage
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAdmin = Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin";
            phStoreLinks.Visible = !isAdmin;
            phAdminLinks.Visible = false;
            phLoyaltyBadge.Visible = false;
            phCustomerAccountLinks.Visible = false;
            phAdminAccountLinks.Visible = false;

            // We check authentication on every load
            if (Context.User.Identity.IsAuthenticated)
            {
                mvAuth.ActiveViewIndex = 1;

                // Set the display name. If you have a 'FullName' in Session, use that instead.
                litUsername.Text = Session["Username"]?.ToString() ?? Context.User.Identity.Name;

                if (isAdmin)
                {
                    phAdminLinks.Visible = true;
                    phAdminAccountLinks.Visible = true;
                }
                else
                {
                    phLoyaltyBadge.Visible = true;
                    phCustomerAccountLinks.Visible = true;
                    litLoyaltyPoints.Text = Session["UserID"] == null
                        ? "0"
                        : LoyaltyService.SyncSession(connString, Session).ToString();
                }
            }
            else
            {
                mvAuth.ActiveViewIndex = 0;
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
