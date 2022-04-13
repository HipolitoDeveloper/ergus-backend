using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface ISkuPriceService
    {
        Task<SkuPrice?> Add(SkuPrice skuPrice);
        Task<SkuPrice?> Delete(int id);
        Task<SkuPrice?> Get(int id);
        Task<List<SkuPrice>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<SkuPrice?> Update(SkuPrice skuPrice);
    }

    internal class SkuPriceService : ISkuPriceService
    {
        private readonly ISkuPriceRepository _skuPriceRepository;

        public SkuPriceService(ISkuPriceRepository skuPriceRepository)
        {
            this._skuPriceRepository = skuPriceRepository;
        }

        public async Task<SkuPrice?> Add(SkuPrice skuPrice)
        {
            if (!skuPrice.EhValido())
                return skuPrice;

            await this._skuPriceRepository.Add(skuPrice);

            var success = await this._skuPriceRepository.UnitOfWork.Commit();

            return success ? skuPrice : null;
        }

        public async Task<SkuPrice?> Delete(int id)
        {
            var skuPrice = await this._skuPriceRepository.Get(id, true);

            if (skuPrice == null)
                return null;

            skuPrice.DefinirComoRemovido(id);

            await this._skuPriceRepository.Update(skuPrice);

            var success = await this._skuPriceRepository.UnitOfWork.Commit();

            return success ? skuPrice : null;
        }

        public async Task<SkuPrice?> Get(int id)
        {
            return await this._skuPriceRepository.Get(id, false);
        }

        public async Task<List<SkuPrice>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._skuPriceRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._skuPriceRepository.GetAllIds();
        }

        public async Task<SkuPrice?> Update(SkuPrice skuPrice)
        {
            var oldSkuPrice = await this._skuPriceRepository.Get(skuPrice.Id, true);

            if (oldSkuPrice == null)
                return null;

            oldSkuPrice.MergeToUpdate(skuPrice);

            if(!oldSkuPrice.EhValido())
                return oldSkuPrice;

            await this._skuPriceRepository.Update(oldSkuPrice);

            var success = await this._skuPriceRepository.UnitOfWork.Commit();

            return success ? skuPrice : null;
        }
    }
}
