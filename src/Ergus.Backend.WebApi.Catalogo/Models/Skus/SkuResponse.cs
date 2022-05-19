using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class SkuResponse
    {
        public SkuResponse(Sku sku)
        {
            if (sku == null)
                return;

            this.Id = sku.Id;
            this.Code = sku.Code;
            this.ExternalCode = sku.ExternalCode;
            this.SkuCode = sku.SkuCode;
            this.Name = sku.Name;
            this.Reference = sku.Reference;
            this.Bar = sku.Bar;
            this.Height = sku.Height;
            this.Width = sku.Width;
            this.Depth = sku.Depth;
            this.Weight = sku.Weight;
            this.Cost = sku.Cost;
            this.ProductId = sku.ProductId;

            this.Active = !sku.WasRemoved;

            this.Product = sku.Product == null ? null : new ProductResponse(sku.Product!);
        }

        public int Id               { get; set; }
        public string? Code         { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? SkuCode      { get; set; } = string.Empty;
        public string? Name         { get; set; } = string.Empty;
        public string? Reference    { get; set; } = string.Empty;
        public string? Bar          { get; set; } = string.Empty;
        public decimal? Height      { get; set; }
        public decimal? Width       { get; set; }
        public decimal? Depth       { get; set; }
        public decimal? Weight      { get; set; }
        public decimal? Cost        { get; set; }
        public int? ProductId       { get; set; }

        public bool Active          { get; set; }

        public virtual ProductResponse? Product { get; set; }
    }
}
