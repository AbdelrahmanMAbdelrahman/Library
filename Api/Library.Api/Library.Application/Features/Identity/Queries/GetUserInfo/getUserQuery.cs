namespace Library.Application.Features.Identity.Queries.GetUserInfo;

public sealed record getUserQuery(string? Id):IRequest<Result<AppUserDto>>;
