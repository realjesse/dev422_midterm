using System.Text.Json.Serialization;

namespace CollaborativePlaylistBuilderAPI.Models;

public record User(Guid Id, string Username, [property: JsonIgnore] string PasswordHash);
