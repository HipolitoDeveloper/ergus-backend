using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomAdvertisementSkuCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAdvertisementSkuRepository _advertisementSkuRepository;

        public CustomAdvertisementSkuCodeBeUniqueValidator(IAdvertisementSkuRepository advertisementSkuRepository) : base()
        {
            this._advertisementSkuRepository = advertisementSkuRepository;
        }

        public override string Name => "CustomAdvertisementSkuCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var advertisementSku = value as AdvertisementSku;
            var advertisementSkuWithCode = this._advertisementSkuRepository.GetByCode(advertisementSku!.Code).Result;

            if (advertisementSkuWithCode == null) return true;

            return advertisementSku.Id == advertisementSkuWithCode.Id;
        }
    }

    public static class StaticAdvertisementSkuCodeBeUniqueValidator
    {
        private static IAdvertisementSkuRepository? _advertisementSkuRepository;

        public static IRuleBuilderOptions<T, TProperty> IsAdvertisementSkuCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAdvertisementSkuCodeBeUniqueValidator<T, TProperty>(_advertisementSkuRepository!));
        }

        public static void Configure(IAdvertisementSkuRepository advertisementSkuRepository)
        {
            _advertisementSkuRepository = advertisementSkuRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ AdvertisementSkuExists]

    public class CustomAdvertisementSkuExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAdvertisementSkuRepository _advertisementSkuRepository;

        public CustomAdvertisementSkuExistsValidator(IAdvertisementSkuRepository advertisementSkuRepository) : base()
        {
            this._advertisementSkuRepository = advertisementSkuRepository;
        }

        public override string Name => "AdvertisementSkuExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int advertisementSkuId))
                return false;

            var advertisementSku = this._advertisementSkuRepository.Get(advertisementSkuId, false).Result;

            return (advertisementSku != null);
        }
    }

    public static class StaticAdvertisementSkuExistsValidator
    {
        private static IAdvertisementSkuRepository? _advertisementSkuRepository;

        public static IRuleBuilderOptions<T, TProperty> AdvertisementSkuExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAdvertisementSkuExistsValidator<T, TProperty>(_advertisementSkuRepository!));
        }

        public static void Configure(IAdvertisementSkuRepository advertisementSkuRepository)
        {
            _advertisementSkuRepository = advertisementSkuRepository;
        }
    }

    #endregion [ FIM - AdvertisementSkuExists]
}
