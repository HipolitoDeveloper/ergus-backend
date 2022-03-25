using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.Core.Helpers;
using Ergus.Backend.WebApi.Auth.Models;
using Ergus.Backend.WebApi.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Auth.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITextEncryptor _textEncryptor;

        public AuthController(IUserService userService, IAuthenticationService authenticationService, IPasswordHasher passwordHasher, ITextEncryptor textEncryptor)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
            this._passwordHasher = passwordHasher;
            this._textEncryptor = textEncryptor;
        }
        
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var user = await this._userService.GetByLogin(model.Login);

            if (user == null)
                return new ApiResult(new BadRequestApiResponse("Usuário e/ou senha inválidos"));

            var modelPassword = this._passwordHasher.HashPassword(user.Login, model.Password);

            if (!modelPassword.Equals(user.Password))
                return new ApiResult(new BadRequestApiResponse("Usuário e/ou senha inválidos"));

            var token = this._authenticationService.GenerateJwtToken(user);

            var encryptedAccessToken = this._textEncryptor.Encrypt(token);

            // Grava os dados atualizados relacionados aos tokens no banco
            await this._userService.HandleUserTokens(user.Id, encryptedAccessToken);

            var response = new AuthenticateResponse(user, token);
            return new ApiResult(new Saida(true, new List<string>() { "Usuário autenticado com sucesso" }, response));
        }
    }
}