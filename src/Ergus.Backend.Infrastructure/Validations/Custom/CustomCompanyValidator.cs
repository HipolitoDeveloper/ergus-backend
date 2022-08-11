using FluentValidation;
using FluentValidation.Validators;
using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    #region [ CodeBeUnique ]

    public class CustomCompanyCodeBeUniqueValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ICompanyRepository _companyRepository;

        public CustomCompanyCodeBeUniqueValidator(ICompanyRepository companyRepository) : base()
        {
            this._companyRepository = companyRepository;
        }

        public override string Name => "CustomCompanyCodeBeUnique";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var company = value as Company;
            var companyWithCode = this._companyRepository.GetByCode(company!.Code).Result;

            if (companyWithCode == null) return true;

            return company.Id == companyWithCode.Id;
        }
    }

    public static class StaticCompanyCodeBeUniqueValidator
    {
        private static ICompanyRepository? _companyRepository;

        public static IRuleBuilderOptions<T, TProperty> IsCompanyCodeUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomCompanyCodeBeUniqueValidator<T, TProperty>(_companyRepository!));
        }

        public static void Configure(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
    }

    #endregion [ FIM - CodeBeUnique ]

    #region [ CompanyExists]

    public class CustomCompanyExistsValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        private readonly ICompanyRepository _companyRepository;

        public CustomCompanyExistsValidator(ICompanyRepository companyRepository) : base()
        {
            this._companyRepository = companyRepository;
        }

        public override string Name => "CompanyExists";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null || !Int32.TryParse(value.ToString(), out int companyId))
                return false;

            var company = this._companyRepository.Get(companyId, false).Result;

            return (company != null);
        }
    }

    public static class StaticCompanyExistsValidator
    {
        private static ICompanyRepository? _companyRepository;

        public static IRuleBuilderOptions<T, TProperty> CompanyExists<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CustomCompanyExistsValidator<T, TProperty>(_companyRepository!));
        }

        public static void Configure(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
    }

    #endregion [ FIM - CompanyExists]
}
