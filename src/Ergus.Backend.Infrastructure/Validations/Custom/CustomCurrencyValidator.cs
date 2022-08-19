using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomCurrencyCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CustomCurrencyCodeBeUniqueValidator(ICurrencyRepository currencyRepository) : base()
        {
            this._currencyRepository = currencyRepository;
        }

        public override string Name => "CustomCurrencyCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var currency = value as Currency;
            var currencyWithCode = this._currencyRepository.GetByCode(currency!.Code).Result;

            if (currencyWithCode == null) return true;

            return currency.Id == currencyWithCode.Id;
        }
    }

    public static class StaticCurrencyCodeBeUniqueValidator
    {
        private static ICurrencyRepository? _currencyRepository;

        public static IRuleBuilderOptions<T, TProperty> IsCurrencyCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomCurrencyCodeBeUniqueValidator<T, TProperty>(_currencyRepository!));
        }

        public static void Configure(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ CurrencyExists]

    public class CustomCurrencyExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CustomCurrencyExistsValidator(ICurrencyRepository currencyRepository) : base()
        {
            this._currencyRepository = currencyRepository;
        }

        public override string Name => "CurrencyExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int currencyId))
                return false;

            var currency = this._currencyRepository.Get(currencyId, false).Result;

            return (currency != null);
        }
    }

    public static class StaticCurrencyExistsValidator
    {
        private static ICurrencyRepository? _currencyRepository;

        public static IRuleBuilderOptions<T, TProperty> CurrencyExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomCurrencyExistsValidator<T, TProperty>(_currencyRepository!));
        }

        public static void Configure(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
    }

    #endregion [ FIM - CurrencyExists]
}
