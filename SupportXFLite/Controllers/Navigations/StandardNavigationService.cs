using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SupportXFLite.Controllers.DJPools;
using SupportXFLite.ViewModels;
using Xamarin.Forms;

namespace SupportXFLite.Controllers.Navigations
{
    public abstract class StandardNavigationService : IStandardNavigationService
    {
        protected readonly Dictionary<Type, Type> poolViewModel;
        protected readonly IStandardLocator standardLocator;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public StandardNavigationService(IStandardLocator _standardLocator)
        {
            standardLocator = _standardLocator;
            poolViewModel = new Dictionary<Type, Type>();
            Initialize();
        }

        private void Initialize()
        {
            CreatePoolViewModel();
        }

        public abstract void CreatePoolViewModel();

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task NavigateBackAsync(bool animate)
        {
            if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync(animate);
            }
        }

        public Task NavigateToAsync<TViewModel>(bool animate) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null, animate);
        }

        public Task NavigateToAsync<TViewModel>(object parameter, bool animate) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter, animate);
        }

        public Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : BaseViewModel
        {
            return NavigateToPopupAsync<TViewModel>(null, animate);
        }

        public async Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : BaseViewModel
        {
            var page = CreateAndBindPage(typeof(TViewModel), parameter);
            await(page.BindingContext as BaseViewModel).InitializeAsync(parameter);

            if (page is PopupPage)
            {
                await PopupNavigation.Instance.PushAsync(page as PopupPage, animate);
            }
            else
            {
                throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
            }
        }

        protected abstract Task NavigationMapping(Page page, Type viewModelType, object parameter, bool animate);


        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool animate)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);
            await NavigationMapping(page, viewModelType, parameter, animate);
            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!poolViewModel.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }
            return poolViewModel[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            var viewModel = standardLocator.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }
    }
}