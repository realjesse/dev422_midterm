using CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;
using CollaborativePlaylistBuilderAPI.Features.Playlists.VoteForSong;
using CollaborativePlaylistBuilderAPI.Repositories;
using CollaborativePlaylistBuilderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IPlaylistRepository, InMemoryPlaylistRepository>();
builder.Services.AddScoped<PlaylistService>();
builder.Services.AddVoteForSong();
builder.Services.AddSongFeature();
builder.Services.AddSingleton<ISongRepository, InMemorySongRepository>();
builder.Services.AddScoped<SongService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName ?? type.Name);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
