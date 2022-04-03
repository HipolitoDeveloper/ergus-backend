using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomAddressCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAddressRepository _addressRepository;

        public CustomAddressCodeBeUniqueValidator(IAddressRepository addressRepository) : base()
        {
            this._addressRepository = addressRepository;
        }

        public override string Name => "CustomAddressCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var address = value as Address;
            var addressWithCode = this._addressRepository.GetByCode(address!.Code).Result;

            if (addressWithCode == null) return true;

            return address.Id == addressWithCode.Id;
        }
    }

    public static class StaticAddressCodeBeUniqueValidator
    {
        private static IAddressRepository? _addressRepository;

        public static IRuleBuilderOptions<T, TProperty> IsAddressCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAddressCodeBeUniqueValidator<T, TProperty>(_addressRepository!));
        }

        public static void Configure(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ AddressExists]

    public class CustomAddressExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IAddressRepository _addressRepository;

        public CustomAddressExistsValidator(IAddressRepository addressRepository) : base()
        {
            this._addressRepository = addressRepository;
        }

        public override string Name => "AddressExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int addressId))
                return false;

            var address = this._addressRepository.Get(addressId, false).Result;

            return (address != null);
        }
    }

    public static class StaticAddressExistsValidator
    {
        private static IAddressRepository? _addressRepository;

        public static IRuleBuilderOptions<T, TProperty> AddressExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomAddressExistsValidator<T, TProperty>(_addressRepository!));
        }

        public static void Configure(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
    }

    #endregion [ FIM - AddressExists]
}
