using Newtonsoft.Json;

namespace c_3po
{
    public static class SerializerFactory
    {
        static JsonSerializer serializer;

        public static JsonSerializer GetSerializer()
        {
            if (serializer == null)
            {
                var settings = DefaultSettings();
                //settings.RegisterContractsConverters();
                serializer = JsonSerializer.Create(settings);
            }

            return serializer;
        }

        static JsonSerializerSettings DefaultSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            settings.TypeNameHandling = TypeNameHandling.None;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            var contractReslover = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            contractReslover.DefaultMembersSearchFlags = contractReslover.DefaultMembersSearchFlags | System.Reflection.BindingFlags.NonPublic;
            settings.ContractResolver = contractReslover;

            return settings;
        }
    }
}
