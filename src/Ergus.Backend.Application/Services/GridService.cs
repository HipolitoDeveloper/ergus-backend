using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IGridService
    {
        Task<Grid?> Add(Grid grid);
        Task<Grid?> Delete(int id);
        Task<Grid?> Get(int id);
        Task<List<Grid>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Grid?> Update(Grid grid);
    }

    internal class GridService : IGridService
    {
        private readonly IGridRepository _gridRepository;

        public GridService(IGridRepository gridRepository)
        {
            this._gridRepository = gridRepository;
        }

        public async Task<Grid?> Add(Grid grid)
        {
            if (!grid.EhValido())
                return grid;

            await this._gridRepository.Add(grid);

            var success = await this._gridRepository.UnitOfWork.Commit();

            return success ? grid : null;
        }

        public async Task<Grid?> Delete(int id)
        {
            var grid = await this._gridRepository.Get(id, true);

            if (grid == null)
                return null;

            grid.DefinirComoRemovido(id);

            await this._gridRepository.Update(grid);

            var success = await this._gridRepository.UnitOfWork.Commit();

            return success ? grid : null;
        }

        public async Task<Grid?> Get(int id)
        {
            return await this._gridRepository.Get(id, false);
        }

        public async Task<List<Grid>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._gridRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._gridRepository.GetAllIds();
        }

        public async Task<Grid?> Update(Grid grid)
        {
            var oldGrid = await this._gridRepository.Get(grid.Id, true);

            if (oldGrid == null)
                return null;

            oldGrid.MergeToUpdate(grid);

            if(!oldGrid.EhValido())
                return oldGrid;

            await this._gridRepository.Update(oldGrid);

            var success = await this._gridRepository.UnitOfWork.Commit();

            return success ? grid : null;
        }
    }
}
