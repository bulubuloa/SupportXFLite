using System;
using System.IO;
using SupportXFLite.DependencyServices;
using SupportXFLite.Droid.DependencyExtended;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileHelperExtended))]
namespace SupportXFLite.Droid.DependencyExtended
{
    public class IFileHelperExtended : IFileHelper
    {
        public string IF_GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
