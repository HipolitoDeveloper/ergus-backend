namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ISku
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? SkuCode         { get; }
        string? Name            { get; }
        string? Reference       { get; }
        string? Bar             { get; }
        decimal? Height         { get; }
        decimal? Width          { get; }
        decimal? Depth          { get; }
        decimal? Weight         { get; }
        decimal? Cost           { get; }
        int? ProductId          { get; }

        bool EhValido();
    }
}
