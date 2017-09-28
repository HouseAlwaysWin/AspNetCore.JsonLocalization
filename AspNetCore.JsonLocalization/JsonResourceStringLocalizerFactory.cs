using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;

namespace AspNetCore.JsonLocalization
{
    public class JsonResourceStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourcesRelativePath;
        public JsonResourceStringLocalizerFactory(
             IOptions<LocalizationOptions> localizationOptions)
        {
            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }
            _resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? String.Empty;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonResourceStringLocalizer(_resourcesRelativePath);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonResourceStringLocalizer(_resourcesRelativePath);
        }
    }
}
