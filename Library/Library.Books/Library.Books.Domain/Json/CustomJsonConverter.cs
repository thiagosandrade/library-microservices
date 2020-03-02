using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Library.Books.Domain.Json
{
    public class CustomJsonConverter<T> : JsonConverter where T : JsonDefault, new()
    {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // read the string array and convert it to dictionary 
            // as declared in your MyCustomType
            var arr = serializer.Deserialize<List<string>>(reader);

            var test = new List<T>();
            foreach (var item in arr)
            {
                if (!string.IsNullOrEmpty(item) && !string.IsNullOrWhiteSpace(item))
                {
                    T t = new T()
                    {
                        Id = Guid.NewGuid(),
                        Name = item.Trim().ToString()
                    };

                    test.Add(t);

                }
            }

            return test;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }

    }
}
