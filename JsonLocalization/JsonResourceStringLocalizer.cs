﻿using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace JsonLocalization
{
    public class JsonResourceStringLocalizer : IStringLocalizer
    {
        List<JsonLocalizationFormat> localization = new List<JsonLocalizationFormat>();
        public JsonResourceStringLocalizer()
        {
            //read all json file
            JsonSerializer serializer = new JsonSerializer();
            localization = JsonConvert.DeserializeObject<List<JsonLocalizationFormat>>(
                File.ReadAllText(@"localization.json"));

        }


        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(
                    name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value,
                    resourceNotFound: format == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(
            bool includeParentCultures)
        {
            return localization.Where(
                l => l.Value.Keys.Any(
                    lv => lv == CultureInfo.CurrentCulture.Name))
                    .Select(l => new LocalizedString(
                        l.Key, l.Value[CultureInfo.CurrentCulture.Name],
                        true));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonResourceStringLocalizer();
        }

        private string GetString(string name)
        {
            var query = localization.Where(
                l => l.Value.Keys.Any(
                    lv => lv == CultureInfo.CurrentCulture.Name));
            var value = query.FirstOrDefault(l => l.Key == name);

            if (value == null)
            {
                return name;
            }

            return value.Value[CultureInfo.CurrentCulture.Name];
        }
    }
}
