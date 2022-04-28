using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("lista_preco")]
    public class PriceList : BaseModel, IPriceList, IGeneric
    {
        public PriceList() { }

        public PriceList(int id, string code, string externalCode, DateTime initDate, DateTime endDate, string name, decimal value, TipoListaPreco type, TipoAjuste adjustmentType,
            TipoOperacao operationType, int saleMaxAmount, int? parentId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            InitDate = initDate;
            EndDate = endDate;
            Name = name;
            Value = value;
            Type = type.DescriptionAttr();
            AdjustmentType = adjustmentType.DescriptionAttr();
            OperationType = operationType.DescriptionAttr();
            SaleMaxAmount = saleMaxAmount;
            ParentId = parentId;
        }

        #region [ Propriedades ]

        [Column("lp_id")]
        public int Id { get; private set; }

        [Column("lp_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("lp_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("lp_data_inicial", TypeName = SqlUtils.VARCHAR)]
        public DateTime InitDate { get; private set; } = DateTime.Now.AddYears(-10);

        [Column("lp_data_final", TypeName = SqlUtils.VARCHAR)]
        public DateTime EndDate { get; private set; } = DateTime.Now.AddYears(10);

        [Column("lp_nome")]
        public string Name { get; private set; } = string.Empty;

        [Column("lp_vl")]
        public decimal Value { get; private set; }

        [Column("lp_tipo")]
        public string? Type { get; private set; } = TipoListaPreco.Nenhum.DescriptionAttr();

        [Column("lp_tipo_ajuste")]
        public string? AdjustmentType { get; private set; } = TipoAjuste.Nenhum.DescriptionAttr();

        [Column("lp_tipo_operacao")]
        public string? OperationType { get; private set; } = TipoOperacao.Nenhum.DescriptionAttr();

        [Column("lp_venda_qtde_max")]
        public int SaleMaxAmount { get; private set; }

        [Column("lp_id_pai")]
        [ForeignKey(nameof(ParentPriceList))]
        public int? ParentId { get; private set; }

        [Column("lp_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("lp_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("lp_removido")]
        public bool WasRemoved { get; private set; }

        [Column("lp_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("lp_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        public virtual PriceList? ParentPriceList { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static PriceList Criar(string code, string externalCode, DateTime initDate, DateTime endDate, string name, decimal value, TipoListaPreco type, TipoAjuste adjustmentType,
            TipoOperacao operationType, int saleMaxAmount, int? parentId)
        {
            var advertisement = new PriceList();

            advertisement.Id = 0;
            advertisement.Code = code;
            advertisement.ExternalCode = externalCode;
            advertisement.InitDate = initDate;
            advertisement.EndDate = endDate;
            advertisement.Name = name;
            advertisement.Value = value;
            advertisement.Type = type.DescriptionAttr();
            advertisement.AdjustmentType = adjustmentType.DescriptionAttr();
            advertisement.OperationType = operationType.DescriptionAttr();
            advertisement.SaleMaxAmount = saleMaxAmount;
            advertisement.ParentId = parentId;
            advertisement.RemovedId = 0;
            advertisement.CreatedDate = DateTime.UtcNow;
            advertisement.UpdatedDate = DateTime.UtcNow;
            advertisement.WasRemoved = false;

            return advertisement;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new PriceListModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(PriceList newPriceList)
        {
            this.Id = newPriceList.Id;
            this.Code = newPriceList.Code;
            this.ExternalCode = newPriceList.ExternalCode;
            this.InitDate = newPriceList.InitDate;
            this.EndDate = newPriceList.EndDate;
            this.Name = newPriceList.Name;
            this.Value = newPriceList.Value;
            this.Type = newPriceList.Type;
            this.AdjustmentType = newPriceList.AdjustmentType;
            this.OperationType = newPriceList.OperationType;
            this.SaleMaxAmount = newPriceList.SaleMaxAmount;
            this.ParentId = newPriceList.ParentId;
            this.UpdatedDate = newPriceList.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class PriceListModelValidation : AbstractValidator<PriceList>
    {
        public PriceListModelValidation()
        {
            Include(new PriceListValidation());

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
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .IsPriceListCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.ParentId.HasValue, () =>
            {
                RuleFor(x => x.ParentId)
                    .Cascade(CascadeMode.Stop)
                    .NotEqual(x => x.Id).WithMessage("O Código da Lista de Preço Pai não pode ser igual ao Código da Lista de Preço")
                    .PriceListExists().WithMessage(x => $"O ParentId {x.ParentId} não faz referência a nenhuma lista de preço no banco de dados");
            });
        }
    }
}
