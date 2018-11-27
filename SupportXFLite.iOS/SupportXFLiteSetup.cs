using System;
using Rg.Plugins.Popup;
using Xamarin.Forms.Platform.iOS;

namespace SupportXFLite.iOS
{
    public static class SupportXFLiteSetup
    {
        public static FormsApplicationDelegate AppDelegate;

        public static void Initialize(FormsApplicationDelegate _AppDelegate)
        {
            AppDelegate = _AppDelegate;
            Popup.Init();
        }
    }
}