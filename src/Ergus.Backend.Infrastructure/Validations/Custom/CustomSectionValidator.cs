using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomSectionCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ISectionRepository _sectionRepository;

        public CustomSectionCodeBeUniqueValidator(ISectionRepository sectionRepository) : base()
        {
            this._sectionRepository = sectionRepository;
        }

        public override string Name => "CustomSectionCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var section = value as Section;
            var sectionWithCode = this._sectionRepository.GetByCode(section!.Code).Result;

            if (sectionWithCode == null) return true;

            return section.Id == sectionWithCode.Id;
        }
    }

    public static class StaticSectionCodeBeUniqueValidator
    {
        private static ISectionRepository? _sectionRepository;

        public static IRuleBuilderOptions<T, TProperty> IsSectionCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomSectionCodeBeUniqueValidator<T, TProperty>(_sectionRepository!));
        }

        public static void Configure(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ SectionExists]

    public class CustomSectionExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ISectionRepository _sectionRepository;

        public CustomSectionExistsValidator(ISectionRepository sectionRepository) : base()
        {
            this._sectionRepository = sectionRepository;
        }

        public override string Name => "SectionExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int sectionId))
                return false;

            var section = this._sectionRepository.Get(sectionId, false).Result;

            return (section != null);
        }
    }

    public static class StaticSectionExistsValidator
    {
        private static ISectionRepository? _sectionRepository;

        public static IRuleBuilderOptions<T, TProperty> SectionExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomSectionExistsValidator<T, TProperty>(_sectionRepository!));
        }

        public static void Configure(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
    }

    #endregion [ FIM - SectionExists]
}
