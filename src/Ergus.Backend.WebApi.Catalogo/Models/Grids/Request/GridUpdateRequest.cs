using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Grids.Request
{
    public class GridUpdateRequest : BaseModel, IGrid
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new GridUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class GridUpdateRequestValidation : AbstractValidator<GridUpdateRequest>
    {
        public GridUpdateRequestValidation()
        {
            Include(new GridValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
