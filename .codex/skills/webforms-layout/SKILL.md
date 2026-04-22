---
name: webforms-layout
description: Improve ASP.NET Web Forms page layout, Bootstrap styling, master-page integration, responsive forms, tables, cards, navigation, validation messages, and canned-fish vendor UI copy in this project. Use when editing .aspx markup, Site.Master, Site.Mobile.Master, Content/Site.css, or admin/customer page presentation.
---

# Web Forms Layout

## Layout Workflow

1. Read the target `.aspx`, `Site.Master`, `Content/Site.css`, and a nearby page with the desired pattern.
2. Preserve Web Forms server controls and event wiring while improving markup structure.
3. Use Bootstrap 5.2.3 classes already present in the project before adding custom CSS.
4. Keep server-side IDs stable unless the code-behind is updated in the same change.
5. Use canned-fish vendor terminology for visible copy.
6. Build with `.\tools\Invoke-WebsiteChecks.ps1` when markup/control changes touch server controls.

## UI Standards

- Use responsive `.container`, `.row`, `.col-*`, `.table-responsive`, `.card`, `.btn`, `.form-control`, and spacing utilities consistently.
- Keep admin pages dense and scannable: clear headings, compact filters/forms, responsive GridViews, visible actions.
- Keep customer pages warmer and product-focused: product images, species/product names, price, stock state, cart/wishlist/order affordances.
- Do not put layout-only text on screen explaining implementation details.
- Avoid heavy one-color themes; use a clean food-retail feel with enough contrast for product images and tables.
- Ensure long product names, pack sizes, and descriptions wrap cleanly on mobile.

## Web Forms Markup Rules

- Keep `<asp:Content>` blocks matched to the master page placeholders.
- Preserve `runat="server"` on server controls.
- Do not convert server controls to plain HTML if code-behind depends on them.
- Prefer `HeaderStyle`, `RowStyle`, templates, and Bootstrap classes for GridView presentation.
- When adding validation controls, ensure `ControlToValidate` matches an existing server control ID.

## References

- Read `references/page-patterns.md` for page-specific layout guidance.
- Read `../canned-fish-domain/references/domain-notes.md` for product wording and content assumptions.
