using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Categories.Request
{
    public class ProductAttributeAddRequest : BaseModel, IProductAttribute
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public int MetadataId       { get; set; }
        public int ProductId        { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new ProductAttributeAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ProductAttributeAddRequestValidation : AbstractValidator<ProductAttributeAddRequest>
    {
        public ProductAttributeAddRequestValidation()
        {
            Include(new ProductAttributeValidation());
        }
    }
}
