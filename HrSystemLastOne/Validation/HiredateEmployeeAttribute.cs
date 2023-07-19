using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.Validation 
{
    public class HiredateEmployeeAttribute : ValidationAttribute
    {

        private readonly DateTime _Date;
        public HiredateEmployeeAttribute()
        {
            _Date = new DateTime(2008, 1, 1);

        }

        public override bool IsValid(object value)
        {
            //DateTimeOffset Value = (DateTimeOffset)value;    
            //DateTime dateTime = DateTime.SpecifyKind(Value.UtcDateTime, DateTimeKind.Utc); 
            DateTime dateTime = (DateTime)value;
            return dateTime >= _Date;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} ,  Hiring date must be after 2008";
        }
    }
}

