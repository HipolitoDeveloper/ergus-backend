using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Categories.Request
{
    public class CategoryUpdateRequest : BaseModel, ICategory
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public bool Active          { get; set; }
        public int? ParentId        { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new CategoryUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CategoryUpdateRequestValidation : AbstractValidator<CategoryUpdateRequest>
    {
        public CategoryUpdateRequestValidation()
        {
            Include(new CategoryValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
