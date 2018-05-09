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
        //http://www.idx.co.id/umbraco/Surface/NewsAnnouncement/GetNewsSearch?pageSize=10
        public NewsService(string baseurl)
        {
            this.Client = new RestClient(baseurl);
        }


        public NewsAnnouncement NewsAnnouncements(string url)
        {
            var request = new RestRequest(url, RestSharp.Method.GET);

            IRestResponse response = Client.Execute(request);

            var result = response.Content;

            var data = JsonConvert.DeserializeObject<NewsAnnouncement>(result, NewsAnnouncementConverter.Settings);

            return data;
        }

    }
}
