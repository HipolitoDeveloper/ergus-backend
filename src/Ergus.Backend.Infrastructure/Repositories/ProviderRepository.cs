using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IProviderRepository : IRepository
    {
        Task<Provider> Add(Provider provider);
        Task<Provider?> Get(int id, bool keepTrack);
        Task<Provider?> GetByCode(string code);
        Task<List<Provider>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Provider> Update(Provider provider);
    }

    internal class ProviderRepository : IProviderRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public ProviderRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Provider> Add(Provider provider)
        {
            var createdProvider = await this._context.Providers!.AddAsync(provider);

            return createdProvider.Entity;
        }

        public async Task<Provider?> Get(int id, bool keepTrack)
        {
            var query = this._context.Providers!.Include(p => p.Address).Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var provider = await query.FirstOrDefaultAsync();

            return provider;
        }

        public async Task<Provider?> GetByCode(string code)
        {
            var query = this._context.Providers!.Where(p => p.Code.ToLower() == code.ToLower());
            var provider = await query.AsNoTracking().FirstOrDefaultAsync();

            return provider;
        }

        public async Task<List<Provider>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.Providers;
            var providers = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await providers.ToListAsync();

            return await providers
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.Providers!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<Provider> Update(Provider provider)
        {
            this._context.Providers!.Update(provider);

            return await Task.FromResult(provider);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
