using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Ergus.Backend.Application.Tests")]
namespace Ergus.Backend.Application.Services
{
    public interface IProductService
    {
        Task<Product?> Add(Product product);
        Task<Product?> Delete(int id);
        Task<Product?> Get(int id);
        Task<List<Product>> GetAll();
        Task<Product?> Update(Product product);
    }

    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<Product?> Add(Product product)
        {
            if (!product.EhValido())
                return product;

            await this._productRepository.Add(product);

            var success = await this._productRepository.UnitOfWork.Commit();

            return success ? product : null;
        }

        public async Task<Product?> Delete(int id)
        {
            var product = await this._productRepository.Get(id, true);

            if (product == null)
                return null;

            product.DefinirComoRemovido(id);

            await this._productRepository.Update(product);

            var success = await this._productRepository.UnitOfWork.Commit();

            return success ? product : null;
        }

        public async Task<Product?> Get(int id)
        {
            return await this._productRepository.Get(id, false);
        }

        public async Task<List<Product>> GetAll()
        {
            return await this._productRepository.GetAll();
        }

        public async Task<Product?> Update(Product product)
        {
            var oldProduct = await this._productRepository.Get(product.Id, true);

            if (oldProduct == null)
                return null;

            oldProduct.MergeToUpdate(product);

            if(!oldProduct.EhValido())
                return oldProduct;

            await this._productRepository.Update(oldProduct);

            var success = await this._productRepository.UnitOfWork.Commit();

            return success ? product : null;
        }
    }
}
