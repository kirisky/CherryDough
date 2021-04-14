using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CherryDough.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CherryDough.Infra.Identity
{
    public class JwtBuilder<TIdentityUser, TKey>
        where TIdentityUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private UserManager<TIdentityUser> _userManager;
        private JwtSettings _jwtSettings;
        private TIdentityUser _user;
        private ICollection<Claim> _userClaims;
        private ICollection<Claim> _jwtClaims;
        private ClaimsIdentity _identityClaims;

        public JwtBuilder<TIdentityUser, TKey> WithUserManager(UserManager<TIdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            return this;
        }

        public JwtBuilder<TIdentityUser, TKey> WithJwtSettings(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings ?? throw new ArgumentException(nameof(jwtSettings));
            return this;
        }

        public JwtBuilder<TIdentityUser, TKey> WithEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException(nameof(email));

            _user = _userManager.FindByEmailAsync(email).Result;
            _userClaims = new List<Claim>();
            _jwtClaims = new List<Claim>();
            _identityClaims = new ClaimsIdentity();
            
            return this;
        }

        public JwtBuilder<TIdentityUser, TKey> WithJwtClaims()
        {
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Email, _user.Email));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            
            _identityClaims.AddClaims(_jwtClaims);

            return this;
        }

        public JwtBuilder<TIdentityUser, TKey> WithUserClaims()
        {
            _userClaims = _userManager.GetClaimsAsync(_user).Result;
            _identityClaims.AddClaims(_userClaims);

            return this;
        }

        public JwtBuilder<TIdentityUser, TKey> WithUserRoles()
        {
            var userRoles = _userManager.GetRolesAsync(_user).Result;
            userRoles.ToList().ForEach(r => _identityClaims.AddClaim(new Claim("role", r)));

            return this;
        }

        public string BuildToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = _identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        public UserResponse<TKey> BuildUserResponse()
        {
            var user = new UserResponse<TKey>
            {
                AccessToken = BuildToken(),
                ExpiresIn = TimeSpan.FromHours(_jwtSettings.Expiration).TotalSeconds,
                UserToken = new UserToken<TKey>
                {
                    Id = _user.Id,
                    Email = _user.Email,
                    Claims = _userClaims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };

            return user;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }

    public class JwtBuilder<TIdentityUser> : JwtBuilder<TIdentityUser, string>
        where TIdentityUser : IdentityUser<string>
    {
        
    }

    public sealed class JwtBuilder : JwtBuilder<IdentityUser>
    {
    }
}