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
        Task<List<Category>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<List<CategoryTree>> GetAllTree();
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
            var query = this._context.Categories!.Include(c => c.Parent).Include(c => c.Text).Where(c => c.Id == id);
            var sql = query.ToQueryString();

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

        public async Task<List<Category>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.Categories!.Include(c => c.Parent);
            var categories = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await categories.ToListAsync();

            return await categories
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var categoryIds = await this._context.Categories!.Select(c => c.Id).OrderBy(q => q).ToListAsync();
            return categoryIds;
        }

        public async Task<List<CategoryTree>> GetAllTree()
        {
            var categoryTrees = await this._context.Categories!.Select(c => new CategoryTree(c.Id, c.Name, c.ParentId)).AsNoTracking().ToListAsync();
            return categoryTrees;
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
