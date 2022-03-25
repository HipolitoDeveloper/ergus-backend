using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomProviderCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IProviderRepository _providerRepository;

        public CustomProviderCodeBeUniqueValidator(IProviderRepository providerRepository) : base()
        {
            this._providerRepository = providerRepository;
        }

        public override string Name => "CustomProviderCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var provider = value as Provider;
            var providerWithCode = this._providerRepository.GetByCode(provider!.Code).Result;

            if (providerWithCode == null) return true;

            return provider.Id == providerWithCode.Id;
        }
    }

    public static class StaticProviderCodeBeUniqueValidator
    {
        private static IProviderRepository? _providerRepository;

        public static IRuleBuilderOptions<T, TProperty> IsProviderCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomProviderCodeBeUniqueValidator<T, TProperty>(_providerRepository!));
        }

        public static void Configure(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ ProviderExists]

    public class CustomProviderExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IProviderRepository _providerRepository;

        public CustomProviderExistsValidator(IProviderRepository providerRepository) : base()
        {
            this._providerRepository = providerRepository;
        }

        public override string Name => "ProviderExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int providerId))
                return false;

            var provider = this._providerRepository.Get(providerId, false).Result;

            return (provider != null);
        }
    }

    public static class StaticProviderExistsValidator
    {
        private static IProviderRepository? _providerRepository;

        public static IRuleBuilderOptions<T, TProperty> ProviderExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomProviderExistsValidator<T, TProperty>(_providerRepository!));
        }

        public static void Configure(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }
    }

    #endregion [ FIM - ProviderExists]
}
