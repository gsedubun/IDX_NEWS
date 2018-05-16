using IDX_NEWS.NewsModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace IDX_NEWS.NewsModule
{
    public class NewsModuleModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public NewsModuleModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ViewA>();
            //_regionManager.AddToRegion("ContentRegion", new ViewA());
        }

        
    }
}