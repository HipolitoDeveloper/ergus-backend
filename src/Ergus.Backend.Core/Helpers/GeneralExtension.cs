using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ergus.Backend.Core.Helpers
{
    public static class GeneralExtension
    {
        public static string DescriptionAttr<T>(this T source)
        {
            if (source == null)
                return String.Empty;

            var sourceName = source!.ToString()!;
            FieldInfo? fi = source?.GetType().GetField(sourceName);

            if (fi == null)
                return sourceName;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi!.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return sourceName;
        }

        public static T? GetEnumValueFromDescription<T>(this string description)
        {
            MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            return default;
        }

        public static string ToOnlyNumbers(this string texto)
        {
            if (String.IsNullOrEmpty(texto))
                return String.Empty;

            Regex regexObj = new Regex(@"[^\d]");
            return regexObj.Replace(texto, "");
        }
    }
}
