using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class ProductAttributeResponse
    {
        public ProductAttributeResponse(ProductAttribute productAttribute)
        {
            if (productAttribute == null)
                return;

            this.Id = productAttribute.Id;
            this.Code = productAttribute.Code;
            this.ExternalCode = productAttribute.ExternalCode;
            this.MetadataId = productAttribute.MetadataId;
            this.ProductId = productAttribute.ProductId;

            this.Metadata = productAttribute.Metadata == null ? null : new MetadataResponse(productAttribute.Metadata!);
            this.Product = productAttribute.Product == null ? null : new ProductResponse(productAttribute.Product!);
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
        public int? MetadataId      { get; set; }
        public int? ProductId       { get; set; }

        public virtual MetadataResponse? Metadata   { get; set; }
        public virtual ProductResponse? Product     { get; set; }
    }
}
