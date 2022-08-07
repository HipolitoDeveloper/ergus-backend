using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IStockUnitRepository : IRepository
    {
        Task<StockUnit> Add(StockUnit stockUnit);
        Task<StockUnit?> Get(int id, bool keepTrack);
        Task<StockUnit?> GetByCode(string code);
        Task<List<StockUnit>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<StockUnit> Update(StockUnit stockUnit);
    }

    internal class StockUnitRepository : IStockUnitRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public StockUnitRepository() { }

        public StockUnitRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<StockUnit> Add(StockUnit stockUnit)
        {
            var createdStockUnit = await this._context.StockUnits!.AddAsync(stockUnit);

            return createdStockUnit.Entity;
        }

        public async Task<StockUnit?> Get(int id, bool keepTrack)
        {
            var query = this._context.StockUnits!.Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var stockUnit = await query.FirstOrDefaultAsync();

            return stockUnit;
        }

        public async Task<StockUnit?> GetByCode(string code)
        {
            var query = this._context.StockUnits!.Where(c => c.Code.ToLower() == code.ToLower());
            var stockUnit = await query.AsNoTracking().FirstOrDefaultAsync();

            return stockUnit;
        }

        public async Task<List<StockUnit>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.StockUnits!;
            var stockUnits = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await stockUnits.ToListAsync();

            return await stockUnits
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.StockUnits!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<StockUnit> Update(StockUnit stockUnit)
        {
            this._context.StockUnits!.Update(stockUnit);

            return await Task.FromResult(stockUnit);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
