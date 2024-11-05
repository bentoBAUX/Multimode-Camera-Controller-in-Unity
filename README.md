## Multimode Camera Controller in Unity

This project demonstrates a versatile camera controller for Unity that enables seamless switching between First-Person, Third-Person, and Real-Time Strategy (RTS) perspectives. Ideal for games with hybrid gameplay styles, this setup provides a flexible and customizable foundation for multi-perspective experiences.

## Table of Contents
- [Getting Started](#getting-started)
- [Controls](#controls)
- [How It Works](#how-it-works)
- [Customization](#customization)
- [Requirements](#requirements)
- [License](#license)

## Getting Started
Clone or download the project files and open it in Unity. This project is set up with Unity’s Input System, so make sure it’s installed and configured in your Unity project.

## Controls
- **Switch between First-Person and Third-Person**: Press '*C*' on your keyboard.
- **Switch to RTS Mode**: Press '*V*' on your keyboard to switch from either FPS or TPS to RTS mode.
Each mode is designed with unique functionality:

- **First-Person View**: Offers an immersive player perspective, common in FPS games.
- **Third-Person View**: Provides a full view of the player character, suitable for platformers and adventure games.
- **RTS View**: A top-down, bird’s-eye view, ideal for strategy and management games.

## How It Works
This camera controller leverages Unity's Cinemachine package (if using) and the Unity Input System for flexible and reliable camera switching. The setup includes:

- A singleton [InputHandler](https://github.com/bentoBAUX/Multimode-Camera-Controller-in-Unity/blob/master/Assets/Script/InputHandler.cs) to manage keybinds and input checks.
- Camera switching logic in [CameraManager](https://github.com/bentoBAUX/Multimode-Camera-Controller-in-Unity/blob/master/Assets/Script/CameraManager.cs) that adjusts the priorities of different CinemachineVirtualCamera components to control which camera is currently active. By modifying the priority, only the desired camera view is shown at any time, providing smooth transitions.
- Body components for CinemachineVirtualCamera that activate different views, including framing transposers for TPS and FPS, and top-down controls for RTS mode.

## Requirements
This project was made in Unity 2022.3.13f1 URP.
- Cinemachine
- Unity Input System package.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
