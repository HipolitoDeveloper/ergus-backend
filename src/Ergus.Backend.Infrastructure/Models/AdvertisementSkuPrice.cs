using FluentValidation;
using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("anuncio_sku_preco")]
    public class AdvertisementSkuPrice : BaseModel, IAdvertisementSkuPrice, IGeneric
    {
        public AdvertisementSkuPrice() { }

        public AdvertisementSkuPrice(int id, string code, string externalCode, decimal value, decimal fictionalValue, DateTime? promotionStart, DateTime? promotionEnd,
            int? priceListId, int? advertisementSkuId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Value = value;
            FictionalValue = fictionalValue;
            PromotionStart = promotionStart;
            PromotionEnd = promotionEnd;
            PriceListId = priceListId;
            AdvertisementSkuId = advertisementSkuId;
        }

        #region [ Propriedades ]

        [Column("pr_id")]
        public int Id { get; private set; }

        [Column("pr_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("pr_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("pr_vl")]
        public decimal Value { get; private set; }

        [Column("pr_vl_ficticio")]
        public decimal FictionalValue { get; private set; }

        [Column("pr_dt_promocao_ini")]
        public DateTime? PromotionStart { get; private set; }

        [Column("pr_dt_promocao_fim")]
        public DateTime? PromotionEnd { get; private set; }

        [Column("lp_id")]
        [ForeignKey(nameof(PriceList))]
        public int? PriceListId { get; set; }

        [Column("asku_id")]
        [ForeignKey(nameof(AdvertisementSku))]
        public int? AdvertisementSkuId { get; set; }

        [Column("pr_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("pr_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("pr_removido")]
        public bool WasRemoved { get; private set; }

        [Column("pr_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("pr_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual PriceList? PriceList                 { get; private set; }
        public virtual AdvertisementSku? AdvertisementSku   { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static AdvertisementSkuPrice Criar(string code, string externalCode, decimal value, decimal fictionalValue, DateTime? promotionStart, DateTime? promotionEnd, 
            int? priceListId, int? advertisementSkuId)
        {
            var advertisementSkuPrice = new AdvertisementSkuPrice();

            advertisementSkuPrice.Id = 0;
            advertisementSkuPrice.Code = code;
            advertisementSkuPrice.ExternalCode = externalCode;
            advertisementSkuPrice.Value = value;
            advertisementSkuPrice.FictionalValue = fictionalValue;
            advertisementSkuPrice.PromotionStart = promotionStart;
            advertisementSkuPrice.PromotionEnd = promotionEnd;
            advertisementSkuPrice.PriceListId = priceListId;
            advertisementSkuPrice.AdvertisementSkuId = advertisementSkuId;
            advertisementSkuPrice.RemovedId = 0;
            advertisementSkuPrice.CreatedDate = DateTime.UtcNow;
            advertisementSkuPrice.UpdatedDate = DateTime.UtcNow;
            advertisementSkuPrice.WasRemoved = false;

            return advertisementSkuPrice;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdvertisementSkuPriceModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(AdvertisementSkuPrice newAdvertisementSkuPrice)
        {
            this.Id = newAdvertisementSkuPrice.Id;
            this.Code = newAdvertisementSkuPrice.Code;
            this.ExternalCode = newAdvertisementSkuPrice.ExternalCode;
            this.Value = newAdvertisementSkuPrice.Value;
            this.FictionalValue = newAdvertisementSkuPrice.FictionalValue;
            this.PromotionStart = newAdvertisementSkuPrice.PromotionStart;
            this.PromotionEnd = newAdvertisementSkuPrice.PromotionEnd;
            this.PriceListId = newAdvertisementSkuPrice.PriceListId;
            this.AdvertisementSkuId = newAdvertisementSkuPrice.AdvertisementSkuId;
            this.UpdatedDate = newAdvertisementSkuPrice.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class AdvertisementSkuPriceModelValidation : AbstractValidator<AdvertisementSkuPrice>
    {
        public AdvertisementSkuPriceModelValidation()
        {
            Include(new AdvertisementSkuPriceValidation());

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
                .IsAdvertisementSkuPriceCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.PriceListId.HasValue, () =>
            {
                RuleFor(x => x.PriceListId)
                    .PriceListExists().WithMessage(x => $"O PriceListId {x.PriceListId} não faz referência a nenhuma lista de preço no banco de dados");
            });

            When(x => x.AdvertisementSkuId.HasValue, () =>
            {
                RuleFor(x => x.AdvertisementSkuId)
                    .AdvertisementSkuExists().WithMessage(x => $"O AdvertisementSkuId {x.AdvertisementSkuId} não faz referência a nenhum anúncio sku no banco de dados");
            });
        }
    }
}
