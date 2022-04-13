using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IPriceListService
    {
        Task<PriceList?> Add(PriceList priceList);
        Task<PriceList?> Delete(int id);
        Task<PriceList?> Get(int id);
        Task<List<PriceList>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<PriceList?> Update(PriceList priceList);
    }

    internal class PriceListService : IPriceListService
    {
        private readonly IPriceListRepository _priceListRepository;

        public PriceListService(IPriceListRepository priceListRepository)
        {
            this._priceListRepository = priceListRepository;
        }

        public async Task<PriceList?> Add(PriceList priceList)
        {
            if (!priceList.EhValido())
                return priceList;

            await this._priceListRepository.Add(priceList);

            var success = await this._priceListRepository.UnitOfWork.Commit();

            return success ? priceList : null;
        }

        public async Task<PriceList?> Delete(int id)
        {
            var priceList = await this._priceListRepository.Get(id, true);

            if (priceList == null)
                return null;

            priceList.DefinirComoRemovido(id);

            await this._priceListRepository.Update(priceList);

            var success = await this._priceListRepository.UnitOfWork.Commit();

            return success ? priceList : null;
        }

        public async Task<PriceList?> Get(int id)
        {
            return await this._priceListRepository.Get(id, false);
        }

        public async Task<List<PriceList>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._priceListRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._priceListRepository.GetAllIds();
        }

        public async Task<PriceList?> Update(PriceList priceList)
        {
            var oldPriceList = await this._priceListRepository.Get(priceList.Id, true);

            if (oldPriceList == null)
                return null;

            oldPriceList.MergeToUpdate(priceList);

            if(!oldPriceList.EhValido())
                return oldPriceList;

            await this._priceListRepository.Update(oldPriceList);

            var success = await this._priceListRepository.UnitOfWork.Commit();

            return success ? priceList : null;
        }
    }
}
