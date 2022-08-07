using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomVerticalVariationCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IVerticalVariationRepository _verticalVariationRepository;

        public CustomVerticalVariationCodeBeUniqueValidator(IVerticalVariationRepository verticalVariationRepository) : base()
        {
            this._verticalVariationRepository = verticalVariationRepository;
        }

        public override string Name => "CustomVerticalVariationCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var verticalVariation = value as VerticalVariation;
            var verticalVariationWithCode = this._verticalVariationRepository.GetByCode(verticalVariation!.Code).Result;

            if (verticalVariationWithCode == null) return true;

            return verticalVariation.Id == verticalVariationWithCode.Id;
        }
    }

    public static class StaticVerticalVariationCodeBeUniqueValidator
    {
        private static IVerticalVariationRepository? _verticalVariationRepository;

        public static IRuleBuilderOptions<T, TProperty> IsVerticalVariationCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomVerticalVariationCodeBeUniqueValidator<T, TProperty>(_verticalVariationRepository!));
        }

        public static void Configure(IVerticalVariationRepository verticalVariationRepository)
        {
            _verticalVariationRepository = verticalVariationRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ VerticalVariationExists]

    public class CustomVerticalVariationExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IVerticalVariationRepository _verticalVariationRepository;

        public CustomVerticalVariationExistsValidator(IVerticalVariationRepository verticalVariationRepository) : base()
        {
            this._verticalVariationRepository = verticalVariationRepository;
        }

        public override string Name => "VerticalVariationExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int verticalVariationId))
                return false;

            var verticalVariation = this._verticalVariationRepository.Get(verticalVariationId, false).Result;

            return (verticalVariation != null);
        }
    }

    public static class StaticVerticalVariationExistsValidator
    {
        private static IVerticalVariationRepository? _verticalVariationRepository;

        public static IRuleBuilderOptions<T, TProperty> VerticalVariationExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomVerticalVariationExistsValidator<T, TProperty>(_verticalVariationRepository!));
        }

        public static void Configure(IVerticalVariationRepository verticalVariationRepository)
        {
            _verticalVariationRepository = verticalVariationRepository;
        }
    }

    #endregion [ FIM - VerticalVariationExists]
}
