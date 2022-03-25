using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IProductAttributeRepository : IRepository
    {
        Task<ProductAttribute> Add(ProductAttribute productAttribute);
        Task<ProductAttribute?> Get(int id, bool keepTrack);
        Task<ProductAttribute?> GetByCode(string code);
        Task<List<ProductAttribute>> GetAll();
        Task<ProductAttribute> Update(ProductAttribute productAttribute);
    }

    internal class ProductAttributeRepository : IProductAttributeRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public ProductAttributeRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<ProductAttribute> Add(ProductAttribute productAttribute)
        {
            var createdProductAttribute = await this._context.ProductAttributes!.AddAsync(productAttribute);

            return createdProductAttribute.Entity;
        }

        public async Task<ProductAttribute?> Get(int id, bool keepTrack)
        {
            var query = this._context.ProductAttributes!.Include(p => p.Metadata).Include(p => p.Product).Where(p => p.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var productAttribute = await query.FirstOrDefaultAsync();

            return productAttribute;
        }

        public async Task<ProductAttribute?> GetByCode(string code)
        {
            var query = this._context.ProductAttributes!.Where(p => p.Code.ToLower() == code.ToLower());
            var productAttribute = await query.AsNoTracking().FirstOrDefaultAsync();

            return productAttribute;
        }

        public async Task<List<ProductAttribute>> GetAll()
        {
            var query = this._context.ProductAttributes!.Include(p => p.Metadata).Include(p => p.Product);
            var productAttrs = await query.AsNoTracking().ToListAsync();

            return productAttrs;
        }

        public async Task<ProductAttribute> Update(ProductAttribute productAttribute)
        {
            this._context.ProductAttributes!.Update(productAttribute);

            return await Task.FromResult(productAttribute);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
