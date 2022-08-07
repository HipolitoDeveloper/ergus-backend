using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IGridRepository : IRepository
    {
        Task<Grid> Add(Grid grid);
        Task<Grid?> Get(int id, bool keepTrack);
        Task<Grid?> GetByCode(string code);
        Task<List<Grid>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Grid> Update(Grid grid);
    }

    internal class GridRepository : IGridRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public GridRepository() { }

        public GridRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Grid> Add(Grid grid)
        {
            var createdGrid = await this._context.Grids!.AddAsync(grid);

            return createdGrid.Entity;
        }

        public async Task<Grid?> Get(int id, bool keepTrack)
        {
            var query = this._context.Grids!.Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var grid = await query.FirstOrDefaultAsync();

            return grid;
        }

        public async Task<Grid?> GetByCode(string code)
        {
            var query = this._context.Grids!.Where(c => c.Code.ToLower() == code.ToLower());
            var grid = await query.AsNoTracking().FirstOrDefaultAsync();

            return grid;
        }

        public async Task<List<Grid>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.Grids!;
            var grids = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await grids.ToListAsync();

            return await grids
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.Grids!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<Grid> Update(Grid grid)
        {
            this._context.Grids!.Update(grid);

            return await Task.FromResult(grid);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
