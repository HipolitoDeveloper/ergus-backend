using FluentValidation;
using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("anuncio")]
    public class Advertisement : BaseModel, IAdvertisement
    {
        public Advertisement() { }

        public Advertisement(int id, string code, string externalCode, string skuCode, string integrationCode, string name, TipoAnuncio advertisementType, TipoStatusAnuncio status, 
            int? integrationId, int? productId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            SkuCode = skuCode;
            IntegrationCode = integrationCode;
            Name = name;
            AdvertisementType = advertisementType.DescriptionAttr();
            Status = status.DescriptionAttr();
            IntegrationId = integrationId;
            ProductId = productId;
        }

        #region [ Propriedades ]

        [Column("anun_id")]
        public int Id { get; private set; }

        [Column("anun_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("anun_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("anun_codigo_sku")]
        public string SkuCode { get; private set; } = string.Empty;

        [Column("anun_id_integracao")]
        public string IntegrationCode { get; private set; } = string.Empty;

        [Column("anun_nome")]
        public string Name { get; private set; }

        [Column("anun_status")]
        public string? Status { get; private set; } = TipoStatusAnuncio.Inativo.DescriptionAttr();

        [Column("anun_tipo_anuncio")]
        public string? AdvertisementType { get; private set; } = TipoAnuncio.None.DescriptionAttr();

        [Column("pro_id")]
        [ForeignKey(nameof(Product))]
        public int? ProductId { get; private set; }

        [Column("int_id")]
        [ForeignKey(nameof(Integration))]
        public int? IntegrationId { get; private set; }

        [Column("anun_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("anun_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("anun_removido")]
        public bool WasRemoved { get; private set; }

        [Column("anun_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("anun_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual Integration? Integration { get; private set; }
        public virtual Product? Product         { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Advertisement Criar(string code, string externalCode, string skuCode, string integrationCode, string name, TipoAnuncio advertisementType, TipoStatusAnuncio status,
            int? integrationId, int? productId)
        {
            var advertisement = new Advertisement();

            advertisement.Id = 0;
            advertisement.Code = code;
            advertisement.ExternalCode = externalCode;
            advertisement.SkuCode = skuCode;
            advertisement.IntegrationCode = integrationCode;
            advertisement.Name = name;
            advertisement.AdvertisementType = advertisementType.DescriptionAttr();
            advertisement.Status = status.DescriptionAttr();
            advertisement.IntegrationId = integrationId;
            advertisement.ProductId = productId;
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
            ValidationResult = new AdvertisementModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Advertisement newAdvertisement)
        {
            this.Id = newAdvertisement.Id;
            this.Code = newAdvertisement.Code;
            this.ExternalCode = newAdvertisement.ExternalCode;
            this.SkuCode = newAdvertisement.SkuCode;
            this.IntegrationCode = newAdvertisement.IntegrationCode;
            this.Name = newAdvertisement.Name;
            this.AdvertisementType = newAdvertisement.AdvertisementType;
            this.Status = newAdvertisement.Status;
            this.IntegrationId = newAdvertisement.IntegrationId;
            this.ProductId = newAdvertisement.ProductId;
            this.UpdatedDate = newAdvertisement.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class AdvertisementModelValidation : AbstractValidator<Advertisement>
    {
        public AdvertisementModelValidation()
        {
            Include(new AdvertisementValidation());

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
                .IsAdvertisementCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.IntegrationId.HasValue, () =>
            {
                RuleFor(x => x.IntegrationId)
                    .IntegrationExists().WithMessage(x => $"O IntegrationId {x.IntegrationId} não faz referência a nenhuma integração no banco de dados");
            });

            When(x => x.ProductId.HasValue, () =>
            {
                RuleFor(x => x.ProductId)
                    .ProductExists().WithMessage(x => $"O ProductId {x.ProductId} não faz referência a nenhum produto no banco de dados");
            });
        }
    }
}
