using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class ProviderResponse
    {
        public ProviderResponse(Provider provider)
        {
            if (provider == null)
                return;

            this.Id = provider.Id;
        }

        public int Id               { get; set; }
    }
}
