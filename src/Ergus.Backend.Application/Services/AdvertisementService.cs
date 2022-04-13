using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IAdvertisementService
    {
        Task<Advertisement?> Add(Advertisement advertisement);
        Task<Advertisement?> Delete(int id);
        Task<Advertisement?> Get(int id);
        Task<List<Advertisement>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Advertisement?> Update(Advertisement advertisement);
    }

    internal class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            this._advertisementRepository = advertisementRepository;
        }

        public async Task<Advertisement?> Add(Advertisement advertisement)
        {
            if (!advertisement.EhValido())
                return advertisement;

            await this._advertisementRepository.Add(advertisement);

            var success = await this._advertisementRepository.UnitOfWork.Commit();

            return success ? advertisement : null;
        }

        public async Task<Advertisement?> Delete(int id)
        {
            var advertisement = await this._advertisementRepository.Get(id, true);

            if (advertisement == null)
                return null;

            advertisement.DefinirComoRemovido(id);

            await this._advertisementRepository.Update(advertisement);

            var success = await this._advertisementRepository.UnitOfWork.Commit();

            return success ? advertisement : null;
        }

        public async Task<Advertisement?> Get(int id)
        {
            return await this._advertisementRepository.Get(id, false);
        }

        public async Task<List<Advertisement>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._advertisementRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._advertisementRepository.GetAllIds();
        }

        public async Task<Advertisement?> Update(Advertisement advertisement)
        {
            var oldAdvertisement = await this._advertisementRepository.Get(advertisement.Id, true);

            if (oldAdvertisement == null)
                return null;

            oldAdvertisement.MergeToUpdate(advertisement);

            if(!oldAdvertisement.EhValido())
                return oldAdvertisement;

            await this._advertisementRepository.Update(oldAdvertisement);

            var success = await this._advertisementRepository.UnitOfWork.Commit();

            return success ? advertisement : null;
        }
    }
}
