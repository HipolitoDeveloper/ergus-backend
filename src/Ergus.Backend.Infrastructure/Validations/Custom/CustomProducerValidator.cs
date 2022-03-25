using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomProducerCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IProducerRepository _producerRepository;

        public CustomProducerCodeBeUniqueValidator(IProducerRepository producerRepository) : base()
        {
            this._producerRepository = producerRepository;
        }

        public override string Name => "CustomProducerCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var producer = value as Producer;
            var producerWithCode = this._producerRepository.GetByCode(producer!.Code).Result;

            if (producerWithCode == null) return true;

            return producer.Id == producerWithCode.Id;
        }
    }

    public static class StaticProducerCodeBeUniqueValidator
    {
        private static IProducerRepository? _producerRepository;

        public static IRuleBuilderOptions<T, TProperty> IsProducerCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomProducerCodeBeUniqueValidator<T, TProperty>(_producerRepository!));
        }

        public static void Configure(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ ProducerExists]

    public class CustomProducerExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IProducerRepository _producerRepository;

        public CustomProducerExistsValidator(IProducerRepository producerRepository) : base()
        {
            this._producerRepository = producerRepository;
        }

        public override string Name => "ProducerExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int producerId))
                return false;

            var producer = this._producerRepository.Get(producerId, false).Result;

            return (producer != null);
        }
    }

    public static class StaticProducerExistsValidator
    {
        private static IProducerRepository? _producerRepository;

        public static IRuleBuilderOptions<T, TProperty> ProducerExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomProducerExistsValidator<T, TProperty>(_producerRepository!));
        }

        public static void Configure(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }
    }

    #endregion [ FIM - ProducerExists]
}
