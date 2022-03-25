using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface ICategoryService
    {
        Task<Category?> Add(Category category);
        Task<Category?> Delete(int id);
        Task<Category?> Get(int id);
        Task<List<Category>> GetAll();
        Task<Category?> Update(Category category);
    }

    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<Category?> Add(Category category)
        {
            if (!category.EhValido())
                return category;

            await this._categoryRepository.Add(category);

            var success = await this._categoryRepository.UnitOfWork.Commit();

            return success ? category : null;
        }

        public async Task<Category?> Delete(int id)
        {
            var category = await this._categoryRepository.Get(id, true);

            if (category == null)
                return null;

            category.DefinirComoRemovido(id);

            await this._categoryRepository.Update(category);

            var success = await this._categoryRepository.UnitOfWork.Commit();

            return success ? category : null;
        }

        public async Task<Category?> Get(int id)
        {
            return await this._categoryRepository.Get(id, false);
        }

        public async Task<List<Category>> GetAll()
        {
            return await this._categoryRepository.GetAll();
        }

        public async Task<Category?> Update(Category category)
        {
            var oldCategory = await this._categoryRepository.Get(category.Id, true);

            if (oldCategory == null)
                return null;

            oldCategory.MergeToUpdate(category);

            if(!oldCategory.EhValido())
                return oldCategory;

            await this._categoryRepository.Update(oldCategory);

            var success = await this._categoryRepository.UnitOfWork.Commit();

            return success ? category : null;
        }
    }
}
