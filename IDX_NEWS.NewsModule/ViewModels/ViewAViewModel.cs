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

        private NewsAnnouncement newsAnnouncement;
        public NewsAnnouncement NewsAnnouncement
        {
            get { return newsAnnouncement; }
            set
            {
                SetProperty(ref newsAnnouncement, value);
                Prev.RaiseCanExecuteChanged();
                Next.RaiseCanExecuteChanged();
            }
        }
        private AnnouncementDetail _announcement;
        private long _pageNumber;

        public AnnouncementDetail Announcement
        {
            get { return _announcement; }
            set
            {
                
                SetProperty(ref _announcement, value);
            }
        }

        public DelegateCommand<long?> SelectArticle { get; set; }
        public DelegateCommand Prev { get; set; }
        public DelegateCommand Next { get; set; }

        public long PageNumber { get => _pageNumber; set { SetProperty(ref _pageNumber, value);} }


        public  ViewAViewModel()
        {

            SelectArticle = new DelegateCommand<long?>(Select, CanSelect);
            Next = new DelegateCommand(GoNext, CanGoNext);
            Prev = new DelegateCommand(GoPrev, CanGoPrev);
            PageNumber=1;

            NewsService = new NewsService("http://www.idx.co.id", Locale.IdId );
            InitData();  
        }
        private async void InitData()
        {
             NewsAnnouncement = await NewsService.NewsAnnouncements(1);
        }
        private async void GoPrev()
        {
            PageNumber = PageNumber-1;
            this.NewsAnnouncement = await NewsService.NewsAnnouncements( PageNumber);
        }

        private bool CanGoPrev()
        {
            return PageNumber>1;
        }

        private async void GoNext()
        {
            PageNumber = PageNumber+1;

            NewsAnnouncement = await NewsService.NewsAnnouncements( PageNumber);
            
        }

        private bool CanGoNext()
        {
            
            return PageNumber < newsAnnouncement.PageCount;
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
            }
            if (Announcement == null)
            {
                MessageBox.Show("Article not found.");
            }
        }

    }
}
