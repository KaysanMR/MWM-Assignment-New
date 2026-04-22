---
name: canned-fish-domain
description: Apply the canned-fish vendor domain model and user-facing language for this project. Use when changing product catalog copy, admin/customer workflows, seed/sample data, images, labels, navigation, checkout, wishlist, orders, categories, or when replacing old keyboard-shop assumptions without necessarily renaming database identifiers.
---

# Canned Fish Domain

## Domain Rule

Treat this application as a canned-fish vendor storefront. Keyboard-shop wording is legacy context unless it appears in existing database identifiers, connection strings, project names, or code paths that have not been migrated yet.

## Use This Vocabulary

- Products: canned fish, seafood tins, sardines, tuna, mackerel, salmon, anchovies, clams, mussels.
- Product attributes: species, flavor, sauce, pack size, net weight, can size, origin, stock, price, description, image.
- Categories: sardines, tuna, mackerel, premium seafood, spicy varieties, family packs, ready-to-eat.
- Customer flows: browse catalog, view product details, add to cart, wishlist, checkout, view orders, profile.
- Admin flows: manage products, categories, users, orders, feedback, inventory.

## Implementation Guidance

- Prefer copy changes and UI labels that align with the canned-fish vendor domain.
- Do not rename `KeyboardShopDB`, table names, project names, or namespaces unless the user specifically asks for a migration.
- If a schema field is generic, reuse it: `ProductName`, `CategoryID`, `Price`, `StockQuantity`, `Description`, `ImagePath`.
- If a schema field is keyboard-specific, pause and inspect usage before renaming; migrations can affect markup, code-behind, database files, and sample data.
- Use appetizing but practical product names, such as "Spicy Sardines in Tomato Sauce" or "Tuna Chunks in Spring Water".

## References

- Read `references/domain-notes.md` for reusable naming, page-copy, and sample-data guidance.
