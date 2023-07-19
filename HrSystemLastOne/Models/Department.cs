using System.ComponentModel.DataAnnotations;

namespace HrSystemLastOne.Models
{
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public virtual List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
