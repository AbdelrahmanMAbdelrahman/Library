using Library.Domain.Common.Results;

namespace Library.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<Success>> SignUp(string Name,string Email,string Phone,string UserName, string Password,CancellationToken ct);
        Task<bool> AuthorizeAsync(string UserId,string? policyName, CancellationToken ct); 
        Task<Result<AppUserDto>> AuthenticateAsync(string Email, string Password, CancellationToken ct);
        Task<Result<AppUserDto>> GetUserByIdAsync(string UserId, CancellationToken ct);
        Task<string> GetUserNameAsync(string UserId, CancellationToken ct);
        Task<bool> IsInRoleAsync(string UserId,string Role, CancellationToken ct);
    }
}
