using IDX_NEWS.Core.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IDX_NEWS.Core.Services
{
    public class NewsService
    {
        private RestClient Client;
        private const int pageSize = 15;
        private readonly Locale locale;
        private const string resourceUrl = "umbraco/Surface/NewsAnnouncement/GetNewsSearch";
        private const string detailUrl = "umbraco/Surface/NewsAnnouncement/GetNewsDetail?id=";
        public NewsService(string baseurl, Locale locale)
        {
            this.Client = new RestClient(baseurl);
            this.locale = locale;
        }


        public NewsAnnouncement NewsAnnouncements()
        {
            var request = new RestRequest(resourceUrl, RestSharp.Method.GET);

            IRestResponse response = Client.Execute(request);

            var result = response.Content;

            var data = JsonConvert.DeserializeObject<NewsAnnouncement>(result, NewsAnnouncementConverter.Settings);

            return data;
        }
        public NewsAnnouncement NewsAnnouncements( long pageNumber = 1)
        {
            string _locale = "id-id";
            switch (locale)
            {
                case Locale.EnUs:
                    _locale = "en-us";
                    break;
            }
            var request = new RestRequest(resourceUrl + "?locale=" + _locale + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize, RestSharp.Method.GET);

            IRestResponse response = Client.Execute(request);
            if (response.IsSuccessful)
            {
                var result = response.Content;
                var data = JsonConvert.DeserializeObject<NewsAnnouncement>(result, NewsAnnouncementConverter.Settings);
                return data;
            }
            return null;
        }
        public async Task<NewsAnnouncement> NewsAnnouncementsAsync(long pageNumber = 1)
        {
            NewsAnnouncement data = new NewsAnnouncement();
            string _locale = "id-id";
            switch (locale)
            {
                case Locale.EnUs:
                    _locale = "en-us";
                    break;
            }
            var request = new RestRequest(resourceUrl + "?locale=" + _locale + "&pageNumber=" + pageNumber + "&pageSize=" + pageSize, RestSharp.Method.GET);
            IRestResponse response;
            var handle = Client.ExecuteAsync(request, resp =>
              {
                  response = resp;
                  if (response.IsSuccessful)
                  {
                      var result = response.Content;
                      data = JsonConvert.DeserializeObject<NewsAnnouncement>(result, NewsAnnouncementConverter.Settings);
                  }
              });
            await handle.WebRequest.GetResponseAsync();
            return data;
            //HttpClient httpClient = new HttpClient();

        }
        public AnnouncementDetail Details(long itemId)
        {
            var request = new RestRequest(detailUrl + itemId, RestSharp.Method.GET);

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
