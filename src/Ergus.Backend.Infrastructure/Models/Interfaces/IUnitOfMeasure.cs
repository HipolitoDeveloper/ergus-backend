namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface IUnitOfMeasure
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Description     { get; }
        string? Acronym         { get; }

        bool EhValido();
    }
}
