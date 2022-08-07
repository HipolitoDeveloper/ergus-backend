using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace Ergus.Backend.WebApi.Catalogo.Models.Grids.Request
{
    public class GridAddRequest : BaseModel, IGrid
    {
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;

        public override bool EhValido()
        {
            ValidationResult = new GridAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class GridAddRequestValidation : AbstractValidator<GridAddRequest>
    {
        public GridAddRequestValidation()
        {
            Include(new GridValidation());
        }
    }
}
