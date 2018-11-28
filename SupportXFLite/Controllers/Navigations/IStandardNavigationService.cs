using System;
using System.Threading.Tasks;
using SupportXFLite.ViewModels;

namespace SupportXFLite.Controllers.Navigations
{
    public interface IStandardNavigationService : IBasicNavigationService
    {
        Task InitializeAsync();
    }
}