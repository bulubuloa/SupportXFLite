using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SupportXFLite.Controllers
{
    public interface ISupportProjectXF
    {
        Task SetupNavigationMap(Page page, Type viewModelType, object parameter, bool animate);
    }
}