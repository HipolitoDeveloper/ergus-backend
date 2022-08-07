namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IHorizontalVariation
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        string? Interface       { get; }
        string? Color           { get; }
        int? Order              { get; }
        int? GridId             { get; }

        bool EhValido();
    }
}
