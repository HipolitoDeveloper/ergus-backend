using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class CategoryResponse
    {
        public CategoryResponse(Category category)
        {
            if (category == null)
                return;

            this.Id = category.Id;
            this.Code = category.Code;
            this.Name = category.Name;
            this.ExternalCode = category.ExternalCode;
            this.Active = category.Active;
            this.ParentId = category.ParentId;

            this.Parent = category.Parent == null ? null : new CategoryResponse(category.Parent!);
            this.Text = category.Text == null ? null : new CategoryTextResponse(category.Text);
        }

        public int Id               { get; set; }
        public string Code          { get; set; } = string.Empty;
        public string Name          { get; set; } = string.Empty;
        public string ExternalCode  { get; set; } = string.Empty;
        public bool Active          { get; set; }
        public int? ParentId        { get; set; }

        public virtual CategoryResponse? Parent     { get; set; }
        public virtual CategoryTextResponse? Text   { get; set; }
    }
}
