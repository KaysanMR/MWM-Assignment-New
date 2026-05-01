using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MWM_Assignment_New.Customer
{
    public partial class Profile : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
                return;
            }

            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadUserProfile();
                LoadProfileDashboard();
            }
        }

        private void LoadUserProfile()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT FullName, Email, Phone, Address FROM Users WHERE UserID = @UID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UID", userId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtName.Text = dr["FullName"]?.ToString() ?? "";
                    txtEmail.Text = dr["Email"]?.ToString() ?? "";
                    txtPhone.Text = dr["Phone"]?.ToString() ?? "";
                    litDashboardName.Text = string.IsNullOrWhiteSpace(txtName.Text) ? "Seafood Regular" : txtName.Text;
                    BindAddressParts(AddressFormatter.Split(dr["Address"]?.ToString()));
                }
            } // This closes the 'using' block
        } // This closes the 'LoadUserProfile' method

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = @"UPDATE Users SET FullName = @Name, Phone = @Phone, Address = @Addr 
                                WHERE UserID = @UID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Addr", AddressFormatter.Combine(ReadAddressParts()));
                cmd.Parameters.AddWithValue("@UID", userId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    pnlMessage.Visible = true;
                    Session["UserName"] = txtName.Text;
                    litDashboardName.Text = string.IsNullOrWhiteSpace(txtName.Text) ? "Seafood Regular" : txtName.Text;
                    LoadProfileDashboard();
                }
            }
        }

        private void LoadProfileDashboard()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            int loyaltyPoints = LoyaltyService.SyncSession(connString, Session);
            int pointsNeeded = Math.Max(0, LoyaltyService.RedemptionCost - loyaltyPoints);
            int progressPercent = Math.Min(100, (int)Math.Round((loyaltyPoints / (double)LoyaltyService.RedemptionCost) * 100));

            lblLoyaltyBalance.Text = loyaltyPoints.ToString();
            lblNextReward.Text = pointsNeeded == 0
                ? "You can redeem RM 5.00 off at checkout."
                : pointsNeeded + " more points until your next RM 5.00 reward.";
            pointsProgressBar.Style["width"] = progressPercent + "%";

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                lblOrderCount.Text = GetScalarInt(con, "SELECT COUNT(*) FROM Orders WHERE UserID = @UID", userId).ToString();
                lblWishlistCount.Text = GetScalarInt(con, "SELECT COUNT(*) FROM Wishlist WHERE UserID = @UID", userId).ToString();
                BindRecentOrders(con, userId);
                BindPointsHistory(con, userId);
            }

            lblCartCount.Text = GetCartItemCount().ToString();
        }

        private int GetScalarInt(SqlConnection con, string query, int userId)
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UID", userId);
                object result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        private void BindRecentOrders(SqlConnection con, int userId)
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT TOP 3 OrderID, OrderDate, TotalAmount, Status
FROM Orders
WHERE UserID = @UID
ORDER BY OrderDate DESC", con))
            {
                cmd.Parameters.AddWithValue("@UID", userId);
                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                rptRecentOrders.DataSource = dt;
                rptRecentOrders.DataBind();
                pnlNoRecentOrders.Visible = dt.Rows.Count == 0;
            }
        }

        private void BindPointsHistory(SqlConnection con, int userId)
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT TOP 4
    'Order #' + CAST(OrderID AS varchar(20)) + ' points earned' AS Activity,
    OrderDate AS ActivityDate,
    CAST(FLOOR(TotalAmount) AS int) AS Points
FROM Orders
WHERE UserID = @UID
ORDER BY OrderDate DESC", con))
            {
                cmd.Parameters.AddWithValue("@UID", userId);
                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                rptPointsHistory.DataSource = dt;
                rptPointsHistory.DataBind();
                pnlNoPointsHistory.Visible = dt.Rows.Count == 0;
            }
        }

        private int GetCartItemCount()
        {
            DataTable cart = Session["Cart"] as DataTable;
            if (cart == null)
            {
                return 0;
            }

            int totalQuantity = 0;
            foreach (DataRow row in cart.Rows)
            {
                totalQuantity += Convert.ToInt32(row["Quantity"]);
            }

            return totalQuantity;
        }

        private AddressParts ReadAddressParts()
        {
            return new AddressParts
            {
                Line1 = txtAddressLine1.Text,
                Line2 = txtAddressLine2.Text,
                City = txtCity.Text,
                State = txtState.Text,
                Postcode = txtPostcode.Text,
                Country = txtCountry.Text
            };
        }

        private void BindAddressParts(AddressParts parts)
        {
            txtAddressLine1.Text = parts.Line1;
            txtAddressLine2.Text = parts.Line2;
            txtCity.Text = parts.City;
            txtState.Text = parts.State;
            txtPostcode.Text = parts.Postcode;
            txtCountry.Text = string.IsNullOrWhiteSpace(parts.Country) ? "Malaysia" : parts.Country;
        }
    } // This closes the Class
} // This closes the Namespace
