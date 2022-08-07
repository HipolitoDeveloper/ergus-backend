namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IStockUnit
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }

        bool EhValido();
    }
}
