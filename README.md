
# 🎮 About the Project

This project is a simple **crowd battle game**.

Two groups of units fight each other:

* basic combat system
* simple target selection logic

Units are represented as:

* different geometric shapes
* various colors
* different sizes
---

# 🧠 Project Architecture

This project is built using the **Entity Component System (ECS)** architecture with **LeoEcsLite**.

You can find more details about the architecture, modules, and tools here:
👉 [https://github.com/Akfi23/AkfiFramework](https://github.com/Akfi23/AkfiFramework)

---

## 🧩 Core Concept

Each unit is represented as an **Entity** that contains a set of components with data.
Game logic is implemented through **Systems**.

---

## 🎯 Entry Point

**Entry Point:** `ContextRoot.cs`

This is where:

* systems are initialized
* services are bound to the DI container

---

## 🔄 Game Flow & UI

Part of the game loop and UI logic is implemented using FSM via **NodeCanvas**.

### Why this approach?

* Allows fast iteration
* Convenient for both developers and game designers
* Provides a clear and visual way to control and tweak the game flow

It is mainly used for UI logic.

You can find the node-based implementation in:

* Component: `FSMOwner`
* Scene: `scene_context`
* Object: `p_context_root`

---

## 📦 Data & Configs

* Services: `Assets/_Source/Code/Services`
* Configs: `Assets/_Source/Databases/`

---

## 🧱 Spawning System

Object spawning is implemented using:

* `AKPoolService`
* Addressables

---

## 📌 Purpose

This project serves as a **minimal example** demonstrating how to use my LeoEcsLite Tools framework in a real scenario.
