using HrSystemLastOne.Constants;
using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using HrSystemLastOne.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryReportController : ControllerBase
    {
        ILeaveAttendRepository AttendanceRepository;
        ISalaryReportRepository SalaryreportRepository;

        public SalaryReportController(ILeaveAttendRepository attendRepository, ISalaryReportRepository reportRepository)
        {
            AttendanceRepository = attendRepository;
            SalaryreportRepository = reportRepository;
        }

        //[HttpGet("{name}")]
        [HttpGet("{name}/{startDate}/{endDate}")]
        //[Authorize(Permissions.SalaryReport.View)]

        public ActionResult Get(string name , DateTime startDate, DateTime endDate)
        {
            SalaryReportDTO salaryReportDTO = new SalaryReportDTO();
            Employee employee = SalaryreportRepository.GetEmployee(name);
            salaryReportDTO.EmployeeName = employee.Name;
            salaryReportDTO.DepartName = employee.Department.Name;
            salaryReportDTO.Salary = employee.Salary;


            TimeSpan timeSpan = endDate - startDate;
            int DaysOnmonth = timeSpan.Days;


            int WeekendHolidays = SalaryreportRepository.CalculateWeekendHolidays(startDate, endDate);
            int numberOfDaysOff = SalaryreportRepository.GetHolidays(startDate, endDate);
            List<LeaveAttend> leaveAttends = AttendanceRepository.GetByDateEmp(name,startDate,endDate);
            salaryReportDTO.AttendanceDays = leaveAttends.Count;
            salaryReportDTO.AbsentDays = DaysOnmonth - leaveAttends.Count - numberOfDaysOff - WeekendHolidays;

            
            salaryReportDTO.BonusCount=SalaryreportRepository.GetBounsByEmpName(name, startDate, endDate);
            salaryReportDTO.DiscoundCount = SalaryreportRepository.GetDiscountByEmpName(name, startDate, endDate);


            salaryReportDTO.TotalBonus = SalaryreportRepository.GetTotalBounsByEmpName(name, startDate, endDate);
            salaryReportDTO.TotalDiscound = SalaryreportRepository.GetTotalDiscountByEmpName(name, startDate, endDate);

            salaryReportDTO.TotalSalary = salaryReportDTO.Salary + salaryReportDTO.TotalBonus - salaryReportDTO.TotalDiscound;

            return Ok(salaryReportDTO);
        }
    }
}
