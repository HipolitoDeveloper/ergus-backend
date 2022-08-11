using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomUnitOfMeasureCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

        public CustomUnitOfMeasureCodeBeUniqueValidator(IUnitOfMeasureRepository unitOfMeasureRepository) : base()
        {
            this._unitOfMeasureRepository = unitOfMeasureRepository;
        }

        public override string Name => "CustomUnitOfMeasureCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var unitOfMeasure = value as UnitOfMeasure;
            var unitOfMeasureWithCode = this._unitOfMeasureRepository.GetByCode(unitOfMeasure!.Code).Result;

            if (unitOfMeasureWithCode == null) return true;

            return unitOfMeasure.Id == unitOfMeasureWithCode.Id;
        }
    }

    public static class StaticUnitOfMeasureCodeBeUniqueValidator
    {
        private static IUnitOfMeasureRepository? _unitOfMeasureRepository;

        public static IRuleBuilderOptions<T, TProperty> IsUnitOfMeasureCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomUnitOfMeasureCodeBeUniqueValidator<T, TProperty>(_unitOfMeasureRepository!));
        }

        public static void Configure(IUnitOfMeasureRepository unitOfMeasureRepository)
        {
            _unitOfMeasureRepository = unitOfMeasureRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ UnitOfMeasureExists]

    public class CustomUnitOfMeasureExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

        public CustomUnitOfMeasureExistsValidator(IUnitOfMeasureRepository unitOfMeasureRepository) : base()
        {
            this._unitOfMeasureRepository = unitOfMeasureRepository;
        }

        public override string Name => "UnitOfMeasureExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int unitOfMeasureId))
                return false;

            var unitOfMeasure = this._unitOfMeasureRepository.Get(unitOfMeasureId, false).Result;

            return (unitOfMeasure != null);
        }
    }

    public static class StaticUnitOfMeasureExistsValidator
    {
        private static IUnitOfMeasureRepository? _unitOfMeasureRepository;

        public static IRuleBuilderOptions<T, TProperty> UnitOfMeasureExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomUnitOfMeasureExistsValidator<T, TProperty>(_unitOfMeasureRepository!));
        }

        public static void Configure(IUnitOfMeasureRepository unitOfMeasureRepository)
        {
            _unitOfMeasureRepository = unitOfMeasureRepository;
        }
    }

    #endregion [ FIM - UnitOfMeasureExists]
}
