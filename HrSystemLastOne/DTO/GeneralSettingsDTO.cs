using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.DTO
{
    public class GeneralSettingsDTO
    {
        public int Id { get; set; }
        public double Discound { get; set; }
        public double Bonus { get; set; }

        [RegularExpression("^(Hour|Pound)$", ErrorMessage = "Must be Hour Or Pounds")]

        public string UnitUsed { get; set; }
       // [RegularExpression("^(Friday | Saturday | Sunday | Monday | Tuesday|Wednesday|Thursday)$", ErrorMessage = "Must be a Day")]

        public string OffDay1 { get; set; }

       [RegularExpression("^(Friday|Saturday|Sunday|Monday|Tuesday|Wednesday|Thursday)$", ErrorMessage = "Must be a Day")]

        public string? OffDay2 { get; set; }
    }
}
