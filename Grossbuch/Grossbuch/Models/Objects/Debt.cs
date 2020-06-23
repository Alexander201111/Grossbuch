using System;
using System.Collections.Generic;
using System.Text;

namespace Grossbuch.Models
{
    public class Debt
    {
        public int Id { get; set; }
        public int Id2 { get; set; }

        public DateTime Date { get; set; }
        public int Term { get; set; }

        public string Title { get; set; }
        public int Type { get; set; } //1-Ежемесячный выплат процентов, 2-Аннуитентный платеж, 3-Единовременный возврат с уплатой процентов
        public float Sum { get; set; }
        public float Rate { get; set; }

        public float MounthlyPayment { get; set; }
        public float TotalSum { get; set; } //всего заплачено

        public User User { get; set; }

        public bool IsClosed { get; set; }
        public DateTime UpdateTime { get; set; }

        public Debt()
        {

        }

        public Debt(User _user)
        {
            Date = DateTime.Now;
            Term = 0;
            Title = "";
            //Type = -1;
            Sum = 0;
            Rate = 0;
            MounthlyPayment = 0;
            TotalSum = 0;
            IsClosed = false;
            User = _user;

            UpdateTime = DateTime.Now;
        }

        public Debt(DateTime _date, int _term, string _title, /*int _type,*/
            float _sum, float _rate, float _mounthlyPayment, float _totalSum, bool _isClosed)
        {
            Date = _date;
            Term = _term;
            Title = _title;
            //Type = _type;
            Sum = _sum;
            Rate = _rate;
            MounthlyPayment = _mounthlyPayment;
            TotalSum = _totalSum;
            IsClosed = _isClosed;
            //User = _user;

            UpdateTime = DateTime.Now;
        }
    }
}
