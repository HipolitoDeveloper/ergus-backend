using Microsoft.Extensions.Configuration;

namespace Ergus.Backend.Core.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string email, string password);
    }

    internal class PasswordHasher : IPasswordHasher
    {
        private readonly IConfiguration _configuration;

        public PasswordHasher(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string HashPassword(string email, string password)
        {
            var passMidGen = this._configuration.GetSection("AppSettings:PassMidGen").Value;
            return $"{email}:{passMidGen}:{password}".ToSHA256();
        }
    }
}
