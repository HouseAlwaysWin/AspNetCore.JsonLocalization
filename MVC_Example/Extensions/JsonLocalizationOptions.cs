using System.Globalization;

namespace MVC_Example.Extensions
{
    public class JsonLocalizationOptions
    {
        public string ResourcePath { get; set; } = "Localizations";
        public bool UseCached { get; set; } = false;
        public List<CultureInfo> SupportedCultures { get; set; } = new List<CultureInfo>();
    }

}
