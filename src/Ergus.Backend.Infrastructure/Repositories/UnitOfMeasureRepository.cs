using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IUnitOfMeasureRepository : IRepository
    {
        Task<UnitOfMeasure> Add(UnitOfMeasure unitOfMeasure);
        Task<UnitOfMeasure?> Get(int id, bool keepTrack);
        Task<UnitOfMeasure?> GetByCode(string code);
        Task<List<UnitOfMeasure>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<UnitOfMeasure> Update(UnitOfMeasure unitOfMeasure);
    }

    internal class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public UnitOfMeasureRepository() { }

        public UnitOfMeasureRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<UnitOfMeasure> Add(UnitOfMeasure unitOfMeasure)
        {
            var createdUnitOfMeasure = await this._context.UnitOfMeasures!.AddAsync(unitOfMeasure);

            return createdUnitOfMeasure.Entity;
        }

        public async Task<UnitOfMeasure?> Get(int id, bool keepTrack)
        {
            var query = this._context.UnitOfMeasures!.Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var unitOfMeasure = await query.FirstOrDefaultAsync();

            return unitOfMeasure;
        }

        public async Task<UnitOfMeasure?> GetByCode(string code)
        {
            var query = this._context.UnitOfMeasures!.Where(c => c.Code.ToLower() == code.ToLower());
            var unitOfMeasure = await query.AsNoTracking().FirstOrDefaultAsync();

            return unitOfMeasure;
        }

        public async Task<List<UnitOfMeasure>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.UnitOfMeasures!;
            var unitOfMeasures = query.OrderBy(q => q.Description).AsNoTracking();

            if (disablePagination)
                return await unitOfMeasures.ToListAsync();

            return await unitOfMeasures
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.UnitOfMeasures!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<UnitOfMeasure> Update(UnitOfMeasure unitOfMeasure)
        {
            this._context.UnitOfMeasures!.Update(unitOfMeasure);

            return await Task.FromResult(unitOfMeasure);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
