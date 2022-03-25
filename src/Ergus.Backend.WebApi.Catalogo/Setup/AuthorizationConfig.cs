using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ergus.Backend.WebApi.Catalogo.Setup
{
    public static class AuthorizationConfig
    {
        public static void AddAuthorizationConfigServices(this IServiceCollection services, IConfiguration configuration)
        {
            var audience = configuration.GetSection("AppSettings:Audience").Value;
            var issuer = configuration.GetSection("AppSettings:Issuer").Value;
            var secret = configuration.GetSection("AppSettings:Secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                };
            });

            services.AddAuthorization();
        }

        public static void UseAuthorizationConfigure(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
