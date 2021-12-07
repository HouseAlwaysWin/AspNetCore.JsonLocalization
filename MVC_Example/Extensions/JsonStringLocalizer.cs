using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace MVC_Example.Extensions
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IDistributedCache _cached;
        private JsonLocalizationOptions _options;

        public JsonStringLocalizer(
            IDistributedCache cached,
            IOptions<JsonLocalizationOptions> options)
        {
            this._cached = cached;
            this._options = options.Value;

        }
        public LocalizedString this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetStringSafely(name, null);

                return new LocalizedString(name, value ?? name, resourceNotFound: string.IsNullOrEmpty(value), searchedLocation: _options.ResourcePath);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        private string GetStringSafely(string name, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var keyCulture = culture ?? CultureInfo.CurrentUICulture;

            var cacheKey = $"name={name}&culture={keyCulture.Name}";

            string data = _cached.GetString(cacheKey);

            return string.IsNullOrEmpty(data) ? string.Empty : data;
        }
    }
}
