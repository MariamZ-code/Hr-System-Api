namespace HrSystemLastOne.DTO
{
    public class SalaryReportDTO
    {
        public string DepartName { get; set; }
        public string EmployeeName { get; set; }
        public int Salary { get; set; }
        public int AttendanceDays { get; set; }
        public int AbsentDays { get; set; }
        public double DiscoundCount { get; set; }
        public double BonusCount { get; set; }
        public double TotalDiscound { get; set; }
        public double TotalBonus { get; set; }
        public double TotalSalary { get; set; }

    }
}
