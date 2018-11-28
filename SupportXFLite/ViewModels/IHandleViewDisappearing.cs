using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SupportXFLite.ViewModels
{
    public interface IHandleViewDisappearing
    {
        Task OnViewDisappearingAsync(VisualElement view);
    }
}