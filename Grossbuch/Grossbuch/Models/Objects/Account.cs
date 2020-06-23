using System;

namespace Grossbuch.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
        public string Title { get; set; }
        public float Balance { get; set; }
        public float PlannedSum { get; set; }
        public float TotalSum { get; set; }
        public bool IsAccount { get; set; }

        public User User { get; set; }
        public DateTime UpdateTime { get; set; }

        public Account()
        {
            Title = "";
            Balance = -1;
            PlannedSum = -1;
            TotalSum = 0;
            IsAccount = true;
            UpdateTime = DateTime.Now;
        }

        public Account(int _id, string _title, float _balance, float _plannedSum, float _totalSum, User _user, bool _isAccount)
        {
            Id = _id;
            Title = _title;
            Balance = _balance;
            PlannedSum = _plannedSum;
            TotalSum = _totalSum;
            IsAccount = _isAccount;
            User = _user;
            UpdateTime = DateTime.Now;
        }

        public Account(string _title, float _balance, float _plannedSum, float _totalSum, User _user, bool _isAccount = true)
        {
            Title = _title;
            Balance = _balance;
            PlannedSum = _plannedSum;
            TotalSum = _totalSum;
            IsAccount = _isAccount;
            User = _user;
            UpdateTime = DateTime.Now;
        }

        public static implicit operator int(Account v)
        {
            throw new NotImplementedException();
        }
    }
}
