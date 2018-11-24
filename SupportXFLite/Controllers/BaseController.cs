using System;
using System.Diagnostics;

namespace SupportXFLite.Controllers
{
    public abstract class BaseController
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