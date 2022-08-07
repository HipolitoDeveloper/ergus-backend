using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class SectionResponse
    {
        public SectionResponse(Section section)
        {
            if (section == null)
                return;

            this.Id = section.Id;
            this.Code = section.Code;
            this.Name = section.Name;
            this.ExternalCode = section.ExternalCode;
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
    }
}
