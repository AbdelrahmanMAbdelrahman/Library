namespace Library.Application.Features.Reservations.Dtos;

public sealed record ReservationDto(DateTime ReservationDate,UserInfoDto UserInfo,CopyDto Copy);

