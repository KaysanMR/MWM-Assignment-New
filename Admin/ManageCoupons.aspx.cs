using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace MWM_Assignment_New.Admin
{
    public partial class ManageCoupons : System.Web.UI.Page
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindCoupons();
            }
        }

        private void BindCoupons()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                CouponService.EnsureSchema(connection);

                using (SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT CouponID, Code, Description, DiscountType, DiscountValue, IsActive
FROM Coupons
ORDER BY IsActive DESC, Code ASC", connection))
                {
                    DataTable coupons = new DataTable();
                    adapter.Fill(coupons);
                    gvCoupons.DataSource = coupons;
                    gvCoupons.DataBind();
                }
            }
        }

        protected void btnAddCoupon_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            decimal value;
            if (!decimal.TryParse(txtDiscountValue.Text, out value) || value <= 0)
            {
                ShowMessage("Enter a valid discount value.", false);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    CouponService.EnsureSchema(connection);

                    using (SqlCommand command = new SqlCommand(@"INSERT INTO Coupons (Code, Description, DiscountType, DiscountValue, IsActive)
VALUES (@Code, @Description, @DiscountType, @DiscountValue, @IsActive)", connection))
                    {
                        command.Parameters.AddWithValue("@Code", CouponService.NormalizeCode(txtCode.Text));
                        command.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                        command.Parameters.AddWithValue("@DiscountType", ddlDiscountType.SelectedValue);
                        command.Parameters.AddWithValue("@DiscountValue", value);
                        command.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                        command.ExecuteNonQuery();
                    }
                }

                txtCode.Text = "";
                txtDescription.Text = "";
                txtDiscountValue.Text = "";
                ddlDiscountType.SelectedValue = "Percent";
                chkIsActive.Checked = true;
                ShowMessage("Coupon added.", true);
                BindCoupons();
            }
            catch (SqlException)
            {
                ShowMessage("Coupon could not be added. Check that the code is unique.", false);
            }
        }

        protected void gvCoupons_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCoupons.EditIndex = e.NewEditIndex;
            BindCoupons();
        }

        protected void gvCoupons_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCoupons.EditIndex = -1;
            BindCoupons();
        }

        protected void gvCoupons_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int couponId = Convert.ToInt32(gvCoupons.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvCoupons.Rows[e.RowIndex];

            string code = CouponService.NormalizeCode(((TextBox)row.FindControl("txtEditCode")).Text);
            string description = ((TextBox)row.FindControl("txtEditDescription")).Text.Trim();
            string discountType = ((DropDownList)row.FindControl("ddlEditDiscountType")).SelectedValue;
            string discountValueText = ((TextBox)row.FindControl("txtEditDiscountValue")).Text;
            bool isActive = ((CheckBox)row.FindControl("chkEditIsActive")).Checked;

            decimal discountValue;
            if (string.IsNullOrWhiteSpace(code) || !decimal.TryParse(discountValueText, out discountValue) || discountValue <= 0)
            {
                ShowMessage("Coupon code and a positive discount value are required.", false);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    CouponService.EnsureSchema(connection);

                    using (SqlCommand command = new SqlCommand(@"UPDATE Coupons
SET Code = @Code,
    Description = @Description,
    DiscountType = @DiscountType,
    DiscountValue = @DiscountValue,
    IsActive = @IsActive
WHERE CouponID = @CouponID", connection))
                    {
                        command.Parameters.AddWithValue("@CouponID", couponId);
                        command.Parameters.AddWithValue("@Code", code);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@DiscountType", discountType);
                        command.Parameters.AddWithValue("@DiscountValue", discountValue);
                        command.Parameters.AddWithValue("@IsActive", isActive);
                        command.ExecuteNonQuery();
                    }
                }

                gvCoupons.EditIndex = -1;
                ShowMessage("Coupon updated.", true);
                BindCoupons();
            }
            catch (SqlException)
            {
                ShowMessage("Coupon could not be updated. Check that the code is unique.", false);
            }
        }

        protected void gvCoupons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int couponId = Convert.ToInt32(gvCoupons.DataKeys[e.RowIndex].Value);

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                CouponService.EnsureSchema(connection);

                using (SqlCommand command = new SqlCommand("DELETE FROM Coupons WHERE CouponID = @CouponID", connection))
                {
                    command.Parameters.AddWithValue("@CouponID", couponId);
                    command.ExecuteNonQuery();
                }
            }

            ShowMessage("Coupon deleted.", true);
            BindCoupons();
        }

        private void ShowMessage(string message, bool success)
        {
            lblCouponAdminMessage.Text = message;
            lblCouponAdminMessage.ForeColor = success ? Color.Green : Color.Red;
        }
    }
}
