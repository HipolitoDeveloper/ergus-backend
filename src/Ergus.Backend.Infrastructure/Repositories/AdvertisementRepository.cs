using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IAdvertisementRepository : IRepository
    {
        Task<Advertisement> Add(Advertisement advertisement);
        Task<Advertisement?> Get(int id, bool keepTrack);
        Task<Advertisement?> GetByCode(string code);
        Task<List<Advertisement>> GetAll();
        Task<Advertisement> Update(Advertisement advertisement);
    }

    internal class AdvertisementRepository : IAdvertisementRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public AdvertisementRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Advertisement> Add(Advertisement advertisement)
        {
            var createdAdvertisement = await this._context.Advertisements!.AddAsync(advertisement);

            return createdAdvertisement.Entity;
        }

        public async Task<Advertisement?> Get(int id, bool keepTrack)
        {
            var query = this._context.Advertisements!.Include(p => p.Integration).Include(p => p.Product).Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var advertisement = await query.FirstOrDefaultAsync();

            return advertisement;
        }

        public async Task<Advertisement?> GetByCode(string code)
        {
            var query = this._context.Advertisements!.Where(p => p.Code.ToLower() == code.ToLower());
            var advertisement = await query.AsNoTracking().FirstOrDefaultAsync();

            return advertisement;
        }

        public async Task<List<Advertisement>> GetAll()
        {
            var query = this._context.Advertisements!;
            var advertisements = await query.AsNoTracking().ToListAsync();

            return advertisements;
        }

        public async Task<Advertisement> Update(Advertisement advertisement)
        {
            this._context.Advertisements!.Update(advertisement);

            return await Task.FromResult(advertisement);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
