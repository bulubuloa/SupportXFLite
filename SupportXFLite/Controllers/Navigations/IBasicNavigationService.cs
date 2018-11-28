using System;
using System.Threading.Tasks;
using SupportXFLite.ViewModels;

namespace SupportXFLite.Controllers.Navigations
{
    public interface IBasicNavigationService
    {
        Task NavigateToAsync<TViewModel>(bool animate) where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter, bool animate) where TViewModel : BaseViewModel;
        Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : BaseViewModel;
        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : BaseViewModel;
        Task NavigateBackAsync(bool animate);
    }
}
