using Newtonsoft.Json.Linq;
using System.Net.Http; //.HttpResponseMessage

namespace TfsCmdlets.Extensions {
    public static class JsonExtensions {

        public static JObject ToJsonObject(this string self)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(self);
        }

        public static T ToJsonObject<T>(this string self)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(self);
        }

        public static JObject ToJsonObject(this HttpResponseMessage self)
        {
            var responseBody = self.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return ToJsonObject<JObject>(responseBody);
        }

        public static T ToJsonObject<T>(this HttpResponseMessage self)
        {
            var responseBody = self.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return ToJsonObject<T>(responseBody);
        }
    }
}