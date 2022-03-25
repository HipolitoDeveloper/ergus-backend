using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class AdvertisementResponse
    {
        public AdvertisementResponse(Advertisement advertisement)
        {
            if (advertisement == null)
                return;

            this.Id = advertisement.Id;
            this.Code = advertisement.Code;
            this.ExternalCode = advertisement.ExternalCode;
            this.SkuCode = advertisement.SkuCode;
            this.IntegrationCode = advertisement.IntegrationCode;
            this.Name = advertisement.Name;
            this.AdvertisementType = advertisement.AdvertisementType;
            this.Status = advertisement.Status;
            this.IntegrationId = advertisement.IntegrationId;
            this.ProductId = advertisement.ProductId;

            this.Integration = advertisement.Integration == null ? null : new IntegrationResponse(advertisement.Integration!);
            this.Product = advertisement.Product == null ? null : new ProductResponse(advertisement.Product!);
        }

        public int Id                       { get; set; }
        public string? Code                 { get; set; } = string.Empty;
        public string? ExternalCode         { get; set; } = string.Empty;
        public string? SkuCode              { get; set; } = string.Empty;
        public string? IntegrationCode      { get; set; } = string.Empty;
        public string? Name                 { get; set; } = string.Empty;
        public string? AdvertisementType    { get; set; } = string.Empty;
        public string? Status               { get; set; } = string.Empty;
        public int? IntegrationId           { get; set; }
        public int? ProductId               { get; set; }

        public virtual IntegrationResponse? Integration { get; set; }
        public virtual ProductResponse? Product         { get; set; }
    }
}
