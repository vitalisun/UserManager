using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace UserManager.Core.Extentions
{
    public static class ExtensionMethods
    {
        private static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };
        public static string Serialize(this object value)
        {
            return JsonConvert.SerializeObject(value, JsonSerializerSettings);
        }

        public static T Deserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value, JsonSerializerSettings);
        }
    }
}
