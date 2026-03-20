using System;

namespace MWM_Assignment_New
{
    public partial class OrderSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Security: If there is no ID in the URL, they shouldn't be here
            if (Request.QueryString["id"] != null)
            {
                litOrderID.Text = Request.QueryString["id"];
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}