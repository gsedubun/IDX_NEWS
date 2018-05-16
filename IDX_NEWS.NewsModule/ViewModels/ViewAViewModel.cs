using IDX_NEWS.Core.Services;
using IDX_NEWS.Core.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IDX_NEWS.NewsModule.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private NewsService NewsService;


        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private NewsAnnouncement newsAnnouncement;
        public NewsAnnouncement NewsAnnouncement
        {
            get { return newsAnnouncement; }
            set
            {
                SetProperty(ref newsAnnouncement, value);
            }
        }
        private AnnouncementDetail _announcement;
        public AnnouncementDetail Announcement
        {
            get { return _announcement; }
            set
            {
                SetProperty(ref _announcement, value);
            }
        }

        public DelegateCommand<long?> SelectArticle { get; set; }
        public ViewAViewModel()
        {
            //Message = "View A from your Prism Module";
            this.NewsService = new NewsService("http://www.idx.co.id");
            this.NewsAnnouncement = NewsService.NewsAnnouncements(Locale.IdId, 20);

            SelectArticle = new DelegateCommand<long?>(Select, CanSelect);
        }

        private bool CanSelect(long? arg)
        {
            return true;
        }

        private void Select(long? itemid)
        {
            Debug.WriteLine(itemid + "==============>");
            if (itemid.HasValue)
            {
                Announcement = NewsService.Details(itemid.Value);//
                Item singlnews = NewsAnnouncement.Items.SingleOrDefault(d => d.Id == itemid);
                //if (singlnews != null)
                //{
                //    Announcement.ImageUrl = string.Format("http://idx.co.id{0}", singlnews.ImageUrl);
                //}
            }
            if (Announcement == null)
            {
                MessageBox.Show("Article not found.");
            }
        }

    }
}
