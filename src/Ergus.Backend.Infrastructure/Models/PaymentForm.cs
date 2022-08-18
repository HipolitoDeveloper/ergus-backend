using Ergus.Backend.Infrastructure.Models.Interfaces;
using Ergus.Backend.Infrastructure.Validations;
using Ergus.Backend.Infrastructure.Validations.Custom;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergus.Backend.Infrastructure.Models
{
    [Table("forma_pagamento")]
    public class PaymentForm : BaseModel, IPaymentForm, IGeneric
    {
        public PaymentForm() { }

        public PaymentForm(int id, string code, string externalCode, string name, bool active, int? providerId)
        {
            Id = id;
            Code = code;
            ExternalCode = externalCode;
            Name = name;
            Active = active;
            ProviderId = providerId;
            UpdatedDate = DateTime.UtcNow;
        }

        #region [ Propriedades ]

        [Column("fp_id")]
        public int Id { get; private set; }

        [Column("fp_codigo")]
        public string Code { get; private set; } = string.Empty;

        [Column("fp_codigo_ext")]
        public string ExternalCode { get; private set; } = string.Empty;

        [Column("fp_nome")]
        public string Name { get; private set; } = String.Empty;

        [Column("fp_ativo")]
        public bool Active { get; private set; }

        [Column("for_id")]
        [ForeignKey(nameof(Provider))]
        public int? ProviderId { get; private set; }


        [Column("fp_dt_inc")]
        public DateTime CreatedDate { get; private set; }

        [Column("fp_dt_alt")]
        public DateTime UpdatedDate { get; private set; }

        [Column("fp_removido")]
        public bool WasRemoved { get; private set; }

        [Column("fp_id_rem")]
        public int? RemovedId { get; private set; }

        [Column("fp_dt_rem")]
        public DateTime? RemovedDate { get; private set; }


        public virtual Provider? Provider { get; private set; }

        #endregion [ FIM - Propriedades ]

        #region [ Metodos ]

        public static PaymentForm Criar(string code, string externalCode, string name, int? providerId)
        {
            var paymentForm = new PaymentForm();

            paymentForm.Id = 0;
            paymentForm.Code = code;
            paymentForm.ExternalCode = externalCode;
            paymentForm.Name = name;
            paymentForm.ProviderId = providerId;
            paymentForm.RemovedId = 0;
            paymentForm.Active = true;
            paymentForm.CreatedDate = DateTime.UtcNow;
            paymentForm.UpdatedDate = DateTime.UtcNow;
            paymentForm.WasRemoved = false;

            return paymentForm;
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
            ValidationResult = new PaymentFormModelValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void MergeToUpdate(PaymentForm newPaymentForm)
        {
            this.Id = newPaymentForm.Id;
            this.Code = newPaymentForm.Code;
            this.ExternalCode = newPaymentForm.ExternalCode;
            this.Name = newPaymentForm.Name;
            this.Active = newPaymentForm.Active;
            this.ProviderId = newPaymentForm.ProviderId;
            this.UpdatedDate = newPaymentForm.UpdatedDate;
        }

        #endregion [ FIM - Metodos ]
    }

    public class PaymentFormModelValidation : AbstractValidator<PaymentForm>
    {
        public PaymentFormModelValidation()
        {
            Include(new PaymentFormValidation());

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
                .IsPaymentFormCodeUnique().WithMessage(x => $"O Código {x.Code} já existe no banco de dados");

            When(x => x.ProviderId.HasValue, () =>
            {
                RuleFor(x => x.ProviderId)
                    .ProviderExists().WithMessage(x => $"O ProviderId {x.ProviderId} não faz referência a nenhum fornecedor no banco de dados");
            });
        }
    }
}
