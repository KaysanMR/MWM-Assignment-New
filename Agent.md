# Agent Notes

## Project Snapshot

- ASP.NET Web Forms application targeting .NET Framework 4.8.
- Business domain: canned fish vendor storefront. Treat old keyboard-shop wording as legacy unless it appears in technical identifiers that have not been migrated yet.
- Assignment brief reference: `Assignment Brief/CT081-3-3-Mobile and Web  Multimedia Assignment (1).md`.
- The assignment requires an individual mobile-optimized .NET e-commerce app with SQL Server `App_Data` storage, Forms Authentication/authorization, 10-20 interlinked pages, Web Forms user controls, advanced data-bound controls, validation, customer/admin flows, feedback, order delivery states, documentation, user manual, and `ReadMe.html` with grader setup/credentials.
- Solution: `MWM-Assignment-New.sln`.
- Project: `MWM-Assignment-New.csproj`.
- Main UI pages live at the repo root, with admin pages in `Admin/` and customer pages in `Customer/`.
- Shared layout is in `Site.Master` and `Site.Mobile.Master`.
- Shared CSS is in `Content/Site.css`; Bootstrap 5.2.3 and jQuery 3.4.1 are checked into `Content/`, `Scripts/`, and `packages/`.
- Local database is `App_Data/myData.mdf`, connected through the legacy `KeyboardShopDB` connection name in `Web.config`.
- Authentication uses ASP.NET Forms Authentication plus session values such as `UserID`, `UserRole`, and `Cart`.

## Current Git State

- Branch observed during inventory: `update-styling-v1`.
- Existing modified files before these notes were added:
  - `Admin/Dashboard.aspx`
  - `Customer/Profile.aspx`
- Treat those as user work unless explicitly told otherwise.

## Build And Check Commands

Use the helper script:

```powershell
.\tools\Invoke-WebsiteChecks.ps1
```

Watch and rebuild after edits:

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -Watch
```

Start IIS Express and smoke-test the site:

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -StartIisExpress
```

Build plus a running-site smoke check:

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -SmokeUrl https://localhost:44357/
```

Run checks against a different configuration, platform, or solution:

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -Configuration Release -Platform "Any CPU" -Solution MWM-Assignment-New.sln
```

The script auto-locates Visual Studio MSBuild. The known local path is:

```text
C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe
```

Baseline build result from inventory: build passed. The app now targets .NET Framework 4.8 to match the Bootstrap ScriptManager package.

There is no dedicated test project in the current solution. For now, "test" means compile the Web Forms project and optionally smoke-test a running local URL.

## Search Workflow

The repo has a `.ignore` file so `rg` skips generated and vendored folders.

```powershell
rg --files
rg -n 'Session\[|SqlConnection|Response.Redirect' .
```

When searching file contents from PowerShell, include `.` as the path to avoid shell/glob surprises.

## Development Notes

- Prefer existing Web Forms patterns: `.aspx` markup, `.aspx.cs` code-behind, and `.designer.cs` generated control declarations.
- Avoid editing `.designer.cs` manually unless regenerating controls is impossible.
- Keep database access consistent with the current `System.Data.SqlClient` style, but use parameterized SQL for new queries.
- Admin-only pages should verify `Session["UserRole"] == "Admin"` before loading protected data.
- Customer-only pages should verify `Session["UserID"]` before loading user data.
- The catalog page is `Products.aspx`; the code-behind class is named `ProductGallery`.
- Do not commit local database files, `bin/`, `obj/`, or Visual Studio state unless specifically asked.

## UI Style Guide

- Keep new UI elements consistent with the existing Golden Catch neobrutalist style: cream surfaces, black 2px borders, chunky rounded corners, offset black shadows, bold readable labels, and the established blue/orange/green accent colors from `Content/Site.css`.
- Do not let utility controls sprawl across the full page width unless the workflow genuinely benefits from it. Search, filters, admin forms, and action groups should use intentional responsive columns with sensible `max-width` constraints.
- Keep primary workflows visible and secondary controls tucked into collapsible or clearly grouped panels when they would clutter a browsing page. For product listing filters, prefer visible Search/Category controls and an expandable Filters panel for stock, sort, badge, apply, and clear actions.
- Buttons, checkboxes, toggles, and form controls should have enough padding to feel tactile and should share the same border/shadow treatment. Avoid tiny native controls when a custom styled control is already used nearby.
- On mobile, controls may stack full width; on tablet and desktop, favor compact columns that match the user’s diagram or nearby page patterns.

## Useful Codex Skills

- `webforms-crud`: use for admin/customer CRUD pages, GridView handlers, and SQL data-binding work.
- `webforms-debug`: use for build, runtime, designer, ScriptManager/UpdatePanel, LocalDB, and Web Forms lifecycle issues.
- `webforms-layout`: use for Web Forms markup, Bootstrap layout, master-page consistency, and responsive page presentation.
- `canned-fish-domain`: use whenever changing user-facing copy, sample data, product/category naming, or replacing legacy keyboard-shop assumptions.
- `imagegen` / `generate-image`: useful if the shop needs canned fish product art, hero images, banners, or visual mockups.
- `github:gh-address-comments`: useful if this work later moves through GitHub pull request review.
- `skill-installer`: useful if we decide to install a web/frontend-specific skill; no ASP.NET Web Forms skill was found in the local skill inventory.
- `docx`, `PowerPoint`, `Excel`: useful only for assignment deliverables, reports, slide decks, or spreadsheet analysis around the project.

## Inventory Summary

- Public/customer flow: default page, product listing/details, cart, checkout, order success, login, register, profile, wishlist, order history.
- Admin flow: dashboard, categories, feedback/contact messages, orders, products, and coupons.
- Assets: product images are stored under `Images/Products/`.
- App startup: `Global.asax.cs`, `App_Start/BundleConfig.cs`, and `App_Start/RouteConfig.cs`.
- Error handling: custom 404 page configured in `Web.config`.

## Remaining Work Notes

These are the visible follow-ups found during the April 28, 2026 pass.

### Confirmed placeholders to replace

- `Default.aspx`: replace the hero image placeholder (`hero-image-placeholder`, "Hero product image coming soon.") with a real Golden Catch product/brand visual.
- `Content/Site.css`: remove or repurpose `.hero-image-placeholder` styling after the homepage hero is replaced.
- `About.aspx` / `Contact.aspx`: removed because the homepage owns the about and contact sections.
- `MockCatalog.cs`: four mock products still use `~/Images/Products/placeholder-tin.svg` (`Anchovies in Chili Oil`, `Smoked Mussels in Brine`, `Family Pack Sardines in Tomato Sauce`, `Sambal Tuna Spread`). Add final product images or accept the generic fallback deliberately.
- `README.md`: intro now uses Golden Catch/canned fish wording. Keep `KeyboardShopDB` only where it refers to the legacy connection string.
- `Assignment Draft/assignment-report-draft.md`: fill bracketed report placeholders, especially product listing, feedback/ratings, browser testing, development issues, and test results.

### Mobile UI cleanup checklist

- Run a real mobile viewport pass at roughly 360px, 390px, 430px, 768px, and desktop width.
- Check the collapsed navbar, account/admin dropdowns, and full-width mobile buttons.
- Check homepage hero after the image replacement; make sure headline, CTAs, image, store locator, map, and contact form do not overlap or create horizontal scroll.
- Check `Products.aspx` filters: search/category should stack cleanly, advanced filter menu should not overflow, and product cards should keep image/name/price/actions aligned.
- Check tables on small screens: cart, wishlist, order history/details, admin products, users, categories, orders, feedback, and coupons now use the `responsive-gridview` stacked-card pattern; still needs visual browser verification at phone widths.
- Check form-heavy pages: checkout, register, login, customer profile, feedback/rating, and admin create/edit forms should have comfortable touch targets and no cramped validation messages.
- Check admin dashboard cards/charts/sidebar at tablet and phone sizes; these are likely to need spacing adjustments after the current dashboard/profile edits settle.

### Content and asset polish

- Decide whether `Images/Product Photos/` and `Images/Products/` both remain in use, or consolidate final catalog assets into one predictable folder.
- Verify all product image paths render when the app is deployed under a virtual directory; prefer `ResolveUrl` or server-bound image paths where needed.
- Confirm the store map/address is intentionally fictional or replace it with the final assignment/business location.
- Review product/category/coupon copy for canned-fish language; avoid reintroducing keyboard-shop wording except in legacy technical identifiers.
- Consider replacing the default `favicon.ico` if it does not match the Golden Catch brand.

### Submission/documentation checks

- Verify `ReadMe.html` setup steps, seed database assumptions, and demo credentials work from a clean checkout.
- Confirm `App_Data/myData.mdf` exists and contains the expected admin/member accounts, categories, products, coupons, orders, wishlist/cart scenarios, and feedback examples.
- Re-run `.\tools\Invoke-WebsiteChecks.ps1` after each markup/control change.
- Before submission, run a browser smoke test for public pages, customer flow, admin flow, invalid validation cases, checkout, order status, and feedback/rating.
