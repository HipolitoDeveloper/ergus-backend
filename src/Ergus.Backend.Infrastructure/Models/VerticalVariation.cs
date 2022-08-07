using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("variacao_vertical")]
    public class VerticalVariation : BaseModel, IVerticalVariation, IGeneric
    {
        public VerticalVariation() { }

        public VerticalVariation(int id, string code, string externalCode, string name, string strInterface, int order, int? gridId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            Interface = strInterface;
            Order = order;
            GridId = gridId;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("vv_id")]
        public int Id { get; private set; }

        [Column("vv_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("vv_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("vv_nome")]
        public string Name { get; private set; } = String.Empty;

        [Column("vv_interface")]
        public string Interface { get; private set; } = String.Empty;

        [Column("vv_ordem")]
        public int? Order { get; private set; }

        [Column("gd_id")]
        [ForeignKey(nameof(Grid))]
        public int? GridId { get; set; }


        [Column("vv_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("vv_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("vv_removido")]
        public bool WasRemoved { get; private set; }

        [Column("vv_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("vv_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual Grid? Grid { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static VerticalVariation Criar(string code, string externalCode, string name, string strInterface, int order, int? gridId)
        {
            var verticalVariation = new VerticalVariation();

            verticalVariation.Id = 0;
            verticalVariation.Code = code;
            verticalVariation.ExternalCode = externalCode;
            verticalVariation.Name = name;
            verticalVariation.Interface = strInterface;
            verticalVariation.Order = order;
            verticalVariation.GridId = gridId;
            verticalVariation.RemovedId = 0;
            verticalVariation.CreatedDate = DateTime.UtcNow;
            verticalVariation.UpdatedDate = DateTime.UtcNow;
            verticalVariation.WasRemoved = false;

            return verticalVariation;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new VerticalVariationModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(VerticalVariation newVerticalVariation)
        {
            this.Id = newVerticalVariation.Id;
            this.Code = newVerticalVariation.Code;
            this.ExternalCode = newVerticalVariation.ExternalCode;
            this.Name = newVerticalVariation.Name;
            this.Interface = newVerticalVariation.Interface;
            this.Order = newVerticalVariation.Order;
            this.GridId = newVerticalVariation.GridId;
            this.UpdatedDate = newVerticalVariation.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class VerticalVariationModelValidation : AbstractValidator<VerticalVariation>
    {
        public VerticalVariationModelValidation()
        {
            Include(new VerticalVariationValidation());

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
                .IsVerticalVariationCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.GridId.HasValue, () =>
            {
                RuleFor(x => x.GridId)
                    .GridExists().WithMessage(x => $"O GridId {x.GridId} não faz referência a nenhuma grade no banco de dados");
            });
        }
    }
}
