using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class PaymentFormResponse
    {
        public PaymentFormResponse(PaymentForm paymentForm)
        {
            if (paymentForm == null)
                return;

            this.Id = paymentForm.Id;
            this.Code = paymentForm.Code;
            this.Name = paymentForm.Name;
            this.ExternalCode = paymentForm.ExternalCode;
            this.Active = paymentForm.Active;
            this.ProviderId = paymentForm.ProviderId;

            this.Provider = paymentForm.Provider == null ? null : new ProviderResponse(paymentForm.Provider!);
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
        public bool Active          { get; set; }
        public int? ProviderId      { get; set; }

        public virtual ProviderResponse? Provider { get; set; }
    }
}
