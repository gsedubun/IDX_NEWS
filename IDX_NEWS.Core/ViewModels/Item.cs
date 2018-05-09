using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IDX_NEWS.Core.ViewModels
{
    public  class Item
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("ItemId")]
        public string ItemId { get; set; }

        [JsonProperty("PublishedDate")]
        public DateTimeOffset PublishedDate { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("Locale")]
        public Locale Locale { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("PathBase")]
        public object PathBase { get; set; }

        [JsonProperty("PathFile")]
        public object PathFile { get; set; }

        [JsonProperty("Tags")]
        public string Tags { get; set; }

        [JsonProperty("IsHeadline")]
        public bool IsHeadline { get; set; }

        [JsonProperty("Summary")]
        public string Summary { get; set; }

        [JsonProperty("Links")]
        public Link[] Links { get; set; }
    }

    public enum Method { Get };

    public enum Locale { EnUs, IdId };

    //public class NewsAnnouncement
    //{
    //    public static NewsAnnouncement FromJson(string json) => JsonConvert.DeserializeObject<NewsAnnouncement>(json, IDX_NEWS.Core.ViewModels.Converter.Settings);
    //}

    public static class NewsAnnouncementSerialize
    {
        public static string ToJson(this NewsAnnouncement self) => JsonConvert.SerializeObject(self, IDX_NEWS.Core.ViewModels.NewsAnnouncementConverter.Settings);
    }

    internal static class NewsAnnouncementConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new MethodConverter(),
                new LocaleConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class MethodConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Method) || t == typeof(Method?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "GET")
            {
                return Method.Get;
            }
            throw new Exception("Cannot unmarshal type Method");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Method)untypedValue;
            if (value == Method.Get)
            {
                serializer.Serialize(writer, "GET"); return;
            }
            throw new Exception("Cannot marshal type Method");
        }
    }

    internal class LocaleConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Locale) || t == typeof(Locale?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "en-us":
                    return Locale.EnUs;
                case "id-id":
                    return Locale.IdId;
            }
            throw new Exception("Cannot unmarshal type Locale");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Locale)untypedValue;
            switch (value)
            {
                case Locale.EnUs:
                    serializer.Serialize(writer, "en-us"); return;
                case Locale.IdId:
                    serializer.Serialize(writer, "id-id"); return;
            }
            throw new Exception("Cannot marshal type Locale");
        }
    }
}
