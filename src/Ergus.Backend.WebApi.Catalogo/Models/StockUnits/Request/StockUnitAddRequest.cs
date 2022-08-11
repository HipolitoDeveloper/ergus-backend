using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Ergus.Backend.WebApi.Catalogo.Models.StockUnits.Request
{
    public class StockUnitAddRequest : BaseModel, IStockUnit
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Complement   { get; set; } = string.Empty;
        public int? AddressId       { get; set; }
        public int? CompanyId       { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new StockUnitAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StockUnitAddRequestValidation : AbstractValidator<StockUnitAddRequest>
    {
        public StockUnitAddRequestValidation()
        {
            Include(new StockUnitValidation());
        }
    }
}
