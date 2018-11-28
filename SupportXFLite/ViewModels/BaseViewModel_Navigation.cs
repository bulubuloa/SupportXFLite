using System;
using System.Threading.Tasks;
using SupportXFLite.Controllers.Navigations;

namespace SupportXFLite.ViewModels
{
    public abstract partial class BaseViewModel
    {
        public abstract TNavigationService IF_GetNavigationService<TNavigationService>() where TNavigationService : IBasicNavigationService;

        protected virtual void NavigationToPage<TViewModel>(bool animatte = true) where TViewModel : BaseViewModel
        {
            IF_GetNavigationService<IBasicNavigationService>().NavigateToAsync<TViewModel>(animatte);
        }

        protected virtual void NavigationToPage<TViewModel>(object parameter, bool animatte = true) where TViewModel : BaseViewModel
        {
            IF_GetNavigationService<IBasicNavigationService>().NavigateToAsync<TViewModel>(parameter,animatte);
        }

        protected virtual void NavigationToPopup<TViewModel>(bool animatte = true) where TViewModel : BaseViewModel
        {
            IF_GetNavigationService<IBasicNavigationService>().NavigateToPopupAsync<TViewModel>(animatte);
        }

        protected virtual void NavigationToPopup<TViewModel>(object parameter, bool animatte = true) where TViewModel : BaseViewModel
        {
            IF_GetNavigationService<IBasicNavigationService>().NavigateToPopupAsync<TViewModel>(parameter,animatte);
        }

    }
}
