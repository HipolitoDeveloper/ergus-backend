using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IAdvertisementSkuPriceRepository : IRepository
    {
        Task<AdvertisementSkuPrice> Add(AdvertisementSkuPrice advertisementSkuPrice);
        Task<AdvertisementSkuPrice?> Get(int id, bool keepTrack);
        Task<AdvertisementSkuPrice?> GetByCode(string code);
        Task<List<AdvertisementSkuPrice>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<AdvertisementSkuPrice> Update(AdvertisementSkuPrice advertisementSkuPrice);
    }

    internal class AdvertisementSkuPriceRepository : IAdvertisementSkuPriceRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public AdvertisementSkuPriceRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<AdvertisementSkuPrice> Add(AdvertisementSkuPrice advertisementSkuPrice)
        {
            var createdAdvertisementSkuPrice = await this._context.AdvertisementSkuPrices!.AddAsync(advertisementSkuPrice);

            return createdAdvertisementSkuPrice.Entity;
        }

        public async Task<AdvertisementSkuPrice?> Get(int id, bool keepTrack)
        {
            var query = this._context.AdvertisementSkuPrices!.Include(p => p.PriceList).Include(p => p.AdvertisementSku).Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var advertisementSkuPrice = await query.FirstOrDefaultAsync();

            return advertisementSkuPrice;
        }

        public async Task<AdvertisementSkuPrice?> GetByCode(string code)
        {
            var query = this._context.AdvertisementSkuPrices!.Where(p => p.Code.ToLower() == code.ToLower());
            var advertisementSkuPrice = await query.AsNoTracking().FirstOrDefaultAsync();

            return advertisementSkuPrice;
        }

        public async Task<List<AdvertisementSkuPrice>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.AdvertisementSkuPrices;
            var advertisementSkuPrices = query.OrderBy(q => q.Id).AsNoTracking();

            if (disablePagination)
                return await advertisementSkuPrices.ToListAsync();

            return await advertisementSkuPrices
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.AdvertisementSkuPrices!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<AdvertisementSkuPrice> Update(AdvertisementSkuPrice advertisementSkuPrice)
        {
            this._context.AdvertisementSkuPrices!.Update(advertisementSkuPrice);

            return await Task.FromResult(advertisementSkuPrice);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
