using Grossbuch.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnaliticsPage : TabbedPage
    {
        public AnaliticsPage()
        {
            InitializeComponent();
        }

        public AnaliticsPage(User user)
        {
            InitializeComponent();
        }
    }
}