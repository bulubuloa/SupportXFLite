using System;
using System.Threading.Tasks;
using SupportXFLite.Controllers;
using SupportXFLite.Controllers.Navigations;

namespace SupportXFLite.ViewModels
{
    public abstract partial class XFLiteBaseViewModel
    {
        protected abstract ISupportProjectXF GetSupportProject();

        protected IStandardNavigationService IF_GetNavigationService()
        {
            return GetSupportProject().GetNavigationService();
        }

        protected virtual async Task NavigationToPage<TViewModel>(bool animatte = true) where TViewModel : XFLiteBaseViewModel
        {
             await IF_GetNavigationService().NavigateToAsync<TViewModel>(animatte);
        }

        protected virtual async Task NavigationToPage<TViewModel>(object parameter, bool animatte = true) where TViewModel : XFLiteBaseViewModel
        {
            await IF_GetNavigationService().NavigateToAsync<TViewModel>(parameter,animatte);
        }

        protected virtual async Task NavigationToPopup<TViewModel>(bool animatte = true) where TViewModel : XFLiteBaseViewModel
        {
            await IF_GetNavigationService().NavigateToPopupAsync<TViewModel>(animatte);
        }

        protected virtual async Task NavigationToPopup<TViewModel>(object parameter, bool animatte = true) where TViewModel : XFLiteBaseViewModel
        {
            await IF_GetNavigationService().NavigateToPopupAsync<TViewModel>(parameter,animatte);
        }

        protected virtual async Task NavigationToPreviousPage(bool animate = true)
        {
            await IF_GetNavigationService().NavigateBackAsync(animate);
        }

        protected TModel ResolveViewModel<TModel>() where TModel : XFLiteBaseViewModel
        {
            return GetSupportProject().GetLocator().Resolve<TModel>();
        }

        protected TModel ResolveObject<TModel>() where TModel : class
        {
            return GetSupportProject().GetLocator().Resolve<TModel>();
        }
    }
}