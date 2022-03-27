using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomAdvertisementSkuPriceCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAdvertisementSkuPriceRepository _advertisementSkuPriceRepository;

        public CustomAdvertisementSkuPriceCodeBeUniqueValidator(IAdvertisementSkuPriceRepository advertisementSkuPriceRepository) : base()
        {
            this._advertisementSkuPriceRepository = advertisementSkuPriceRepository;
        }

        public override string Name => "CustomAdvertisementSkuPriceCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var advertisementSkuPrice = value as AdvertisementSkuPrice;
            var advertisementSkuPriceWithCode = this._advertisementSkuPriceRepository.GetByCode(advertisementSkuPrice!.Code).Result;

            if (advertisementSkuPriceWithCode == null) return true;

            return advertisementSkuPrice.Id == advertisementSkuPriceWithCode.Id;
        }
    }

    public static class StaticAdvertisementSkuPriceCodeBeUniqueValidator
    {
        private static IAdvertisementSkuPriceRepository? _advertisementSkuPriceRepository;

        public static IRuleBuilderOptions<T, TProperty> IsAdvertisementSkuPriceCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAdvertisementSkuPriceCodeBeUniqueValidator<T, TProperty>(_advertisementSkuPriceRepository!));
        }

        public static void Configure(IAdvertisementSkuPriceRepository advertisementSkuPriceRepository)
        {
            _advertisementSkuPriceRepository = advertisementSkuPriceRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ AdvertisementSkuPriceExists]

    public class CustomAdvertisementSkuPriceExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAdvertisementSkuPriceRepository _advertisementSkuPriceRepository;

        public CustomAdvertisementSkuPriceExistsValidator(IAdvertisementSkuPriceRepository advertisementSkuPriceRepository) : base()
        {
            this._advertisementSkuPriceRepository = advertisementSkuPriceRepository;
        }

        public override string Name => "AdvertisementSkuPriceExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int advertisementSkuPriceId))
                return false;

            var advertisementSkuPrice = this._advertisementSkuPriceRepository.Get(advertisementSkuPriceId, false).Result;

            return (advertisementSkuPrice != null);
        }
    }

    public static class StaticAdvertisementSkuPriceExistsValidator
    {
        private static IAdvertisementSkuPriceRepository? _advertisementSkuPriceRepository;

        public static IRuleBuilderOptions<T, TProperty> AdvertisementSkuPriceExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAdvertisementSkuPriceExistsValidator<T, TProperty>(_advertisementSkuPriceRepository!));
        }

        public static void Configure(IAdvertisementSkuPriceRepository advertisementSkuPriceRepository)
        {
            _advertisementSkuPriceRepository = advertisementSkuPriceRepository;
        }
    }

    #endregion [ FIM - AdvertisementSkuPriceExists]
}
