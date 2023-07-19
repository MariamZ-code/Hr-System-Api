using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AttendLeaveTimeAttribute : ValidationAttribute
    {
        private readonly string _attendTimePropertyName;
        private readonly string _leaveTimePropertyName;

        public AttendLeaveTimeAttribute(string attendTimePropertyName, string leaveTimePropertyName)
        {
            _attendTimePropertyName = attendTimePropertyName;
            _leaveTimePropertyName = leaveTimePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var attendTimeProperty = validationContext.ObjectType.GetProperty(_attendTimePropertyName);
            var leaveTimeProperty = validationContext.ObjectType.GetProperty(_leaveTimePropertyName);

            if (attendTimeProperty == null || leaveTimeProperty == null)
            {
                return new ValidationResult($"Unknown property names: {_attendTimePropertyName}, {_leaveTimePropertyName}");
            }

            var attendTime = (TimeSpan)attendTimeProperty.GetValue(validationContext.ObjectInstance, null);
            var leaveTime = (TimeSpan)leaveTimeProperty.GetValue(validationContext.ObjectInstance, null);

            if (attendTime >= leaveTime)
            {
                return new ValidationResult("Attend time must be less than leave time");
            }

            return ValidationResult.Success;
        }
    }
}
