using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.Application.Tests.Helpers
{
    internal static class CreateObject
    {
        public static Category GetCategory(int id, int? parentId)
        {
            return new Category(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                name: "Categoria",
                active: true,
                parentId: parentId
            );
        }

        public static Producer GetProducer(int id)
        {
            return new Producer(
                id: id,
                code: "COD"
            );
        }

        public static Product GetProduct(int id)
        {
            return new Product(
                id: id,
                code: "COD",
                externalCode: "ECOD",
                skuCode: "SCOD",
                name: "Produto",
                ncm: "NCM",
                advertisementType: Infrastructure.Helpers.TipoAnuncio.GoldSpecial,
                active: true,
                producerId: 1,
                categoryId: 2,
                providerId: 3
            );
        }

        public static Provider GetProvider(int id)
        {
            return new Provider(
                id: id,
                code: "COD"
            );
        }
    }
}
