using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.CategoryTexts.Request
{
    public class CategoryTextAddRequest : BaseModel, ITAddress
    {
        public string? Description      { get; set; } = string.Empty;
        public string? MetaTitle        { get; set; } = string.Empty;
        public string? MetaKeyword      { get; set; } = string.Empty;
        public string? MetaDescription  { get; set; } = string.Empty;
        public string? LongDescription  { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new CategoryTextAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CategoryTextAddRequestValidation : AbstractValidator<CategoryTextAddRequest>
    {
        public CategoryTextAddRequestValidation()
        {
            Include(new CategoryTextValidation());
        }
    }
}
