using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IVerticalVariationRepository : IRepository
    {
        Task<VerticalVariation> Add(VerticalVariation verticalVariation);
        Task<VerticalVariation?> Get(int id, bool keepTrack);
        Task<VerticalVariation?> GetByCode(string code);
        Task<List<VerticalVariation>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<VerticalVariation> Update(VerticalVariation verticalVariation);
    }

    internal class VerticalVariationRepository : IVerticalVariationRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public VerticalVariationRepository() { }

        public VerticalVariationRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<VerticalVariation> Add(VerticalVariation verticalVariation)
        {
            var createdVerticalVariation = await this._context.VerticalVariations!.AddAsync(verticalVariation);

            return createdVerticalVariation.Entity;
        }

        public async Task<VerticalVariation?> Get(int id, bool keepTrack)
        {
            var query = this._context.VerticalVariations!.Include(p => p.Grid).Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var verticalVariation = await query.FirstOrDefaultAsync();

            return verticalVariation;
        }

        public async Task<VerticalVariation?> GetByCode(string code)
        {
            var query = this._context.VerticalVariations!.Where(c => c.Code.ToLower() == code.ToLower());
            var verticalVariation = await query.AsNoTracking().FirstOrDefaultAsync();

            return verticalVariation;
        }

        public async Task<List<VerticalVariation>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.VerticalVariations!;
            var verticalVariations = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await verticalVariations.ToListAsync();

            return await verticalVariations
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.VerticalVariations!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<VerticalVariation> Update(VerticalVariation verticalVariation)
        {
            this._context.VerticalVariations!.Update(verticalVariation);

            return await Task.FromResult(verticalVariation);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
