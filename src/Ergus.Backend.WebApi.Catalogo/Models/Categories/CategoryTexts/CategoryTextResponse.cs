using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class CategoryTextResponse
    {
        public CategoryTextResponse(CategoryText categoryText)
        {
            if (categoryText == null)
                return;

            this.Id = categoryText.Id ?? 0;
            this.Description = categoryText.Description;
            this.MetaTitle = categoryText.MetaTitle;
            this.MetaKeyword = categoryText.MetaKeyword;
            this.MetaDescription = categoryText.MetaDescription;
            this.LongDescription = categoryText.LongDescription;
        }

        public int Id                   { get; set; }
        public string? Description      { get; set; } = string.Empty;
        public string? MetaTitle        { get; set; } = string.Empty;
        public string? MetaKeyword      { get; set; } = string.Empty;
        public string? MetaDescription  { get; set; }
        public string? LongDescription  { get; set; }
    }
}
