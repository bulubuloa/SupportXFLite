using System;
using System.Threading.Tasks;
using SupportXFLite.Controllers.DJPools;
using SupportXFLite.Controllers.Navigations;
using Xamarin.Forms;

namespace SupportXFLite.Controllers
{
    public interface ISupportProjectXF
    {
        Task SetupNavigationMap(Page page, Type viewModelType, object parameter, bool animate);
        IStandardLocator GetLocator();
        IStandardNavigationService GetNavigationService();
    }
}