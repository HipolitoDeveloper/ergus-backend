using FluentValidation;
using Ergus.Backend.Core.Helpers;
using Ergus.Backend.Infrastructure.Helpers;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("produto")]
    public class Product : BaseModel, IProduct
    {
        public Product() { }

        public Product(int id, string code, string externalCode, string skuCode, string name, string ncm, TipoAnuncio advertisementType, bool active, int? producerId, int? categoryId,
            int? providerId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            SkuCode = skuCode;
            Name = name;
            NCM = ncm;
            Active = active;
            AdvertisementType = advertisementType.DescriptionAttr();
            ProducerId = producerId;
            CategoryId = categoryId;
            ProviderId = providerId;
        }

        #region [ Propriedades ]

        [Column("pro_id")]
        public int Id { get; private set; }

        [Column("pro_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("pro_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("pro_codigo_sku")]
        public string SkuCode { get; private set; } = string.Empty;

        [Column("pro_ativo")]
        public bool Active { get; private set; }

        [Column("pro_nome")]
        public string Name { get; private set; }

        [Column("pro_ncm")]
        public string NCM { get; private set; } = string.Empty;

        [Column("pro_tipo_anuncio")]
        public string? AdvertisementType { get; private set; } = TipoAnuncio.None.DescriptionAttr();

        [Column("fab_id")]
        [ForeignKey(nameof(Producer))]
        public int? ProducerId { get; private set; }

        [Column("cat_id")]
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; private set; }

        [Column("for_id")]
        [ForeignKey(nameof(Provider))]
        public int? ProviderId { get; private set; }

        [Column("pro_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("pro_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("pro_removido")]
        public bool WasRemoved { get; private set; }

        [Column("pro_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("pro_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual Category? Category { get; private set; }
        public virtual Producer? Producer { get; private set; }
        public virtual Provider? Provider { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Product Criar(string code, string externalCode, string skuCode, string name, string ncm, TipoAnuncio advertisementType, int? categoryId, int? producerId, int? providerId)
        {
            var product = new Product();

            product.Id = 0;
            product.Code = code;
            product.ExternalCode = externalCode;
            product.SkuCode = skuCode;
            product.Name = name;
            product.NCM = ncm;
            product.AdvertisementType = advertisementType.DescriptionAttr();
            product.CategoryId = categoryId;
            product.ProducerId = producerId;
            product.ProviderId = providerId;
            product.RemovedId = 0;
            product.CreatedDate = DateTime.UtcNow;
            product.UpdatedDate = DateTime.UtcNow;
            product.WasRemoved = false;

            product.Active = true;

            return product;
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
            ValidationResult = new ProductModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Product newProduct)
        {
            this.Id = newProduct.Id;
            this.Code = newProduct.Code;
            this.ExternalCode = newProduct.ExternalCode;
            this.SkuCode = newProduct.SkuCode;
            this.Name = newProduct.Name;
            this.NCM = newProduct.NCM;
            this.AdvertisementType = newProduct.AdvertisementType;
            this.CategoryId = newProduct.CategoryId;
            this.ProducerId = newProduct.ProducerId;
            this.ProviderId = newProduct.ProviderId;
            this.UpdatedDate = newProduct.UpdatedDate;
            this.Active = newProduct.Active;
        }

        #endregion [ FIM - Metodos ]
    }

    public class ProductModelValidation : AbstractValidator<Product>
    {
        public ProductModelValidation()
        {
            Include(new ProductValidation());

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
                .IsProductCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.CategoryId.HasValue, () =>
            {
                RuleFor(x => x.CategoryId)
                    .CategoryExists().WithMessage(x => $"O CategoryId {x.CategoryId} não faz referência a nenhuma categoria no banco de dados");
            });

            When(x => x.ProducerId.HasValue, () =>
            {
                RuleFor(x => x.ProducerId)
                    .ProducerExists().WithMessage(x => $"O ProducerId {x.ProducerId} não faz referência a nenhum fabricante no banco de dados");
            });

            When(x => x.ProviderId.HasValue, () =>
            {
                RuleFor(x => x.ProviderId)
                    .ProviderExists().WithMessage(x => $"O ProviderId {x.ProviderId} não faz referência a nenhum fornecedor no banco de dados");
            });
        }
    }
}
