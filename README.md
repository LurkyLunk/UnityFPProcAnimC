# Procedural FPS Controller

The Procedural FPS Controller is a Unity project that provides a first-person shooter (FPS) controller with fully procedurally generated animations, health and ammo pickups, and systems, featuring original sounds and assets.

## Features

- Smooth and responsive first-person movement and aiming controls.
- Procedurally generated animations for realistic player movements and weapon handling.
- Health and ammo pickup objects to replenish player resources.
- Health and ammo systems that manage player's health and ammunition.
- Original sound effects and assets to enhance the gameplay experience.

## Installation

1. Clone or download this repository to your local machine.

2. Open the project in Unity.

3. In the Unity Editor, navigate to the **Scenes** folder.

4. Open the main game scene to start working with the FPS controller.

## Usage

1. Attach the FPS controller scripts to your player character:
   - Select your player character object in the Unity Scene view.
   - Drag and drop the following scripts from the **Scripts** folder onto your player character object:
     - `FPController.cs`
     - `FootStepController.cs`
     - `PlayerHealth.cs'
    - Drag and drop the following scripts from the **Scripts** folder onto your player gun object:
     - `Gun.cs`
     - `GunFireAnimation.cs`
     - 'DisarmController.cs'
     - `GunAnimation.cs'
     - 'GunCameraBasedAnimation'

2. Customize the FPS controller and game parameters:
   - Adjust variables exposed in the **Inspector** window of the attached scripts to modify player movement speed, weapon handling, health and ammo pickup behavior, etc.
   - Import your own sound effects and assets by replacing the existing ones in the **Assets** folder.

3. Set up your game levels and add health and ammo pickups at appropriate locations.

4. Press the **Play** button in Unity to test and play the game with the FPS controller.


## Contact

If you have any questions or feedback, please feel free to contact the project owner at [email@example.com](mailto:email@example.com).
