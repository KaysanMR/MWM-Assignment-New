using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data;

namespace MWM_Assignment_New
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
        }

        // MAKE SURE THIS IS SEPARATE
        protected void Session_Start(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("ImagePath", typeof(string));

            Session["Cart"] = dt;
        }
    }
}