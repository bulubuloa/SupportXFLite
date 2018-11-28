using System;
using System.Threading.Tasks;
using SupportXFLite.Controllers.Navigations;

namespace SupportXFLite.ViewModels
{
    public abstract partial class BaseViewModel
    {
        public abstract IStandardNavigationService IF_GetNavigationService();

        protected virtual async Task NavigationToPage<TViewModel>(bool animatte = true) where TViewModel : BaseViewModel
        {
             await IF_GetNavigationService().NavigateToAsync<TViewModel>(animatte);
        }

        protected virtual async Task NavigationToPage<TViewModel>(object parameter, bool animatte = true) where TViewModel : BaseViewModel
        {
            await IF_GetNavigationService().NavigateToAsync<TViewModel>(parameter,animatte);
        }

        protected virtual async Task NavigationToPopup<TViewModel>(bool animatte = true) where TViewModel : BaseViewModel
        {
            await IF_GetNavigationService().NavigateToPopupAsync<TViewModel>(animatte);
        }

        protected virtual async Task NavigationToPopup<TViewModel>(object parameter, bool animatte = true) where TViewModel : BaseViewModel
        {
            await IF_GetNavigationService().NavigateToPopupAsync<TViewModel>(parameter,animatte);
        }

        protected virtual async Task NavigationToPreviousPage(bool animate = true)
        {
            await IF_GetNavigationService().NavigateBackAsync(animate);
        }
    }
}