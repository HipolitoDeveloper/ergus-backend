using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomPaymentFormCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IPaymentFormRepository _paymentFormRepository;

        public CustomPaymentFormCodeBeUniqueValidator(IPaymentFormRepository paymentFormRepository) : base()
        {
            this._paymentFormRepository = paymentFormRepository;
        }

        public override string Name => "CustomPaymentFormCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var paymentForm = value as PaymentForm;
            var paymentFormWithCode = this._paymentFormRepository.GetByCode(paymentForm!.Code).Result;

            if (paymentFormWithCode == null) return true;

            return paymentForm.Id == paymentFormWithCode.Id;
        }
    }

    public static class StaticPaymentFormCodeBeUniqueValidator
    {
        private static IPaymentFormRepository? _paymentFormRepository;

        public static IRuleBuilderOptions<T, TProperty> IsPaymentFormCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomPaymentFormCodeBeUniqueValidator<T, TProperty>(_paymentFormRepository!));
        }

        public static void Configure(IPaymentFormRepository paymentFormRepository)
        {
            _paymentFormRepository = paymentFormRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ PaymentFormExists]

    public class CustomPaymentFormExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IPaymentFormRepository _paymentFormRepository;

        public CustomPaymentFormExistsValidator(IPaymentFormRepository paymentFormRepository) : base()
        {
            this._paymentFormRepository = paymentFormRepository;
        }

        public override string Name => "PaymentFormExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int paymentFormId))
                return false;

            var paymentForm = this._paymentFormRepository.Get(paymentFormId, false).Result;

            return (paymentForm != null);
        }
    }

    public static class StaticPaymentFormExistsValidator
    {
        private static IPaymentFormRepository? _paymentFormRepository;

        public static IRuleBuilderOptions<T, TProperty> PaymentFormExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomPaymentFormExistsValidator<T, TProperty>(_paymentFormRepository!));
        }

        public static void Configure(IPaymentFormRepository paymentFormRepository)
        {
            _paymentFormRepository = paymentFormRepository;
        }
    }

    #endregion [ FIM - PaymentFormExists]
}
