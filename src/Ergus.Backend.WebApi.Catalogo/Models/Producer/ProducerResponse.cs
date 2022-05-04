using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class ProducerResponse
    {
        public ProducerResponse(Producer provider)
        {
            if (provider == null)
                return;

            this.Id = provider.Id;
            this.Code = provider.Code;
            this.ExternalCode = provider.ExternalCode;
            this.Name = provider.Name;
            this.Email = provider.Email;
            this.Contact = provider.Contact;
            this.Site = provider.Site;
            this.FiscalDocument = provider.FiscalDocument;
            this.Document = provider.Document;
            this.Active = provider.Active;
            this.AddressId = provider.AddressId;
            this.PersonType = provider.PersonType;

            this.Address = provider.Address == null ? null : new AddressResponse(provider.Address!);
        }

        public int Id { get; set; }
        public string? Code { get; set; } = string.Empty;
        public string? ExternalCode { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Contact { get; set; } = string.Empty;
        public string? Site { get; set; } = string.Empty;
        public string? FiscalDocument { get; set; } = string.Empty;
        public string? Document { get; set; } = string.Empty;
        public bool Active { get; set; }
        public int? AddressId { get; set; }
        public string? PersonType { get; set; } = string.Empty;

        public virtual AddressResponse? Address { get; set; }
    }
}
