using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomStockUnitCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IStockUnitRepository _stockUnitRepository;

        public CustomStockUnitCodeBeUniqueValidator(IStockUnitRepository stockUnitRepository) : base()
        {
            this._stockUnitRepository = stockUnitRepository;
        }

        public override string Name => "CustomStockUnitCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var stockUnit = value as StockUnit;
            var stockUnitWithCode = this._stockUnitRepository.GetByCode(stockUnit!.Code).Result;

            if (stockUnitWithCode == null) return true;

            return stockUnit.Id == stockUnitWithCode.Id;
        }
    }

    public static class StaticStockUnitCodeBeUniqueValidator
    {
        private static IStockUnitRepository? _stockUnitRepository;

        public static IRuleBuilderOptions<T, TProperty> IsStockUnitCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomStockUnitCodeBeUniqueValidator<T, TProperty>(_stockUnitRepository!));
        }

        public static void Configure(IStockUnitRepository stockUnitRepository)
        {
            _stockUnitRepository = stockUnitRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ StockUnitExists]

    public class CustomStockUnitExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IStockUnitRepository _stockUnitRepository;

        public CustomStockUnitExistsValidator(IStockUnitRepository stockUnitRepository) : base()
        {
            this._stockUnitRepository = stockUnitRepository;
        }

        public override string Name => "StockUnitExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int stockUnitId))
                return false;

            var stockUnit = this._stockUnitRepository.Get(stockUnitId, false).Result;

            return (stockUnit != null);
        }
    }

    public static class StaticStockUnitExistsValidator
    {
        private static IStockUnitRepository? _stockUnitRepository;

        public static IRuleBuilderOptions<T, TProperty> StockUnitExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomStockUnitExistsValidator<T, TProperty>(_stockUnitRepository!));
        }

        public static void Configure(IStockUnitRepository stockUnitRepository)
        {
            _stockUnitRepository = stockUnitRepository;
        }
    }

    #endregion [ FIM - StockUnitExists]
}
