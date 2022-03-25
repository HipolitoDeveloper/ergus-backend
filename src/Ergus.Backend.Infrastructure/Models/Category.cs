using FluentValidation;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Repositories;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("categoria")]
    public class Category : BaseModel, ICategory
    {
        public Category() { }

        public Category(int id, string code, string externalCode, string name, bool active, int? parentId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            Active = active;
            ParentId = parentId;
            UpdatedDate = DateTime.UtcNow;
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

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Category Criar(string code, string externalCode, string name, int? parentId)
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
        }

        #endregion [ FIM - Metodos ]
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
        }
    }
}
