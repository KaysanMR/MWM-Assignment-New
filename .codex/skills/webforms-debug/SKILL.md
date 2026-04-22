---
name: webforms-debug
description: Diagnose and fix ASP.NET Web Forms build, runtime, IIS Express, database, designer, ScriptManager, UpdatePanel, Forms Authentication, session, and page lifecycle issues in this project. Use when errors mention controls, GridView events, missing declarations, Web.config, LocalDB, master pages, validation, or broken canned-fish vendor pages.
---

# Web Forms Debug

## Triage Workflow

1. Read `Agent.md` first for the current build command, known framework target, and local conventions.
2. Reproduce or inspect the smallest failing surface: target page markup, code-behind, designer declarations, `Web.config`, and related master page.
3. Classify the failure as compile-time, runtime server error, client/AJAX issue, data issue, or auth/session routing issue.
4. Fix the root cause with the smallest project-consistent change.
5. Run `.\tools\Invoke-WebsiteChecks.ps1` after code changes when feasible. Use `-StartIisExpress` or `-SmokeUrl https://localhost:44357/` for runtime checks when needed.

## Common Checks

- Control errors: ensure `.aspx` `ID` values match code-behind references and `.designer.cs` declarations.
- Event errors: ensure handler names in markup match methods in `.aspx.cs`.
- Namespace/class errors: compare the page directive `Inherits` with the code-behind namespace and partial class name.
- Master/content errors: ensure `ContentPlaceHolderID` values exist in `Site.Master`.
- Validation errors: check `UnobtrusiveValidationMode` when Web Forms validation controls behave oddly.
- AJAX errors: ensure only one compatible `ScriptManager` is active for the rendered page and inspect `UpdatePanel` triggers.
- SQL errors: verify `KeyboardShopDB` in `Web.config`, table/column names, parameter names, and LocalDB attachment paths.
- Auth loops: inspect Forms Authentication settings plus `Session["UserRole"]` and `Session["UserID"]` guards.

## Canned-Fish Context

- Treat references to keyboard products as legacy naming unless they are database identifiers or connection names.
- Prefer user-facing fixes that speak in canned-fish vendor terms: canned fish, seafood, tins, species, pack size, stock, price, orders, customers.
- Do not rename database identifiers casually during debugging; separate cosmetic copy fixes from schema migrations.

## References

- Read `references/check-commands.md` for the verification command map.
- Read `../canned-fish-domain/references/domain-notes.md` before changing copy or labels while debugging.
