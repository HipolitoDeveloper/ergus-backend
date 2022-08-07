using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("secao")]
    public class Section : BaseModel, ISection, IGeneric
    {
        public Section() { }

        public Section(int id, string code, string externalCode, string name)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("sec_id")]
        public int Id { get; private set; }

        [Column("sec_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("sec_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("sec_nome")]
        public string Name { get; private set; } = String.Empty;


        [Column("sec_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("sec_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("sec_removido")]
        public bool WasRemoved { get; private set; }

        [Column("sec_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("sec_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Section Criar(string code, string externalCode, string name)
        {
            var section = new Section();

            section.Id = 0;
            section.Code = code;
            section.ExternalCode = externalCode;
            section.Name = name;
            section.RemovedId = 0;
            section.CreatedDate = DateTime.UtcNow;
            section.UpdatedDate = DateTime.UtcNow;
            section.WasRemoved = false;

            return section;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new SectionModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Section newSection)
        {
            this.Id = newSection.Id;
            this.Code = newSection.Code;
            this.ExternalCode = newSection.ExternalCode;
            this.Name = newSection.Name;
            this.UpdatedDate = newSection.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class SectionModelValidation : AbstractValidator<Section>
    {
        public SectionModelValidation()
        {
            Include(new SectionValidation());

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
                .IsSectionCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");
        }
    }
}
