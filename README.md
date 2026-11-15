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

## Album Inventory — Vitoria Resende

### Features
- Add, find, update, and delete albums using gRPC.
- Display available albums using server streaming.
- Refresh available albums approximately every 3 seconds using
  a bidirectional gRPC stream.
- Validate required fields and year.
- Display errors for missing albums and connection problems.

### Run the module
1. Use Windows 11, Visual Studio, the .NET 8 SDK, and
   the .NET Framework 4.7.2 targeting pack.
2. Restore NuGet packages and build the solution.
3. Set AlbumInventoryMicroserviceAPI and dev422_midterm
   as multiple startup projects, both with action Start.
4. Run and trust the development HTTPS certificate if prompted.
5. Wait for the server to listen at https://localhost:7112.
6. Click Album Inventory in the VibeHive menu.

### Notes
- Albums are stored in memory and reset when the server restarts.
- The table displays only available albums.
- Find by ID can retrieve unavailable albums.
- After a connection failure, restart the server and reopen
  the album screen to resume automatic updates.
- The client and server share Protos/album_service.proto
  through a linked file.

## Collaborative Playlist Builder — Jesse Wattenhofer

### Features

- Register an account, then log in using cookie authentication.
- Create playlists and songs, and add songs to playlists.
- Invite registered users to edit collaborative playlists.
- Vote for songs and view playlists ranked by vote count.
- Refresh playlists and rankings automatically every 5 seconds.
- Load the song library when opened and refresh it after creating a song.

### Run the module

1. Restore NuGet packages and build the solution using the .NET 8 SDK
   and the .NET Framework 4.7.2 targeting pack on Windows.
2. Start CollaborativePlaylistBuilderAPI using its **http** launch profile,
   or run this command from the repository root:
   `dotnet run --project CollaborativePlaylistBuilderAPI --launch-profile http`.
3. Wait for the API to listen at http://localhost:5123.
   HTTPS and a development certificate are not required for this profile.
4. Start dev422_midterm and click Collaborative Playlist Builder
   in the VibeHive menu.
5. Register an account, then click Login to enter the playlist builder.

### Notes

- Users, songs, playlists, and votes are stored in memory and reset
  when the API restarts.
- Registration does not log the user in. Login establishes the session,
  and signing out discards the client's authentication cookie.
- Only the owner or an invited collaborator can add songs to a playlist.
  Only the owner can invite collaborators, and the playlist must be collaborative.
- Each user can vote once per song in each playlist.
- The API address is set by PlaylistApiBaseUrl in dev422_midterm/App.config.
