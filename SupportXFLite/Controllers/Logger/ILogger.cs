using System;
namespace SupportXFLite.Controllers.Logger
{
    public interface ILogger
    {
        void DebugMessage(string content, string prefix = "");
    }
}