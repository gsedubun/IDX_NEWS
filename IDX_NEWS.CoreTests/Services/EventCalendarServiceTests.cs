using NUnit.Framework;
using IDX_NEWS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDX_NEWS.Core.Services.Tests
{
    [TestFixture()]
    public class EventCalendarServiceTests
    {
        [Test()]
        public void EventCalendarTest()
        {
            EventCalendarService newsService = new EventCalendarService("http://www.idx.co.id");

            var data = newsService.EventCalendar("umbraco/Surface/Home/GetCalendar?range=m&date=20180509");

            Assert.Greater(data.Results.Count(),0);
            Console.WriteLine(data.Results.Count());
        }
    }
}