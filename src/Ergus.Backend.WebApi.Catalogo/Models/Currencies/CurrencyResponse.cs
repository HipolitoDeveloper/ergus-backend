using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class CurrencyResponse
    {
        public CurrencyResponse(Currency currency)
        {
            if (currency == null)
                return;

            this.Id = currency.Id;
            this.Code = currency.Code;
            this.ExternalCode = currency.ExternalCode;
            this.Name = currency.Name;
            this.Symbol = currency.Symbol;
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string Symbol        { get; set; } = string.Empty;
    }
}
