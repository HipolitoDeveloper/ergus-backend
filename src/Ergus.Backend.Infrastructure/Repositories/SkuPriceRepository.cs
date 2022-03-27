using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface ISkuPriceRepository : IRepository
    {
        Task<SkuPrice> Add(SkuPrice skuPrice);
        Task<SkuPrice?> Get(int id, bool keepTrack);
        Task<SkuPrice?> GetByCode(string code);
        Task<List<SkuPrice>> GetAll();
        Task<SkuPrice> Update(SkuPrice skuPrice);
    }

    internal class SkuPriceRepository : ISkuPriceRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public SkuPriceRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<SkuPrice> Add(SkuPrice skuPrice)
        {
            var createdSkuPrice = await this._context.SkuPrices!.AddAsync(skuPrice);

            return createdSkuPrice.Entity;
        }

        public async Task<SkuPrice?> Get(int id, bool keepTrack)
        {
            var query = this._context.SkuPrices!.Include(p => p.PriceList).Include(p => p.Sku).Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var skuPrice = await query.FirstOrDefaultAsync();

            return skuPrice;
        }

        public async Task<SkuPrice?> GetByCode(string code)
        {
            var query = this._context.SkuPrices!.Where(p => p.Code.ToLower() == code.ToLower());
            var skuPrice = await query.AsNoTracking().FirstOrDefaultAsync();

            return skuPrice;
        }

        public async Task<List<SkuPrice>> GetAll()
        {
            var query = this._context.SkuPrices!;
            var skuPrices = await query.AsNoTracking().ToListAsync();

            return skuPrices;
        }

        public async Task<SkuPrice> Update(SkuPrice skuPrice)
        {
            this._context.SkuPrices!.Update(skuPrice);

            return await Task.FromResult(skuPrice);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
