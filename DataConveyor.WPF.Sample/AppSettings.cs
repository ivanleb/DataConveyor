using DataConveyor.Views.WPF.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConveyor.WPF.Sample
{
    public class AppSettings
    {
        public class AppearanceSettings
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public Themes Theme { get; set; }

            public String DarkThemePath { get; set; }
            public String LightThemePath { get; set; }
        }

        public AppearanceSettings Appearance { get; set; }
    }
}
