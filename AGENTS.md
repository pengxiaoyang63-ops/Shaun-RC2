# AGENTS.md - Shaun RC2 / GAL2025-2026

> This file contains essential information for AI coding agents working on this Unity 2D action game project.

---

## Project Overview

**Shaun RC2** (also referenced as **GAL2025-2026**) is a Unity 2D action platformer game with visual novel-style story elements. The project combines fast-paced combat mechanics with narrative-driven gameplay sections.

- **Engine**: Unity 2022.3.48f1c1 (LTS)
- **Project Type**: 2D Game (Universal Render Pipeline)
- **Target Framework**: .NET Standard 2.1
- **Language**: C# 9.0
- **Primary Language**: Chinese (comments, documentation), English (code identifiers)

---

## Technology Stack

### Core Unity Packages
| Package | Version | Purpose |
|---------|---------|---------|
| Unity 2D Feature | 2.0.1 | Core 2D functionality (sprites, tilemaps, physics) |
| Universal Render Pipeline | 14.0.11 | 2D rendering pipeline |
| Cinemachine | 2.10.5 | Advanced camera control and screen shake |
| Input System | 1.11.2 | New Unity input handling (note: project also uses legacy Input) |
| TextMeshPro | 3.0.7 | Text rendering |
| Visual Scripting | 1.9.4 | Node-based scripting support |

### Development Tools
- **IDE**: Visual Studio Code (configured via `.vscode/`)
- **Solution Files**: Multiple `.sln` files exist (`Shaun-RC2.sln`, `GAL2025-2026.sln`, etc.)
- **Build System**: Unity's internal build pipeline with Bee backend

---

## Project Structure

```
Assets/
├── RC1 Assets/                    # Combat gameplay system
│   ├── MainCharcter/Script/       # Player character controller
│   ├── YX warrier/                # Boss enemy (YX Warrior) AI and behaviors
│   ├── Script/                    # Camera, utilities, key bindings
│   ├── Decorations/               # Visual effects and particles
│   ├── Effects/                   # Particle effects
│   └── Materials/                 # Shader materials
├── 雪山前/                        # Story/visual novel system ("Before Snow Mountain")
│   ├── 代码/                      # Story system scripts
│   └── ... (art assets)
├── Resources/                     # Runtime-loaded assets
│   └── TextAssets/                # CSV data files (story, art locations)
├── Scenes/                        # Game scenes
│   ├── SampleScene.unity          # Main combat scene
│   ├── FT1.unity                  # Another gameplay scene
│   └── 山洞.unity                 # Cave scene
├── Settings/                      # URP and render settings
└── Tile/                          # Tilemap assets
```

---

## Code Organization

### Two Main Gameplay Systems

#### 1. Combat System (`RC1 Assets/`)
Player action combat with platforming mechanics.

**Key Scripts**:
| Script | Purpose |
|--------|---------|
| `MainCharacterController.cs` | Core player controller (movement, jumping, attacking) |
| `MainCharacterHealth.cs` | Player health management, death handling |
| `Movement.cs` | Alternative/refactored movement system (includes dash, wall jump) |
| `GroundChecker.cs` / `PreGroundChecker.cs` | Ground detection for platforming |
| `DamageCollider.cs` | Attack hitbox management |
| `YXwarriermove.cs` | Boss AI movement and attack patterns |
| `YXWarrierHealth.cs` | Boss health system |
| `Animation Behaviors/` | State machine behaviors for animations |

**Controls (Combat)**:
- `A/D`: Move left/right
- `Space`: Jump (hold for higher jump)
- `J`: Attack (ground/air combos)
- `S+J`: Downward attack (in air)
- `W+J`: Upward attack
- `K`: Alternative jump key (in newer system)
- `R`: Reset position

#### 2. Story System (`雪山前/代码/`)
Visual novel-style dialogue and narrative system.

**Key Scripts**:
| Script | Purpose |
|--------|---------|
| `PlotPlayer.cs` | CSV story data loader and parser |
| `StoryUI.cs` | Story trigger zones and camera targets |
| `StoryUILogOut.cs` | Story playback controller, choice handling |
| `ArtLoaderBehaviors.cs` | Character illustration asset management |
| `BackgroundLocations.cs` | Background image management |
| `CameraControl.cs` | Story-mode camera (follow vs. fixed positions) |
| `CharacterInteractions.cs` | NPC interaction handling |
| `Characters.cs` | Character data structure (health, states) |

**Story Data Format**:
Stories are loaded from CSV files in `Resources/TextAssets/`:
- `CSV test`: Main story data
- `CSV ArtLocations`: Character illustration paths

CSV columns: Chapter, Scene, Clip, Number, Name, Illustration, IllState, Background, Text, Re1-4, Goto1-4

**Controls (Story)**:
- `1/2/3/4`: Select dialogue choices

---

## Input System

The project uses a **hybrid input approach**:
1. **Legacy Input Manager**: Primary input via `Input.GetKey()` calls
2. **ScriptableObject Keybinds**: `keyBindConfig.cs` provides configurable key bindings

Default key mappings:
- Movement: `A`/`D` or arrow keys
- Jump: `Space` or `K`
- Attack: `J`
- Reset: `R`
- Pause/Restart: `Escape` (debug), `Space` (after death)

---

## Development Conventions

### Code Style
- **Class Names**: PascalCase (English)
- **Variables**: camelCase
- **Public Fields**: Exposed to Unity Inspector for configuration
- **Comments**: Primarily in Chinese (GB2312 encoding - may display as garbled in some editors)

### Architecture Patterns
1. **MonoBehaviour-based**: All gameplay scripts inherit from MonoBehaviour
2. **Singleton Pattern**: Used for managers (`PlotPlayer.Instance`, `ArtLoaderBehaviors.Instance`, `CameraShake.instance`)
3. **ScriptableObject**: Used for keybind configuration (`keyBindConfig`)
4. **State Machine**: Animator-driven with `StateMachineBehaviour` scripts

### Animation System
- Uses Unity's Animator controller with parameters:
  - `RunSpeed`: Float (controls movement animation speed)
  - `Idle`: Bool
  - `Jump`: Int (0=ground, 1=jumping, 2=falling, 3=landed)
  - `ATK`: Int (attack state)
  - `Hitted`: Trigger (damage reaction)

---

## Build and Test

### Building
1. Open project in Unity 2022.3.48f1c1
2. Build Settings: File → Build Settings
3. Target Platform: Standalone Windows (x64) - default
4. Build output: Standard Unity build process

### Testing
- **Test Framework**: Unity Test Framework 1.1.33 (included but no visible test files)
- **Manual Testing**: Play in Editor, scenes under `Assets/Scenes/`
- **Frame Rate**: Locked to 120 FPS via `Application.targetFrameRate = 120`

### Scene Flow
1. `SampleScene.unity` - Main combat/boss fight scene
2. `FT1.unity` - Secondary gameplay scene
3. `山洞.unity` - Story/cave scene

---

## Key Technical Details

### Physics
- Uses **2D Physics** (`Rigidbody2D`, `Collider2D`)
- Gravity scale: 5-6f (player), 3f (boss)
- Fixed timestep: Default (1/500f in some scripts)

### Rendering
- **URP 2D Renderer** configured
- Color space: Linear (`m_ActiveColorSpace: 1`)
- Default resolution: 1920x1080

### Time Management
- Slow-motion effects implemented via `Time.timeScale`
- Impact freeze: 0.3s freeze + 0.08s slow-motion
- Uses `WaitForSecondsRealtime` for real-time delays

---

## File Encoding Notice

> ⚠️ **Important**: Some source files use **GB2312/GBK encoding** (Chinese Windows default). Comments may appear garbled in UTF-8 editors. When editing files with Chinese comments, preserve the original encoding or convert carefully.

---

## Git Configuration

### Ignored Files (`.gitignore`)
```
UserSettings/
Temp/
Library/
Logs/
.vscode/
*.psd                    # Large Photoshop files
Packages/packages-lock.json
Assembly-CSharp.csproj   # Auto-generated
```

### Version Control Notes
- `meta` files are tracked (Unity YAML asset references)
- `Library/` is excluded (can be regenerated)
- Large binary assets (.psd) are excluded from git

---

## Common Tasks for Agents

### Adding a New Story Scene
1. Create CSV data in `Resources/TextAssets/`
2. Add `StoryUI` trigger zones in scene
3. Configure `PlotPlayer` with CSV reference
4. Set up `CameraControl` for scene angles

### Adding a New Enemy
1. Create Animator Controller with states
2. Add `StateMachineBehaviour` scripts for AI logic
3. Implement health class (follow `YXWarrierHealth` pattern)
4. Add collision detection for attacks

### Modifying Player Controls
1. Update `MainCharacterController.cs` or `Movement.cs`
2. Check both files - project has overlapping systems
3. Update `keyBindConfig` ScriptableObject if adding configurable keys

---

## External Dependencies

- **ReSharper/JetBrains Annotations**: Some files use `JetBrains.Annotations` (optional)
- **Visual Studio Tools for Unity**: VS Code extensions configured

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Chinese comments garbled | Open with GB2312/GBK encoding |
| Missing script references | Check `meta` files are tracked in git |
| Build failures | Delete `Library/` and `Temp/`, reopen Unity |
| Input not working | Check both legacy and new Input System are configured |

---

*Last updated: 2026-03-01*
*Project: Shaun RC2 / GAL2025-2026*
