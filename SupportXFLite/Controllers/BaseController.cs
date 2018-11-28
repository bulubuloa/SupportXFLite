using System;
using System.Diagnostics;
using SupportXFLite.Controllers.Logger;

namespace SupportXFLite.Controllers
{
    public abstract class BaseController : ILogger
    {
        public void DebugMessage(string content, string prefix = "")
        {
            Debug.WriteLine(prefix + ": " + content);
        }

        public BaseController()
        {
        }
    }
}