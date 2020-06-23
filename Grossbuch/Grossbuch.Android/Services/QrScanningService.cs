using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;
using Grossbuch.Services;

[assembly: Dependency(typeof(Grossbuch.Droid.Services.QrScanningService))]
namespace Grossbuch.Droid.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}