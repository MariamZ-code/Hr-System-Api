using HrSystemLastOne.Models;

namespace HrSystemLastOne.Repository
{
    public interface ISalaryReportRepository
    {
        public Employee GetEmployee(string name);
        public double GetBounsByEmpName(string empName, DateTime startDate, DateTime endDate);
        public double GetDiscountByEmpName(string empName, DateTime startDate, DateTime endDate);
        public double GetTotalBounsByEmpName(string empName, DateTime startDate, DateTime endDate);
        public double GetTotalDiscountByEmpName(string empName, DateTime startDate, DateTime endDate);
        public int GetHolidays(DateTime startDate, DateTime endDate);
        public int CalculateWeekendHolidays(DateTime startDate, DateTime endDate);
        public DayOfWeek GetDayOfWeek(string day);
    }
}
