using System;
using SupportXFLite.Controllers.DJPools;
using SupportXFLite.Controllers.Navigations;
using Xamarin.Forms;

namespace SupportXFLite.Controllers
{
    public abstract class SupportProjectXF<TLocator,TNavigationService>
        where TLocator : IStandardLocator
        where TNavigationService : IStandardNavigationService
    {

        public TLocator LocatorManager;
        public TNavigationService NavigationManager;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        protected abstract void RegisterController(TLocator locator);

        protected abstract void RegisterViewModel(TLocator locator);


        //public abstract void SetupNavigationMap(Type viewModelType, object parameter, bool animate);

        protected virtual void SetupFinish()
        {
            LocatorManager.Build();
        }

        private void Initialize()
        {
            LocatorManager = (TLocator)Activator.CreateInstance(typeof(TLocator));
            NavigationManager = (TNavigationService)Activator.CreateInstance(typeof(TNavigationService),LocatorManager);

            RegisterController(LocatorManager);
            RegisterViewModel(LocatorManager);

            SetupFinish();
        }

        public SupportProjectXF()
        {
            Initialize();
        }
    }
}