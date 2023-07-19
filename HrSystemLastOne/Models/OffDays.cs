using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.Models
{
    public class OffDays
    {
        public int Id { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime Date { get; set; }
        public string Name { get; set; }

    }
}
