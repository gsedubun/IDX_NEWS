using Newtonsoft.Json;

namespace IDX_NEWS.Core.ViewModels
{
    /// <summary>
    /// http://www.idx.co.id/umbraco/Surface/NewsAnnouncement/GetNewsSearch?pageSize=10
    /// </summary>

    public class NewsAnnouncement
    {
        public NewsAnnouncement()
        {
        }


        [JsonProperty("Items")]
        public Item[] Items { get; set; }

        [JsonProperty("ItemCount")]
        public long ItemCount { get; set; }

        [JsonProperty("PageSize")]
        public long PageSize { get; set; }

        [JsonProperty("Links")]
        public Link[] Links { get; set; }

        [JsonProperty("PageNumber")]
        public long PageNumber { get; set; }

        [JsonProperty("PageCount")]
        public long PageCount { get; set; }
    }

    
}
