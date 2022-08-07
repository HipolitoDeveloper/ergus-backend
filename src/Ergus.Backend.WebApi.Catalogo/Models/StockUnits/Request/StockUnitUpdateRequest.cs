using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.StockUnits.Request
{
    public class StockUnitUpdateRequest : BaseModel, IStockUnit
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new StockUnitUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StockUnitUpdateRequestValidation : AbstractValidator<StockUnitUpdateRequest>
    {
        public StockUnitUpdateRequestValidation()
        {
            Include(new StockUnitValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
