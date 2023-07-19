using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HrSystemLastOne.Validation;

namespace HrSystemLastOne.Models
{
    public class LeaveAttend
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

        [ForeignKey("Employee")]
        public int Emp_id { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
