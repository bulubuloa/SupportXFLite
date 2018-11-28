using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SupportXFLite.ViewModels
{
    public interface IHandleViewAppearing
    {
        Task OnViewAppearingAsync(VisualElement view);
    }
}