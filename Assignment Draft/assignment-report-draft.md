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

Golden Catch is a mobile-optimized e-commerce web application for browsing and purchasing canned fish products such as sardines, tuna, mackerel, and premium seafood tins. The application is developed using ASP.NET Web Forms and SQL Server LocalDB.

The project is designed to demonstrate an interactive and dynamic mobile web multimedia application with customer-facing shopping features and administrator-facing management tools.

### 1.2 Project Objectives

- Develop a functional e-commerce website using .NET technologies.
- Provide customer registration, login, profile management, product browsing, cart, checkout, order history, and feedback features.
- Provide administrator tools to manage users, products, categories, orders, and feedback.
- Store and retrieve application data using a SQL Server database in the `App_Data` folder.
- Demonstrate mobile-friendly layout, multimedia product presentation, and clear navigation.
- Include optional engagement features such as wishlist, loyalty points, coupons, stock badges, and order tracking.

### 1.3 Target Users

- **Customers:** users who browse canned fish products, add items to cart, checkout, view order history, and submit feedback.
- **Administrators:** staff members who manage products, categories, users, orders, feedback, stock, and dashboard summaries.

### 1.4 Project Scope

The project covers a small online canned fish storefront with customer and admin workflows. It includes product listing, shopping cart, checkout, order management, feedback/rating, profile management, and selected gamification or retention features.

### 1.5 Development Timeline

| Phase | Activities | Week |
|---|---|---|
| Planning | Topic selection, requirements review, proposal outline | [Week] |
| Design | ERD, wireframes, navigation structure, UI direction | [Week] |
| Core Development | Registration, login, catalog, cart, checkout | [Week] |
| Admin Development | Manage users, products, categories, orders, feedback | [Week] |
| Enhancements | Wishlist, loyalty points, coupons, tracking, dashboard metrics | [Week] |
| Testing & Documentation | Browser testing, validation, report, user manual | [Week] |

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
- Manage users.
- Manage products.
- Manage categories.
- Manage orders and delivery statuses.
- View and remove customer feedback.
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

[Describe the ASP.NET Web Forms architecture here. Mention `.aspx` pages, code-behind files, master pages, SQL Server database, session state, Forms Authentication, and Web.config.]

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
- Manage Users
- Manage Products
- Manage Categories
- Manage Orders
- Manage Feedback

### 3.4 User Interface Design

[Describe the mobile-first layout, color palette, typography, product cards, dashboard cards, and responsive behavior.]

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

[Explain Forms Authentication, login flow, session values such as `UserID` and `UserRole`, and protected customer/admin pages.]

### 4.4 Product Catalog

[Explain product listing, category filtering, product cards, stock badges, and product details.]

### 4.5 Shopping Cart And Checkout

[Explain session cart, checkout process, order creation, order details, address fields, payment method, coupon discounts, and loyalty points.]

### 4.6 Admin Management

[Explain admin dashboard, user management, product CRUD, category CRUD, order management, feedback management, and dashboard metrics.]

### 4.7 Feedback And Ratings

[Explain how customers can submit ratings/comments from order details and how admins can view/manage feedback.]

### 4.8 Special Features

#### Wishlist

[Explain wishlist behavior.]

#### Loyalty Points And Coupons

[Explain points earning, point redemption, and coupon codes.]

#### Order Tracking Timeline

[Explain status visualization.]

#### Simulated Notifications

[Explain checkout confirmation notification.]

---

## 5. Testing And Evaluation

### 5.1 Test Plan

| Test Case | Steps | Expected Result | Actual Result |
|---|---|---|---|
| Register account | Fill registration form and submit | Account is created | [Fill in] |
| Login member | Enter member credentials | Redirect to home/customer area | [Fill in] |
| Add to cart | Add product from details page | Product appears in cart | [Fill in] |
| Checkout | Enter shipping details and confirm | Order is created | [Fill in] |
| View order history | Open order history page | Orders are listed | [Fill in] |
| Submit feedback | Enter rating/comment | Feedback saved | [Fill in] |
| Admin login | Enter admin credentials | Admin dashboard opens | [Fill in] |
| Manage products | Add/edit/delete product | Product changes persist | [Fill in] |
| Responsive layout | Test mobile viewport | Layout remains usable | [Fill in] |

### 5.2 Browser Testing

[List browsers tested, e.g. Microsoft Edge, Chrome, Firefox.]

### 5.3 Issues Encountered

[Describe development issues, such as database constraints, relative URL routing, responsive layout adjustments, and how they were fixed.]

---

## 6. Conclusion

[Summarize what the project achieved, how it meets the assignment objectives, limitations, and possible future improvements.]

Possible future improvements:

- Real payment gateway integration.
- Real email notification service.
- More detailed product recommendations.
- Better admin reporting charts.
- Persistent loyalty/coupon database tables.
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
3. Manage users, products, categories, orders, and feedback.
4. Review dashboard metrics.
5. Update order statuses.

---

## 8. References

[Add APA references here.]

Examples:

- Microsoft. (n.d.). ASP.NET Web Forms documentation. [URL]
- Bootstrap. (n.d.). Bootstrap documentation. [URL]
- Microsoft. (n.d.). SQL Server LocalDB documentation. [URL]

---

## 9. Appendices

### Appendix A: Screenshots

[Insert screenshots here.]

### Appendix B: ERD

[Insert ERD here.]

### Appendix C: Wireframes

[Insert wireframes here.]

### Appendix D: Source Code Highlights

[Insert selected code snippets and explanations here.]
