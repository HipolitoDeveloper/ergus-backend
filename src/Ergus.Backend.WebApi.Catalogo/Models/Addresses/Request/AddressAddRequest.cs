using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Addresses.Request
{
    public class AddressAddRequest : BaseModel, IAddress
    {
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
            ValidationResult = new AddressAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddressAddRequestValidation : AbstractValidator<AddressAddRequest>
    {
        public AddressAddRequestValidation()
        {
            Include(new AddressValidation());
        }
    }
}
