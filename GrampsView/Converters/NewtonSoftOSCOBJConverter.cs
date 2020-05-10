namespace GrampsView.Converters
{
    using Newtonsoft.Json;

    using GrampsView.Data.Collections;

    using System;

    internal class NewtonSoftOSCOBJConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(HLinkBackLinkModelCollection));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = Newtonsoft.Json.Linq.JObject.Load(reader);

            return new object();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Xamarin.Forms.Color)value).ToHex());
        }
    }
}