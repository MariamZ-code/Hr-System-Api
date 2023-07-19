using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.Models
{
    public class General_Settings
    {
        public int Id { get; set; }
        public double Discound { get; set; }
        public double Bonus { get; set; }

        [RegularExpression("^(Hour|Pound)$", ErrorMessage = "Must be Hour Or Pound")]

        public string UnitUsed { get; set; }
        [RegularExpression("^(Friday | Saturday | Sunday | Monday | Tuesday|Wednesday|Thursday)$", ErrorMessage = "Must be a Day")]

        public string OffDay1 { get; set; }

        [RegularExpression("^(Friday | Saturday | Sunday | Monday | Tuesday|Wednesday|Thursday)$", ErrorMessage = "Must be a Day")]

        public string? OffDay2 { get; set; }
    }
}
