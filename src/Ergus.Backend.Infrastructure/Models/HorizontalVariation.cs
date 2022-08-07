using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("variacao_horizontal")]
    public class HorizontalVariation : BaseModel, IHorizontalVariation, IGeneric
    {
        public HorizontalVariation() { }

        public HorizontalVariation(int id, string code, string externalCode, string name, string strInterface, string color, int order, int? gridId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            Interface = strInterface;
            Color = color;
            Order = order;
            GridId = gridId;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("vh_id")]
        public int Id { get; private set; }

        [Column("vh_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("vh_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("vh_nome")]
        public string Name { get; private set; } = String.Empty;

        [Column("vh_interface")]
        public string Interface { get; private set; } = String.Empty;

        [Column("vh_cor")]
        public string Color { get; private set; } = String.Empty;

        [Column("vh_ordem")]
        public int? Order { get; private set; }

        [Column("gd_id")]
        [ForeignKey(nameof(Grid))]
        public int? GridId { get; set; }


        [Column("vh_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("vh_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("vh_removido")]
        public bool WasRemoved { get; private set; }

        [Column("vh_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("vh_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual Grid? Grid { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static HorizontalVariation Criar(string code, string externalCode, string name, string strInterface, string color, int order, int? gridId)
        {
            var horizontalVariation = new HorizontalVariation();

            horizontalVariation.Id = 0;
            horizontalVariation.Code = code;
            horizontalVariation.ExternalCode = externalCode;
            horizontalVariation.Name = name;
            horizontalVariation.Interface = strInterface;
            horizontalVariation.Color = color;
            horizontalVariation.Order = order;
            horizontalVariation.GridId = gridId;
            horizontalVariation.RemovedId = 0;
            horizontalVariation.CreatedDate = DateTime.UtcNow;
            horizontalVariation.UpdatedDate = DateTime.UtcNow;
            horizontalVariation.WasRemoved = false;

            return horizontalVariation;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new HorizontalVariationModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(HorizontalVariation newHorizontalVariation)
        {
            this.Id = newHorizontalVariation.Id;
            this.Code = newHorizontalVariation.Code;
            this.ExternalCode = newHorizontalVariation.ExternalCode;
            this.Name = newHorizontalVariation.Name;
            this.Interface = newHorizontalVariation.Interface;
            this.Color = newHorizontalVariation.Color;
            this.Order = newHorizontalVariation.Order;
            this.GridId = newHorizontalVariation.GridId;
            this.UpdatedDate = newHorizontalVariation.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class HorizontalVariationModelValidation : AbstractValidator<HorizontalVariation>
    {
        public HorizontalVariationModelValidation()
        {
            Include(new HorizontalVariationValidation());

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
                .IsHorizontalVariationCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.GridId.HasValue, () =>
            {
                RuleFor(x => x.GridId)
                    .GridExists().WithMessage(x => $"O GridId {x.GridId} não faz referência a nenhuma grade no banco de dados");
            });
        }
    }
}
