# ![unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white) Screen Flow

[![Unity Version](https://img.shields.io/badge/Unity-2022.3+-blue.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/License-MIT-green.svg)]([LICENSE.md](https://github.com/LLarean/screen-flow/blob/main/LICENSE))
![Development Status](https://img.shields.io/badge/Status-In%20Development-orange)
![Build](https://img.shields.io/badge/Build-Passing-brightgreen)

⚠️ WARNING: This project is in early development stage. The API is subject to major changes, functionality is incomplete. Not recommended for production use.

*Elegant screen management system for Unity with MVP architecture and code generation*

## Overview

ScreenFlow is a powerful Unity package that simplifies UI screen management through a clean MVP (Model-View-Presenter) architecture. Designed for developers who are tired of copy-pasting the same screen management code across projects.

### Key Features

- **MVP Architecture**: Clean separation of concerns with automated binding
- **Smart Navigation**: Intuitive screen stack management with history support
- **Code Generation**: Auto-generate Screen-Presenter-Model classes from templates
- **Smooth Transitions**: Built-in animation system with customizable effects
- **Modular Design**: Use only what you need, extend what you want
- **Editor Tools**: Visual screen configuration and debugging tools

## Quick Start

```csharp
// Define a screen

// Auto-generated presenter

// Navigate between screens
```

## Installation

- 

### Via Package Manager (Recommended)

- 

### Via Unity Package Manager (Local)

- 

## Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Screen View   │◄──►│   Presenter     │◄──►│     Model       │
│   (MonoBehaviour│    │   (Logic)       │    │   (Data)        │
│    + UI refs)   │    │                 │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                        │                        │
         └────────────────────────┼────────────────────────┘
                                  ▼
                      ┌─────────────────┐
                      │ Screen Manager  │
                      │ (Navigation)    │
                      └─────────────────┘
```

## Core Components

### ScreenManager
Central navigation hub that manages the screen stack and transitions.

### BaseScreen<T>
Base class for all screens with lifecycle management and presenter binding.

### BasePresenter<T>
Base presenter class with view reference and common functionality.

### Code Generator
Editor tools for automatically creating Screen-Presenter-Model templates.

## Roadmap

- [ ] Core screen management system
- [ ] MVP architecture implementation  
- [ ] Basic navigation (Push/Pop/Replace)
- [ ] Code generation templates
- [ ] Basic transition animations
- [ ] Editor window for screen configuration
- [ ] Advanced animation system
- [ ] Screen preloading and caching
- [ ] Dependency injection integration
- [ ] Async/await navigation support
- [ ] Screen lifecycle debugging tools
- [ ] Custom transition effects editor
- [ ] Screen analytics and metrics
- [ ] Multi-canvas support
- [ ] Memory optimization tools
- [ ] Screen A/B testing framework
- [ ] Advanced inspector tools
- [ ] Performance profiling dashboard

## Use Cases

- **Mobile Games**: Menu systems, popups, game state screens
- **Desktop Applications**: Multi-window navigation, settings panels
- **VR/AR Projects**: Spatial UI management, context switching
- **Prototyping**: Quick screen mockups and navigation flow testing

## Contributing

🚧 Coming Soon: Contribution guidelines will be available once the core architecture is established.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/LLarean/screen-flow?tab=MIT-1-ov-file) file for details.

## Links

- [Documentation](...) *(coming soon)*
- [API Reference](...) *(coming soon)*
- [Examples](...) *(coming soon)*

