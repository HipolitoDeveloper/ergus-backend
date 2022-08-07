using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Sections.Request
{
    public class SectionUpdateRequest : BaseModel, ISection
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new SectionUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SectionUpdateRequestValidation : AbstractValidator<SectionUpdateRequest>
    {
        public SectionUpdateRequestValidation()
        {
            Include(new SectionValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
