using Library.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        Task<Result<TokenResponse>> GenerateJwtToken(AppUserDto appUser, CancellationToken ct);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string Token);
    }
}
