using Ergus.Backend.Infrastructure.Models;

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
            this.ExternalCode = stockUnit.ExternalCode;
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
    }
}
