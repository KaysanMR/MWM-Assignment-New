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
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
                return;
            }

            if (!IsPostBack) BindCart();
        }

        private void BindCart()
        {
            DataTable dt = Session["Cart"] as DataTable ?? CreateEmptyCart();
            Session["Cart"] = dt;

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

        private DataTable CreateEmptyCart()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            return dt;
        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["Cart"];
            dt.Rows[e.RowIndex].Delete();
            Session["Cart"] = dt;
            BindCart();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout.aspx");
        }
    }
}
