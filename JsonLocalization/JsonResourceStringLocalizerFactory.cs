using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonLocalization
{
    public class JsonResourceStringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonResourceStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonResourceStringLocalizer();
        }
    }
}
