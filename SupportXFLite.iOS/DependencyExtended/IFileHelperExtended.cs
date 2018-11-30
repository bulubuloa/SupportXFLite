using System;
using System.IO;
using SupportXFLite.DependencyServices;
using SupportXFLite.iOS.DependencyExtended;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileHelperExtended))]
namespace SupportXFLite.iOS.DependencyExtended
{
    public class IFileHelperExtended : IFileHelper
    {
        public string IF_GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}