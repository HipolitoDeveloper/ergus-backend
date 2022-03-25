using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    public class CustomMetadataValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly IMetadataRepository _metadataRepository;

        public CustomMetadataValidator(IMetadataRepository metadataRepository) : base()
        {
            this._metadataRepository = metadataRepository;
        }

        public override string Name => "MetadataExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int metadataId))
                return false;

            var metadata = this._metadataRepository.Get(metadataId, false).Result;

            return (metadata != null);
        }
    }

    public static class StaticMetadataExistsValidator
    {
        private static IMetadataRepository? _metadataRepository;

        public static IRuleBuilderOptions<T, TProperty> MetadataExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomMetadataValidator<T, TProperty>(_metadataRepository!));
        }

        public static void Configure(IMetadataRepository metadataRepository)
        {
            _metadataRepository = metadataRepository;
        }
    }
}
