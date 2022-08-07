using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class VerticalVariationResponse
    {
        public VerticalVariationResponse(VerticalVariation verticalVariation)
        {
            if (verticalVariation == null)
                return;

            this.Id = verticalVariation.Id;
            this.Code = verticalVariation.Code;
            this.Name = verticalVariation.Name;
            this.ExternalCode = verticalVariation.ExternalCode;
            this.Interface = verticalVariation.Interface;
            this.Order = verticalVariation.Order;
            this.GridId = verticalVariation.GridId;

            this.Grid = verticalVariation.Grid == null ? null : new GridResponse(verticalVariation.Grid!);
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
        public string Interface     { get; set; } = string.Empty;
        public int? Order           { get; set; }
        public int? GridId          { get; set; }

        public virtual GridResponse? Grid { get; set; }
    }
}
