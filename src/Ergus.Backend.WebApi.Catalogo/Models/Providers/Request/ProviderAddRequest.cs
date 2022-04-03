using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.WebApi.Catalogo.Models.Addresses.Request;

namespace Ergus.Backend.WebApi.Catalogo.Models.Providers.Request
{
    public class ProviderAddRequest : BaseModel, IProvider<AddressAddRequest>
    {
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public string? Name             { get; set; } = string.Empty;
        public string? Email            { get; set; } = string.Empty;
        public string? Contact          { get; set; } = string.Empty;
        public string? Site             { get; set; } = string.Empty;
        public string? FiscalDocument   { get; set; } = string.Empty;
        public string? Document         { get; set; } = string.Empty;
        public bool Active              { get; set; }
        public int? AddressId           { get; set; }
        public string? PersonType       { get; set; } = string.Empty;

        public AddressAddRequest? Address   { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new ProviderAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ProviderAddRequestValidation : AbstractValidator<ProviderAddRequest>
    {
        public ProviderAddRequestValidation()
        {
            Include(new ProviderValidation());
        }
    }
}
