using IDX_NEWS.Core.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace IDX_NEWS.Core.Services
{
    public class EventCalendarService
    {
        private RestClient Client;
        //http://www.idx.co.id/umbraco/Surface/Home/GetCalendar?range=m&date=20180509
        public EventCalendarService(string baseUrl)
        {
            this.Client = new RestClient(baseUrl);
        }

        public EventCalendar EventCalendar(string url)
        {
            var request = new RestRequest(url, RestSharp.Method.GET);

            IRestResponse response = Client.Execute(request);

            var result = response.Content;

            var data = JsonConvert.DeserializeObject<EventCalendar>(result, EventCalendarConverter.Settings);

            return data;
        }
    }
}
