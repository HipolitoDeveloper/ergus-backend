namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IProductAttribute
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        int MetadataId          { get; }
        int ProductId           { get; }

        bool EhValido();
    }
}
