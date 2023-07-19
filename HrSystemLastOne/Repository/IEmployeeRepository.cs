using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;

namespace HrSystemLastOne.Repository
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetAll();
        public Employee Add(AddEmployeeDTO EmployeeDTO);
        public void Save();
        public Employee GetEmployee(int id);
        public Employee Edit(AddEmployeeDTO EmployeeDTO, int id);
        public Employee Delete(int id);
    }
}
