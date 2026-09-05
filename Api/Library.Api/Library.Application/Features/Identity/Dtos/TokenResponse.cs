namespace Library.Application.Features.Identity.Dtos;

public sealed  record TokenResponse(string AccessToken,string RefreshToken,DateTimeOffset ExpireOn);
