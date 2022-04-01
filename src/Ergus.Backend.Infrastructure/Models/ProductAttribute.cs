using FluentValidation;
using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("produto_atributo")]
    public class ProductAttribute : BaseModel, IProductAttribute, IGeneric
    {
        public ProductAttribute() { }

        public ProductAttribute(int id, string code, string externalCode, int? metadataId, int? productId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            MetadataId = metadataId;
            ProductId = productId;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("pro_att_id")]
        public int Id { get; private set; }

        [Column("pro_att_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("pro_att_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("md_id")]
        [ForeignKey(nameof(Metadata))]
        public int? MetadataId { get; private set; }

        [Column("pro_id")]
        [ForeignKey(nameof(Product))]
        public int? ProductId { get; private set; }

        [Column("pro_att_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("pro_att_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("pro_att_removido")]
        public bool WasRemoved { get; private set; }

        [Column("pro_att_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("pro_att_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        public Metadata? Metadata { get; private set; }

        public Product? Product { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static ProductAttribute Criar(string code, string externalCode, int? metadataId, int? productId)
        {
            var productAttr = new ProductAttribute();

            productAttr.Id = 0;
            productAttr.Code = code;
            productAttr.ExternalCode = externalCode;
            productAttr.MetadataId = metadataId;
            productAttr.ProductId = productId;
            productAttr.RemovedId = 0;
            productAttr.CreatedDate = DateTime.UtcNow;
            productAttr.UpdatedDate = DateTime.UtcNow;
            productAttr.WasRemoved = false;

            return productAttr;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new ProductAttributeModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(ProductAttribute newProductAttribute)
        {
            this.Id = newProductAttribute.Id;
            this.Code = newProductAttribute.Code;
            this.ExternalCode = newProductAttribute.ExternalCode;
            this.MetadataId = newProductAttribute.MetadataId;
            this.ProductId = newProductAttribute.ProductId;
            this.UpdatedDate = newProductAttribute.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class ProductAttributeModelValidation : AbstractValidator<ProductAttribute>
    {
        public ProductAttributeModelValidation()
        {
            Include(new ProductAttributeValidation());

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
                .IsProductAttributeCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            RuleFor(x => x.MetadataId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MetadataExists().WithMessage(x => $"O MetadataId {x.MetadataId} não faz referência a nenhum metadado no banco de dados");

            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ProductExists().WithMessage(x => $"O ProductId {x.ProductId} não faz referência a nenhum produto no banco de dados");
        }
    }
}
