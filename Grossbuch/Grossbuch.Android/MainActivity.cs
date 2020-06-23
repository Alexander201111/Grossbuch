using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Grossbuch.Droid.Services;
using ZXing.Mobile;
using Xamd.ImageCarousel.Forms.Plugin.Droid;

namespace Grossbuch.Droid
{
    [Activity(Label = "Grossbuch", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            DependencyService.Register<QrScanningService>();
            MobileBarcodeScanner.Initialize(Application);
            ImageCarouselRenderer.Init();

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}