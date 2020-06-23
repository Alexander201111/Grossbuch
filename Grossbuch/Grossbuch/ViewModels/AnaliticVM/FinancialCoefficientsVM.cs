using Grossbuch.Models;
using Grossbuch.Repositories;
using System;

namespace Grossbuch.ViewModels
{
    class FinancialCoefficientsVM : BaseViewModel
    {
        public User User;

        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }

        #region Coefficients
        public float incomes;
        public float Incomes { get { return incomes; } set { incomes = value; OnPropertyChanged(); } } //Доходы

        public float costs;
        public float Costs { get { return costs; } set { costs = value; OnPropertyChanged(); } } //Расходы

        public float yield;
        public float Yield { get { return yield; } set { yield = value; OnPropertyChanged(); } } //Доходнось

        public float finincialDiscipline;
        public float FinincialDiscipline { get { return finincialDiscipline; } set { finincialDiscipline = value; OnPropertyChanged(); } } //Финансовая дисциплина

        public float welfare;
        public float Welfare { get { return welfare; } set { welfare = value; OnPropertyChanged(); } } //Обеспеченность
        #endregion

        public OperationRepository repository;

        public FinancialCoefficientsVM(User _user)
        {
            User = _user;
            repository = new OperationRepository(_user);
            DateFinish = DateTime.Now;
            DateStart = DateFinish.Subtract(new TimeSpan(30, 0, 0, 0));
            ChangedDatesAsync();
        }

        public async void ChangedDatesAsync()
        {
            DateStart = DateFinish.Subtract(new TimeSpan(30, 0, 0, 0));
            Incomes = await repository.GetIncomeAsync(DateStart, DateFinish);
            Costs = await repository.GetCostsAsync(DateStart, DateFinish);

            AccountRepository accRepository = new AccountRepository(User);
            float totalActivies = await accRepository.GetTotalActiviesAsync();
            Yield = Incomes / Costs;
            Welfare = totalActivies / Costs;
        }
    }
}
