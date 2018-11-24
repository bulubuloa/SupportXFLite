﻿using System;
using System.IO;
using SupportXFLite.DependencyServices;
using SupportXFLite.Droid;
using SupportXFLite.Droid.DependencyExtended;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileHelperExtended))]
namespace SupportXFLite.Droid.DependencyExtended
{
    public class IFileHelperExtended : IFileHelper
    {
        public void IF_GetImageSourceFilePath(ImageSource imageSource, string filePath)
        {
            throw new NotImplementedException();
        }

        public string IF_GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public Stream IF_GetStreamFilePath(string filePath)
        {
            var myBitmap = Android.Graphics.BitmapFactory.DecodeFile(filePath);
            var ms = new MemoryStream();
            myBitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 70, ms);
            ms.Seek(0L, SeekOrigin.Begin);
            return ms;
        }
    }
}
