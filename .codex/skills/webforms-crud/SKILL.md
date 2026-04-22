---
name: webforms-crud
description: Build, modify, or review ASP.NET Web Forms CRUD features in this project. Use when working on admin/customer .aspx pages, .aspx.cs code-behind, GridView edit/delete/update handlers, data binding, SQL Server LocalDB access, product/category/order/user management, or canned-fish vendor catalog operations.
---

# Web Forms CRUD

## Core Workflow

1. Read `Agent.md`, the target `.aspx`, `.aspx.cs`, and neighboring pages before editing.
2. Keep the existing Web Forms pattern: markup in `.aspx`, behavior in `.aspx.cs`, and generated declarations in `.designer.cs`.
3. Avoid manual `.designer.cs` edits unless control generation is impossible; if manual edits are needed, keep them minimal and consistent.
4. Use `ConfigurationManager.ConnectionStrings["KeyboardShopDB"].ConnectionString` until the project connection name is renamed.
5. Use `System.Data.SqlClient` and parameterized SQL for every new or changed query.
6. Use the canned-fish vendor vocabulary from `../canned-fish-domain/references/domain-notes.md`.
7. Run `.\tools\Invoke-WebsiteChecks.ps1` after behavior changes when feasible.

## CRUD Pattern

- Guard admin pages with `Session["UserRole"]?.ToString() != "Admin"` before loading protected data.
- Guard customer pages with `Session["UserID"]` before loading private data.
- Put initial binding inside `if (!IsPostBack)`.
- Name bind methods after the entity, such as `BindProducts()`, `BindCategories()`, or `BindOrders()`.
- Set GridView `DataKeyNames` for update/delete operations and read keys with `GridView.DataKeys[e.RowIndex].Value`.
- Reset `GridView.EditIndex = -1` after successful updates or canceled edits.
- Rebind after insert, update, delete, and edit-state changes.
- Show user-facing success/failure messages through existing labels or alerts, but do not expose raw exception details in polished flows.

## Data Access

- Prefer `using` blocks for `SqlConnection`, `SqlCommand`, and readers/adapters.
- Prefer explicit parameter types when changing fragile numeric/date fields; existing `AddWithValue` can be preserved in small local edits.
- Validate and parse user input before SQL execution.
- Keep table and column names aligned with the current database even if the UI language changes from keyboard shop to canned fish vendor.
- For uploaded product images, save under the existing product image folder unless the project has already introduced a new seafood-specific path.

## References

- Read `references/gridview-patterns.md` when adding or repairing GridView CRUD handlers.
- Read `../canned-fish-domain/references/domain-notes.md` when naming labels, sample data, product fields, or user-facing copy.
