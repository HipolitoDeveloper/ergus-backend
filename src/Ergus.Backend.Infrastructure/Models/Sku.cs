using FluentValidation;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("sku")]
    public class Sku : BaseModel, ISku
    {
        public Sku() { }

        public Sku(int id, string code, string externalCode, string skuCode, string name, string reference, string bar, decimal height, decimal width, decimal depth, decimal weight,
            decimal cost, int? productId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            SkuCode = skuCode;
            Name = name;
            Reference = reference;
            Bar = bar;
            Height = height;
            Width = width;
            Depth = depth;
            Weight = weight;
            Cost = cost;
            ProductId = productId;
        }

        #region [ Propriedades ]

        [Column("sku_id")]
        public int Id { get; private set; }

        [Column("sku_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("sku_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("sku_codigo_sku")]
        public string SkuCode { get; private set; } = string.Empty;

        [Column("sku_nome")]
        public string Name { get; private set; }

        [Column("sku_referencia")]
        public string Reference { get; private set; } = string.Empty;

        [Column("sku_barra")]
        public string Bar { get; private set; } = string.Empty;

        [Column("sku_altura")]
        public decimal? Height { get; set; }

        [Column("sku_largura")]
        public decimal? Width { get; set; }

        [Column("sku_profundidade")]
        public decimal? Depth { get; set; }

        [Column("sku_peso")]
        public decimal? Weight { get; set; }

        [Column("sku_custo")]
        public decimal? Cost { get; set; }

        [Column("pro_id")]
        [ForeignKey(nameof(Product))]
        public int? ProductId { get; private set; }

        [Column("sku_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("sku_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("sku_removido")]
        public bool WasRemoved { get; private set; }

        [Column("sku_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("sku_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual Product? Product { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Sku Criar(string code, string externalCode, string skuCode, string name, string reference, string bar, decimal height, decimal width, decimal depth, decimal weight,
            decimal cost, int? productId)
        {
            var sku = new Sku();

            sku.Id = 0;
            sku.Code = code;
            sku.ExternalCode = externalCode;
            sku.SkuCode = skuCode;
            sku.Name = name;
            sku.Reference = reference;
            sku.Bar = bar;
            sku.Height = height;
            sku.Width = width;
            sku.Depth = depth;
            sku.Weight = weight;
            sku.Cost = cost;
            sku.ProductId = productId;
            sku.RemovedId = 0;
            sku.CreatedDate = DateTime.UtcNow;
            sku.UpdatedDate = DateTime.UtcNow;
            sku.WasRemoved = false;

            return sku;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new SkuModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Sku newSku)
        {
            this.Id = newSku.Id;
            this.Code = newSku.Code;
            this.ExternalCode = newSku.ExternalCode;
            this.SkuCode = newSku.SkuCode;
            this.Name = newSku.Name;
            this.Reference = newSku.Reference;
            this.Bar = newSku.Bar;
            this.Height = newSku.Height;
            this.Width = newSku.Width;
            this.Cost = newSku.Cost;
            this.ProductId = newSku.ProductId;
            this.UpdatedDate = newSku.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class SkuModelValidation : AbstractValidator<Sku>
    {
        public SkuModelValidation()
        {
            Include(new SkuValidation());

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
                .IsSkuCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.ProductId.HasValue, () =>
            {
                RuleFor(x => x.ProductId)
                    .ProductExists().WithMessage(x => $"O ProductId {x.ProductId} não faz referência a nenhum produto no banco de dados");
            });
        }
    }
}
