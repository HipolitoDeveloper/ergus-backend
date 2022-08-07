using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class HorizontalVariationResponse
    {
        public HorizontalVariationResponse(HorizontalVariation horizontalVariation)
        {
            if (horizontalVariation == null)
                return;

            this.Id = horizontalVariation.Id;
            this.Code = horizontalVariation.Code;
            this.Name = horizontalVariation.Name;
            this.ExternalCode = horizontalVariation.ExternalCode;
            this.Interface = horizontalVariation.Interface;
            this.Color = horizontalVariation.Color;
            this.Order = horizontalVariation.Order;
            this.GridId = horizontalVariation.GridId;

            this.Grid = horizontalVariation.Grid == null ? null : new GridResponse(horizontalVariation.Grid!);
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
        public string Interface     { get; set; } = string.Empty;
        public string Color         { get; set; } = string.Empty;
        public int? Order           { get; set; }
        public int? GridId          { get; set; }

        public virtual GridResponse? Grid { get; set; }
    }
}
