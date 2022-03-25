using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Categories.Request
{
    public class ProductAttributeUpdateRequest : BaseModel, IProductAttribute
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public int MetadataId       { get; set; }
        public int ProductId        { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new ProductAttributeUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ProductAttributeUpdateRequestValidation : AbstractValidator<ProductAttributeUpdateRequest>
    {
        public ProductAttributeUpdateRequestValidation()
        {
            Include(new ProductAttributeValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
