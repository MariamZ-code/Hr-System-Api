using HrSystemLastOne.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HrSystemLastOne.Repository
{
    public class SalaryReportRepository:ISalaryReportRepository
    {
        ITIContext db;
        public SalaryReportRepository(ITIContext context)
        {
            db = context;
        }
        public Employee GetEmployee(string name) 
        {
            return db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Name == name);
        }

        public double GetBounsByEmpName(string empName , DateTime startDate, DateTime endDate)
        {
            var bouns= db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).Where(e => e.Date >= startDate && e.Date <= endDate && e.Employee.Name == empName).Select(e=> e.LeaveTime-e.Employee.LeaveTime).ToList();
            double bounsCount = 0.0;

            foreach (var item in bouns)
            {
                if (item.TotalMinutes >= 0) 
                {
                    bounsCount += item.TotalMinutes;
                }
            }

            return bounsCount / 60;
        }


        public double GetDiscountByEmpName(string empName,DateTime startDate, DateTime endDate)
        {
            var discount = db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).Where(e => e.Date >= startDate && e.Date <= endDate && e.Employee.Name == empName).Select(e => e.Employee.LeaveTime - e.LeaveTime).ToList();
            var discount1 = db.LeaveAttends.Include(l => l.Employee).ThenInclude(l => l.Department).Where(e => e.Date >= startDate && e.Date <= endDate && e.Employee.Name == empName).Select(e => e.AttendTime - e.Employee.AttendTime).ToList();

            double discountCount = 0.0;
            foreach (var item in discount)
            {
                if (item.TotalMinutes >= 0)
                {
                    discountCount += item.TotalMinutes;
                }
            }

            foreach (var item in discount1)
            {
                if (item.TotalMinutes >= 0)
                {
                    discountCount += item.TotalMinutes;
                }
            }
            return discountCount / 60;
        }

        public double GetTotalBounsByEmpName(string empName, DateTime startDate, DateTime endDate) 
        {
            double TotalBouns = 0.0;
            double BounsCount = GetBounsByEmpName(empName,startDate,endDate);

            General_Settings general = db.General_Settings.FirstOrDefault();
            if (general.UnitUsed == "Hours")
            {
                TotalBouns = BounsCount * general.Bonus * 100;
            }
            else 
            {
                TotalBouns = BounsCount * general.Bonus;
            }

            return TotalBouns;
        }

        public double GetTotalDiscountByEmpName(string empName, DateTime startDate, DateTime endDate) 
        {
            double TotalDiscount = 0.0;
            double DiscountCount = GetDiscountByEmpName(empName, startDate, endDate);

            General_Settings general = db.General_Settings.FirstOrDefault();
            if (general.UnitUsed == "Hour")
            {
                TotalDiscount = DiscountCount * general.Discound * 100;
            }
            else
            {
                TotalDiscount = DiscountCount * general.Discound;
            }

            return TotalDiscount;
        }

        public int GetHolidays(DateTime startDate, DateTime endDate) 
        {
            var holiday = db.OffDays.Where(e => e.Date >= startDate && e.Date <= endDate).ToList();
            return holiday.Count;
        }

        public int CalculateWeekendHolidays(DateTime startDate, DateTime endDate)
        {
            int weekendHolidays = 0;

            General_Settings general_Settings = db.General_Settings.FirstOrDefault();
            DayOfWeek offDay1 = GetDayOfWeek(general_Settings.OffDay1);
            DayOfWeek offDay2 = GetDayOfWeek(general_Settings.OffDay2);

            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == offDay1 || date.DayOfWeek == offDay2)
                {
                    weekendHolidays++;
                }
            }

            return weekendHolidays;
        }

        public DayOfWeek GetDayOfWeek(string day) 
        {
            DayOfWeek dayOfWeek;
            switch (day)
            {
                case "Saturday":
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                case "Sunday":
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
                case "Monday":
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case "Teusday":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "Wedensday":
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case "Thursday":
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case "Friday":
                    dayOfWeek = DayOfWeek.Friday;
                    break;
                default:
                    dayOfWeek = DayOfWeek.Friday;
                    break;
            }


            return dayOfWeek;
        }

    }
}
