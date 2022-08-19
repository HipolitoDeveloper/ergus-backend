using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface ICurrencyService
    {
        Task<Currency?> Add(Currency currency);
        Task<Currency?> Delete(int id);
        Task<Currency?> Get(int id);
        Task<List<Currency>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Currency?> Update(Currency currency);
    }

    internal class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            this._currencyRepository = currencyRepository;
        }

        public async Task<Currency?> Add(Currency currency)
        {
            if (!currency.EhValido())
                return currency;

            await this._currencyRepository.Add(currency);

            var success = await this._currencyRepository.UnitOfWork.Commit();

            return success ? currency : null;
        }

        public async Task<Currency?> Delete(int id)
        {
            var currency = await this._currencyRepository.Get(id, true);

            if (currency == null)
                return null;

            currency.DefinirComoRemovido(id);

            await this._currencyRepository.Update(currency);

            var success = await this._currencyRepository.UnitOfWork.Commit();

            return success ? currency : null;
        }

        public async Task<Currency?> Get(int id)
        {
            return await this._currencyRepository.Get(id, false);
        }

        public async Task<List<Currency>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._currencyRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._currencyRepository.GetAllIds();
        }

        public async Task<Currency?> Update(Currency currency)
        {
            var oldCurrency = await this._currencyRepository.Get(currency.Id, true);

            if (oldCurrency == null)
                return null;

            oldCurrency.MergeToUpdate(currency);

            if(!oldCurrency.EhValido())
                return oldCurrency;

            await this._currencyRepository.Update(oldCurrency);

            var success = await this._currencyRepository.UnitOfWork.Commit();

            return success ? currency : null;
        }
    }
}
