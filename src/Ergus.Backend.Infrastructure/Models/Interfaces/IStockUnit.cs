namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IStockUnit
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        string? Complement      { get; }
        int? AddressId          { get; }
        int? CompanyId          { get; }

        bool EhValido();
    }
}
