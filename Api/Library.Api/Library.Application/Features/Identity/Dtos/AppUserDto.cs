
namespace Library.Application.Features.Identity.Dtos;
public sealed record AppUserDto(string Id,string Email,IList<string>Roles,IList<Claim> Claims);
