namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IAdvertisement
    {
        string? Code                    { get; }
        string? ExternalCode            { get; }
        string? SkuCode                 { get; }
        string? IntegrationCode         { get; }
        string? Name                    { get; }
        string? AdvertisementType       { get; }
        string? Status                  { get; }
        int? IntegrationId              { get; }
        int? ProductId                  { get; }

        bool EhValido();
    }
}
