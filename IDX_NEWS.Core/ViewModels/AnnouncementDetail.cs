using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
namespace IDX_NEWS.Core.ViewModels
{
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using IDX_NEWS.Core.ViewModels;
    //
    //    var announcementDetail = AnnouncementDetail.FromJson(jsonString);




    public partial class AnnouncementDetail
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("ItemId")]
        public string ItemId { get; set; }

        [JsonProperty("PublishedDate")]
        public DateTime PublishedDate { get; set; }

        private string _imageUrl;


        [JsonProperty("ImageUrl")]
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = string.Format("http://idx.co.id{0}", value);
            }
        }

        [JsonProperty("Locale")]
        public string Locale { get; set; }

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

        [JsonProperty("Contents")]
        public string Contents { get; set; }
    }

    public partial class AnnouncementDetail
    {
        public static AnnouncementDetail FromJson(string json) => JsonConvert.DeserializeObject<AnnouncementDetail>(json, AnnouncementDetailConverter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AnnouncementDetail self) => JsonConvert.SerializeObject(self, AnnouncementDetailConverter.Settings);
    }

    internal static class AnnouncementDetailConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
