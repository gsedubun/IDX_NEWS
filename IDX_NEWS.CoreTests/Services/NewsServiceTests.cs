﻿using NUnit.Framework;
using IDX_NEWS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDX_NEWS.Core.ViewModels;

namespace IDX_NEWS.Core.Services.Tests
{
    [TestFixture()]
    public class NewsServiceTests
    {
        //http://www.idx.co.id/umbraco/Surface/NewsAnnouncement/GetNewsSearch?pageSize=10

        [Test()]
        public void NewsAnnouncementsTest()
        {
            NewsService newsService = new NewsService("http://www.idx.co.id", ViewModels.Locale.EnUs);

            var data = newsService.NewsAnnouncements();

            Assert.IsNotNull(data);
            Console.WriteLine(data.Items.Count());
        }

        [Test()]
        public void AnnouncementDetailTest()
        {
            NewsService newsService = new NewsService("http://www.idx.co.id", Locale.EnUs);

            var data = newsService.Details(1301);

            Assert.IsNotNull(data);
            Console.WriteLine(data);
        }
    }
}