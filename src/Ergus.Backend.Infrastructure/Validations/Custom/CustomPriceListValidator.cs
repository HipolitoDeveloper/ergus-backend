using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomPriceListCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IPriceListRepository _priceListRepository;

        public CustomPriceListCodeBeUniqueValidator(IPriceListRepository priceListRepository) : base()
        {
            this._priceListRepository = priceListRepository;
        }

        public override string Name => "CustomPriceListCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var priceList = value as PriceList;
            var priceListWithCode = this._priceListRepository.GetByCode(priceList!.Code).Result;

            if (priceListWithCode == null) return true;

            return priceList.Id == priceListWithCode.Id;
        }
    }

    public static class StaticPriceListCodeBeUniqueValidator
    {
        private static IPriceListRepository? _priceListRepository;

        public static IRuleBuilderOptions<T, TProperty> IsPriceListCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomPriceListCodeBeUniqueValidator<T, TProperty>(_priceListRepository!));
        }

        public static void Configure(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ PriceListExists]

    public class CustomPriceListExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IPriceListRepository _priceListRepository;

        public CustomPriceListExistsValidator(IPriceListRepository priceListRepository) : base()
        {
            this._priceListRepository = priceListRepository;
        }

        public override string Name => "PriceListExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int priceListId))
                return false;

            var priceList = this._priceListRepository.Get(priceListId, false).Result;

            return (priceList != null);
        }
    }

    public static class StaticPriceListExistsValidator
    {
        private static IPriceListRepository? _priceListRepository;

        public static IRuleBuilderOptions<T, TProperty> PriceListExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomPriceListExistsValidator<T, TProperty>(_priceListRepository!));
        }

        public static void Configure(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }
    }

    #endregion [ FIM - PriceListExists]
}
