# Pokémon Battle Simulator (C# WinForms)

A turn-based Pokémon-style battle game built with C# and Windows Forms.
The game features a graphical interface, multiple battle levels, and a save/load system.

---

## 🎮 Features

* Character selection system
* Turn-based battle mechanics
* Multiple attack types (Light, Medium, Heavy)
* Type effectiveness system (Fire, Water, Grass, Electric, etc.)
* Critical hits and miss chances
* Heal ability (limited use per battle)
* Multiple levels with increasing difficulty
* Save & Load game system (3 slots + auto-save)
* GUI built with Windows Forms

---

## 🖥️ Technologies Used

* C#
* .NET (WinForms)
* Windows Forms (GUI)

---

## ▶️ How to Run

### Option 1 — Using Visual Studio (Recommended)

1. Open the project in **Visual Studio**
2. Open the file:

   ```
   PokemonBattleSimulatorGUI.sln
   ```
3. Click **Run (▶️)**

---

### Option 2 — Run Executable

1. Download the project or release
2. Navigate to:

   ```
   bin/Debug/netX.X-windows/
   ```
3. Run:

   ```
   PokemonBattleSimulatorGUI.exe
   ```

⚠️ Make sure these folders/files are in the same directory:

* Images/
* BattleImages/
* MenuImages/
* SelectionImages/
* intro.wav

---

## 📂 Project Structure

* `MainMenuForm` → Main menu (Play, Continue, Load, Rules)
* `CharacterSelectionForm` → Choose your Pokémon
* `BattleForm` → Core battle system
* `BattleSystem` → Damage, type logic, critical hits
* `GameManager` → Game state & level progression
* `SaveManager` → Save/load system
* `Pokemon` → Data model
* `PokemonFactory` → Pokémon list generator

---

## ⚠️ Requirements

* Windows OS
* .NET Desktop Runtime
* Visual Studio (for development)

---

## 🚀 Future Improvements

* Animations for attacks
* Better UI styling
* More Pokémon and abilities
* Sound effects for attacks
* Difficulty scaling system

---

## 📌 Notes

This project is a student-built application for learning purposes and demonstrating GUI programming, object-oriented design, and game logic in C#.

---

## 👤 Author

Luka Nergadze
