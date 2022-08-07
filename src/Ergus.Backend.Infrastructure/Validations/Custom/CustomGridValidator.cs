using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomGridCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IGridRepository _gridRepository;

        public CustomGridCodeBeUniqueValidator(IGridRepository gridRepository) : base()
        {
            this._gridRepository = gridRepository;
        }

        public override string Name => "CustomGridCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var grid = value as Grid;
            var gridWithCode = this._gridRepository.GetByCode(grid!.Code).Result;

            if (gridWithCode == null) return true;

            return grid.Id == gridWithCode.Id;
        }
    }

    public static class StaticGridCodeBeUniqueValidator
    {
        private static IGridRepository? _gridRepository;

        public static IRuleBuilderOptions<T, TProperty> IsGridCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomGridCodeBeUniqueValidator<T, TProperty>(_gridRepository!));
        }

        public static void Configure(IGridRepository gridRepository)
        {
            _gridRepository = gridRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ GridExists]

    public class CustomGridExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IGridRepository _gridRepository;

        public CustomGridExistsValidator(IGridRepository gridRepository) : base()
        {
            this._gridRepository = gridRepository;
        }

        public override string Name => "GridExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int gridId))
                return false;

            var grid = this._gridRepository.Get(gridId, false).Result;

            return (grid != null);
        }
    }

    public static class StaticGridExistsValidator
    {
        private static IGridRepository? _gridRepository;

        public static IRuleBuilderOptions<T, TProperty> GridExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomGridExistsValidator<T, TProperty>(_gridRepository!));
        }

        public static void Configure(IGridRepository gridRepository)
        {
            _gridRepository = gridRepository;
        }
    }

    #endregion [ FIM - GridExists]
}
