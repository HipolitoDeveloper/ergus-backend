using FluentValidation;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Addresses.Request
{
    public class AddressUpdateRequest : BaseModel, IAddress
    {
        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? CityCode     { get; set; } = string.Empty;
        public string? District     { get; set; } = string.Empty;
        public string? Complement   { get; set; } = string.Empty;
        public string? Number       { get; set; } = string.Empty;
        public string? Reference    { get; set; } = string.Empty;
        public string? ZipCode      { get; set; }
        public string? AddressValue { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AddressUpdateRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddressUpdateRequestValidation : AbstractValidator<AddressUpdateRequest>
    {
        public AddressUpdateRequestValidation()
        {
            Include(new AddressValidation());

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id é obrigatório")
                .GreaterThan(0).WithMessage("O Id deve ser maior do que zero");
        }
    }
}
