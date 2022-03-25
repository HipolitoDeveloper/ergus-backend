namespace Ergus.Backend.Core.Settings
{
    public class AppSettings
    {
        public string Audience              { get; set; } = String.Empty;
        public string Issuer                { get; set; } = String.Empty;
        public string Secret                { get; set; } = String.Empty;

        public string CryptographySecret    { get; set; } = String.Empty;
        public string PassMidGen            { get; set; } = String.Empty;
    }
}
