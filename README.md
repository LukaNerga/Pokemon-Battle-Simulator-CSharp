# Pokemon Type Battle Simulator (C# WinForms)

A simple turn-based Pokémon battle game built using **C# and Windows Forms**.  
Choose your Pokémon, battle enemies with different types, and progress through levels to defeat the final boss.

---

## Features

- Choose from 10 different Pokémon
- Turn-based battle system (Light / Medium / Heavy attacks)
- Type effectiveness system (Fire, Water, Grass, Electric, etc.)
- Heal option (once per battle)
- HP bars and battle log
- Manual save system with 3 rotating slots
- Continue last saved game
- Rules screen
- Win/Lose result screen

---

## How to Play

1. Click **Play New** to start a new game
2. Select a Pokémon
3. Battle enemies in order:
   - Level 1: Fire
   - Level 2: Water
   - Level 3: Grass
   - Final Boss: Pikachu ⚡
4. Use:
   - Light Attack (low damage, low miss chance)
   - Medium Attack (balanced)
   - Heavy Attack (high damage, high miss chance)
5. Use **Heal** once per battle
6. Defeat all enemies to win

---

## Save System

- Game saves manually using **Save Game** button
- Uses **3 rotating slots**:
  - Slot 1 = newest save
  - Slot 2 → Slot 3
  - Slot 1 → Slot 2
- **Continue** loads the last saved game
- **Load Game** lets you choose a slot

---

## Game Mechanics

### Type Advantage

| Attack Type | Strong Against | Weak Against |
|------------|---------------|-------------|
| Fire       | Grass         | Water       |
| Water      | Fire          | Grass       |
| Grass      | Water         | Fire        |
| Electric   | Water         | —           |

- Strong: **1.5x damage**
- Weak: **0.75x damage**

### Critical Hits
- 15% chance
- Deals **1.5x damage**

### Miss Chance
- Light: 10%
- Medium: 20%
- Heavy: 35%

---

## Project Structure
Forms/ → UI screens (menus, battle, selection)
GameLogic/ → battle system and game manager
Models/ → data classes (Pokemon, SaveData)
Factories/ → Pokemon creation
SaveSystem/ → saving and loading logic

---

## Technologies Used

- C#
- .NET WinForms
- JSON Serialization (System.Text.Json)

---

## How to Run

1. Open the project in **Visual Studio**
2. Build the solution
3. Run the project

---

## Game Screens

<p align="center">
  <img src="https://github.com/user-attachments/assets/d233c781-3c3b-4941-b0e8-356d9a38ff25" width="400"/>
  <img src="https://github.com/user-attachments/assets/21f9b46c-3115-4905-9ad8-ad7453f86266" width="400"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/3db8c84f-501a-4e86-9593-46aaec849130" width="400"/>
  <img src="https://github.com/user-attachments/assets/7a1001af-1648-43b2-8c46-2e344a908d96" width="400"/>
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/1417a58c-8762-44f7-a970-ae45167cfdae" width="400"/>
</p>

---

## Notes

- This project was created for learning purposes (Compe-361 Final Project)
- Focused on object-oriented design and UI development

---

## 👤 Author

Luka Nergadze, Papuna Gorgodze
