using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.PriceLists.Request
{
    public class PriceListUpdateRequest : BaseModel, IPriceList
    {
        public int Id                   { get; set; }
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
            ValidationResult = new PriceListUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PriceListUpdateRequestValidation : AbstractValidator<PriceListUpdateRequest>
    {
        public PriceListUpdateRequestValidation()
        {
            Include(new PriceListValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
