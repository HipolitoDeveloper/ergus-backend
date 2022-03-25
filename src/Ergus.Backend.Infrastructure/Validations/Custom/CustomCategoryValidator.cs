using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomCategoryCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CustomCategoryCodeBeUniqueValidator(ICategoryRepository categoryRepository) : base()
        {
            this._categoryRepository = categoryRepository;
        }

        public override string Name => "CustomCategoryCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var category = value as Category;
            var categoryWithCode = this._categoryRepository.GetByCode(category!.Code).Result;

            if (categoryWithCode == null) return true;

            return category.Id == categoryWithCode.Id;
        }
    }

    public static class StaticCategoryCodeBeUniqueValidator
    {
        private static ICategoryRepository? _categoryRepository;

        public static IRuleBuilderOptions<T, TProperty> IsCategoryCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomCategoryCodeBeUniqueValidator<T, TProperty>(_categoryRepository!));
        }

        public static void Configure(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ CategoryExists]

    public class CustomCategoryExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CustomCategoryExistsValidator(ICategoryRepository categoryRepository) : base()
        {
            this._categoryRepository = categoryRepository;
        }

        public override string Name => "CategoryExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int categoryId))
                return false;

            var category = this._categoryRepository.Get(categoryId, false).Result;

            return (category != null);
        }
    }

    public static class StaticCategoryExistsValidator
    {
        private static ICategoryRepository? _categoryRepository;

        public static IRuleBuilderOptions<T, TProperty> CategoryExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomCategoryExistsValidator<T, TProperty>(_categoryRepository!));
        }

        public static void Configure(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
    }

    #endregion [ FIM - CategoryExists]
}
