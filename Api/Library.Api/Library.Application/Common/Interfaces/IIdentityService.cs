using Library.Domain.Common.Results;

namespace Library.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<Success>> SignUp(string Name,string Email,string Phone,string UserName, string Password);
        Task<bool> AuthorizeAsync(string UserId,string? policyName); 
        Task<Result<AppUserDto>> AuthenticateAsync(string Email, string Password);
        Task<Result<AppUserDto>> GetUserByIdAsync(string UserId);
        Task<string> GetUserNameAsync(string UserId);
        Task<bool> IsInRoleAsync(string UserId,string Role);
    }
}
