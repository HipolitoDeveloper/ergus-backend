using FluentValidation;
using FluentValidation.Results;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;

namespace Ergus.Backend.WebApi.Catalogo.Models.Advertisements.Request
{
    public class AdvertisementAddRequest : BaseModel, IAdvertisement
    {
        public string? Code                 { get; set; } = string.Empty;
        public string? ExternalCode         { get; set; } = string.Empty;
        public string? SkuCode              { get; set; } = string.Empty;
        public string? IntegrationCode      { get; set; } = string.Empty;
        public string? Name                 { get; set; } = string.Empty;
        public string? AdvertisementType    { get; set; } = string.Empty;
        public string? Status               { get; set; } = string.Empty;
        public int? IntegrationId           { get; set; }
        public int? ProductId               { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdvertisementAddRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdvertisementAddRequestValidation : AbstractValidator<AdvertisementAddRequest>
    {
        public AdvertisementAddRequestValidation()
        {
            Include(new AdvertisementValidation());
        }
    }
}
