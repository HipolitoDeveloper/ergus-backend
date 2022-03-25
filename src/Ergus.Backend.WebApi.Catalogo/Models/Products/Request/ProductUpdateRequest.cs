using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Products.Request
{
    public class ProductUpdateRequest : BaseModel, IProduct
    {
        public int Id                       { get; set; }
        public string? Code                 { get; set; } = string.Empty;
        public string? ExternalCode         { get; set; } = string.Empty;
        public string? SkuCode              { get; set; } = string.Empty;
        public string? Name                 { get; set; } = string.Empty;
        public string? NCM                  { get; set; } = string.Empty;
        public string? AdvertisementType    { get; set; } = string.Empty;
        public bool Active                  { get; set; }
        public int? CategoryId              { get; set; }
        public int? ProducerId              { get; set; }
        public int? ProviderId              { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new ProductUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ProductUpdateRequestValidation : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidation()
        {
            Include(new ProductValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
