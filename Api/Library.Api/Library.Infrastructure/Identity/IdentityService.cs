

using Library.Domain.Identity.Users;
using Microsoft.AspNetCore.Authorization;

namespace Library.Infrastructure.Identity
{
    public sealed class IdentityService(UserManager<AppUser> manager,
        IAuthorizationService authorizationService,
        IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory) : IIdentityService
    {
        public async Task<Result<AppUserDto>> AuthenticateAsync(string Email, string Password)
        {
            AppUser? appUser=await manager.FindByEmailAsync(Email);
            if (appUser is null) return Error.NotFound("AppUser.NotFound","No user found for this email");

            bool confirmed = await manager.IsEmailConfirmedAsync(appUser);
            if (!confirmed) return Error.Conflict("AppUser.EmailNotConfirmed", "Must confirm your email");
            bool CorrectPass = await manager.CheckPasswordAsync(appUser,Password);
            if (!confirmed) return Error.Conflict("Invalid Authentication", "Incorrect email or password");

            return new AppUserDto(appUser.Id,
                appUser.Email!,await manager.GetRolesAsync(appUser),
                await manager.GetClaimsAsync(appUser));
        }

        public async Task<bool> AuthorizeAsync(string UserId, string? policyName)
        {
            AppUser? appUser = await manager.FindByIdAsync(UserId);
            if(appUser is null)return false;
            ClaimsPrincipal principal=await userClaimsPrincipalFactory.CreateAsync(appUser);
            if(principal is null)return false;
            AuthorizationResult authorizeResult =await authorizationService.AuthorizeAsync(principal,policyName!);
            return authorizeResult.Succeeded;
        }

        public async Task<Result<AppUserDto>> GetUserByIdAsync(string UserId)
        {
            AppUser? appUser = await manager.FindByIdAsync(UserId);
            if (appUser is null) throw new InvalidOperationException("id not found");
            IList<string> roles = await manager.GetRolesAsync(appUser);
            IList<Claim> claims=await manager.GetClaimsAsync(appUser);
            return new AppUserDto(UserId,appUser.Email!,roles,claims);
        }

        public async Task<string> GetUserNameAsync(string UserId)
        {
            AppUser? appUser=await manager.FindByIdAsync(UserId);
            return appUser?.UserName??"";
        }

        public async Task<bool> IsInRoleAsync(string UserId, string Role)
        {
           AppUser? appUser=await manager.FindByIdAsync(UserId);
            return (appUser is null) || (await manager.IsInRoleAsync(appUser,Role));
        }

        public async Task<Result<Success>> SignUp(string Name, string Email, string Phone, string UserName, string Password)
        {
            AppUser? appUser = await manager.FindByEmailAsync(Email);
            if (appUser is not null) return Error.Conflict("AppUser.UserExist", "User Already Exist");
            var appUserResult = AppUser.Create(Name,Email,Phone,UserName); 
            if (appUserResult.IsError) return appUserResult.Errors;
            var AddUserRes=await manager.CreateAsync(appUser!,Password);
            if (!AddUserRes.Succeeded) return Error.Conflict("AppUser.CreateFailed","Unable to create user");
            appUser = appUserResult.Value;
            IdentityResult AddRoleToUserResult = await manager.AddToRoleAsync(appUser,"User");
            if (!AddRoleToUserResult.Succeeded) return Error.Conflict("AppUser.RoleInvalid","Invalid role");
            return Result.Success;
        }
    }
}
