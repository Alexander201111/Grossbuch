using Grossbuch.ViewModels;
using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grossbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinancialCoefficientsPage : ContentPage
    {
        FinancialCoefficientsVM viewModel;

        public FinancialCoefficientsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new FinancialCoefficientsVM(null);
        }

        private void DatePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            viewModel.ChangedDatesAsync();
        }

        private void YieldButton_Clicked(object sender, EventArgs e)
        {
            string information = "Доходность=Доходы / Расходы\n" +
                "Нормальный уровень показателя: 1,2 – 2.\n" +
                "Близко к единице (1,0 – 1,1) – вы живете только сегодняшним днем. Вам не хватает денег на плановые расходы и финансовые цели.\n" +
                "Меньше единицы – вы просто транжира, т.к. текущее потребление у вас превышает то, что вы зарабатываете.\n" +
                "Большая цифра (3 – 5) говорит о том, что вы либо миллиардер, либо жертвуете настоящим ради будущего. Долгосрочные жертвы тяжелы - велик риск сорваться и все бросить. " +
                "Текущее потребление тоже нужно увеличивать, если ваши доходы растут. Иначе какой смысл в богатстве? ";
            DisplayAlert("Информация о 'Доходность'", information, "ОK");
        }

        private void WelfareButton_Clicked(object sender, EventArgs e)
        {
            string information = "Доходность=Все сбережения / Сумма месячных расходов\n" +
                " Индикатор показывает, сколько времени вы сможете прожить без доходов.\n" +
                " Оптимально – 3-5." +
                " Меньше 2 – вы не защищены финансово.\n" +
                "Больше 10 – вы можете использовать свои сбережения более эффективно.\n";
            DisplayAlert("Информация о 'Обеспеченность'", information, "ОK");
        }
    }
}
