# Music Management Application

## Overview

This project is a **.NET 8 solution** that combines a Windows Forms desktop application, two Web APIs, and a gRPC microservice.

The Windows Forms application serves as the main graphical user interface (GUI), allowing users to interact with all three backend services from one centralized application.

## Solution Structure

The solution contains four projects:

```text
Solution/
│
├── dev422_midterm/
│   └── Windows Forms Application (Main GUI)
│
├── MusicRentalManagementAPI/
│   └── Music Rental Management Web API
│
├── CollaborativePlaylistBuilderAPI/
│   └── Collaborative Playlist Builder Web API
│
└── AlbumInventoryMicroserviceAPI/
    └── Album Inventory gRPC Microservice
```

### 1. dev422_midterm (Windows Forms Application)

Contributor: Everyone

This is the main desktop application that users will interact with.

The application will have a **main menu with three buttons**, each navigating to a separate GUI for interacting with one of the backend services:

- **Music Rental Management** — Interacts with `MusicRentalManagementAPI`.
- **Collaborative Playlist Builder** — Interacts with `CollaborativePlaylistBuilderAPI`.
- **Album Inventory** — Interacts with `AlbumInventoryMicroserviceAPI`.

Within the Windows Forms project, separate folders will be used to organize the forms and UI-related code for each service. This keeps each GUI component independent and makes collaboration easier.

### 2. MusicRentalManagementAPI (Web API)

Contributor: Shayla Rohrer

An ASP.NET Core Web API responsible for music rental management functionality.

The Windows Forms application communicates with this API through HTTP requests.

### 3. CollaborativePlaylistBuilderAPI (Web API)

Contributor: Jesse Wattenhofer

An ASP.NET Core Web API responsible for collaborative playlist functionality.

The Windows Forms application communicates with this API through HTTP requests.

### 4. AlbumInventoryMicroserviceAPI (gRPC Microservice)

Contributor: Vitoria Resende

A gRPC-based microservice responsible for album inventory functionality.

Unlike the other two APIs, this service uses **gRPC** for communication with the Windows Forms application.

## Application Architecture

The Windows Forms application acts as the frontend, while the APIs and microservice handle their respective backend functionality.

```text
            Windows Forms Application
                (dev422_midterm)
                       |
                  Main Menu
                       |
          +------------+------------+
          |            |            |
     Music Rental   Collaborative  Album
         GUI        Playlist GUI   Inventory GUI
          |            |            |
          | HTTP       | HTTP       | gRPC
          |            |            |
     MusicRental   Collaborative   AlbumInventory
     ManagementAPI PlaylistBuilder MicroserviceAPI
```

Each GUI section is responsible for communicating with its corresponding backend service.

## Development Workflow

To avoid merge conflicts and maintain a stable `main` branch, all team members should work on **separate feature branches**.

### Branching Guidelines

1. Pull the latest changes from `main` before starting work.
2. Create a new branch for your feature or assigned component.
3. Make your changes and commit them to your branch.
4. Push your branch to GitHub.
5. Open a **Pull Request (PR)** to merge your changes into `main`.
6. Have the PR reviewed before merging.

**Do not push directly to `main`. All changes must go through a Pull Request.**

### Example Git Workflow

```bash
# Switch to main and pull latest changes
git checkout main
git pull origin main

# Create a new feature branch
git checkout -b feature/music-rental-gui

# Make changes, then stage and commit
git add .
git commit -m "Add music rental GUI"

# Push your branch to GitHub
git push -u origin feature/music-rental-gui
```

After pushing, create a Pull Request on GitHub targeting the `main` branch.

## Running the Application

1. Clone the repository.
2. Open the `.sln` solution file in Visual Studio.
3. Ensure the **.NET 8 SDK** and required Visual Studio workloads are installed.
4. Restore NuGet packages.
5. Configure and start the two Web APIs and the gRPC microservice.
6. Start the `dev422_midterm` Windows Forms application.
7. Use the main menu to navigate to the desired service GUI.

**Note:** The backend services must be running and accessible for their corresponding Windows Forms GUI functionality to work.