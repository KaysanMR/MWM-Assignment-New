# Page Patterns

## Admin Pages

- Use clear page headings and concise management forms.
- Put wide GridViews inside `.table-responsive`.
- Use Bootstrap button variants consistently: primary for save/add, secondary for cancel/back, danger for delete.
- Keep status labels near the action that produced them.

## Customer Pages

- Lead with product clarity: image, product name, price, stock state, and primary action.
- Use canned-fish wording in labels and headings.
- Keep cart, wishlist, checkout, and order pages easy to scan on mobile.

## Master Page

- Use `Site.Master` for shared desktop layout and navigation.
- Use `Site.Mobile.Master` only when intentionally changing the mobile-specific master.
- Preserve content placeholder IDs and Web Forms server-control IDs used by code-behind.
