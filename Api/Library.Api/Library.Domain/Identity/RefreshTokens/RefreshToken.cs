namespace Library.Domain.Identity.RefreshTokens
{
   
    public class RefreshToken:Audit
    {
        public string? Token { get; set; }
        public string? UserId{ get; set; }
        public DateTimeOffset ExpireOn { get; set; }

        private RefreshToken(){}
        private RefreshToken(Guid id,string token,string userId,DateTimeOffset expireOn):base(id){
            this.Id = id;
        ExpireOn = expireOn;
            Token = token;
            UserId = userId;
        }

        public static Result<RefreshToken> Create(string token, string userId, DateTimeOffset expireOn) {
            if (string.IsNullOrEmpty(token))return RefreshTokenErrors.TokenRequired;
            if (!Guid.TryParse(userId,out Guid UserId)) return RefreshTokenErrors.UserIdRequired;
            if (expireOn <= DateTime.UtcNow) return RefreshTokenErrors.InvalidExpirationDate;

            return new RefreshToken(Guid.NewGuid(),token,userId,expireOn);
        }

    }
}
