namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ICategoryText
    {
        string? Description     { get; }
        string? MetaTitle       { get; }
        string? MetaKeyword     { get; }
        string? MetaDescription { get; }
        string? LongDescription { get; }

        bool EhValido();
    }
}
