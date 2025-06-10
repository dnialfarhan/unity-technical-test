# Unity Card Game Technical Test

This project is a technical test for a modular card game system in Unity. It demonstrates ScriptableObject-driven card data, dynamic hand management, interactive card UI, and ability execution with feedback.

## Video Presentation

[Watch the presentation video on Google Drive](https://drive.google.com/file/d/1w65j6msLFNRGMe14CCEsr6GbwiiJYsut/view?usp=sharing)

---

## Features

- **ScriptableObject Card Data:**  
  All card stats, visuals, and abilities are defined as `CardSO` assets for easy editing and extensibility.

- **Dynamic Hand Management:**  
  The `Hand` script manages drawing, clearing, and selecting cards, as well as wiring up UI buttons for card actions.

- **Interactive Card UI:**  
  Each card in the hand uses `CardDisplayUI` for displaying stats, handling hover/click pop effects, and showing details.

- **Card Isolation View:**  
  Clicking "View Details" opens an enlarged, detailed view of the card using `IsolationCardDisplayUI`, with pop effect on hover.

- **Ability Execution:**  
  Cards can execute abilities (damage, heal, stun, spawn) and display results in the UI.

---

## Project Structure

```
Assets/
  Scripts/
    CardSO.cs                # ScriptableObject for card data
    Card.cs                  # Card logic and ability execution
    CardDisplayUI.cs         # Handles card UI and interaction in hand
    IsolationCardDisplayUI.cs# Handles detailed card view UI
    Hand.cs                  # Manages the player's hand and card selection
```

---

## How to Use

1. **Setup Card Data:**
   - Create new `CardSO` assets via `Assets > Create > ScriptableObjects > Card`.
   - Fill in card stats, visuals, and ability parameters in the Inspector.

2. **Hand & UI Setup:**
   - Assign your card prefab (with `CardDisplayUI` attached) to the `Hand` script in the scene.
   - Assign UI buttons for "Execute" and "View Details" to the `Hand` script.
   - Assign the `IsolationCardDisplayUI` reference in your card prefab or via the manager.

3. **Running the Game:**
   - On play, the hand draws cards from the pool.
   - Click a card to select it; action buttons become active.
   - Use "Execute" to perform the card's ability and see feedback.
   - Use "View Details" to open the isolation view for the selected card.

---

## Key Scripts

### `CardSO.cs`
Defines all card data, including stats, visuals, and ability parameters.  
Uses enums for rarity, origin, evolution, and type.

### `CardDisplayUI.cs`
Handles card UI in the hand, including:
- Displaying stats and icons
- Hover/click pop effects
- Selection logic
- Executing abilities and showing results

### `IsolationCardDisplayUI.cs`
Shows a detailed, enlarged view of a card with all stats and ability info.  
Includes pop effect on hover.

### `Hand.cs`
Manages the player's hand:
- Drawing and clearing cards
- Tracking selection
- Wiring up UI buttons to the selected card

### `Card.cs`
Executes card abilities and builds result descriptions for the UI.

---

## Customization

- **Add new card types:**  
  Extend the `CardType` enum and update `Card.cs` and UI scripts as needed.
- **Add new visuals or stats:**  
  Add fields to `CardSO` and update UI scripts to display them.

---

## Time breakdown

- Card ScriptableObject Mechanic - 1 Hour
- Card System Mechanics and Manager - 2 Hours
- UI/UX Implementation - 1 Hour
- Testing & documentation - 20 - 30 minutes

---

## Outcomes

- **Gained an Introduction to Card Game Mechanics:**  
  Explored the fundamentals of card game systems, which were new and unfamiliar to me prior to this project.

- **Learned and Applied ScriptableObjects:**  
  Used ScriptableObjects for card data management, providing a flexible and scalable alternative to traditional classes.

- **Designed and Implemented Character Abilities:**  
  Setting up character abilities was both fun and challenging. I am interested in further exploring how to support multiple abilities per character card.

- **Improved UI/UX Design:**  
  Enhanced the overall user interface and user experience, focusing on clarity and interactivity.

- **Implemented a Training Dummy:**  
  Added a training dummy feature, allowing character cards to test and demonstrate their abilities in real time.

- **Explored Better Card Creation Workflows:**  
  Investigated and iterated on more efficient and robust methods for creating and managing character cards.

---

## Requirements

- Unity 2021.3 or newer (recommended)
- TextMeshPro package (for UI text)

---

## License

This project is for technical demonstration and educational purposes.
