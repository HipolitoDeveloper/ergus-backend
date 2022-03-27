namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IPriceList
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        DateTime InitDate       { get; }
        DateTime EndDate        { get; }
        string Name             { get; }
        decimal Value           { get; }
        string? Type            { get; }
        string? AdjustmentType  { get; }
        string? OperationType   { get; }
        int SaleMaxAmount       { get; }
        int? ParentId           { get; }

        bool EhValido();
    }
}
