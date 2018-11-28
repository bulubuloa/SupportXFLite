using System;
using SupportXFLite.Controllers.DJPools;

namespace SupportXFLite.Controllers.Navigations
{
    public class BaseNavigationService : StandardNavigationService
    {
        public BaseNavigationService(IStandardLocator _standardLocator, ISupportProjectXF _supportProjectXF) : base(_standardLocator, _supportProjectXF)
        {
        }
    }
}
