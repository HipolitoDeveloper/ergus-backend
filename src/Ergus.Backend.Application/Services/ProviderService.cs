using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IProviderService
    {
        Task<Provider?> Add(Provider provider);
        Task<Provider?> Delete(int id);
        Task<Provider?> Get(int id);
        Task<List<Provider>> GetAll();
        Task<Provider?> Update(Provider provider);
    }

    internal class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;

        public ProviderService(IProviderRepository providerRepository)
        {
            this._providerRepository = providerRepository;
        }

        public async Task<Provider?> Add(Provider provider)
        {
            if (!provider.EhValido())
                return provider;

            await this._providerRepository.Add(provider);

            var success = await this._providerRepository.UnitOfWork.Commit();

            return success ? provider : null;
        }

        public async Task<Provider?> Delete(int id)
        {
            var provider = await this._providerRepository.Get(id, true);

            if (provider == null)
                return null;

            provider.DefinirComoRemovido(id);

            await this._providerRepository.Update(provider);

            var success = await this._providerRepository.UnitOfWork.Commit();

            return success ? provider : null;
        }

        public async Task<Provider?> Get(int id)
        {
            return await this._providerRepository.Get(id, false);
        }

        public async Task<List<Provider>> GetAll()
        {
            return await this._providerRepository.GetAll();
        }

        public async Task<Provider?> Update(Provider provider)
        {
            var oldProvider = await this._providerRepository.Get(provider.Id, true);

            if (oldProvider == null)
                return null;

            oldProvider.MergeToUpdate(provider);

            if(!oldProvider.EhValido())
                return oldProvider;

            await this._providerRepository.Update(oldProvider);

            var success = await this._providerRepository.UnitOfWork.Commit();

            return success ? provider : null;
        }
    }
}
