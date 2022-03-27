using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IAdvertisementSkuRepository : IRepository
    {
        Task<AdvertisementSku> Add(AdvertisementSku advertisementSku);
        Task<AdvertisementSku?> Get(int id, bool keepTrack);
        Task<AdvertisementSku?> GetByCode(string code);
        Task<List<AdvertisementSku>> GetAll();
        Task<AdvertisementSku> Update(AdvertisementSku advertisementSku);
    }

    internal class AdvertisementSkuRepository : IAdvertisementSkuRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public AdvertisementSkuRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<AdvertisementSku> Add(AdvertisementSku advertisementSku)
        {
            var createdAdvertisementSku = await this._context.AdvertisementSkus!.AddAsync(advertisementSku);

            return createdAdvertisementSku.Entity;
        }

        public async Task<AdvertisementSku?> Get(int id, bool keepTrack)
        {
            var query = this._context.AdvertisementSkus!.Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var advertisementSku = await query.FirstOrDefaultAsync();

            return advertisementSku;
        }

        public async Task<AdvertisementSku?> GetByCode(string code)
        {
            var query = this._context.AdvertisementSkus!.Where(p => p.Code.ToLower() == code.ToLower());
            var advertisementSku = await query.AsNoTracking().FirstOrDefaultAsync();

            return advertisementSku;
        }

        public async Task<List<AdvertisementSku>> GetAll()
        {
            var query = this._context.AdvertisementSkus!;
            var advertisementSkus = await query.AsNoTracking().ToListAsync();

            return advertisementSkus;
        }

        public async Task<AdvertisementSku> Update(AdvertisementSku advertisementSku)
        {
            this._context.AdvertisementSkus!.Update(advertisementSku);

            return await Task.FromResult(advertisementSku);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
