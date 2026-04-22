# Canned-Fish Vendor Domain Notes

## Product Examples

- Spicy Sardines in Tomato Sauce
- Tuna Chunks in Spring Water
- Mackerel in Olive Oil
- Premium Salmon Flakes
- Anchovies in Chili Oil
- Smoked Mussels in Brine

## Category Examples

- Sardines
- Tuna
- Mackerel
- Premium Seafood
- Spicy Varieties
- Family Packs
- Ready-to-Eat

## UI Copy Guidance

- Say "products", "canned fish", "seafood tins", "stock", "cart", "orders", and "customers".
- Avoid "keyboard", "switches", "keycaps", "mechanical", and hardware-specific terms in visible UI.
- Existing technical names such as `KeyboardShopDB`, `Products`, and `ProductName` can remain until a deliberate rename/migration is requested.

## Data Guidance

- Keep generic product fields: name, category, price, stock quantity, description, image path.
- Useful optional future fields: net weight, origin, flavor/sauce, expiry date, halal status, bundle size.
- Do not invent schema changes during copy/layout work unless the user asks for data-model changes.
