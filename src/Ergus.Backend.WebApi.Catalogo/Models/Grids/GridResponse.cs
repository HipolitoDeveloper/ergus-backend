using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class GridResponse
    {
        public GridResponse(Grid grid)
        {
            if (grid == null)
                return;

            this.Id = grid.Id;
            this.Code = grid.Code;
            this.Name = grid.Name;
            this.ExternalCode = grid.ExternalCode;
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
    }
}
