# TowerDefense

A small tower defense game prototype built with Unity.  
This project explores the core mechanics of a classic tower defense game: path-based enemies, placeable towers, and wave management.

---

## 🎮 Overview

**TowerDefense** is a learning / prototype project that implements the basic systems you would expect in a tower defense game:

- Enemies that follow a predefined path.
- Buildable tower spots.
- Towers that automatically attack enemies in range.
- Wave-based spawning and difficulty scaling.
- Simple resource and health management.

The project is intended as a starting point for experimenting with tower defense mechanics, gameplay balancing, and general Unity architecture.

---

## ✨ Key Features

- **Path-following enemies** – Enemies move along a set path from spawn to goal.
- **Automatic targeting** – Towers detect enemies in range and fire at them.
- **Multiple tower types (extensible)** – Code is structured so you can easily add new towers with different stats or behaviors.
- **Wave system** – Enemies spawn in waves with configurable counts and timing.
- **Basic game state flow** – Start, in-game, and game-over logic.
- **Unity-friendly architecture** – Uses standard Unity patterns with components, prefabs, and scriptable configurations.

---

## 🧱 Project Structure

At a high level, the project follows a typical Unity layout:

- `Assets/`  
  - **Scenes** – Main game scene(s) for running the tower defense level.  
  - **_Scripts** – C# scripts for towers, enemies, wave management, UI, and core gameplay logic.  
  - **_Prefabs** – Reusable towers, enemies, and environment pieces.  
  - **_Models / _Materials** – Materials, textures, and shaders used by the project.
- `Packages/`  
  Unity package configuration (managed automatically by Unity/Unity Hub).
- `ProjectSettings/`  
  Unity project configuration: input, graphics, quality settings, etc.

---

## 🧰 Requirements

- **Unity** – Any recent Unity version that can open the project (ideally an LTS release).  
- **Git** (optional) – To clone the repository.

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mangelsr/TowerDefense.git
cd TowerDefense
