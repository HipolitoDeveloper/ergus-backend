using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Skus.Request
{
    public class SkuUpdateRequest : BaseModel, ISku
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? SkuCode      { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Reference    { get; set; } = string.Empty;
        public string? Bar          { get; set; } = string.Empty;
        public decimal? Height      { get; set; }
        public decimal? Width       { get; set; }
        public decimal? Depth       { get; set; }
        public decimal? Weight      { get; set; }
        public decimal? Cost        { get; set; }
        public int? ProductId       { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new SkuUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SkuUpdateRequestValidation : AbstractValidator<SkuUpdateRequest>
    {
        public SkuUpdateRequestValidation()
        {
            Include(new SkuValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
