using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("categoria_texto")]
    public class CategoryText : BaseModel, ITAddress
    {
        public CategoryText() { }

        public CategoryText(int id, string? description, string? metaTitle, string? metaKeyword, string? metaDescription, string? longDescription)
        {
            Id = id;
            Description = description;
            MetaTitle = metaTitle;
            MetaKeyword = metaKeyword;
            MetaDescription = metaDescription;
            LongDescription = longDescription;
        }

        #region [ Propriedades ]

        [Column("cat_id")]
        public int? Id { get; private set; }

        [Column("cat_descricao")]
        public string? Description { get; private set; } = string.Empty;

        [Column("cat_metatitle")]
        public string? MetaTitle { get; private set; } = string.Empty;

        [Column("cat_metakeyword")]
        public string? MetaKeyword { get; private set; } = string.Empty;

        [Column("cat_metadesc")]
        public string? MetaDescription { get; private set; } = string.Empty;

        [Column("cat_descricao_longa")]
        public byte[]? RawLongDescription { get; private set; }

        [NotMapped]
        public string? LongDescription
        {
            get
            {
                if (RawLongDescription != null)
                    return Encoding.UTF8.GetString(RawLongDescription!);

                return null;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    RawLongDescription = Encoding.UTF8.GetBytes(value);
            }
        }

        [ForeignKey(nameof(Id))]
        public virtual Category Category { get; set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static CategoryText Criar(string? description, string? metaTitle, string? metaKeyword, string? metaDescription, string? longDescription)
        {
            var categoryText = new CategoryText();

            categoryText.Description = description;
            categoryText.MetaTitle = metaTitle;
            categoryText.MetaKeyword = metaKeyword;
            categoryText.MetaDescription = metaDescription;
            categoryText.LongDescription = longDescription;

            return categoryText;
        }

        public void DefinirId(int id)
        {
            this.Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new CategoryTextModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(CategoryText newCategoryText)
        {
            this.Id = newCategoryText.Id;
            this.Description = newCategoryText.Description;
            this.MetaTitle = newCategoryText.MetaTitle;
            this.MetaKeyword = newCategoryText.MetaKeyword;
            this.MetaDescription = newCategoryText.MetaDescription;
            this.LongDescription = newCategoryText.LongDescription;
        }

        #endregion [ FIM - Metodos ]
    }

    public class CategoryTextModelValidation : AbstractValidator<CategoryText>
    {
        public CategoryTextModelValidation()
        {
            Include(new CategoryTextValidation());

            When(x => x.Id.HasValue, () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .CategoryExists().WithMessage(x => $"O Id {x.Id} não faz referência a nenhuma categoria no banco de dados");
            });
        }
    }
}
