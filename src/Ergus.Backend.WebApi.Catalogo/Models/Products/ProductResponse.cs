using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class ProductResponse
    {
        public ProductResponse(Product product)
        {
            if (product == null)
                return;

            this.Id = product.Id;
            this.Code = product.Code;
            this.ExternalCode = product.ExternalCode;
            this.SkuCode = product.SkuCode;
            this.Name = product.Name;
            this.NCM = product.NCM;
            this.AdvertisementType = product.AdvertisementType;
            this.Active = product.Active;
            this.CategoryId = product.CategoryId;
            this.ProducerId = product.ProducerId;
            this.ProviderId = product.ProviderId;

            this.Category = product.Category == null ? null : new CategoryResponse(product.Category!);
            this.Producer = product.Producer == null ? null : new ProducerResponse(product.Producer!);
            this.Provider = product.Provider == null ? null : new ProviderResponse(product.Provider!);
        }

        public int Id                       { get; set; }
        public string? Code                 { get; set; } = string.Empty;
        public string? ExternalCode         { get; set; } = string.Empty;
        public string? SkuCode              { get; set; } = string.Empty;
        public string? Name                 { get; set; } = string.Empty;
        public string? NCM                  { get; set; } = string.Empty;
        public string? AdvertisementType    { get; set; } = string.Empty;
        public bool Active                  { get; set; }
        public int? CategoryId              { get; set; }
        public int? ProducerId              { get; set; }
        public int? ProviderId              { get; set; }

        public virtual CategoryResponse? Category { get; set; }
        public virtual ProducerResponse? Producer { get; set; }
        public virtual ProviderResponse? Provider { get; set; }
    }
}
