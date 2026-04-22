using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MWM_Assignment_New
{
    internal static class ProductBadgeService
    {
        internal static string RenderBadges(object productId, object productName, object categoryName, object stockQuantity)
        {
            return RenderBadges(productId, productName, categoryName, stockQuantity, null);
        }

        internal static string RenderBadges(object productId, object productName, object categoryName, object stockQuantity, object storedBadges)
        {
            IEnumerable<string> badges = ParseStoredBadges(storedBadges).ToList();
            if (!badges.Any())
            {
                badges = GetInferredBadges(productId, productName, categoryName, stockQuantity);
            }

            return string.Join("", badges.Select(RenderBadge));
        }

        internal static void EnsureSchema(SqlConnection connection, SqlTransaction transaction = null)
        {
            using (SqlCommand command = new SqlCommand(@"IF COL_LENGTH('dbo.Products', 'Badges') IS NULL
BEGIN
    ALTER TABLE dbo.Products
    ADD Badges NVARCHAR(200) NULL;
END", connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }

        internal static string NormalizeBadges(string badges)
        {
            return string.Join(", ", ParseStoredBadges(badges));
        }

        private static IEnumerable<string> ParseStoredBadges(object storedBadges)
        {
            string raw = SafeString(storedBadges);
            if (string.IsNullOrWhiteSpace(raw))
            {
                yield break;
            }

            foreach (string badge in raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string trimmed = badge.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    yield return trimmed;
                }
            }
        }

        private static IEnumerable<string> GetInferredBadges(object productId, object productName, object categoryName, object stockQuantity)
        {
            int id = SafeInt(productId);
            int stock = SafeInt(stockQuantity);
            string name = SafeString(productName).ToLowerInvariant();
            string category = SafeString(categoryName).ToLowerInvariant();

            if (id == -101 || id == -102 || name.Contains("tomato") || name.Contains("olive oil"))
            {
                yield return "Best Seller";
            }

            if (id <= -101 && id >= -104)
            {
                yield return "New";
            }

            if (category.Contains("premium") || name.Contains("yuzu") || name.Contains("lavender") || name.Contains("mussels"))
            {
                yield return "Premium";
            }

            if (category.Contains("spicy") || name.Contains("chili") || name.Contains("sambal"))
            {
                yield return "Spicy";
            }

            if (stock > 0 && stock <= 8)
            {
                yield return "Limited";
            }

            if (category.Contains("family") || name.Contains("family pack"))
            {
                yield return "Family Size";
            }
        }

        private static string RenderBadge(string badge)
        {
            string cssClass = "product-feature-badge product-feature-badge-" + badge.ToLowerInvariant().Replace(" ", "-");
            string encodedBadge = HttpUtility.HtmlEncode(badge);
            string iconClass = "bi " + GetIconClass(badge);
            return "<span class=\"" + HttpUtility.HtmlAttributeEncode(cssClass) + "\" title=\"" + HttpUtility.HtmlAttributeEncode(badge) + "\"><i class=\"" + HttpUtility.HtmlAttributeEncode(iconClass) + "\" aria-hidden=\"true\"></i><span class=\"product-badge-text\">" + encodedBadge + "</span></span>";
        }

        private static string GetIconClass(string badge)
        {
            switch ((badge ?? "").Trim().ToLowerInvariant())
            {
                case "best seller":
                    return "bi-star-fill";
                case "new":
                    return "bi-stars";
                case "premium":
                    return "bi-gem";
                case "spicy":
                    return "bi-fire";
                case "limited":
                    return "bi-hourglass-split";
                case "family size":
                    return "bi-people-fill";
                default:
                    return "bi-tag-fill";
            }
        }

        private static int SafeInt(object value)
        {
            int parsed;
            return value == null || value == DBNull.Value || !int.TryParse(value.ToString(), out parsed) ? 0 : parsed;
        }

        private static string SafeString(object value)
        {
            return value == null || value == DBNull.Value ? string.Empty : value.ToString();
        }
    }
}
