namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IVerticalVariation
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        string? Interface       { get; }
        int? Order              { get; }
        int? GridId             { get; }

        bool EhValido();
    }
}
