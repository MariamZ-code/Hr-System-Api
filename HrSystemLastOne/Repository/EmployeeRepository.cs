using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using Microsoft.EntityFrameworkCore;

namespace HrSystemLastOne.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        ITIContext db;
        public EmployeeRepository(ITIContext context)
        {
            db = context;   
        }
        public Employee Add(AddEmployeeDTO EmployeeDTO)
        {
            
            Employee employee = new Employee()
            {

                SSN = EmployeeDTO.SSN,
                Name = EmployeeDTO.Name,
                Nationality = EmployeeDTO.Nationality,
                Gender = EmployeeDTO.Gender,
                phone = EmployeeDTO.phone,
                City = EmployeeDTO.City,
                Country = EmployeeDTO.Country,
                street = EmployeeDTO.street,
                Salary = EmployeeDTO.Salary,
                HireDate = EmployeeDTO.HireDate,
                BirthDate = EmployeeDTO.BirthDate,
                LeaveTime  = EmployeeDTO.LeaveTime,
                AttendTime = EmployeeDTO.AttendTime,
                Dept_id = EmployeeDTO.Dept_id
            };
            db.Employees.Add(employee);
            return employee;
            
        }

        public void Save() 
        {
            db.SaveChanges();
        }

        public Employee GetEmployee(int id) 
        {
            return db.Employees.Include(e => e.Department).FirstOrDefault(n => n.Id == id);
        }

        public Employee Edit(AddEmployeeDTO EmployeeDTO, int id)
        {
            
            Employee employee = GetEmployee(id);
            employee.Id = id;
            employee.SSN = EmployeeDTO.SSN;
            employee.Name = EmployeeDTO.Name;
            employee.Nationality = EmployeeDTO.Nationality;
            employee.Gender = EmployeeDTO.Gender;
            employee.phone = EmployeeDTO.phone;
            employee.City = EmployeeDTO.City;
            employee.Country = EmployeeDTO.Country;
            employee.street = EmployeeDTO.street;
            employee.Salary = EmployeeDTO.Salary;
            employee.HireDate = EmployeeDTO.HireDate ;
            employee.BirthDate = EmployeeDTO.BirthDate ;
            employee.LeaveTime = EmployeeDTO.LeaveTime;
            employee.AttendTime = EmployeeDTO.AttendTime;
            employee.Dept_id = EmployeeDTO.Dept_id;
            

            return employee;
            
        }

        
        public Employee Delete(int id)
        { 
            Employee employee = GetEmployee(id);
            db.Employees.Remove(employee);
            return employee;
        }

        public List<Employee> GetAll()
        {
            return db.Employees.Include(e=>e.Department).ToList();

        }
    }
}
