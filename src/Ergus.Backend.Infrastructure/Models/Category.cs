using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("categoria")]
    public class Category : BaseModel, ICategory<CategoryText>, IGeneric
    {
        public Category() { }

        public Category(int id, string code, string externalCode, string name, bool active, int? parentId, CategoryText? text)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            Active = active;
            ParentId = parentId;
            UpdatedDate = DateTime.UtcNow;

            Text = text;
        }

        #region [ Propriedades ]

        [Column("cat_id")]
        public int Id { get; private set; }

        [Column("cat_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("cat_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("cat_nome")]
        public string Name { get; private set; } = String.Empty;

        [Column("cat_ativo")]
        public bool Active { get; private set; }

        [Column("cat_id_pai")]
        [ForeignKey(nameof(Parent))]
        public int? ParentId { get; private set; }


        [Column("cat_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("cat_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("cat_removido")]
        public bool WasRemoved { get; private set; }

        [Column("cat_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("cat_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        public Category? Parent { get; private set; }

        [InverseProperty(nameof(CategoryText.Category))]
        public CategoryText? Text { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Category Criar(string code, string externalCode, string name, int? parentId, CategoryText? text)
        {
            var category = new Category();

            category.Id = 0;
            category.Code = code;
            category.ExternalCode = externalCode;
            category.Name = name;
            category.ParentId = parentId;
            category.RemovedId = 0;
            category.CreatedDate = DateTime.UtcNow;
            category.UpdatedDate = DateTime.UtcNow;
            category.WasRemoved = false;

            category.Active = true;

            category.Text = text;

            return category;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
            this.Active = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new CategoryModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Category newCategory)
        {
            this.Id = newCategory.Id;
            this.Code = newCategory.Code;
            this.ExternalCode = newCategory.ExternalCode;
            this.Name = newCategory.Name;
            this.Active = newCategory.Active;
            this.ParentId = newCategory.ParentId;
            this.UpdatedDate = newCategory.UpdatedDate;

            if (newCategory.Text == null)
                this.Text = null;
            else
            {
                if (this.Text == null)
                    this.Text = new CategoryText();

                this.Text.MergeToUpdate(newCategory.Text);
            }
        }

        #endregion [ FIM - Metodos ]
    }

    public class CategoryTree
    {
        public CategoryTree()
        {
            this.Children = new List<CategoryTree>();
        }

        public CategoryTree(int id, string name, int? parentId) : this()
        {
            this.Id = id;
            this.Name = name;
            this.ParentId = parentId;
        }

        #region [ Propriedades ]

        public int Id                       { get; private set; }
        public string Name                  { get; private set; }
        public List<CategoryTree> Children  { get; private set; }
        public int? ParentId                { get; private set; }

        #endregion [ FIM - Propriedades ]
    }

    public class CategoryModelValidation : AbstractValidator<Category>
    {
        public CategoryModelValidation()
        {
            Include(new CategoryValidation());

            RuleFor(x => x.CreatedDate)
                .IsValidDateTime(true).WithMessage("A Data de Criação está inválida");

            RuleFor(x => x.UpdatedDate)
                .IsValidDateTime(true).WithMessage("A Data de Atualização está inválida");

            When(x => x.WasRemoved, () =>
            {
                RuleFor(x => x.RemovedDate)
                    .IsValidDateTime(true).WithMessage("A Data de Exclusão está inválida");

                RuleFor(x => x.RemovedId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("O Código da Exclusão é obrigatório")
                    .GreaterThan(0).WithMessage("O Código da Exclusão está inválido");
            });

            RuleFor(x => x)
                .NotEmpty()
                .IsCategoryCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.ParentId.HasValue, () =>
            {
                RuleFor(x => x.ParentId)
                    .Cascade(CascadeMode.Stop)
                    .NotEqual(x => x.Id).WithMessage("O Código da Categoria Pai não pode ser igual ao Código da Categoria")
                    .CategoryExists().WithMessage(x => $"O ParentId {x.ParentId} não faz referência a nenhuma categoria no banco de dados");
            });
        }
    }
}
