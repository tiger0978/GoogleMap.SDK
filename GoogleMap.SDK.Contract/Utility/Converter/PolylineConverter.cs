using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contracts.GoogleAPI.Models.Direction.Response.DirectionNewResponse;

namespace GoogleMap.SDK.Contract.Utility.Converter
{
    public class PolylineConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Polyline))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = serializer.Deserialize<JObject>(reader);
            string polyline = jo["encodedPolyline"].Value<string>();
            var points = PolylineEncoder.Decode(polyline);

            return new Polyline() { encodedPolyline = points};
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
