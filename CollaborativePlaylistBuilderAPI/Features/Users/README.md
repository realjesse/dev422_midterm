# User registration and login

Each use case has its own request, endpoint, response, handler, and dependency
registration. `Register` writes to `IUserStore`; `Login` reads credentials from
the store, and its endpoint issues the authentication cookie. No mediator is
required, matching the existing feature slices.

Run the API locally using the HTTP development profile (no certificate needed):

```sh
dotnet run --project CollaborativePlaylistBuilderAPI --launch-profile http
```

## Register

`POST /api/users/register`

```json
{
  "username": "jesse",
  "password": "ExamplePassword123!"
}
```

Returns `201` with `{ "id": "<guid>", "username": "jesse" }`.
Usernames are trimmed, case-insensitive, unique, and 3–50 characters long.
Passwords are 8–128 characters long and are never trimmed. Missing or invalid
input returns `400`; a duplicate username returns `409`.
Only salted password hashes from ASP.NET Core Identity's `PasswordHasher<User>`
are stored. Passwords and hashes are never included in response DTOs.

## Login

`POST /api/users/login` with the same JSON fields.

Returns `200` with the user ID and username, plus a `PlaylistBuilder.Auth` cookie.
Invalid credentials return `401` with the same message for unknown usernames
and incorrect passwords. Missing or oversized fields return `400`.

The encrypted cookie is HttpOnly, SameSite=Strict, and expires after one hour
without renewal. In Development, HTTP requests are allowed without HTTPS
redirection, and the cookie's Secure flag follows the request protocol. The sample
requests use `http://localhost:5123`. The HTTPS development profile still works.
Outside Development, cookies always require HTTPS and HTTP requests use HTTPS
redirection when an HTTPS destination is configured. HTTP development traffic
is unencrypted, including submitted passwords and cookies.
Clients must retain and send cookies on subsequent requests (for WinForms,
use an `HttpClientHandler` with a `CookieContainer`).
Registration does not automatically log the user in.

## Current scope

Users are held in a singleton in-memory store and disappear when the API
restarts. Cookies for users absent from the store are rejected. Existing song
and playlist endpoints remain public; add `[Authorize]` and ownership checks
when implementing access restrictions. Playlist `CreatedBy` is still a legacy
integer and has not been linked to the new GUID user IDs.

This basic flow does not include logout, password resets, email verification,
login throttling, or account lockout.
