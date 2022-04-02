using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.CategoryTexts.Request
{
    public class CategoryTextUpdateRequest : BaseModel, ICategoryText
    {
        public int Id                   { get; set; }
        public string? Description      { get; set; } = string.Empty;
        public string? MetaTitle        { get; set; } = string.Empty;
        public string? MetaKeyword      { get; set; } = string.Empty;
        public string? MetaDescription  { get; set; } = string.Empty;
        public string? LongDescription  { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new CategoryTextUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CategoryTextUpdateRequestValidation : AbstractValidator<CategoryTextUpdateRequest>
    {
        public CategoryTextUpdateRequestValidation()
        {
            Include(new CategoryTextValidation());
        }
    }
}
