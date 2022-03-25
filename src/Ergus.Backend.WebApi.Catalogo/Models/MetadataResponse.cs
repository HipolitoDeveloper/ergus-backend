using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class MetadataResponse
    {
        public MetadataResponse(Metadata metadata)
        {
            if (metadata == null)
                return;

            this.Id = metadata.Id;
        }

        public int Id               { get; set; }
    }
}
