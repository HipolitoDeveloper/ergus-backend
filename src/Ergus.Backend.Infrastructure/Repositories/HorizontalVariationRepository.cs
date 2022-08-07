using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IHorizontalVariationRepository : IRepository
    {
        Task<HorizontalVariation> Add(HorizontalVariation horizontalVariation);
        Task<HorizontalVariation?> Get(int id, bool keepTrack);
        Task<HorizontalVariation?> GetByCode(string code);
        Task<List<HorizontalVariation>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<HorizontalVariation> Update(HorizontalVariation horizontalVariation);
    }

    internal class HorizontalVariationRepository : IHorizontalVariationRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public HorizontalVariationRepository() { }

        public HorizontalVariationRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<HorizontalVariation> Add(HorizontalVariation horizontalVariation)
        {
            var createdHorizontalVariation = await this._context.HorizontalVariations!.AddAsync(horizontalVariation);

            return createdHorizontalVariation.Entity;
        }

        public async Task<HorizontalVariation?> Get(int id, bool keepTrack)
        {
            var query = this._context.HorizontalVariations!.Include(p => p.Grid).Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var horizontalVariation = await query.FirstOrDefaultAsync();

            return horizontalVariation;
        }

        public async Task<HorizontalVariation?> GetByCode(string code)
        {
            var query = this._context.HorizontalVariations!.Where(c => c.Code.ToLower() == code.ToLower());
            var horizontalVariation = await query.AsNoTracking().FirstOrDefaultAsync();

            return horizontalVariation;
        }

        public async Task<List<HorizontalVariation>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.HorizontalVariations!;
            var horizontalVariations = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await horizontalVariations.ToListAsync();

            return await horizontalVariations
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.HorizontalVariations!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<HorizontalVariation> Update(HorizontalVariation horizontalVariation)
        {
            this._context.HorizontalVariations!.Update(horizontalVariation);

            return await Task.FromResult(horizontalVariation);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
