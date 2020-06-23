using System;
using System.Collections.Generic;

namespace Grossbuch.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
        public DateTime Date { get; set; }
        public float Summ { get; set; }
        public int Type { get; set; } //1-приход, 2-расход, 3-перевод
        public string Description { get; set; }
        public float Remainder { get; set; } //остаток
        public string Images { get; set; }
        public bool Performed { get; set; }

        public Category Category { get; set; }
        public Currency Currency { get; set; }
        public Account Account { get; set; }
        public Account Purpose { get; set; }
        public User User { get; set; }

        public DateTime UpdateTime { get; set; }

        public Operation()
        {
            Date = new DateTime();
            Summ = -1;
            Type = -1;
            Description = "";
            Performed = false;

            Category = null;
            Currency = null;
            Account = null;
            Purpose = null;
            User = null;
            UpdateTime = DateTime.Now;
            //Color = "Red";
        }

        public Operation(DateTime _date, float _sum, int _type, string _decription,
            Category _category, Currency _currency,
            Account _account, Account _purpose, User _user)
        {
            Date = (_date != null) ? _date : DateTime.Now;
            Summ = _sum;
            Type = _type;
            Description = _decription;
            Performed = false;
            Category = _category ?? null;
            Currency = _currency ?? null;
            Account = _account ?? null;
            Purpose = _purpose ?? null;
            User = _user ?? null;
            UpdateTime = DateTime.Now;
        }

        public void CommitOperation()
        {
            Remainder = (Type == 1) ? Account.Balance + Summ : Account.Balance - Summ;
            UpdateTime = DateTime.Now;
        }

        public string PathsToString(List<ViewModels.OperationDetailVM.ImageItem> paths)
        {
            Images = "";

            foreach(var im in paths)
            {
                Images = Images + im.ImageUrl + "@";
            }

            return Images;
        }

        public List<ViewModels.OperationDetailVM.ImageItem> StringToPaths()
        {
            List<ViewModels.OperationDetailVM.ImageItem> paths = new List<ViewModels.OperationDetailVM.ImageItem>();

            String[] words = Images.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var im in words)
            {
                paths.Add(new ViewModels.OperationDetailVM.ImageItem(im));
            }

            return paths;
        }
    }
}
