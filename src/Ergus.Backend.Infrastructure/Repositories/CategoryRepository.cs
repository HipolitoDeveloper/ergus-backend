using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<Category> Add(Category category);
        Task<Category?> Get(int id, bool keepTrack);
        Task<Category?> GetByCode(string code);
        Task<List<Category>> GetAll();
        Task<Category> Update(Category category);
    }

    internal class CategoryRepository : ICategoryRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public CategoryRepository() { }

        public CategoryRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Category> Add(Category category)
        {
            var createdCategory = await this._context.Categories!.AddAsync(category);

            return createdCategory.Entity;
        }

        public async Task<Category?> Get(int id, bool keepTrack)
        {
            var query = this._context.Categories!.Include(c => c.Parent).Where(c => c.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var category = await query.FirstOrDefaultAsync();

            return category;
        }

        public async Task<Category?> GetByCode(string code)
        {
            var query = this._context.Categories!.Where(c => c.Code.ToLower() == code.ToLower());
            var category = await query.AsNoTracking().FirstOrDefaultAsync();

            return category;
        }

        public async Task<List<Category>> GetAll()
        {
            var query = this._context.Categories!.Include(c => c.Parent);
            var categories = await query.AsNoTracking().ToListAsync();

            return categories;
        }

        public async Task<Category> Update(Category category)
        {
            this._context.Categories!.Update(category);

            return await Task.FromResult(category);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
