using System;
using System.Collections.Generic;
using System.Text;

namespace Grossbuch.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
        public string Title { get; set; }
        public float TotalSum { get; set; }
        public User User { get; set; }

        public DateTime UpdateTime { get; set; }

        public Category()
        {
            Title = "";
            TotalSum = 0;
            User = null;
            UpdateTime = DateTime.Now;
        }

        public Category(string _title, User _user)
        {
            Title = _title;
            TotalSum = 0;
            User = _user ?? null;
            UpdateTime = DateTime.Now;
        }
    }
}
