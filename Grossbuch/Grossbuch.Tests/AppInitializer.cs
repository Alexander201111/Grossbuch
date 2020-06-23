using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Grossbuch.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                //return ConfigureApp.Android.StartApp();
                //return ConfigureApp.Android.ApkFile("../../Grossbuch.Android/bin/Debug/com.companyname.Grossbuch.apk").StartApp();
                return ConfigureApp.Android.ApkFile(@"C:\Users\sashk\Desktop\Grossbuch\Grossbuch\Grossbuch.Android\bin\Release/com.companyname.Grossbuch.apk").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}