using System.Threading.Tasks;

namespace Grossbuch.Services
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}