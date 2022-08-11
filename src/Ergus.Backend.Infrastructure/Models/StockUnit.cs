using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("unidade_estoque")]
    public class StockUnit : BaseModel, IStockUnit, IGeneric
    {
        public StockUnit() { }

        public StockUnit(int id, string code, string externalCode, string name, string complement, int? addressId, int? companyId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            Complement = complement;
            AddressId = addressId;
            CompanyId = companyId;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("ue_id")]
        public int Id { get; private set; }

        [Column("ue_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("ue_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("ue_nome")]
        public string Name { get; private set; } = String.Empty;

        [Column("ue_complemento")]
        public string? Complement { get; private set; } = String.Empty;

        [Column("end_id")]
        [ForeignKey(nameof(Address))]
        public int? AddressId { get; set; }

        [Column("emp_id")]
        [ForeignKey(nameof(Company))]
        public int? CompanyId { get; set; }


        [Column("ue_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("ue_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("ue_removido")]
        public bool WasRemoved { get; private set; }

        [Column("ue_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("ue_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual Address? Address { get; private set; }
        public virtual Company? Company { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static StockUnit Criar(string code, string externalCode, string name, string? complement, int? addressId, int? companyId)
        {
            var stockUnit = new StockUnit();

            stockUnit.Id = 0;
            stockUnit.Code = code;
            stockUnit.ExternalCode = externalCode;
            stockUnit.Name = name;
            stockUnit.Complement = complement;
            stockUnit.AddressId = addressId;
            stockUnit.CompanyId = companyId;
            stockUnit.RemovedId = 0;
            stockUnit.CreatedDate = DateTime.UtcNow;
            stockUnit.UpdatedDate = DateTime.UtcNow;
            stockUnit.WasRemoved = false;

            return stockUnit;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new StockUnitModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(StockUnit newStockUnit)
        {
            this.Id = newStockUnit.Id;
            this.Code = newStockUnit.Code;
            this.ExternalCode = newStockUnit.ExternalCode;
            this.Name = newStockUnit.Name;
            this.Complement = newStockUnit.Complement;
            this.AddressId = newStockUnit.AddressId;
            this.CompanyId = newStockUnit.CompanyId;
            this.UpdatedDate = newStockUnit.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class StockUnitModelValidation : AbstractValidator<StockUnit>
    {
        public StockUnitModelValidation()
        {
            Include(new StockUnitValidation());

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
                .IsStockUnitCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.AddressId.HasValue, () =>
            {
                RuleFor(x => x.AddressId)
                    .AddressExists().WithMessage(x => $"O AddressId {x.AddressId} não faz referência a nenhum endereço no banco de dados");
            });

            When(x => x.CompanyId.HasValue, () =>
            {
                RuleFor(x => x.CompanyId)
                    .CompanyExists().WithMessage(x => $"O CompanyId {x.CompanyId} não faz referência a nenhuma empresa no banco de dados");
            });
        }
    }
}
