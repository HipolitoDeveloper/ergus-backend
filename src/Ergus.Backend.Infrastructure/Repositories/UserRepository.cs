using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task AddUserToken(UserToken userToken);
        Task<User?> Get(int id, bool keepTrack);
        Task<User?> GetByLogin(string login);
        Task<IEnumerable<UserToken>> GetUserTokensByUserId(int userId);
        Task RemoveOldUserTokens(IEnumerable<UserToken> userTokens);
    }

    internal class UserRepository : IUserRepository
    {
        #region [ Propriedades ]

        private readonly AppServerContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public UserRepository(AppServerContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public Task AddUserToken(UserToken userToken)
        {
            this._context.UserTokens!.Add(userToken);
            return Task.CompletedTask;
        }

        public async Task<User?> Get(int id, bool keepTrack)
        {
            var query = this._context.Users!.Where(c => c.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var user = await query.FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetByLogin(string login)
        {
            var user = await this._context.Users!.Where(u => u.Login.ToLower().Equals(login.ToLower())).AsNoTracking().FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<UserToken>> GetUserTokensByUserId(int userId)
        {
            var userTokens = this._context.UserTokens!.Where(ut => ut.TokenType == TokenType.AccessToken.DescriptionAttr() && ut.SchemaType == AuthSchemaType.Bearer.DescriptionAttr() && ut.UserId == userId);
            return await Task.FromResult(userTokens);
        }

        public Task RemoveOldUserTokens(IEnumerable<UserToken> userTokens)
        {
            this._context.UserTokens!.RemoveRange(userTokens);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
