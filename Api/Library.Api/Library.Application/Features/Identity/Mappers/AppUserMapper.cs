namespace Library.Application.Features.Identity.Mappers;

public static class AppUserMapper
{
    public static UserInfoDto ToDto(this AppUser user)
    {
        return new UserInfoDto(user.Id, user.Name!, user.Email!, user.PhoneNumber!);
    }
}
