using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class IntegrationResponse
    {
        public IntegrationResponse(Integration integration)
        {
            if (integration == null)
                return;

            this.Id = integration.Id;
        }

        public int Id               { get; set; }
    }
}
