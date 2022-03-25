using System.ComponentModel.DataAnnotations;

namespace Ergus.Backend.WebApi.Auth.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login     { get; set; } = String.Empty;

        [Required]
        public string Password  { get; set; } = String.Empty;
    }
}
