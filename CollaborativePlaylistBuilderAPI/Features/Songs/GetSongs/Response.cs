namespace CollaborativePlaylistBuilderAPI.Features.Songs.GetSongs;

public record Response(int Id, string Title, string Artist, string Genre, string Duration, int Votes);
