using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class PriceListResponse
    {
        public PriceListResponse(PriceList priceList)
        {
            if (priceList == null)
                return;

            this.Id = priceList.Id;
            this.Code = priceList.Code;
            this.ExternalCode = priceList.ExternalCode;
            this.InitDate = priceList.InitDate;
            this.EndDate = priceList.EndDate;
            this.Name = priceList.Name;
            this.Value = priceList.Value;
            this.Type = priceList.Type;
            this.AdjustmentType = priceList.AdjustmentType;
            this.OperationType = priceList.OperationType;
            this.SaleMaxAmount = priceList.SaleMaxAmount;
            this.ParentId = priceList.ParentId;

            this.Active = !priceList.WasRemoved;
        }

        public int Id                   { get; set; }
        public string? Code             { get; set; } = string.Empty;
        public string? ExternalCode     { get; set; } = string.Empty;
        public DateTime InitDate        { get; set; }
        public DateTime EndDate         { get; set; }
        public string? Name             { get; set; } = string.Empty;
        public decimal Value            { get; set; }
        public string? Type             { get; set; }
        public string? AdjustmentType   { get; set; }
        public string? OperationType    { get; set; }
        public int SaleMaxAmount        { get; set; }
        public int? ParentId            { get; set; }

        public bool Active              { get; set; }
    }
}
