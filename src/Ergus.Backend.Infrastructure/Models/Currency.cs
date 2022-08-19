using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("moeda")]
    public class Currency : BaseModel, ICurrency, IGeneric
    {
        public Currency() { }

        public Currency(int id, string code, string externalCode, string name, string symbol)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            Symbol = symbol;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("md_id")]
        public int Id { get; private set; }

        [Column("md_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("md_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("md_nome")]
        public string Name { get; private set; } = String.Empty;

        [Column("md_sigla")]
        public string Symbol { get; private set; } = String.Empty;


        [Column("md_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("md_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("md_removido")]
        public bool WasRemoved { get; private set; }

        [Column("md_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("md_dt_rem")]
        public DateTime? RemovedDate { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static Currency Criar(string code, string externalCode, string name, string symbol)
        {
            var currency = new Currency();

            currency.Id = 0;
            currency.Code = code;
            currency.ExternalCode = externalCode;
            currency.Name = name;
            currency.Symbol = symbol;
            currency.RemovedId = 0;
            currency.CreatedDate = DateTime.UtcNow;
            currency.UpdatedDate = DateTime.UtcNow;
            currency.WasRemoved = false;

            return currency;
        }

        public void DefinirComoRemovido(int removedId)
        {
            this.RemovedId = removedId;
            this.RemovedDate = this.UpdatedDate = DateTime.UtcNow;
            this.WasRemoved = true;
        }

        public override bool EhValido()
        {
            ValidationResult = new CurrencyModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(Currency newCurrency)
        {
            this.Id = newCurrency.Id;
            this.Code = newCurrency.Code;
            this.ExternalCode = newCurrency.ExternalCode;
            this.Name = newCurrency.Name;
            this.Symbol = newCurrency.Symbol;
            this.UpdatedDate = newCurrency.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class CurrencyModelValidation : AbstractValidator<Currency>
    {
        public CurrencyModelValidation()
        {
            Include(new CurrencyValidation());

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
                .IsCurrencyCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");
        }
    }
}
