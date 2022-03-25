using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IProductAttributeService
    {
        Task<ProductAttribute?> Add(ProductAttribute productAttribute);
        Task<ProductAttribute?> Delete(int id);
        Task<ProductAttribute?> Get(int id);
        Task<List<ProductAttribute>> GetAll();
        Task<ProductAttribute?> Update(ProductAttribute productAttribute);
    }

    internal class ProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeRepository _productAttributeRepository;

        public ProductAttributeService(IProductAttributeRepository productAttributeRepository)
        {
            this._productAttributeRepository = productAttributeRepository;
        }

        public async Task<ProductAttribute?> Add(ProductAttribute productAttribute)
        {
            if (!productAttribute.EhValido())
                return productAttribute;

            await this._productAttributeRepository.Add(productAttribute);

            var success = await this._productAttributeRepository.UnitOfWork.Commit();

            return success ? productAttribute : null;
        }

        public async Task<ProductAttribute?> Delete(int id)
        {
            var productAttribute = await this._productAttributeRepository.Get(id, true);

            if (productAttribute == null)
                return null;

            productAttribute.DefinirComoRemovido(id);

            await this._productAttributeRepository.Update(productAttribute);

            var success = await this._productAttributeRepository.UnitOfWork.Commit();

            return success ? productAttribute : null;
        }

        public async Task<ProductAttribute?> Get(int id)
        {
            return await this._productAttributeRepository.Get(id, false);
        }

        public async Task<List<ProductAttribute>> GetAll()
        {
            return await this._productAttributeRepository.GetAll();
        }

        public async Task<ProductAttribute?> Update(ProductAttribute productAttribute)
        {
            var oldProductAttribute = await this._productAttributeRepository.Get(productAttribute.Id, true);

            if (oldProductAttribute == null)
                return null;

            oldProductAttribute.MergeToUpdate(productAttribute);

            if(!oldProductAttribute.EhValido())
                return oldProductAttribute;

            await this._productAttributeRepository.Update(oldProductAttribute);

            var success = await this._productAttributeRepository.UnitOfWork.Commit();

            return success ? productAttribute : null;
        }
    }
}
