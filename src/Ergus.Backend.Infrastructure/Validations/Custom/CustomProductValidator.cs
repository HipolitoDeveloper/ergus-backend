using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomProductCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IProductRepository _productRepository;

        public CustomProductCodeBeUniqueValidator(IProductRepository productRepository) : base()
        {
            this._productRepository = productRepository;
        }

        public override string Name => "CustomProductCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var product = value as Product;
            var productWithCode = this._productRepository.GetByCode(product!.Code).Result;

            if (productWithCode == null) return true;

            return product.Id == productWithCode.Id;
        }
    }

    public static class StaticProductCodeBeUniqueValidator
    {
        private static IProductRepository? _productRepository;

        public static IRuleBuilderOptions<T, TProperty> IsProductCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomProductCodeBeUniqueValidator<T, TProperty>(_productRepository!));
        }

        public static void Configure(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ ProductExists]

    public class CustomProductExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IProductRepository _productRepository;

        public CustomProductExistsValidator(IProductRepository productRepository) : base()
        {
            this._productRepository = productRepository;
        }

        public override string Name => "ProductExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int productId))
                return false;

            var product = this._productRepository.Get(productId, false).Result;

            return (product != null);
        }
    }

    public static class StaticProductExistsValidator
    {
        private static IProductRepository? _productRepository;

        public static IRuleBuilderOptions<T, TProperty> ProductExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomProductExistsValidator<T, TProperty>(_productRepository!));
        }

        public static void Configure(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }

    #endregion [ FIM - ProductExists]
}
