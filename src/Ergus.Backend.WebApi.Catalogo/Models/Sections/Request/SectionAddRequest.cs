using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Ergus.Backend.WebApi.Catalogo.Models.Sections.Request
{
    public class SectionAddRequest : BaseModel, ISection
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new SectionAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SectionAddRequestValidation : AbstractValidator<SectionAddRequest>
    {
        public SectionAddRequestValidation()
        {
            Include(new SectionValidation());
        }
    }
}
