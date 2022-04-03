namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IAddress
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? CityCode        { get; }
        string? District        { get; }
        string? Complement      { get; }
        string? Number          { get; }
        string? Reference       { get; }
        string? ZipCode         { get; }
        string? AddressValue    { get; }

        bool EhValido();
    }
}
