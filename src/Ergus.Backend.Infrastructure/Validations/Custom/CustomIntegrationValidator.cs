using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomIntegrationCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IIntegrationRepository _integrationRepository;

        public CustomIntegrationCodeBeUniqueValidator(IIntegrationRepository integrationRepository) : base()
        {
            this._integrationRepository = integrationRepository;
        }

        public override string Name => "CustomIntegrationCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var integration = value as Integration;
            var integrationWithCode = this._integrationRepository.GetByCode(integration!.Code).Result;

            if (integrationWithCode == null) return true;

            return integration.Id == integrationWithCode.Id;
        }
    }

    public static class StaticIntegrationCodeBeUniqueValidator
    {
        private static IIntegrationRepository? _integrationRepository;

        public static IRuleBuilderOptions<T, TProperty> IsIntegrationCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomIntegrationCodeBeUniqueValidator<T, TProperty>(_integrationRepository!));
        }

        public static void Configure(IIntegrationRepository integrationRepository)
        {
            _integrationRepository = integrationRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ IntegrationExists]

    public class CustomIntegrationExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IIntegrationRepository _integrationRepository;

        public CustomIntegrationExistsValidator(IIntegrationRepository integrationRepository) : base()
        {
            this._integrationRepository = integrationRepository;
        }

        public override string Name => "IntegrationExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int integrationId))
                return false;

            var integration = this._integrationRepository.Get(integrationId, false).Result;

            return (integration != null);
        }
    }

    public static class StaticIntegrationExistsValidator
    {
        private static IIntegrationRepository? _integrationRepository;

        public static IRuleBuilderOptions<T, TProperty> IntegrationExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomIntegrationExistsValidator<T, TProperty>(_integrationRepository!));
        }

        public static void Configure(IIntegrationRepository integrationRepository)
        {
            _integrationRepository = integrationRepository;
        }
    }

    #endregion [ FIM - IntegrationExists]
}
