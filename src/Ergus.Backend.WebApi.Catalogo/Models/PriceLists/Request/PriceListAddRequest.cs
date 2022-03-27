using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.PriceLists.Request
{
    public class PriceListAddRequest : BaseModel, IPriceList
    {
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public DateTime InitDate        { get; set; }
        public DateTime EndDate         { get; set; }
        public string? Name             { get; set; } = string.Empty;
        public decimal Value            { get; set; }
        public string? Type             { get; set; }
        public string? AdjustmentType   { get; set; }
        public string? OperationType    { get; set; }
        public int SaleMaxAmount        { get; set; }
        public int? ParentId            { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new PriceListAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PriceListAddRequestValidation : AbstractValidator<PriceListAddRequest>
    {
        public PriceListAddRequestValidation()
        {
            Include(new PriceListValidation());
        }
    }
}
