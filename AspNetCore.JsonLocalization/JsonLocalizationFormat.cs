using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.JsonLocalization
{
    public class JsonLocalizationFormat
    {
        public string Key { get; set; }
        public Dictionary<string, string> Value =
            new Dictionary<string, string>();
    }
}
