using HrSystemLastOne.Validation;
using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.DTO
{
    public class EditAttendLeaveDTO
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
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
