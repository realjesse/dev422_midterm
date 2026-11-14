using System.Collections.Concurrent;
using Grpc.Core;


namespace AlbumInventoryMicroserviceAPI.Services
{
    public class AlbumService : AlbumInventory.AlbumInventoryBase
    {
        // Keep albums while the server runs.
        private static readonly ConcurrentDictionary<int, Album> albums = new();
        private static int nextId = 0;

        // Add an album with a new ID.
        public override Task<Album> AddAlbum(
            Album request, ServerCallContext context)
        {
            CheckAlbum(request);

            Album album = request.Clone();
            album.Id = Interlocked.Increment(ref nextId);

            albums.TryAdd(album.Id, album);

            return Task.FromResult(album);
        }

        // Find an album by ID.
        public override Task<Album> GetAlbum(
            AlbumIdRequest request, ServerCallContext context)
        {
            if (!albums.TryGetValue(request.Id, out Album? album))
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, "Album not found."));
            }

            return Task.FromResult(album);
        }

        // Update an existing album.
        public override Task<Album> UpdateAlbum(
            Album request, ServerCallContext context)
        {
            CheckAlbum(request);

            if (!albums.TryGetValue(request.Id, out Album? currentAlbum))
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, "Album not found."));
            }

            Album updatedAlbum = request.Clone();

            if (!albums.TryUpdate(request.Id, updatedAlbum, currentAlbum))
            {
                throw new RpcException(
                    new Status(StatusCode.Aborted,
                        "Album changed. Please try again."));
            }

            return Task.FromResult(updatedAlbum);
        }

        // Remove an album.
        public override Task<DeleteAlbumResponse> DeleteAlbum(
            AlbumIdRequest request, ServerCallContext context)
        {
            if (!albums.TryRemove(request.Id, out _))
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, "Album not found."));
            }

            return Task.FromResult(new DeleteAlbumResponse
            {
                Message = "Album deleted."
            });
        }

        // Stream available albums one at a time.
        public override async Task ListAlbums(
            ListAlbumsRequest request,
            IServerStreamWriter<Album> responseStream,
            ServerCallContext context)
        {
            foreach (Album album in albums.Values)
            {
                context.CancellationToken.ThrowIfCancellationRequested();

                if (album.Available)
                {
                    await responseStream.WriteAsync(album);
                }
            }
        }

        // Send available albums for each refresh request.
        public override async Task WatchAlbums(
            IAsyncStreamReader<ListAlbumsRequest> requestStream,
            IServerStreamWriter<AlbumList> responseStream,
            ServerCallContext context)
        {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                AlbumList response = new AlbumList();

                foreach (Album album in albums.Values)
                {
                    if (album.Available)
                    {
                        response.Albums.Add(album);
                    }
                }

                await responseStream.WriteAsync(response);
            }
        }

        // Validate album details.

        private static void CheckAlbum(Album album)
        {
            if (string.IsNullOrWhiteSpace(album.Title) ||
                string.IsNullOrWhiteSpace(album.Artist) ||
                string.IsNullOrWhiteSpace(album.Genre))
            {
                throw new RpcException(
                    new Status(StatusCode.InvalidArgument,
                        "Title, artist, and genre are required."));
            }

            if (album.Year <= 0)
            {
                throw new RpcException(
                    new Status(StatusCode.InvalidArgument,
                        "Enter a valid year."));
            }
        }
    }
}