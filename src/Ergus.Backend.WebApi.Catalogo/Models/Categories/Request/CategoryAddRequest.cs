using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Categories.Request
{
    public class CategoryAddRequest : BaseModel, ICategory
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public int? ParentId        { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new CategoryAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CategoryAddRequestValidation : AbstractValidator<CategoryAddRequest>
    {
        public CategoryAddRequestValidation()
        {
            Include(new CategoryValidation());
        }
    }
}
