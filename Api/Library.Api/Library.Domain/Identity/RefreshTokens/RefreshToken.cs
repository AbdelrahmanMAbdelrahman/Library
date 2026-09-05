using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Identity.RefreshTokens
{
   
    public class RefreshToken:Audit
    {
        public string? Token { get; set; }
        public string? UserId{ get; set; }
        public DateTimeOffset ExpireOn { get; set; }

        private RefreshToken(){}
        private RefreshToken(Guid id,string token,string userId,DateTimeOffset expireOn):base(id){
        ExpireOn = expireOn;
            Token = token;
            UserId = userId;
        }

        public static Result<RefreshToken> Create(Guid id,string token, string userId, DateTimeOffset expireOn) {
            if (id == Guid.Empty) return RefreshTokenErrors.IdRequired;
            if (string.IsNullOrEmpty(token))return RefreshTokenErrors.TokenRequired;
            if (string.IsNullOrEmpty(userId)) return RefreshTokenErrors.UserIdRequired;
            if (expireOn <= DateTime.UtcNow) return RefreshTokenErrors.InvalidExpirationDate;
            return new RefreshToken(id,token,userId,expireOn);
        }

    }
}
