namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    public interface ICategory<out TCategoryText> where TCategoryText : ITAddress
    {
        string? Code            { get; }
        string? ExternalCode    { get; }
        string? Name            { get; }
        int? ParentId           { get; }

        TCategoryText? Text     { get; }

        bool EhValido();
    }
}
