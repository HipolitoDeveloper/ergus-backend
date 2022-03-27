using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class AdvertisementSkuResponse
    {
        public AdvertisementSkuResponse(AdvertisementSku advertisementSku)
        {
            if (advertisementSku == null)
                return;

            this.Id = advertisementSku.Id;
        }

        public int Id               { get; set; }
    }
}
