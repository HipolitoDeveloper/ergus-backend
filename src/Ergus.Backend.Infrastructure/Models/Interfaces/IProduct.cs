namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IProduct
    {
        string? Code                    { get; }
        string? ExternalCode            { get; }
        string? SkuCode                 { get; }
        string? Name                    { get; }
        string? NCM                     { get; }
        string? AdvertisementType       { get; }
        int? CategoryId                 { get; }
        int? ProducerId                 { get; }
        int? ProviderId                 { get; }

        bool EhValido();
    }
}
