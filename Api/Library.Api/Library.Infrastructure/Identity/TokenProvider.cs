using Library.Domain.Identity.RefreshTokens;

namespace Library.Infrastructure.Identity
{
    public sealed class TokenProvider(
        IAppDbContext context,IOptions<JwtOptions> jwtOptions
       ) : ITokenProvider
    {
        public async Task<Result<TokenResponse>> GenerateJwtToken(AppUserDto appUser, CancellationToken ct)
        {
            Result<TokenResponse> tokenResponseResult = await CreateAsync(appUser,ct);
            if(tokenResponseResult.IsError)return tokenResponseResult.Errors;
            return tokenResponseResult.Value;
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string Token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime=false,
                ValidateIssuerSigningKey = true,
                ValidIssuer=jwtOptions.Value.Issuer,
                ValidAudience=jwtOptions.Value.Audience,
                ClockSkew=TimeSpan.Zero,
                IssuerSigningKey=new SymmetricSecurityKey( Encoding.UTF8.GetBytes(jwtOptions.Value.Key))
            };
            ClaimsPrincipal principal = new JwtSecurityTokenHandler()
                .ValidateToken(Token,tokenValidationParameters,out SecurityToken validatedToken);
            if(validatedToken is not JwtSecurityToken jwtSecurityToken ||
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("invalid security token");
            }
            return principal;
        }
        private  async Task<Result<TokenResponse>> CreateAsync(AppUserDto appUser,CancellationToken ct) {
            DateTime ExpireOn = DateTime.UtcNow.AddMinutes(jwtOptions.Value.Period);
            List<Claim> claims = new List<Claim> {
        new Claim(JwtRegisteredClaimNames.Sub,appUser.Id),
        new Claim(JwtRegisteredClaimNames.Email,appUser.Email)
        };
           
            foreach (string role in appUser.Roles) {
                claims.Add(new(ClaimTypes.Role, role));
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Issuer=jwtOptions.Value.Issuer,
                Audience=jwtOptions.Value.Audience,
                SigningCredentials=new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key)),
                    SecurityAlgorithms.HmacSha256),
                Expires=ExpireOn

            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = handler.CreateToken(descriptor);
            int OldRefreshTokens =await context.RefreshTokens
                .Where(rt => rt.UserId == appUser.Id).ExecuteDeleteAsync(ct);

           Result<RefreshToken> refreshTokenResult = RefreshToken.Create(Guid.NewGuid(),GenerateRefreshToken(),
                appUser.Id,DateTime.UtcNow.AddDays(7));
            if (refreshTokenResult.IsError) { return refreshTokenResult.Errors; }
            await context.RefreshTokens.AddAsync(refreshTokenResult.Value);
            await context.SaveChangesAsync(ct);
            return new TokenResponse(
                handler.WriteToken( securityToken),
                refreshTokenResult.Value.Token??"",
                ExpireOn);
        }
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
