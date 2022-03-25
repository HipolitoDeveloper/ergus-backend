using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomAdvertisementCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public CustomAdvertisementCodeBeUniqueValidator(IAdvertisementRepository advertisementRepository) : base()
        {
            this._advertisementRepository = advertisementRepository;
        }

        public override string Name => "CustomAdvertisementCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var advertisement = value as Advertisement;
            var advertisementWithCode = this._advertisementRepository.GetByCode(advertisement!.Code).Result;

            if (advertisementWithCode == null) return true;

            return advertisement.Id == advertisementWithCode.Id;
        }
    }

    public static class StaticAdvertisementCodeBeUniqueValidator
    {
        private static IAdvertisementRepository? _advertisementRepository;

        public static IRuleBuilderOptions<T, TProperty> IsAdvertisementCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAdvertisementCodeBeUniqueValidator<T, TProperty>(_advertisementRepository!));
        }

        public static void Configure(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ AdvertisementExists]

    public class CustomAdvertisementExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public CustomAdvertisementExistsValidator(IAdvertisementRepository advertisementRepository) : base()
        {
            this._advertisementRepository = advertisementRepository;
        }

        public override string Name => "AdvertisementExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int advertisementId))
                return false;

            var advertisement = this._advertisementRepository.Get(advertisementId, false).Result;

            return (advertisement != null);
        }
    }

    public static class StaticAdvertisementExistsValidator
    {
        private static IAdvertisementRepository? _advertisementRepository;

        public static IRuleBuilderOptions<T, TProperty> AdvertisementExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAdvertisementExistsValidator<T, TProperty>(_advertisementRepository!));
        }

        public static void Configure(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }
    }

    #endregion [ FIM - AdvertisementExists]
}
