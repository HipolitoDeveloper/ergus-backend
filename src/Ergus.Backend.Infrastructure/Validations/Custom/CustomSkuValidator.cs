using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomSkuCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ISkuRepository _skuRepository;

        public CustomSkuCodeBeUniqueValidator(ISkuRepository skuRepository) : base()
        {
            this._skuRepository = skuRepository;
        }

        public override string Name => "CustomSkuCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var sku = value as Sku;
            var skuWithCode = this._skuRepository.GetByCode(sku!.Code).Result;

            if (skuWithCode == null) return true;

            return sku.Id == skuWithCode.Id;
        }
    }

    public static class StaticSkuCodeBeUniqueValidator
    {
        private static ISkuRepository? _skuRepository;

        public static IRuleBuilderOptions<T, TProperty> IsSkuCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomSkuCodeBeUniqueValidator<T, TProperty>(_skuRepository!));
        }

        public static void Configure(ISkuRepository skuRepository)
        {
            _skuRepository = skuRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ SkuExists]

    public class CustomSkuExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ISkuRepository _skuRepository;

        public CustomSkuExistsValidator(ISkuRepository skuRepository) : base()
        {
            this._skuRepository = skuRepository;
        }

        public override string Name => "SkuExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int skuId))
                return false;

            var sku = this._skuRepository.Get(skuId, false).Result;

            return (sku != null);
        }
    }

    public static class StaticSkuExistsValidator
    {
        private static ISkuRepository? _skuRepository;

        public static IRuleBuilderOptions<T, TProperty> SkuExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomSkuExistsValidator<T, TProperty>(_skuRepository!));
        }

        public static void Configure(ISkuRepository skuRepository)
        {
            _skuRepository = skuRepository;
        }
    }

    #endregion [ FIM - SkuExists]
}
