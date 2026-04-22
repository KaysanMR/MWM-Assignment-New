using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MWM_Assignment_New
{
    internal static class MockCatalog
    {
        private const string PlaceholderImagePath = "~/Images/Products/placeholder-tin.svg";

        private static readonly List<MockCategory> Categories = new List<MockCategory>
        {
            new MockCategory(-1, "Sardines"),
            new MockCategory(-2, "Tuna"),
            new MockCategory(-3, "Mackerel"),
            new MockCategory(-4, "Premium Seafood"),
            new MockCategory(-5, "Spicy Varieties"),
            new MockCategory(-6, "Family Packs")
        };

        private static readonly List<MockProduct> Products = new List<MockProduct>
        {
            new MockProduct(-101, -1, "Pink Salt Sardines in Olive Oil", 10.90m, 34, "Golden Catch sardines finished with pink salt and olive oil for a clean, pantry-ready bite.", "~/Images/Products/pinksalt_oliveoil.PNG"),
            new MockProduct(-102, -1, "Vine Tomato Sardines with Shoyu", 11.40m, 28, "Bright vine tomato meets savory shoyu in a richly seasoned sardine tin.", "~/Images/Products/vinetomato_shoyu.PNG"),
            new MockProduct(-103, -1, "Japanese Yuzu Sardines with Tarragon", 12.20m, 24, "A fragrant sardine tin with citrusy yuzu and tarragon for a fresh, lifted finish.", "~/Images/Products/japanezeyuzu_tarragon.PNG"),
            new MockProduct(-104, -1, "Lavender Sardines with Black Garlic", 12.80m, 18, "Soft floral notes and black garlic depth give these sardines a distinctive Golden Catch profile.", "~/Images/Products/lavender_blackgarlic.PNG"),
            new MockProduct(-105, -5, "Anchovies in Chili Oil", 9.40m, 31, "Bold anchovies in fragrant chili oil for noodles, vegetables, and sharing plates."),
            new MockProduct(-106, -4, "Smoked Mussels in Brine", 13.60m, 20, "Smoky mussels with a clean brine finish, made for snack boards and quick tapas."),
            new MockProduct(-107, -6, "Family Pack Sardines in Tomato Sauce", 18.90m, 16, "A larger pantry tin of classic sardines in tomato sauce for family meals."),
            new MockProduct(-108, -5, "Sambal Tuna Spread", 10.20m, 24, "Tuna blended with sambal-style heat for toast, crackers, and fast lunches.")
        };

        internal static DataTable CreateCategoryTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CategoryID", typeof(int));
            table.Columns.Add("CategoryName", typeof(string));

            foreach (MockCategory category in Categories)
            {
                table.Rows.Add(category.CategoryId, category.CategoryName);
            }

            return table;
        }

        internal static DataTable CreateProductTable(string categoryId = "0")
        {
            int parsedCategoryId;
            bool filterByCategory = int.TryParse(categoryId, out parsedCategoryId) && parsedCategoryId != 0;

            DataTable table = CreateProductSchema();
            IEnumerable<MockProduct> rows = filterByCategory
                ? Products.Where(product => product.CategoryId == parsedCategoryId)
                : Products;

            foreach (MockProduct product in rows)
            {
                MockCategory category = Categories.First(item => item.CategoryId == product.CategoryId);
                table.Rows.Add(
                    product.ProductId,
                    product.CategoryId,
                    category.CategoryName,
                    product.ProductName,
                    product.Price,
                    product.StockQuantity,
                    product.Description,
                    product.ImagePath);
            }

            return table;
        }

        internal static DataRow FindProduct(string productId)
        {
            int parsedProductId;
            if (!int.TryParse(productId, out parsedProductId))
            {
                return null;
            }

            DataTable table = CreateProductTable();
            DataRow[] rows = table.Select("ProductID = " + parsedProductId);
            return rows.Length == 0 ? null : rows[0];
        }

        internal static bool IsMockProductId(int productId)
        {
            return Products.Any(product => product.ProductId == productId);
        }

        internal static bool HasFullPreviewCatalog(DataTable products)
        {
            if (products == null)
            {
                return false;
            }

            HashSet<int> productIds = new HashSet<int>(
                products.AsEnumerable().Select(row => row.Field<int>("ProductID")));

            return Products.All(product => productIds.Contains(product.ProductId));
        }

        internal static void EnsurePreviewCatalogExists(SqlConnection connection)
        {
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                foreach (MockProduct product in Products)
                {
                    EnsureProductExists(connection, transaction, product.ProductId);
                }

                transaction.Commit();
            }
        }

        internal static void EnsureProductExists(SqlConnection connection, SqlTransaction transaction, int productId)
        {
            MockProduct product = Products.FirstOrDefault(item => item.ProductId == productId);
            if (product == null)
            {
                return;
            }

            MockCategory category = Categories.First(item => item.CategoryId == product.CategoryId);
            Execute(connection, transaction, "IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryID = @ID) BEGIN SET IDENTITY_INSERT Categories ON; INSERT INTO Categories (CategoryID, CategoryName) VALUES (@ID, @Name); SET IDENTITY_INSERT Categories OFF; END",
                new SqlParameter("@ID", category.CategoryId),
                new SqlParameter("@Name", category.CategoryName));

            Execute(connection, transaction, @"IF NOT EXISTS (SELECT 1 FROM Products WHERE ProductID = @ID)
BEGIN
    SET IDENTITY_INSERT Products ON;
    INSERT INTO Products (ProductID, ProductName, CategoryID, Price, StockQuantity, Description, ImagePath)
    VALUES (@ID, @Name, @CatID, @Price, @Stock, @Desc, @Img);
    SET IDENTITY_INSERT Products OFF;
END
ELSE
BEGIN
    UPDATE Products
    SET ProductName = @Name,
        CategoryID = @CatID,
        Price = @Price,
        StockQuantity = @Stock,
        Description = @Desc,
        ImagePath = @Img
    WHERE ProductID = @ID;
END",
                new SqlParameter("@ID", product.ProductId),
                new SqlParameter("@Name", product.ProductName),
                new SqlParameter("@CatID", product.CategoryId),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Stock", product.StockQuantity),
                new SqlParameter("@Desc", product.Description),
                new SqlParameter("@Img", product.ImagePath));
        }

        private static DataTable CreateProductSchema()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID", typeof(int));
            table.Columns.Add("CategoryID", typeof(int));
            table.Columns.Add("CategoryName", typeof(string));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("StockQuantity", typeof(int));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("ImagePath", typeof(string));
            return table;
        }

        private static void Execute(SqlConnection connection, SqlTransaction transaction, string commandText, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
            {
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        private sealed class MockCategory
        {
            internal MockCategory(int categoryId, string categoryName)
            {
                CategoryId = categoryId;
                CategoryName = categoryName;
            }

            internal int CategoryId { get; private set; }
            internal string CategoryName { get; private set; }
        }

        private sealed class MockProduct
        {
            internal MockProduct(int productId, int categoryId, string productName, decimal price, int stockQuantity, string description, string imagePath = PlaceholderImagePath)
            {
                ProductId = productId;
                CategoryId = categoryId;
                ProductName = productName;
                Price = price;
                StockQuantity = stockQuantity;
                Description = description;
                ImagePath = imagePath;
            }

            internal int ProductId { get; private set; }
            internal int CategoryId { get; private set; }
            internal string ProductName { get; private set; }
            internal decimal Price { get; private set; }
            internal int StockQuantity { get; private set; }
            internal string Description { get; private set; }
            internal string ImagePath { get; private set; }
        }
    }
}
