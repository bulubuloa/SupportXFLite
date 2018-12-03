using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupportXFLite.Controllers.DJPools;
using SupportXFLite.Controllers.Navigations;
using Xamarin.Forms;

namespace SupportXFLite.Controllers
{
    public abstract class SupportProjectXF<TLocator,TNavigationService> : ISupportProjectXF
        where TLocator : IStandardLocator
        where TNavigationService : IStandardNavigationService
    {

        private TLocator LocatorManager;
        private TNavigationService NavigationManager;
        private Dictionary<string, object> PoolStateSaved;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        protected abstract void RegisterController(TLocator locator);

        protected abstract void RegisterViewModel(TLocator locator);

        protected abstract void MappingViewAndViewModel(TNavigationService navigationManager);

        public abstract Task SetupNavigationMap(Page page, Type viewModelType, object parameter, bool animate);

        protected virtual void SetupFinish()
        {
            PoolStateSaved = new Dictionary<string, object>();
            LocatorManager.Build();
        }

        private void Initialize()
        {
            LocatorManager = (TLocator)Activator.CreateInstance(typeof(TLocator));
            NavigationManager = (TNavigationService)Activator.CreateInstance(typeof(TNavigationService),LocatorManager,this);

            RegisterController(LocatorManager);
            RegisterViewModel(LocatorManager);
            MappingViewAndViewModel(NavigationManager);

            SetupFinish();
        }

        public IStandardLocator GetLocator()
        {
            return LocatorManager;
        }

        public IStandardNavigationService GetNavigationService()
        {
            return NavigationManager;
        }

        public SupportProjectXF()
        {
            Initialize();
        }
    }
}