using System;
using Xamarin.Forms;

namespace SupportXFLite.DependencyServices
{
    public interface IFileHelper
    {
        string IF_GetLocalFilePath(string filename);
        System.IO.Stream IF_GetStreamFilePath(string filePath);
        void IF_GetImageSourceFilePath(ImageSource imageSource, string filePath);
    }
}