namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ICategory
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        int? ParentId           { get; }

        bool EhValido();
    }
}
