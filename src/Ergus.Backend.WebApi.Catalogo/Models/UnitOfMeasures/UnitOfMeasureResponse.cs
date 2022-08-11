using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class UnitOfMeasureResponse
    {
        public UnitOfMeasureResponse(UnitOfMeasure unitOfMeasure)
        {
            if (unitOfMeasure == null)
                return;

            this.Id = unitOfMeasure.Id;
            this.Code = unitOfMeasure.Code;
            this.Description = unitOfMeasure.Description;
            this.Acronym = unitOfMeasure.Acronym;
            this.ExternalCode = unitOfMeasure.ExternalCode;
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Description   { get; set; } = string.Empty;
        public string? Acronym      { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
    }
}
