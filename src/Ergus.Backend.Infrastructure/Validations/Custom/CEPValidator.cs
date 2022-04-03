using Ergus.Backend.Core.Helpers;
using FluentValidation;
using FluentValidation.Validators;

namespace Ergus.Backend.Infrastructure.Validations.Custom
{
    public class CEPValidator<T, TProp> : PropertyValidator<T, TProp>
    {
        public override string Name => "CEPValidator";

        public override bool IsValid(ValidationContext<T> context, TProp value)
        {
            if (value == null) return true;

            var cep = value.ToString()!.ToOnlyNumbers();

            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
            }

            return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }
    }

    public static class StaticCEPValidator
    {
        public static IRuleBuilderOptions<T, TProperty> IsValidCEP<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CEPValidator<T, TProperty>());
        }
    }
}
