namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ICurrency
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        string? Symbol          { get; }

        bool EhValido();
    }
}
