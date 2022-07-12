using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface ICategoryService
    {
        Task<Category?> Add(Category category);
        Task<Category?> Delete(int id);
        Task<Category?> Get(int id);
        Task<List<Category>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<List<CategoryTree>> GetAllTree(List<CategoryTree> categoriesList);
        Task<List<CategoryTree>> GetTree();
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

        public async Task<List<Category>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._categoryRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._categoryRepository.GetAllIds();
        }

        public async Task<List<CategoryTree>> GetAllTree(List<CategoryTree> categoriesList)
        {
            var rootList = categoriesList.Where(c => c.ParentId == null).OrderBy(c => c.Name).ToList();

            AssociateParentNodeWithChildren(rootList, categoriesList);

            return await Task.FromResult(rootList);
        }

        public async Task<List<CategoryTree>> GetTree()
        {
            var categoriesList = await this._categoryRepository.GetAllTree();

            return await GetAllTree(categoriesList);
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



        private void AssociateParentNodeWithChildren(List<CategoryTree> parentList, List<CategoryTree> completeTree)
        {
            if (parentList == null)
                parentList = new List<CategoryTree>();

            if (completeTree == null)
                completeTree = new List<CategoryTree>();

            var parentListIds = parentList.Select(c => c.Id).ToList();
            var childrenList = completeTree.Where(c => c.ParentId.HasValue && parentListIds.Contains(c.ParentId.Value)).OrderBy(c => c.ParentId).ThenBy(c => c.Id).ToList();

            if (!childrenList.Any())
                return;

            foreach (var parent in parentList)
            {
                var list = childrenList?.Where(c => c.ParentId == parent.Id)?.OrderBy(c => c.Name).ToList();

                if (list != null && list.Count > 0)
                    parent.Children.AddRange(list);
            }

            AssociateParentNodeWithChildren(childrenList, completeTree);
        }
    }
}
