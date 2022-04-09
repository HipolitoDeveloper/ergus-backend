namespace Ergus.Backend.Infrastructure.Helpers
{
    internal static class SqlUtils
    {
        public const string NUMERIC = "numeric";

        public const string NUMERIC_18_4 = NUMERIC + "(18,4)";

        public const string NUMERIC_18_2 = NUMERIC + "(18,2)";

        public const string NUMERIC_15_2 = NUMERIC + "(15, 2)";

        public const string NUMERIC_8_4 = NUMERIC + "(8, 4)";

        public const string VARCHAR = "varchar";

        public const string MAIN = "main";

        public const string NOW = "now()";

        public const string NOWMIN10_YEARS = "(" + NOW + " - '10 years'::interval)";

        public const string NOWMAX10_YEARS = "(" + NOW + " + '10 years'::interval)";
    }
}
