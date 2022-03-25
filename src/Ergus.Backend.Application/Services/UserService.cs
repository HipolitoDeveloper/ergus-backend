using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IUserService
    {
        Task<User?> GetByLogin(string login);
        Task<bool> HandleUserTokens(int userId, string encryptedAccessToken);
    }

    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await this._userRepository.GetByLogin(login);
        }

        public async Task<bool> HandleUserTokens(int userId, string encryptedAccessToken)
        {
            var oldAccessTokens = await this._userRepository.GetUserTokensByUserId(userId);

            await this._userRepository.RemoveOldUserTokens(oldAccessTokens);

            var newUserToken = new UserToken()
            {
                TokenType = TokenType.AccessToken.DescriptionAttr(),
                SchemaType = AuthSchemaType.Bearer.DescriptionAttr(),
                TokenValue = encryptedAccessToken,
                UserId = userId
            };

            await this._userRepository.AddUserToken(newUserToken);

            var success = await this._userRepository.UnitOfWork.Commit();

            return success;
        }
    }
}
