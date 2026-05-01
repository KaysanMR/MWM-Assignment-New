# Golden Catch Canned Fish Storefront

## Cover Page

**Name:** [Your Name]  
**Student ID:** [Your Student ID]  
**Intake Code:** [Your Intake Code]  
**Subject:** CT081-3-3 Mobile & Web Multimedia  
**Project Title:** Golden Catch Canned Fish Storefront  
**Date Assigned:** [Date Assigned]  
**Date Completed:** [Date Completed]  

---

## Acknowledgement

[Write a short acknowledgement here. Mention any lecturer guidance, references, tools, or learning resources used during development.]

---

## Table Of Contents

1. Introduction / Project Plan
2. Requirement Specification
3. Design
4. Implementation
5. Testing And Evaluation
6. Conclusion
7. User Manual
8. References
9. Appendices

---

## 1. Introduction / Project Plan

### 1.1 Project Overview

Golden Catch is a mobile-optimized e-commerce web application for browsing and purchasing canned fish products such as sardines, tuna, mackerel, and premium seafood tins. The application was developed using ASP.NET Web Forms and SQL Server LocalDB. The database is kept inside the `App_Data` folder, which makes the project easier to run and test as a coursework submission.

The project demonstrates an interactive mobile web multimedia application with both customer-facing shopping features and administrator tools. Customers can browse product images, view detailed product information, save items to a wishlist, apply discounts, check out, and review their order history. Administrators have a separate workspace for maintaining products, categories, orders, customer feedback, coupons, stock alerts, and dashboard summaries.

The interface uses a canned seafood theme instead of a general e-commerce template. Product images, product cards, status badges, responsive tables, and mobile-friendly action controls are used to make the website feel more complete. The homepage also includes the main business and contact information, so separate About and Contact pages are not needed in the current version.

### 1.2 Project Objectives

- Develop a functional e-commerce website using ASP.NET Web Forms, C#, Bootstrap, and SQL Server LocalDB.
- Provide customer registration, login, profile management, product browsing, cart, checkout, order history, and feedback features.
- Provide an administrator workspace for products, categories, orders, feedback, coupons, stock alerts, and dashboard metrics.
- Store and retrieve application data using a SQL Server database in the `App_Data` folder, with connection settings managed through `Web.config`.
- Demonstrate mobile-friendly layout, multimedia product presentation, responsive table behavior, and clear navigation on both desktop and phone screens.
- Include optional engagement features such as wishlist, loyalty points, coupons, stock badges, simulated notifications, and order tracking.
- Separate customer and administrator workflows so administrators can access management tools without customer shopping features appearing in the admin navigation.

### 1.3 Target Users

- **Customers:** users who browse canned fish products, compare categories, add items to cart, check out, view order history, track delivery status, save wishlist items, and submit feedback.
- **Administrators:** staff members who manage the store data, including products, categories, orders, feedback, coupons, stock levels, and dashboard summaries.

### 1.4 Project Scope

The project covers a small online canned fish storefront with customer and admin workflows. It includes product listing, product details, shopping cart, checkout, order management, feedback/rating, profile management, and extra retention features such as wishlist, loyalty points, coupons, stock badges, and order tracking.

The scope is focused on the features needed for a coursework e-commerce prototype rather than a full commercial system. Payment is simulated, inventory and order status are managed by administrators, and customer-facing content is presented through the main storefront pages. Customers can create accounts and shop through the storefront, while administrators are restricted to the management area.

### 1.5 Development Timeline

| Phase | Activities | Week |
|---|---|---|
| Planning | Topic selection, requirements review, proposal outline | Week 10 |
| Design | ERD, wireframes, navigation structure, UI direction | Week 10 |
| Core Development | Registration, login, catalog, cart, checkout | Week 11 |
| Admin Development | Manage products, categories, orders, feedback, coupons, and dashboard metrics | Week 12 |
| Enhancements | Wishlist, loyalty points, coupons, tracking, dashboard metrics | Week 13 |
| Testing & Documentation | Browser testing, validation, report, user manual | Week 14 |

---

## 2. Requirement Specification

### 2.1 Functional Requirements

#### Customer Requirements

- Register a new member account.
- Log in and log out using Forms Authentication.
- Manage profile and shipping address.
- Browse canned fish products.
- Filter products by category.
- View product details.
- Add products to cart.
- Update or remove cart items.
- Checkout and place an order.
- View purchase history.
- View order details and delivery status.
- Submit feedback and rating for an order.
- Save products to wishlist.
- Use coupons or loyalty point discounts during checkout.

#### Administrator Requirements

- Log in as administrator.
- View dashboard summary.
- Manage products.
- Manage categories.
- Manage orders and delivery statuses.
- View and remove customer feedback.
- Manage coupons.
- View stock alerts and summary metrics.

### 2.2 Non-Functional Requirements

- The website should be mobile-friendly and usable in modern browsers.
- The system should use SQL Server LocalDB with the database stored in `App_Data`.
- The connection string should be stored in `Web.config`.
- Forms should include validation for required input.
- Customer-only and admin-only pages should be protected.
- Navigation should be clear and consistent.
- The project files should be organized with meaningful naming conventions.

### 2.3 Optional / Enhanced Features

- Wishlist.
- Loyalty points.
- Redeemable coupons.
- Order tracking timeline.
- Simulated order notification.
- Low stock badges.
- Admin dashboard metrics.

---

## 3. Design

### 3.1 System Architecture

The system follows the standard ASP.NET Web Forms page model. Each page has an `.aspx` markup file and a related C# code-behind file. The markup file defines the layout, Web Forms controls, GridViews, buttons, validators, and placeholders. The code-behind file handles events such as `Page_Load`, button clicks, data binding, validation, redirects, and database updates.

`Site.Master` provides the shared layout for the application. It contains the main navigation, account links, admin links, notification area, loyalty badge, common CSS and script references, and the `ContentPlaceHolder` where each page renders its own content. The master page also checks the current user role so customer shopping links can be hidden from administrators and admin links can be shown only to admin users.

The application uses Forms Authentication for login and role-based navigation. After login, the user's ID, name, role, and loyalty information are stored in session variables. Customer pages check for a valid customer session before showing profile, wishlist, checkout, and order pages. Admin pages check for the admin role before allowing access to management tools.

Data is stored in a SQL Server LocalDB database under `App_Data`. Pages and service classes connect to the database using the connection string in `Web.config`. The main data tables include users, products, categories, orders, order details, wishlist entries, feedback, and coupons. `Web.config` is also used for authentication rules, compilation settings, connection strings, and runtime configuration.

### 3.2 Database Design

[Insert ERD screenshot or diagram here.]

Suggested tables to discuss:

- `Users`
- `Categories`
- `Products`
- `Orders`
- `OrderDetails`
- `Wishlist`
- `Feedbacks`

### 3.3 Navigation Structure

[Insert navigation diagram or bullet list here.]

Example:

- Home
- Products
- Product Details
- Cart
- Checkout
- Login / Register
- Customer Profile
- Wishlist
- Order History
- Order Details
- Admin Dashboard
- Manage Products
- Manage Categories
- Manage Orders
- Manage Feedback
- Manage Coupons

### 3.4 User Interface Design

The user interface is designed around a mobile-first canned seafood storefront style. The color palette uses warm off-white surfaces, dark borders, muted coastal accents, and blue highlights for prices or important actions. Headings and buttons use stronger typography, while product descriptions are quieter so shoppers can scan categories and item details quickly.

Product browsing is presented through product cards and responsive grids. Each product card includes an image, name, category, price, stock/status cues, and a clear path to product details or cart actions. The homepage gives users quick access to featured products and business information, which reduces the need for separate static content pages.

Administrator pages use denser layouts because they are meant for repeated management tasks. Dashboard cards show totals and operational signals, while GridViews list products, categories, orders, feedback, coupons, and related records. On desktop, these GridViews stay as tables so rows can be compared easily. On smaller screens, responsive CSS changes GridView rows into stacked card-style records with cell labels, which prevents action buttons and long values from being pushed off screen.

Action buttons are aligned consistently on mobile so controls such as "View Details" and delete/remove actions are easier to find. Header rows are kept where they help desktop scanning, while mobile rows use repeated labels inside each stacked record instead of relying on a crowded table header.

### 3.5 Wireframes

[Insert mobile wireframes/screenshots here.]

Suggested wireframes:

- Home page
- Product catalog
- Product details
- Cart
- Checkout
- Customer profile
- Admin dashboard

### 3.6 Use Case Diagram

[Insert use case diagram here.]

### 3.7 Activity Diagram

[Insert activity diagram here.]

Suggested activity flow:

1. Customer logs in.
2. Customer browses products.
3. Customer adds product to cart.
4. Customer checks out.
5. Order is created.
6. Admin updates order status.
7. Customer views order history and gives feedback.

---

## 4. Implementation

### 4.1 Technologies Used

- ASP.NET Web Forms
- C#
- .NET Framework 4.8
- SQL Server LocalDB
- HTML5
- CSS3
- Bootstrap
- JavaScript
- IIS Express

### 4.2 File And Folder Organization

- `Admin/` - administrator pages.
- `Customer/` - customer-only pages.
- `App_Data/` - database files.
- `Content/` - CSS and styling.
- `Images/Products/` - product images.
- `Scripts/` - JavaScript files.
- `Site.Master` - shared layout.
- `Web.config` - configuration and connection string.

### 4.3 Authentication And Authorization

Forms Authentication is used for login sessions. `Session["UserID"]` identifies the current account, while `Session["UserRole"]` separates customers from administrators. Admin users are redirected to `Admin/Dashboard.aspx`, and customer-only storefront pages redirect admins back to the dashboard. This keeps the admin area focused on management tasks instead of shopping features.

### 4.4 Product Catalog

The product catalog is implemented in `Products.aspx` and `Products.aspx.cs`. It loads categories from the database and can fall back to a preview catalog if the database is unavailable or incomplete. The catalog supports search, category filtering, stock filtering, badge filtering, and sorting. Product cards show the image, price, category, stock state, badges, wishlist controls, and a link to the product details page.

### 4.5 Shopping Cart And Checkout

The cart is stored in session as a `DataTable`, which allows products to be added before checkout. Checkout requires a logged-in customer, validates the shipping address, calculates the subtotal, coupon discount, loyalty redemption, and grand total, and then inserts the order and order-detail records inside a SQL transaction. The page also stores a confirmation message in session for the order-success page.

Payment support is implemented as a simulated coursework payment flow rather than a real payment gateway. The assignment brief states that online payment verification is not required, so the checkout page focuses on confirming the order. It calculates the payable amount, accepts shipping details, applies coupon and loyalty discounts, creates the order, deducts stock, awards loyalty points, and redirects the customer to an order-success page. A simulated notification is stored in session after checkout to represent the confirmation message that a production system would normally send by email or SMS.

Delivery status is handled through the order-management workflow. New orders start as `Pending`. Administrators can update the order status from the admin order page as the order moves through fulfillment. The application uses `Processing` and `Shipped` to represent preparation and sent-out-for-delivery states, `Completed` to represent delivered orders, and `Cancelled` for cancelled orders. Customers can view these updates from order history and order details, where the delivery timeline shows the current progress.

### 4.6 Admin Management

The admin area focuses on the core management tools: dashboard metrics, product CRUD, category CRUD, coupon management, order status management, and feedback/contact-message review. Customer storefront features such as cart, wishlist, loyalty badge, profile, and order history are hidden from admin navigation.

### 4.7 Feedback And Ratings

Customers submit a 1-5 rating and written comment from `Customer/OrderDetails.aspx`. The feedback form uses Web Forms validation and updates the existing feedback row if the customer edits a previous review for the same order. Admins can review and remove ratings, as well as homepage contact inquiries, from `Admin/ManageFeedback.aspx`.

### 4.8 Special Features

#### Wishlist

Customers can save products to a wishlist from product pages and review them later from `Customer/Wishlist.aspx`. Wishlist rows show the product image, category, price, a details shortcut, and a remove action.

#### Loyalty Points And Coupons

Customers earn loyalty points after checkout and can redeem points for a fixed discount when they have enough balance. Coupons are stored in the `Coupons` table and can be either percentage-based discounts or fixed RM discounts.

#### Order Tracking Timeline

Order details show a delivery timeline that changes styling based on the order status. Pending, processing, shipped, delivered, and cancelled states give customers a clearer view of what is happening after checkout.

#### Simulated Notifications

After checkout, the application stores a short notification message in session and displays it on the order success page. This simulates an order confirmation notification without needing an external email or SMS provider.

---

## 5. Testing And Evaluation

### 5.1 Test Plan

| Test Case | Steps | Expected Result | Actual Result |
|---|---|---|---|
| Register account | Fill registration form and submit | Account is created | Pending final manual test |
| Login member | Enter member credentials | Redirect to home/customer area | Pending final manual test |
| Add to cart | Add product from details page | Product appears in cart | Pending final manual test |
| Checkout | Enter shipping details and confirm | Order is created | Pending final manual test |
| View order history | Open order history page | Orders are listed | Pending final manual test |
| Submit feedback | Enter rating/comment | Feedback saved | Pending final manual test |
| Admin login | Enter admin credentials | Admin dashboard opens without customer navigation | Pending final manual test |
| Manage products | Add/edit/delete product | Product changes persist | Pending final manual test |
| Responsive layout | Test mobile viewport | Layout remains usable | Checked in Zen Browser, Microsoft Edge, and Google Chrome |

### 5.2 Browser Testing

Build and HTTP smoke testing were completed through IIS Express using `.\tools\Invoke-WebsiteChecks.ps1 -SmokeUrl http://localhost:64094/`. The website was also opened and checked manually in Zen Browser, Microsoft Edge, and Google Chrome. These tests confirmed that the main storefront pages, admin pages, navigation, product cards, and responsive table/card layouts were usable across Chromium-based and Firefox-based browsers.

The mobile view was checked using narrow browser widths to confirm that GridView tables did not push actions off screen. On mobile, table rows are displayed as stacked cards with labels for each field. On desktop, the same data still appears in table form for easier scanning.

### 5.3 Issues Encountered

- **Runtime compiler failure:** ASP.NET runtime compilation failed with compiler exit code `-1073741502`. The old Roslyn CodeDOM provider package was removed from `Web.config`, `packages.config`, and the project file so .NET Framework runtime compilation could work reliably.
- **Responsive GridView overflow:** GridView tables became overcrowded on mobile. A reusable `responsive-gridview` CSS pattern now converts tables into stacked cards below tablet width and hides repeated header rows.
- **Admin/customer feature separation:** Admin users originally had access to customer navigation such as cart, wishlist, profile, order history, and loyalty points. The master page now renders role-specific navigation, and customer pages redirect admin users back to the dashboard.
- **Duplicate template pages:** The stock `About.aspx` and `Contact.aspx` pages repeated content already found on the homepage, so they were removed from the project.

---

## 6. Conclusion

Golden Catch meets the main objectives of the assignment by providing a functional ASP.NET Web Forms e-commerce application with both customer and administrator workflows. Customers can browse canned fish products, filter the catalog, view product details, manage a cart, check out, track orders, save wishlist items, earn or redeem loyalty points, apply coupons, and submit feedback. Administrators can manage the store through dashboard metrics, product management, category management, order fulfillment, feedback/contact review, and coupon management.

The project also considers mobile layout and usability. Product cards, responsive navigation, stacked mobile GridView records, role-specific menus, and clear action placement make the application more usable on phone-sized screens than a default desktop-only Web Forms layout. Removing duplicate stock pages and old user-management features also helped keep the interface aligned with the current homepage and admin workflow.

The main limitation is that payment, notification delivery, and reporting are simulated rather than connected to external providers. This is suitable for a coursework prototype, but a production version would need payment gateway integration, real email/SMS delivery, stronger reporting, stricter inventory validation, and more complete security hardening.

Possible future improvements:

- Real payment gateway integration.
- Real email notification service.
- More detailed product recommendations.
- Better admin reporting charts.
- Coupon expiry dates and usage limits.
- Product image upload improvements.

---

## 7. User Manual

### 7.1 Setup Instructions

1. Open `MWM-Assignment-New.sln` in Visual Studio.
2. Ensure SQL Server LocalDB is installed.
3. Confirm `App_Data/myData.mdf` exists.
4. Restore NuGet packages if prompted.
5. Run using IIS Express.
6. Open the website in a browser.

### 7.2 Test Accounts

| Role | Username | Password |
|---|---|---|
| Admin | `admin` | `Admin@123` |
| Member | `member` | `Member@123` |

### 7.3 Customer Walkthrough

1. Register or log in.
2. Browse the product catalog.
3. View a product.
4. Add item to cart.
5. Proceed to checkout.
6. Enter shipping address.
7. Apply coupon or redeem points if available.
8. Confirm order.
9. View order history.
10. Submit feedback.

### 7.4 Admin Walkthrough

1. Log in as admin.
2. Open admin dashboard.
3. Manage products, categories, orders, feedback, and coupons.
4. Review dashboard metrics.
5. Update order statuses.

---

## 8. References

- Microsoft. (n.d.). *ASP.NET Web Forms*. Microsoft Learn. https://learn.microsoft.com/en-us/aspnet/web-forms/
- Microsoft. (n.d.). *What is Web Forms*. Microsoft Learn. https://learn.microsoft.com/en-us/aspnet/web-forms/what-is-web-forms
- Bootstrap. (n.d.). *Bootstrap documentation*. https://getbootstrap.com/docs/
- OpenAI. (n.d.). *Codex*. https://openai.com/codex

---

## 9. Appendices

### Appendix A: Screenshots

[Insert screenshots here.]

### Appendix B: ERD

[Insert ERD here.]

### Appendix C: Wireframes

[Insert wireframes here.]

### Appendix D: Source Code Highlights

#### Admin-Only Navigation And Customer Feature Isolation

The shared master page checks the session role before rendering navigation. Admin users see the management dropdown and an admin account menu, while customer links such as cart, wishlist, profile, order history, and loyalty points are hidden.

```csharp
bool isAdmin = Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin";
phStoreLinks.Visible = !isAdmin;
phAdminLinks.Visible = false;
phLoyaltyBadge.Visible = false;
phCustomerAccountLinks.Visible = false;
phAdminAccountLinks.Visible = false;

if (Context.User.Identity.IsAuthenticated)
{
    mvAuth.ActiveViewIndex = 1;
    litUsername.Text = Session["Username"]?.ToString() ?? Context.User.Identity.Name;

    if (isAdmin)
    {
        phAdminLinks.Visible = true;
        phAdminAccountLinks.Visible = true;
    }
    else
    {
        phLoyaltyBadge.Visible = true;
        phCustomerAccountLinks.Visible = true;
        litLoyaltyPoints.Text = Session["UserID"] == null
            ? "0"
            : LoyaltyService.SyncSession(connString, Session).ToString();
    }
}
```

#### Product Catalog Filtering And Sorting

The catalog builds a parameterized SQL query from category, search, stock, badge, and sort controls. This keeps the product page flexible while avoiding direct string insertion from user input.

```csharp
string query = @"SELECT p.*, c.CategoryName
FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID";
List<string> filters = new List<string>();
SqlCommand cmd = new SqlCommand();
cmd.Connection = con;

if (selectedCategory != "0")
{
    filters.Add("p.CategoryID = @CatID");
    cmd.Parameters.AddWithValue("@CatID", selectedCategory);
}

if (!string.IsNullOrWhiteSpace(search))
{
    filters.Add("(p.ProductName LIKE @Search OR p.Description LIKE @Search OR c.CategoryName LIKE @Search)");
    cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
}

if (filters.Count > 0)
{
    query += " WHERE " + string.Join(" AND ", filters);
}

query += " " + GetSortClause();
```

#### Checkout Transaction

Checkout creates the order, writes each order detail, reduces stock, applies loyalty points, clears discounts, and empties the cart inside one SQL transaction. If any step fails, the transaction is rolled back so the system does not save a partial order.

```csharp
SqlTransaction trans = con.BeginTransaction();

try
{
    SaveShippingAddress(con, trans, userId);

    string orderQuery = @"INSERT INTO Orders (UserID, OrderDate, TotalAmount, Status)
                         OUTPUT INSERTED.OrderID
                         VALUES (@UID, GETDATE(), @Total, 'Pending')";

    SqlCommand cmdOrder = new SqlCommand(orderQuery, con, trans);
    cmdOrder.Parameters.AddWithValue("@UID", userId);
    cmdOrder.Parameters.AddWithValue("@Total", grandTotal);
    int newOrderId = (int)cmdOrder.ExecuteScalar();

    foreach (DataRow row in dt.Rows)
    {
        string detailQuery = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice) VALUES (@OID, @PID, @Qty, @Price)";
        SqlCommand cmdDetail = new SqlCommand(detailQuery, con, trans);
        cmdDetail.Parameters.AddWithValue("@OID", newOrderId);
        cmdDetail.Parameters.AddWithValue("@PID", row["ProductID"]);
        cmdDetail.Parameters.AddWithValue("@Qty", row["Quantity"]);
        cmdDetail.Parameters.AddWithValue("@Price", row["Price"]);
        cmdDetail.ExecuteNonQuery();
    }

    LoyaltyService.ApplyOrderPoints(con, trans, userId, chkRedeemPoints.Checked, pointsEarned);
    trans.Commit();
}
catch (Exception)
{
    trans.Rollback();
    lblError.Text = "We could not place your order. Please review your cart and shipping details, then try again.";
}
```

#### Responsive GridView Cards

GridView tables stay compact on desktop, but switch to stacked mobile cards on small screens. Header rows are hidden on mobile because each cell receives its own label through CSS.

```css
@media (max-width: 767.98px) {
    .responsive-gridview .table thead,
    .responsive-gridview .table tr:has(> th) {
        display: none;
    }

    .responsive-gridview .table tr {
        margin-bottom: 1rem;
        border: 2px solid var(--site-line);
        border-radius: var(--site-radius);
        background: var(--site-surface);
        box-shadow: var(--site-shadow-sm);
    }

    .responsive-gridview .table td {
        display: grid;
        grid-template-columns: minmax(7.25rem, 38%) minmax(0, 1fr);
        gap: 0.75rem;
    }
}
```

#### Coupon Discounts

Coupons are checked against active records during checkout. The service also creates the coupon schema if it is missing, which helps the feature run in a fresh local database.

```csharp
internal static CouponInfo FindActiveCoupon(string connectionString, string code)
{
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
                if (!reader.Read()) return null;
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
```

#### Loyalty Points

The loyalty service keeps the points logic in one place. Orders can earn points and optionally redeem an existing balance for a discount.

```csharp
string query = redeemPoints
    ? @"UPDATE Users
SET LoyaltyPoints = LoyaltyPoints - @RedeemCost + @PointsEarned
WHERE UserID = @UserID AND LoyaltyPoints >= @RedeemCost"
    : @"UPDATE Users
SET LoyaltyPoints = LoyaltyPoints + @PointsEarned
WHERE UserID = @UserID";
```

#### Feedback And Contact Message Inbox

The homepage inquiry form stores visitor messages in `ContactMessages`, while order feedback is stored in `Feedbacks`. Admins can review both from the feedback page.

```csharp
ContactMessageService.EnsureSchema(con);
using (SqlCommand cmd = new SqlCommand(@"INSERT INTO ContactMessages
    (FullName, Email, Subject, Message)
    VALUES (@FullName, @Email, @Subject, @Message)", con))
{
    cmd.Parameters.AddWithValue("@FullName", txtContactName.Text.Trim());
    cmd.Parameters.AddWithValue("@Email", txtContactEmail.Text.Trim());
    cmd.Parameters.AddWithValue("@Subject", txtContactSubject.Text.Trim());
    cmd.Parameters.AddWithValue("@Message", txtContactMessage.Text.Trim());
    cmd.ExecuteNonQuery();
}
```
