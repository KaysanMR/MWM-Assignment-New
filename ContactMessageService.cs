using System.Data.SqlClient;

namespace MWM_Assignment_New
{
    internal static class ContactMessageService
    {
        internal static void EnsureSchema(SqlConnection connection, SqlTransaction transaction = null)
        {
            using (SqlCommand command = new SqlCommand(@"IF OBJECT_ID('dbo.ContactMessages', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ContactMessages
    (
        ContactMessageID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(120) NOT NULL,
        Subject NVARCHAR(150) NOT NULL,
        Message NVARCHAR(MAX) NOT NULL,
        DateSubmitted DATETIME NOT NULL CONSTRAINT DF_ContactMessages_DateSubmitted DEFAULT(GETDATE())
    );
END", connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
