using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Models.Interfaces;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class StockUnitResponse
    {
        public StockUnitResponse(StockUnit stockUnit)
        {
            if (stockUnit == null)
                return;

            this.Id = stockUnit.Id;
            this.Code = stockUnit.Code;
            this.Name = stockUnit.Name;
            this.Complement = stockUnit.Complement;
            this.ExternalCode = stockUnit.ExternalCode;
            this.AddressId = stockUnit.AddressId;
            this.CompanyId = stockUnit.CompanyId;

            this.Address = stockUnit.Address == null ? null : new AddressResponse(stockUnit.Address!);
            this.Company = stockUnit.Company == null ? null : new CompanyResponse(stockUnit.Company!);
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string? Complement   { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
        public int? AddressId       { get; set; }
        public int? CompanyId       { get; set; }

        public virtual AddressResponse? Address { get; set; }
        public virtual CompanyResponse? Company { get; set; }
    }
}
