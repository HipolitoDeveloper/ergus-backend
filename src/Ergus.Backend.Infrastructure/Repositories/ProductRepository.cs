using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository
    {
        Task<Product> Add(Product product);
        Task<Product?> Get(int id, bool keepTrack);
        Task<Product?> GetByCode(string code);
        Task<List<Product>> GetAll();
        Task<Product> Update(Product product);
    }

    internal class ProductRepository : IProductRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public ProductRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Product> Add(Product product)
        {
            var createdProduct = await this._context.Products!.AddAsync(product);

            return createdProduct.Entity;
        }

        public async Task<Product?> Get(int id, bool keepTrack)
        {
            var query = this._context.Products!.Include(p => p.Category).Include(p => p.Producer).Include(p => p.Provider).Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var product = await query.FirstOrDefaultAsync();

            return product;
        }

        public async Task<Product?> GetByCode(string code)
        {
            var query = this._context.Products!.Where(p => p.Code.ToLower() == code.ToLower());
            var product = await query.AsNoTracking().FirstOrDefaultAsync();

            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            var query = this._context.Products!;
            var products = await query.AsNoTracking().ToListAsync();

            return products;
        }

        public async Task<Product> Update(Product product)
        {
            this._context.Products!.Update(product);

            return await Task.FromResult(product);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
