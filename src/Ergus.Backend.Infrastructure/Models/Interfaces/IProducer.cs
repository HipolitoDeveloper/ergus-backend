namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IProducer<out TAddress> where TAddress : IAddress
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        string? Email           { get; }
        string? Contact         { get; }
        string? Site            { get; }
        string? FiscalDocument  { get; }
        string? Document        { get; }
        string? PersonType      { get; }
        int? AddressId          { get; }

        TAddress? Address       { get; }

        bool EhValido();
    }
}
