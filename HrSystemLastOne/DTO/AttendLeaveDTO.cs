

using HrSystemLastOne.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.DTO
{
    public class AttendLeaveDTO
    {
        public int Id { get; set; }

        public string DeptName { get; set; }
        public string EmpName { get; set; }
        [DataType(DataType.Date)]


        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh-mm}")]
        [Required]
        public TimeSpan LeaveTime { get; set; }


        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh-mm}")]
        [Required]
        [AttendLeaveTime("AttendTime", "LeaveTime")]
        public TimeSpan AttendTime { get; set; }

        public int Emp_id { get; set; }
    }
}
