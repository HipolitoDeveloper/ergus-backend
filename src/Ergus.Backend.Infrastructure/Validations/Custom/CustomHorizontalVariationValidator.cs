using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomHorizontalVariationCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IHorizontalVariationRepository _horizontalVariationRepository;

        public CustomHorizontalVariationCodeBeUniqueValidator(IHorizontalVariationRepository horizontalVariationRepository) : base()
        {
            this._horizontalVariationRepository = horizontalVariationRepository;
        }

        public override string Name => "CustomHorizontalVariationCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var horizontalVariation = value as HorizontalVariation;
            var horizontalVariationWithCode = this._horizontalVariationRepository.GetByCode(horizontalVariation!.Code).Result;

            if (horizontalVariationWithCode == null) return true;

            return horizontalVariation.Id == horizontalVariationWithCode.Id;
        }
    }

    public static class StaticHorizontalVariationCodeBeUniqueValidator
    {
        private static IHorizontalVariationRepository? _horizontalVariationRepository;

        public static IRuleBuilderOptions<T, TProperty> IsHorizontalVariationCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomHorizontalVariationCodeBeUniqueValidator<T, TProperty>(_horizontalVariationRepository!));
        }

        public static void Configure(IHorizontalVariationRepository horizontalVariationRepository)
        {
            _horizontalVariationRepository = horizontalVariationRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ HorizontalVariationExists]

    public class CustomHorizontalVariationExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IHorizontalVariationRepository _horizontalVariationRepository;

        public CustomHorizontalVariationExistsValidator(IHorizontalVariationRepository horizontalVariationRepository) : base()
        {
            this._horizontalVariationRepository = horizontalVariationRepository;
        }

        public override string Name => "HorizontalVariationExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int horizontalVariationId))
                return false;

            var horizontalVariation = this._horizontalVariationRepository.Get(horizontalVariationId, false).Result;

            return (horizontalVariation != null);
        }
    }

    public static class StaticHorizontalVariationExistsValidator
    {
        private static IHorizontalVariationRepository? _horizontalVariationRepository;

        public static IRuleBuilderOptions<T, TProperty> HorizontalVariationExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomHorizontalVariationExistsValidator<T, TProperty>(_horizontalVariationRepository!));
        }

        public static void Configure(IHorizontalVariationRepository horizontalVariationRepository)
        {
            _horizontalVariationRepository = horizontalVariationRepository;
        }
    }

    #endregion [ FIM - HorizontalVariationExists]
}
