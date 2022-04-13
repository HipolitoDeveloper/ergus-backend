using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface ISkuRepository : IRepository
    {
        Task<Sku> Add(Sku sku);
        Task<Sku?> Get(int id, bool keepTrack);
        Task<Sku?> GetByCode(string code);
        Task<List<Sku>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Sku> Update(Sku sku);
    }

    internal class SkuRepository : ISkuRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public SkuRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Sku> Add(Sku sku)
        {
            var createdSku = await this._context.Skus!.AddAsync(sku);

            return createdSku.Entity;
        }

        public async Task<Sku?> Get(int id, bool keepTrack)
        {
            var query = this._context.Skus!.Include(p => p.Product).Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var sku = await query.FirstOrDefaultAsync();

            return sku;
        }

        public async Task<Sku?> GetByCode(string code)
        {
            var query = this._context.Skus!.Where(p => p.Code.ToLower() == code.ToLower());
            var sku = await query.AsNoTracking().FirstOrDefaultAsync();

            return sku;
        }

        public async Task<List<Sku>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.Skus;
            var skus = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await skus.ToListAsync();

            return await skus
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.Skus!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<Sku> Update(Sku sku)
        {
            this._context.Skus!.Update(sku);

            return await Task.FromResult(sku);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
