using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace MWM_Assignment_New
{
    internal static class LoyaltyService
    {
        internal const int StartingPoints = 120;
        internal const int RedemptionCost = 50;
        internal const decimal RedemptionDiscount = 5m;

        internal static int GetPoints(string connectionString, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                EnsureSchema(connection);

                using (SqlCommand command = new SqlCommand("SELECT LoyaltyPoints FROM Users WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    object result = command.ExecuteScalar();
                    return result == null || result == DBNull.Value ? StartingPoints : Convert.ToInt32(result);
                }
            }
        }

        internal static int SyncSession(string connectionString, HttpSessionStateBase session)
        {
            int userId = Convert.ToInt32(session["UserID"]);
            int points = GetPoints(connectionString, userId);
            session["LoyaltyPoints"] = points;
            return points;
        }

        internal static int SyncSession(string connectionString, HttpSessionState session)
        {
            int userId = Convert.ToInt32(session["UserID"]);
            int points = GetPoints(connectionString, userId);
            session["LoyaltyPoints"] = points;
            return points;
        }

        internal static void ApplyOrderPoints(SqlConnection connection, SqlTransaction transaction, int userId, bool redeemPoints, int pointsEarned)
        {
            EnsureSchema(connection, transaction);

            string query = redeemPoints
                ? @"UPDATE Users
SET LoyaltyPoints = LoyaltyPoints - @RedeemCost + @PointsEarned
WHERE UserID = @UserID AND LoyaltyPoints >= @RedeemCost"
                : @"UPDATE Users
SET LoyaltyPoints = LoyaltyPoints + @PointsEarned
WHERE UserID = @UserID";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@PointsEarned", pointsEarned);
                command.Parameters.AddWithValue("@RedeemCost", RedemptionCost);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new InvalidOperationException("Loyalty points could not be updated.");
                }
            }
        }

        internal static void EnsureSchema(SqlConnection connection, SqlTransaction transaction = null)
        {
            using (SqlCommand command = new SqlCommand(@"IF COL_LENGTH('dbo.Users', 'LoyaltyPoints') IS NULL
BEGIN
    ALTER TABLE dbo.Users
    ADD LoyaltyPoints INT NOT NULL
        CONSTRAINT DF_Users_LoyaltyPoints DEFAULT(120) WITH VALUES;
END", connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
