using Ergus.Backend.Core.Settings;
using Ergus.Backend.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ergus.Backend.WebApi.Auth.Services
{
    public interface IAuthenticationService
    {
        string GenerateJwtToken(User user);
    }

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;

        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = GenerateAuthenticateResult(user, "Bearer").Principal!.Claims;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTimeOffset.Now.AddHours(4).UtcDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = this._appSettings.Audience,
                Issuer = this._appSettings.Issuer,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private AuthenticateResult GenerateAuthenticateResult(User userAuth, string schemeName)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userAuth.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(ClaimTypes.Name, userAuth.Login, ClaimValueTypes.String),
                new Claim(ClaimTypes.Email, userAuth.Email, ClaimValueTypes.String),
            };

            var identity = new ClaimsIdentity(claims, schemeName);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, schemeName);

            return AuthenticateResult.Success(ticket);
        }
    }
}
