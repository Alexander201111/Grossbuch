using System;

namespace Grossbuch.Models
{
    public class Aim
    {
        public int Id { get; set; }
        public int Id2 { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }

        public string Title { get; set; }
        public int Type { get; set; } //1-Накопление (не менее), 2-Трата (не более)
        public float PlannedSum { get; set; }
        public float CurrentSum { get; set; }
        public string Description { get; set; }

        public Account Facility1 { get; set; }
        public Category Facility2 { get; set; }
        public User User { get; set; }

        public DateTime UpdateTime { get; set; }

        public Aim()
        {

        }

        public Aim(User _user)
        {
            DateStart = DateTime.Now;
            DateFinish = DateTime.Now;
            Title = "";
            Type = -1;
            PlannedSum = 0;
            CurrentSum = 0;
            Description = "";
            Facility1 = null;
            Facility2 = null;
            User = _user;

            UpdateTime = DateTime.Now;
        }

        public Aim(DateTime _dateStart, DateTime _dateFinish, string _title, int _type, float _plannedSum, float _currentSum, string _description, Account _facility1, Category _facility2)
        {
            DateStart = _dateStart;
            DateFinish = _dateFinish;
            Title = _title;
            Type = _type;
            PlannedSum = _plannedSum;
            CurrentSum = _currentSum;
            Description = _description;
            Facility1 = _facility1;
            Facility2 = _facility2;

            UpdateTime = DateTime.Now;
        }
    }
}
