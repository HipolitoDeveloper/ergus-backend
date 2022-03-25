using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class ProducerResponse
    {
        public ProducerResponse(Producer producer)
        {
            if (producer == null)
                return;

            this.Id = producer.Id;
        }

        public int Id               { get; set; }
    }
}
