using System;
using System.Threading.Tasks;
using SupportXFLite.ViewModels;
using Xamarin.Forms;

namespace SupportXFLite.Controllers.Navigations
{
    public interface IStandardNavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>(bool animate) where TViewModel : XFLiteBaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter, bool animate) where TViewModel : XFLiteBaseViewModel;
        Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : XFLiteBaseViewModel;
        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : XFLiteBaseViewModel;
        Task NavigateBackAsync(bool animate);
        void Map<TViewModel, TView>() where TViewModel : XFLiteBaseViewModel where TView : Xamarin.Forms.Page;
        Page CreateAndBindPage(Type viewModelType, object parameter);
    }
}