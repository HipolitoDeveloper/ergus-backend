using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Auth.Models
{
    public class AuthenticateResponse
    {
        public string Name  { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Name = user.Name;
            Login = user.Login;
            Email = user.Email;
            Token = token;
        }
    }
}
