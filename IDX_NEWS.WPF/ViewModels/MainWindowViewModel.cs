using IDX_NEWS.NewsModule.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace IDX_NEWS.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }

    }
}
