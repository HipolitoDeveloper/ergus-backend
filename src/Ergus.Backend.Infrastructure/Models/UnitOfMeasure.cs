using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("unidade_medida")]
    public class UnitOfMeasure : BaseModel, IUnitOfMeasure, IGeneric
    {
        public UnitOfMeasure() { }

        public UnitOfMeasure(int id, string code, string externalCode, string description, string? acronym)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Description = description;
            Acronym = acronym;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("um_id")]
        public int Id { get; private set; }

        [Column("um_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("um_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("um_descricao")]
        public string Description { get; private set; } = String.Empty;

        [Column("um_sigla")]
        public string? Acronym { get; private set; } = String.Empty;


        [Column("um_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("um_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("um_removido")]
        public bool WasRemoved { get; private set; }

        [Column("um_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("um_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static UnitOfMeasure Criar(string code, string externalCode, string description, string? acronym)
        {
            var unitOfMeasure = new UnitOfMeasure();

            unitOfMeasure.Id = 0;
            unitOfMeasure.Code = code;
            unitOfMeasure.ExternalCode = externalCode;
            unitOfMeasure.Description = description;
            unitOfMeasure.Acronym = acronym;
            unitOfMeasure.RemovedId = 0;
            unitOfMeasure.CreatedDate = DateTime.UtcNow;
            unitOfMeasure.UpdatedDate = DateTime.UtcNow;
            unitOfMeasure.WasRemoved = false;

            return unitOfMeasure;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new UnitOfMeasureModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(UnitOfMeasure newUnitOfMeasure)
        {
            this.Id = newUnitOfMeasure.Id;
            this.Code = newUnitOfMeasure.Code;
            this.ExternalCode = newUnitOfMeasure.ExternalCode;
            this.Description = newUnitOfMeasure.Description;
            this.Acronym = newUnitOfMeasure.Acronym;
            this.UpdatedDate = newUnitOfMeasure.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class UnitOfMeasureModelValidation : AbstractValidator<UnitOfMeasure>
    {
        public UnitOfMeasureModelValidation()
        {
            Include(new UnitOfMeasureValidation());

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
                .IsUnitOfMeasureCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");
        }
    }
}
