using System;
using System.Data.SqlClient;

namespace MWM_Assignment_New
{
    internal sealed class CouponInfo
    {
        internal string Code { get; set; }
        internal string Description { get; set; }
        internal string DiscountType { get; set; }
        internal decimal DiscountValue { get; set; }

        internal string Summary
        {
            get
            {
                return DiscountType == "Percent"
                    ? DiscountValue.ToString("0.##") + "% off"
                    : "RM " + DiscountValue.ToString("N2") + " off";
            }
        }
    }

    internal static class CouponService
    {
        internal static void EnsureSchema(SqlConnection connection, SqlTransaction transaction = null)
        {
            using (SqlCommand command = new SqlCommand(@"IF OBJECT_ID('dbo.Coupons', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Coupons
    (
        CouponID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Code NVARCHAR(30) NOT NULL UNIQUE,
        Description NVARCHAR(120) NULL,
        DiscountType NVARCHAR(20) NOT NULL,
        DiscountValue DECIMAL(10,2) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Coupons_IsActive DEFAULT(1)
    );
END", connection, transaction))
            {
                command.ExecuteNonQuery();
            }

            SeedDefaultCoupon(connection, transaction, "TIN10", "Pantry starter discount", "Percent", 10m);
            SeedDefaultCoupon(connection, transaction, "CATCH5", "RM 5 off a seafood tin order", "Fixed", 5m);
        }

        internal static CouponInfo FindActiveCoupon(string connectionString, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                EnsureSchema(connection);

                using (SqlCommand command = new SqlCommand(@"SELECT TOP 1 Code, Description, DiscountType, DiscountValue
FROM Coupons
WHERE Code = @Code AND IsActive = 1", connection))
                {
                    command.Parameters.AddWithValue("@Code", NormalizeCode(code));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new CouponInfo
                        {
                            Code = reader["Code"].ToString(),
                            Description = reader["Description"].ToString(),
                            DiscountType = reader["DiscountType"].ToString(),
                            DiscountValue = Convert.ToDecimal(reader["DiscountValue"])
                        };
                    }
                }
            }
        }

        internal static decimal CalculateDiscount(CouponInfo coupon, decimal subtotal)
        {
            if (coupon == null || subtotal <= 0)
            {
                return 0;
            }

            decimal discount = coupon.DiscountType == "Percent"
                ? subtotal * (coupon.DiscountValue / 100m)
                : coupon.DiscountValue;

            return Math.Max(0, Math.Min(discount, subtotal));
        }

        internal static string NormalizeCode(string code)
        {
            return (code ?? "").Trim().ToUpperInvariant();
        }

        private static void SeedDefaultCoupon(SqlConnection connection, SqlTransaction transaction, string code, string description, string discountType, decimal discountValue)
        {
            using (SqlCommand command = new SqlCommand(@"IF NOT EXISTS (SELECT 1 FROM Coupons WHERE Code = @Code)
BEGIN
    INSERT INTO Coupons (Code, Description, DiscountType, DiscountValue, IsActive)
    VALUES (@Code, @Description, @DiscountType, @DiscountValue, 1);
END", connection, transaction))
            {
                command.Parameters.AddWithValue("@Code", code);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@DiscountType", discountType);
                command.Parameters.AddWithValue("@DiscountValue", discountValue);
                command.ExecuteNonQuery();
            }
        }
    }
}
