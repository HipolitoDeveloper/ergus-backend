namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ICategory<out TCategoryText> where TCategoryText : ICategoryText
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        int? ParentId           { get; }

        TCategoryText? Text     { get; }

        bool EhValido();
    }
}
