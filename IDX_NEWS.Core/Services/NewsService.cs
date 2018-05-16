using IDX_NEWS.Core.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDX_NEWS.Core.Services
{
    public class NewsService
    {
        private RestClient Client;

        private const string resourceUrl = "umbraco/Surface/NewsAnnouncement/GetNewsSearch";
        private const string detailUrl = "umbraco/Surface/NewsAnnouncement/GetNewsDetail?id=";
        public NewsService(string baseurl)
        {
            this.Client = new RestClient(baseurl);
        }


        public NewsAnnouncement NewsAnnouncements()
        {
            var request = new RestRequest(resourceUrl, RestSharp.Method.GET);

            IRestResponse response = Client.Execute(request);

            var result = response.Content;

            var data = JsonConvert.DeserializeObject<NewsAnnouncement>(result, NewsAnnouncementConverter.Settings);

            return data;
        }
        public NewsAnnouncement NewsAnnouncements( Locale locale, int pagesize)
        {
            string _locale = "id-id";
            switch (locale)
            {
                case Locale.EnUs:
                    _locale = "en-us";
                    break;
            }
            var request = new RestRequest(resourceUrl+"?locale="+_locale+"&pageSize="+pagesize, RestSharp.Method.GET);

            IRestResponse response = Client.Execute(request);
            if (response.IsSuccessful)
            {
                var result = response.Content;
                var data = JsonConvert.DeserializeObject<NewsAnnouncement>(result, NewsAnnouncementConverter.Settings);
                return data;
            }
            return null;
        }

        public AnnouncementDetail Details(long itemId)
        {
            var request = new RestRequest(detailUrl  + itemId, RestSharp.Method.GET);

            IRestResponse response = Client.Execute(request);
            if (response.IsSuccessful)
            {
                var result = response.Content;
                var data = JsonConvert.DeserializeObject<AnnouncementDetail>(result, AnnouncementDetailConverter.Settings);
                return data;
            }
            return null;
        }
    }
}
