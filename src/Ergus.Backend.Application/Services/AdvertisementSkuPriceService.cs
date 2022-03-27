using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IAdvertisementSkuPriceService
    {
        Task<AdvertisementSkuPrice?> Add(AdvertisementSkuPrice advertisementSkuPrice);
        Task<AdvertisementSkuPrice?> Delete(int id);
        Task<AdvertisementSkuPrice?> Get(int id);
        Task<List<AdvertisementSkuPrice>> GetAll();
        Task<AdvertisementSkuPrice?> Update(AdvertisementSkuPrice advertisementSkuPrice);
    }

    internal class AdvertisementSkuPriceService : IAdvertisementSkuPriceService
    {
        private readonly IAdvertisementSkuPriceRepository _advertisementSkuPriceRepository;

        public AdvertisementSkuPriceService(IAdvertisementSkuPriceRepository advertisementSkuPriceRepository)
        {
            this._advertisementSkuPriceRepository = advertisementSkuPriceRepository;
        }

        public async Task<AdvertisementSkuPrice?> Add(AdvertisementSkuPrice advertisementSkuPrice)
        {
            if (!advertisementSkuPrice.EhValido())
                return advertisementSkuPrice;

            await this._advertisementSkuPriceRepository.Add(advertisementSkuPrice);

            var success = await this._advertisementSkuPriceRepository.UnitOfWork.Commit();

            return success ? advertisementSkuPrice : null;
        }

        public async Task<AdvertisementSkuPrice?> Delete(int id)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceRepository.Get(id, true);

            if (advertisementSkuPrice == null)
                return null;

            advertisementSkuPrice.DefinirComoRemovido(id);

            await this._advertisementSkuPriceRepository.Update(advertisementSkuPrice);

            var success = await this._advertisementSkuPriceRepository.UnitOfWork.Commit();

            return success ? advertisementSkuPrice : null;
        }

        public async Task<AdvertisementSkuPrice?> Get(int id)
        {
            return await this._advertisementSkuPriceRepository.Get(id, false);
        }

        public async Task<List<AdvertisementSkuPrice>> GetAll()
        {
            return await this._advertisementSkuPriceRepository.GetAll();
        }

        public async Task<AdvertisementSkuPrice?> Update(AdvertisementSkuPrice advertisementSkuPrice)
        {
            var oldAdvertisementSkuPrice = await this._advertisementSkuPriceRepository.Get(advertisementSkuPrice.Id, true);

            if (oldAdvertisementSkuPrice == null)
                return null;

            oldAdvertisementSkuPrice.MergeToUpdate(advertisementSkuPrice);

            if(!oldAdvertisementSkuPrice.EhValido())
                return oldAdvertisementSkuPrice;

            await this._advertisementSkuPriceRepository.Update(oldAdvertisementSkuPrice);

            var success = await this._advertisementSkuPriceRepository.UnitOfWork.Commit();

            return success ? advertisementSkuPrice : null;
        }
    }
}
