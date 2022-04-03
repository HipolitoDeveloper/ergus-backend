namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ITAddress
    {
        string? Description     { get; }
        string? MetaTitle       { get; }
        string? MetaKeyword     { get; }
        string? MetaDescription { get; }
        string? LongDescription { get; }

        bool EhValido();
    }
}
