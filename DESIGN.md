# Design System: The Editorial Sanctuary

## 1. Overview & Creative North Star
The Creative North Star for this design system is **"The Digital Archivist’s Clearing."** 

Unlike standard note-taking apps that feel like cold, digital filing cabinets, this system treats information as a living collection. We are moving away from the "app-grid" aesthetic toward a high-end editorial layout. We achieve this through **intentional asymmetry**—offsetting headers and using generous, breath-like margins—and **tonal depth**. The interface should feel like light filtering through a dense canopy: layered, organic, and quiet. By prioritizing soft transitions over harsh borders, we create a sanctuary for thought.

---

## 2. Colors & Surface Philosophy
The palette is rooted in the deep shadows of a forest floor, punctuated by the warmth of raw earth.

### The "No-Line" Rule
**Strict Mandate:** Designers are prohibited from using 1px solid borders for sectioning or containment. 
Structure must be defined solely through:
- **Background Shifts:** Placing a `surface-container-low` component against a `surface` backdrop.
- **Tonal Transitions:** Using the `surface-container` hierarchy to denote importance.
- **Negative Space:** Utilizing the spacing scale to create invisible boundaries.

### Surface Hierarchy & Nesting
Treat the UI as a series of physical layers—like stacked sheets of heavy, vellum paper.
*   **Base Layer (`surface` / `surface-dim`):** The forest floor. Use for the deepest background of the application.
*   **Secondary Layer (`surface-container-low`):** For sidebars or navigation drawers that sit "behind" the main content.
*   **Content Layer (`surface-container-high`):** For the primary note editor or reading pane.
*   **Elevated Layer (`surface-container-highest`):** For active modals or floating menus.

### The "Glass & Gradient" Rule
To evoke the "Aura" of the brand, floating elements (like FABs or active tooltips) should utilize **Glassmorphism**. Use semi-transparent versions of `surface-bright` with a `backdrop-blur` of 12px-20px. 
*   **Signature Texture:** Use a subtle linear gradient from `primary` (#a4d393) to `primary-container` (#0a3204) at a 135-degree angle for primary CTA backgrounds to provide a "lit-from-within" organic glow.

---

## 3. Typography: The Editorial Voice
We use **Manrope** exclusively. Its geometric yet humane construction bridges the gap between modern tech and classic typesetting.

*   **Display (lg/md):** Reserved for "Moment of Arrival" screens or notebook titles. Use `on-surface` (off-white) with `-0.02em` letter spacing to feel authoritative.
*   **Headlines:** The backbone of the "Editorial" feel. Use `headline-sm` for note titles within the editor to provide clear hierarchy without crowding the page.
*   **Body (lg/md):** Our "Sanctuary" text. Ensure `body-lg` is used for the actual note content to maximize readability. Use `on-surface-variant` for secondary notes to reduce visual noise.
*   **Labels:** Use `label-md` in `tertiary` (soft gold) for metadata like tags or timestamps. This acts as a "highlight" without the aggression of a standard notification color.

---

## 4. Elevation & Depth
In this system, depth is felt, not seen. We abandon traditional Material shadows for **Tonal Layering**.

*   **The Layering Principle:** Place a `surface-container-lowest` card on a `surface-container-low` section. This creates a soft "sunken" effect, perfect for archived notes or secondary feeds.
*   **Ambient Shadows:** For floating elements that require a "lift" (e.g., a floating formatting bar), use a multi-layered shadow:
    *   `0px 10px 30px rgba(0, 18, 8, 0.4)` (A tinted shadow using the `surface-container-lowest` hue).
*   **The Ghost Border Fallback:** If accessibility requires a stroke (e.g., input focus), use `outline-variant` at **20% opacity**. Never use 100% opaque outlines.
*   **Atmospheric Blur:** Use `surface-tint` sparingly as a background "glow" behind high-level containers to simulate light passing through leaves.

---

## 5. Components

### Buttons
*   **Primary:** A gradient-filled container (`primary` to `primary-container`) with `on-primary` text. Shape: `md` (0.375rem) for a grounded, architectural feel.
*   **Secondary:** No fill. Use `on-surface` text with a `Ghost Border` (20% `outline-variant`).
*   **Tertiary:** Text-only in `tertiary` (gold). Reserved for low-priority actions like "Cancel" or "Dismiss."

### Cards & Lists
*   **Forbid Dividers:** Do not use lines to separate notes in a list. Instead, use a `2.5` (0.85rem) spacing gap or a subtle shift from `surface-container-low` to `surface-container`.
*   **The "Leaf" Chip:** Use `secondary-container` for tags. The roundedness should be `full` to contrast against the more structured `md` cards.

### Note Editor (The Sanctuary)
*   **Input Fields:** Ghost-style. No background or borders by default. The `outline` token only appears as a soft 10% opacity glow on focus.
*   **Text Selection:** Use `secondary` (#ffb596) at 30% opacity for text highlights to mimic the color of clay-stained paper.

### Editorial Sidebar
*   Use `surface-container-lowest` to create a deep, recessed "drawer" for navigation. This makes the main `surface` content area appear as if it is floating toward the user.

---

## 6. Do’s and Don’ts

### Do
*   **DO** use white space as a structural element. If a section feels crowded, increase spacing using the `10` (3.5rem) or `12` (4rem) tokens.
*   **DO** use `tertiary-fixed` for "Gold" accents on interactive icons to guide the eye to the most important action.
*   **DO** ensure all text on `surface` backgrounds uses `on-surface` (off-white) for a soft-contrast reading experience.

### Don’t
*   **DON'T** use pure black (#000000) or pure white (#FFFFFF). This destroys the "Forest Sanctuary" immersion.
*   **DON'T** use `error` red for anything other than destructive actions. For "warnings," use `secondary` (clay) to maintain the organic tone.
*   **DON'T** use standard 8px grids. Use our custom **Spacing Scale** (0.35rem, 0.7rem, 1.4rem) to create an asymmetrical, "non-templated" rhythm.