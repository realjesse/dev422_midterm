using CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;
using CollaborativePlaylistBuilderAPI.Features.Playlists.InviteCollaborator;
using CollaborativePlaylistBuilderAPI.Features.Playlists.CreatePlaylist;
using CollaborativePlaylistBuilderAPI.Features.Playlists.GetPlaylists;
using CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;
using CollaborativePlaylistBuilderAPI.Features.Playlists.VoteForSong;
using CollaborativePlaylistBuilderAPI.Features.Songs.CreateSong;
using CollaborativePlaylistBuilderAPI.Features.Songs.GetSongs;
using CollaborativePlaylistBuilderAPI.Repositories;
using CollaborativePlaylistBuilderAPI.Features.Users.Login;
using CollaborativePlaylistBuilderAPI.Features.Users.Register;
using CollaborativePlaylistBuilderAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserStore, InMemoryUserStore>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddRegister();
builder.Services.AddLogin();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "PlaylistBuilder.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
            ? CookieSecurePolicy.SameAsRequest
            : CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = false;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
        options.Events.OnValidatePrincipal = context =>
        {
            var userStore = context.HttpContext.RequestServices.GetRequiredService<IUserStore>();
            var id = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(id, out var userId) || userStore.GetById(userId) is null)
            {
                context.RejectPrincipal();
            }

            return Task.CompletedTask;
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IPlaylistRepository, InMemoryPlaylistRepository>();
builder.Services.AddCreatePlaylist();
builder.Services.AddInviteCollaborator();
builder.Services.AddVoteForSong();
builder.Services.AddSongFeature();
builder.Services.AddGetRankings();
builder.Services.AddGetPlaylists();
builder.Services.AddSingleton<ISongRepository, InMemorySongRepository>();
builder.Services.AddCreateSong();
builder.Services.AddGetSongs();
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

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
