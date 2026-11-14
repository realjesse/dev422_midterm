using AlbumInventoryMicroserviceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Enable gRPC.
builder.Services.AddGrpc();

var app = builder.Build();

// Register our album service.
app.MapGrpcService<AlbumService>();

app.MapGet("/", () => "Album Inventory gRPC service is running.");

app.Run();