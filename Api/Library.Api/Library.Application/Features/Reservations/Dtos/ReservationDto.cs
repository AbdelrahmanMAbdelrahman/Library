namespace Library.Application.Features.Reservations.Dtos;

public sealed record ReservationDto(Guid Id,DateTime ReservationDate,UserInfoDto UserInfo,CopyDto Copy);

