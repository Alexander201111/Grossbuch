using System;
using System.Collections.Generic;
using System.Text;

namespace Grossbuch.Models
{
    public class Currency //Валюта
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public float Coefficient { get; set; }
        public float TotalSum { get; set; }

        public Currency()
        {
            Title = "";
            Coefficient = -1;
        }

        public Currency(string _key, string _title, float _coefficient)
        {
            Key = _key;
            Title = _title;
            Coefficient = _coefficient;
        }
    }
}
