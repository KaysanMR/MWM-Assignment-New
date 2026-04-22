# GridView CRUD Patterns

## Add/Edit/Delete Checklist

- Markup has `AutoGenerateColumns="False"` and `DataKeyNames` set to the primary key.
- Command fields or template buttons use the correct command names: `Edit`, `Update`, `Cancel`, `Delete`, or a custom `CommandName`.
- Markup event attributes match code-behind handler names.
- Code-behind sets `EditIndex`, rebinds, reads template controls with `FindControl`, validates values, executes parameterized SQL, resets `EditIndex`, and rebinds again.
- Delete handlers should confirm the key from `DataKeys`; product image deletion should verify the physical path maps under the intended product image folder.

## Binding Pattern

Use `SqlDataAdapter` with `DataTable` for GridView binding when following existing pages. Use `SqlCommand.ExecuteReader()` for dropdown binding.

Keep initial data loading inside:

```csharp
if (!IsPostBack)
{
    BindCategories();
    BindProducts();
}
```

## Canned-Fish Product Notes

Keep database fields generic unless asked to migrate schema. Product UI can describe canned fish through labels and text while continuing to store data in `Products`, `Categories`, `ProductName`, `StockQuantity`, and `Description`.
