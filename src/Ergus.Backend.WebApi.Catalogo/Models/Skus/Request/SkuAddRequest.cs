using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Skus.Request
{
    public class SkuAddRequest : BaseModel, ISku
    {
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
            ValidationResult = new SkuAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SkuAddRequestValidation : AbstractValidator<SkuAddRequest>
    {
        public SkuAddRequestValidation()
        {
            Include(new SkuValidation());
        }
    }
}
