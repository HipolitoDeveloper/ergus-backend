using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface ISkuService
    {
        Task<Sku?> Add(Sku sku);
        Task<Sku?> Delete(int id);
        Task<Sku?> Get(int id);
        Task<List<Sku>> GetAll();
        Task<Sku?> Update(Sku sku);
    }

    internal class SkuService : ISkuService
    {
        private readonly ISkuRepository _skuRepository;

        public SkuService(ISkuRepository skuRepository)
        {
            this._skuRepository = skuRepository;
        }

        public async Task<Sku?> Add(Sku sku)
        {
            if (!sku.EhValido())
                return sku;

            await this._skuRepository.Add(sku);

            var success = await this._skuRepository.UnitOfWork.Commit();

            return success ? sku : null;
        }

        public async Task<Sku?> Delete(int id)
        {
            var sku = await this._skuRepository.Get(id, true);

            if (sku == null)
                return null;

            sku.DefinirComoRemovido(id);

            await this._skuRepository.Update(sku);

            var success = await this._skuRepository.UnitOfWork.Commit();

            return success ? sku : null;
        }

        public async Task<Sku?> Get(int id)
        {
            return await this._skuRepository.Get(id, false);
        }

        public async Task<List<Sku>> GetAll()
        {
            return await this._skuRepository.GetAll();
        }

        public async Task<Sku?> Update(Sku sku)
        {
            var oldSku = await this._skuRepository.Get(sku.Id, true);

            if (oldSku == null)
                return null;

            oldSku.MergeToUpdate(sku);

            if(!oldSku.EhValido())
                return oldSku;

            await this._skuRepository.Update(oldSku);

            var success = await this._skuRepository.UnitOfWork.Commit();

            return success ? sku : null;
        }
    }
}
