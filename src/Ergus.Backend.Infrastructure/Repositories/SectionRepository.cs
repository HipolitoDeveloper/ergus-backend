using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface ISectionRepository : IRepository
    {
        Task<Section> Add(Section section);
        Task<Section?> Get(int id, bool keepTrack);
        Task<Section?> GetByCode(string code);
        Task<List<Section>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Section> Update(Section section);
    }

    internal class SectionRepository : ISectionRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public SectionRepository() { }

        public SectionRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Section> Add(Section section)
        {
            var createdSection = await this._context.Sections!.AddAsync(section);

            return createdSection.Entity;
        }

        public async Task<Section?> Get(int id, bool keepTrack)
        {
            var query = this._context.Sections!.Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var section = await query.FirstOrDefaultAsync();

            return section;
        }

        public async Task<Section?> GetByCode(string code)
        {
            var query = this._context.Sections!.Where(c => c.Code.ToLower() == code.ToLower());
            var section = await query.AsNoTracking().FirstOrDefaultAsync();

            return section;
        }

        public async Task<List<Section>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.Sections!;
            var sections = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await sections.ToListAsync();

            return await sections
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.Sections!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<Section> Update(Section section)
        {
            this._context.Sections!.Update(section);

            return await Task.FromResult(section);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
