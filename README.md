# Battleship – Mobile Game

A mobile implementation of the classic Battleship game, built with Unity and deployed as an Android APK.

---

## Installation (Android)

### 1. Build the APK

- Open the project in **Unity**
- Go to **File → Build Settings**
- Select **Android** as the target platform
- Click **Build** to generate the APK file

### 2. Transfer the APK to Your Device

Choose one of the following methods:

- Connect your phone via USB cable and copy the file
- Send the APK via email, cloud storage, or another file-sharing service

### 3. Enable Unknown Sources

- Open **Settings → Security** (location may vary by device)
- Enable installation from **unknown sources** if prompted

### 4. Install & Launch

- Locate the APK file on your device and tap it
- Follow the on-screen installation instructions
- Find the **Battleship** icon in your app list and open it

---

## Gameplay Overview

### Starting the Game

When the game launches, two 10×10 boards are displayed:

- **Player board** – your ships are visible
- **Bot board** – the bot's ships are hidden

Ships are automatically placed on both boards at the start.

### Player Turn

Tap any cell on the **bot's board** to attack:

- **Hit** – the cell updates to show a successful strike
- **Miss** – the cell updates to show a missed shot

### Bot Turn

After each player attack, the bot automatically selects a cell on the **player's board** and the result is displayed.

### Sinking Ships

Each ship's health equals its size. A ship sinks when all of its cells have been hit (health reaches zero).

### Winning

The game ends when all ships of one fleet are destroyed:

- All **bot ships sunk** → Player wins
- All **player ships sunk** → Bot wins

---

## Controls

| Action         | Input                                   |
| -------------- | --------------------------------------- |
| Attack a cell  | Tap on a cell on the bot's board        |
| Start game     | Launch the application                  |
| Play next turn | Wait for the bot's move after attacking |

---

## Technologies

- **Unity Engine** with **C#**
- **Android APK** deployment
- **Physics Raycasting** for touch input detection
- **Object-Oriented Programming** principles

---

## Project Structure

| Class           | Responsibility                                 |
| --------------- | ---------------------------------------------- |
| `GameManager`   | Controls game flow and turn management         |
| `Grid`          | Stores board cells in a 10×10 matrix           |
| `Cell`          | Represents an individual board field           |
| `Fleet`         | Manages a collection of ships                  |
| `Ship`          | Handles ship state, occupied cells, and health |
| `ShipPlacement` | Automatically places ships on the board        |
| `InputHandler`  | Processes player touch input                   |
| `BotController` | Performs bot attacks and turn logic            |
