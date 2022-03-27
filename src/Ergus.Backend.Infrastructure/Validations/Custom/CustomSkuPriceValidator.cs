using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomSkuPriceCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ISkuPriceRepository _skuPriceRepository;

        public CustomSkuPriceCodeBeUniqueValidator(ISkuPriceRepository skuPriceRepository) : base()
        {
            this._skuPriceRepository = skuPriceRepository;
        }

        public override string Name => "CustomSkuPriceCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var skuPrice = value as SkuPrice;
            var skuPriceWithCode = this._skuPriceRepository.GetByCode(skuPrice!.Code).Result;

            if (skuPriceWithCode == null) return true;

            return skuPrice.Id == skuPriceWithCode.Id;
        }
    }

    public static class StaticSkuPriceCodeBeUniqueValidator
    {
        private static ISkuPriceRepository? _skuPriceRepository;

        public static IRuleBuilderOptions<T, TProperty> IsSkuPriceCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomSkuPriceCodeBeUniqueValidator<T, TProperty>(_skuPriceRepository!));
        }

        public static void Configure(ISkuPriceRepository skuPriceRepository)
        {
            _skuPriceRepository = skuPriceRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ SkuPriceExists]

    public class CustomSkuPriceExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ISkuPriceRepository _skuPriceRepository;

        public CustomSkuPriceExistsValidator(ISkuPriceRepository skuPriceRepository) : base()
        {
            this._skuPriceRepository = skuPriceRepository;
        }

        public override string Name => "SkuPriceExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int skuPriceId))
                return false;

            var skuPrice = this._skuPriceRepository.Get(skuPriceId, false).Result;

            return (skuPrice != null);
        }
    }

    public static class StaticSkuPriceExistsValidator
    {
        private static ISkuPriceRepository? _skuPriceRepository;

        public static IRuleBuilderOptions<T, TProperty> SkuPriceExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomSkuPriceExistsValidator<T, TProperty>(_skuPriceRepository!));
        }

        public static void Configure(ISkuPriceRepository skuPriceRepository)
        {
            _skuPriceRepository = skuPriceRepository;
        }
    }

    #endregion [ FIM - SkuPriceExists]
}
