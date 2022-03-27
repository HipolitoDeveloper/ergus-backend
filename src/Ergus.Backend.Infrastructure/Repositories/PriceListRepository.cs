using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IPriceListRepository : IRepository
    {
        Task<PriceList> Add(PriceList priceList);
        Task<PriceList?> Get(int id, bool keepTrack);
        Task<PriceList?> GetByCode(string code);
        Task<List<PriceList>> GetAll();
        Task<PriceList> Update(PriceList priceList);
    }

    internal class PriceListRepository : IPriceListRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public PriceListRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<PriceList> Add(PriceList priceList)
        {
            var createdPriceList = await this._context.PriceLists!.AddAsync(priceList);

            return createdPriceList.Entity;
        }

        public async Task<PriceList?> Get(int id, bool keepTrack)
        {
            var query = this._context.PriceLists!.Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var priceList = await query.FirstOrDefaultAsync();

            return priceList;
        }

        public async Task<PriceList?> GetByCode(string code)
        {
            var query = this._context.PriceLists!.Where(p => p.Code.ToLower() == code.ToLower());
            var priceList = await query.AsNoTracking().FirstOrDefaultAsync();

            return priceList;
        }

        public async Task<List<PriceList>> GetAll()
        {
            var query = this._context.PriceLists!;
            var priceLists = await query.AsNoTracking().ToListAsync();

            return priceLists;
        }

        public async Task<PriceList> Update(PriceList priceList)
        {
            this._context.PriceLists!.Update(priceList);

            return await Task.FromResult(priceList);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
