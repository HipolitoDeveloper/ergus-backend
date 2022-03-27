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
        Task<List<AdvertisementSkuPrice>> GetAll();
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

        public async Task<List<AdvertisementSkuPrice>> GetAll()
        {
            var query = this._context.AdvertisementSkuPrices!;
            var advertisementSkuPrices = await query.AsNoTracking().ToListAsync();

            return advertisementSkuPrices;
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
