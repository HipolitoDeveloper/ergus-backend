using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class AddressResponse
    {
        public AddressResponse(Address address)
        {
            if (address == null)
                return;

            this.Id = address.Id!.Value;
            this.Code = address.Code;
            this.ExternalCode = address.ExternalCode;
            this.CityCode = address.CityCode;
            this.District = address.District;
            this.Complement = address.Complement;
            this.Number = address.Number;
            this.Reference = address.Reference;
            this.ZipCode = address.ZipCode;
            this.AddressValue = address.AddressValue;
        }

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

        public bool Active          { get; } = true;
    }
}
