# Playlist API tests

Run from the repository root:

```sh
dotnet test tests/CollaborativePlaylistBuilderAPI.Tests/CollaborativePlaylistBuilderAPI.Tests.csproj
```

These xUnit tests cover service validation and creation, controller success and
error responses, integer song IDs, and repository ID assignment. Service and
controller tests use a recording repository to keep their state isolated.
The repository test uses the real shared in-memory storage and checks relative
IDs rather than assuming storage starts empty.

Controller tests call action methods directly; they do not exercise HTTP routing,
JSON binding, automatic model validation, or middleware.
