using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IStockUnitService
    {
        Task<StockUnit?> Add(StockUnit stockUnit);
        Task<StockUnit?> Delete(int id);
        Task<StockUnit?> Get(int id);
        Task<List<StockUnit>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<StockUnit?> Update(StockUnit stockUnit);
    }

    internal class StockUnitService : IStockUnitService
    {
        private readonly IStockUnitRepository _stockUnitRepository;

        public StockUnitService(IStockUnitRepository stockUnitRepository)
        {
            this._stockUnitRepository = stockUnitRepository;
        }

        public async Task<StockUnit?> Add(StockUnit stockUnit)
        {
            if (!stockUnit.EhValido())
                return stockUnit;

            await this._stockUnitRepository.Add(stockUnit);

            var success = await this._stockUnitRepository.UnitOfWork.Commit();

            return success ? stockUnit : null;
        }

        public async Task<StockUnit?> Delete(int id)
        {
            var stockUnit = await this._stockUnitRepository.Get(id, true);

            if (stockUnit == null)
                return null;

            stockUnit.DefinirComoRemovido(id);

            await this._stockUnitRepository.Update(stockUnit);

            var success = await this._stockUnitRepository.UnitOfWork.Commit();

            return success ? stockUnit : null;
        }

        public async Task<StockUnit?> Get(int id)
        {
            return await this._stockUnitRepository.Get(id, false);
        }

        public async Task<List<StockUnit>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._stockUnitRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._stockUnitRepository.GetAllIds();
        }

        public async Task<StockUnit?> Update(StockUnit stockUnit)
        {
            var oldStockUnit = await this._stockUnitRepository.Get(stockUnit.Id, true);

            if (oldStockUnit == null)
                return null;

            oldStockUnit.MergeToUpdate(stockUnit);

            if(!oldStockUnit.EhValido())
                return oldStockUnit;

            await this._stockUnitRepository.Update(oldStockUnit);

            var success = await this._stockUnitRepository.UnitOfWork.Commit();

            return success ? stockUnit : null;
        }
    }
}
