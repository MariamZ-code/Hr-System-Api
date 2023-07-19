using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.DTO
{
    public class RoleFormDto
    {
        [Required, StringLength(250)]
        public string Name { get; set; }
    }
}
