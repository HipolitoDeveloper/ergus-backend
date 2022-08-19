using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface ICurrencyRepository : IRepository
    {
        Task<Currency> Add(Currency currency);
        Task<Currency?> Get(int id, bool keepTrack);
        Task<Currency?> GetByCode(string code);
        Task<List<Currency>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Currency> Update(Currency currency);
    }

    internal class CurrencyRepository : ICurrencyRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public CurrencyRepository() { }

        public CurrencyRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Currency> Add(Currency currency)
        {
            var createdCurrency = await this._context.Currencies!.AddAsync(currency);

            return createdCurrency.Entity;
        }

        public async Task<Currency?> Get(int id, bool keepTrack)
        {
            var query = this._context.Currencies!.Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var currency = await query.FirstOrDefaultAsync();

            return currency;
        }

        public async Task<Currency?> GetByCode(string code)
        {
            var query = this._context.Currencies!.Where(c => c.Code.ToLower() == code.ToLower());
            var currency = await query.AsNoTracking().FirstOrDefaultAsync();

            return currency;
        }

        public async Task<List<Currency>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.Currencies!;
            var currencys = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await currencys.ToListAsync();

            return await currencys
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.Currencies!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<Currency> Update(Currency currency)
        {
            this._context.Currencies!.Update(currency);

            return await Task.FromResult(currency);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
