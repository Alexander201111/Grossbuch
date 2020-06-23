using System;
using System.Collections.Generic;
using System.Text;

namespace Grossbuch.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public float Count { get; set; }
        public float TotalSum { get; set; }
        public DateTime UpdateTime { get; set; }

        public User User { get; set; }

        public Product()
        {
            Title = "";
            Price = 0;
            Count = 0;
            TotalSum = 0;
            UpdateTime = new DateTime();
        }
        public Product(string title, float price, float count)
        {
            Title = title;
            Price = price;
            Count = count;
            TotalSum = price * count;
            UpdateTime = new DateTime();
        }
    }
}
