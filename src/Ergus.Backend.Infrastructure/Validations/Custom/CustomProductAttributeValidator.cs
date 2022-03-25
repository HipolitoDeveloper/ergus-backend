using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomProductAttributeCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IProductAttributeRepository _productAttributeRepository;

        public CustomProductAttributeCodeBeUniqueValidator(IProductAttributeRepository productAttributeRepository) : base()
        {
            this._productAttributeRepository = productAttributeRepository;
        }

        public override string Name => "CustomProductAttributeCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var prodAttr = value as ProductAttribute;
            var prodAttrWithCode = this._productAttributeRepository.GetByCode(prodAttr!.Code).Result;

            if (prodAttrWithCode == null) return true;

            return prodAttr.Id == prodAttrWithCode.Id;
        }
    }

    public static class StaticProductAttributeCodeBeUniqueValidator
    {
        private static IProductAttributeRepository? _productAttributeRepository;

        public static IRuleBuilderOptions<T, TProperty> IsProductAttributeCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomProductAttributeCodeBeUniqueValidator<T, TProperty>(_productAttributeRepository!));
        }

        public static void Configure(IProductAttributeRepository productAttributeRepository)
        {
            _productAttributeRepository = productAttributeRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]
}
