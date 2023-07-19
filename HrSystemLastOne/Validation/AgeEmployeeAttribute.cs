using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.Validation
{
    public class AgeEmployeeAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;
        private readonly DateTime _maxDate;


        public AgeEmployeeAttribute()
        {
            _minDate = new DateTime(1950, 1, 1);
            _maxDate = new DateTime(2003, 1, 1);

        }

        public override bool IsValid(object value)
        {
            //DateTimeOffset Value = (DateTimeOffset)value;
            //DateTime dateTime = DateTime.SpecifyKind(Value.UtcDateTime, DateTimeKind.Utc);
            DateTime dateTime = (DateTime)value;

            return dateTime >= _minDate && dateTime <= _maxDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} Employee Age must be more than 20 years";
        }
    }
}
