namespace Ergus.Backend.Infrastructure.Models.Interfaces
{
    internal interface IGeneric
    {
        public DateTime CreatedDate     { get; }
        public DateTime UpdatedDate     { get; }
        public bool WasRemoved          { get; }
        public int? RemovedId           { get; }
        public DateTime? RemovedDate    { get; }
    }
}
