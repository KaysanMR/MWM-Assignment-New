using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MWM_Assignment_New
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindCart();
        }

        private void BindCart()
        {
            DataTable dt = (DataTable)Session["Cart"];
            gvCart.DataSource = dt;
            gvCart.DataBind();

            decimal grandTotal = 0;
            foreach (DataRow row in dt.Rows)
            {
                grandTotal += Convert.ToDecimal(row["Total"]);
            }
            lblGrandTotal.Text = "RM " + grandTotal.ToString("N2");

            btnCheckout.Enabled = (dt.Rows.Count > 0);
        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["Cart"];
            dt.Rows[e.RowIndex].Delete();
            Session["Cart"] = dt;
            BindCart();
        }
    }
}