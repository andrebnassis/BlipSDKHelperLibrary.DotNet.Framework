using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlipSDKHelperLibrary.Models
{
    internal class Button
    {
        [JsonConverter(typeof(StringEnumConverter))]
        internal ButtonType Type { get; set; }
        internal string Text { get; set; }
        internal string Value { get; set; }
        internal int Order { get; set; }
    }
    
}
